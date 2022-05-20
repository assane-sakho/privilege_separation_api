using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Monitor
{
    internal class DataResult
    {
        [JsonPropertyName("article_name")]
        public string ArticleName { get; set; }

        [JsonPropertyName("buying_price")]
        public Decimal BuyingPrice { get; set; }

        [JsonPropertyName("quantity_ordored")]
        public int QuantityOrdored { get; set; }

        [JsonPropertyName("command_date")]
        public DateTime CommandDate { get; set; }

        [JsonPropertyName("client_reference")]
        public string ClientReference { get; set; }

        [JsonPropertyName("store_name")]
        public string StoreName { get; set; }

        [JsonPropertyName("store_place")]
        public string StorePlace { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }
    }
}
