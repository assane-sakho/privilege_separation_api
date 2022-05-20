using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using ClassLibrary;

namespace Child.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private IConfiguration _configuration;
        public SalesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(Name = "GetSales")]
        public IActionResult Get()
        {
            string result = "";

            //Check if the request contains a JWT Token, otherwise we send a 401 code
            var re = Request;
            var headers = re.Headers.ToDictionary(p => p.Key, y => y.Value);
            if (!headers.ContainsKey("Authorization") || !headers["Authorization"].ToString().Contains("Bearer "))
            {
                return Unauthorized();
            }

            //Get the JWT Token
            string token = headers["Authorization"].ToString()
                                                    .Split("Bearer ")
                                                    .FirstOrDefault(x => x != String.Empty);

            //Create a pipe connected to the server
            // var pipeClient =
            //      new NamedPipeClientStream(".", "testpipe",
            //          PipeDirection.InOut, PipeOptions.None,
            //          TokenImpersonationLevel.Impersonation);

                            var pipeClient =
                    new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous);


            pipeClient.Connect();

            var ss = new StreamString(pipeClient);
            
            // We validate the server's signature string.
            if (ss.ReadString() == Environment.GetEnvironmentVariable("serverSignature"))
            {
                //We send the JWT Token
                ss.WriteString(token);

                //We receive a result
                result = ss.ReadString();
            }
            else
            {
                Console.WriteLine("Server could not be verified.");
            }
            pipeClient.Close();

            //if the token send was incorrect, we received an "unauthorized" result
            if (result == "unauthorized")
                return Unauthorized();

            //Else, we receive a json result
            return Ok(result);
        }
    }
}