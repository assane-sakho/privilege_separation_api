using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Monitor
{
    internal class DbHelper
    {
        private readonly MySqlConnection _mySqlConnection;
        private static DbHelper _instance;

        private DbHelper()
        {
            _mySqlConnection = new MySqlConnection(Environment.GetEnvironmentVariable("connectionString"));
        }
        public bool TokenIsValid(string token)
        {
            _mySqlConnection.Open();

            string query =
                "SELECT COUNT(*) " +
                "FROM users " +
                $"WHERE token ='{token}'";

            bool result = ExecuteScalar(query) == 1;

            _mySqlConnection.Close();

            return result;
        }

        public string GetRolesId(string token)
        {
            _mySqlConnection.Open();

            string query =
                "SELECT role_region.region_id " +
                "FROM role_region, roles, regions, role_user, users " +
                "WHERE role_region.region_id = regions.id " +
                "AND role_region.role_id = roles.id " +
                "AND roles.id = role_user.role_id " +
                "AND users.id = role_user.user_id " +
                $"AND users.token ='{token}'";

            var reader = ExecuteReader(query);
            List<int> regions = new List<int>();

            while (reader.Read())
            {
                regions.Add(Convert.ToInt32(reader["region_id"]));
            }

            _mySqlConnection.Close();

            return String.Join(", ", regions.ToArray());
        }

        public string GetSalesData(string rolesId)
        {
            _mySqlConnection.Open();

            string query =
                "SELECT a.name as nom_article, " +
                "sa.price as prix_achat, " + 
                "sa.amount as quantie_commandee, " +
                "sa.date as date_commade,  " +
                "sa.client_reference as reference_client, " +
                "st.name as nom_magasin,  " +
                "st.address as adresse_magasin, " +
                "r.name as nom_region " +
                "FROM sales sa, article_store sta, stores st, articles a, regions r " +
                "WHERE sa.article_store_id = sta.id " +
                "AND sta.article_id = a.id " +
                "AND sta.store_id = st.id " +
                "AND st.region_id = r.id " +
                $"AND st.region_id in ({rolesId})";

            var reader = ExecuteReader(query);
            List<DataResult> result = new List<DataResult>();

            while (reader.Read())
            {
                result.Add(new DataResult {
                    ArticleName = (string)reader["nom_article"],
                    BuyingPrice = (Decimal)reader["prix_achat"],
                    QuantityOrdored = (int)reader["quantie_commandee"],
                    CommandDate = (DateTime)reader["date_commade"],
                    ClientReference = (string)reader["reference_client"],
                    StoreName = (string)reader["nom_magasin"],
                    StorePlace = (string)reader["adresse_magasin"],
                    RegionName = (string)reader["nom_region"]
                });
            }

            string jsonString = JsonSerializer.Serialize(result);

            _mySqlConnection.Close();

            return jsonString;
        }

        private int ExecuteScalar(String stm)
        {
            MySqlCommand command = new MySqlCommand(stm, _mySqlConnection);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        private MySqlDataReader ExecuteReader(String stm)
        {
            MySqlCommand command = new MySqlCommand(stm, _mySqlConnection);
            MySqlDataReader reader = command.ExecuteReader();

            return reader;
        }

        public static DbHelper GetInstance()
        {
            if(_instance == null)
                _instance = new DbHelper();
            return _instance;
        }
    }
}
