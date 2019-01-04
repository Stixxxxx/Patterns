using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class IpsController : ApiController
    {
        // GET: api/Ips
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ips/5
        public HttpResponseMessage Get(string id)
        {
            string ipTable = string.Empty;
            string city = string.Empty;
            string country = string.Empty;
            string longitude = string.Empty;
            string latitude = string.Empty;

            string ip = id.Replace('-','.');

            string Host = "localhost";
            string User = "postgres";
            string DBname = "postgres";
            string Password = "Po_Fa14730d";
            string Port = "5432";

            string connString =
              String.Format(
                  "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};",
                  Host,
                  User,
                  DBname,
                  Port,
                  Password);

            NpgsqlConnection conn = new NpgsqlConnection(connString);

            Console.WriteLine("Opening connection");
            conn.Open();

            //NpgsqlCommand com = new NpgsqlCommand("select * from ips order by \"ip\"", conn);
            NpgsqlCommand com = new NpgsqlCommand("select * from ips where ip='"+ip+"'", conn);

            NpgsqlDataReader reader;
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                     ipTable = reader.GetString(1);
                     city = reader.GetString(2);
                     country = reader.GetString(3);
                     longitude = reader.GetString(4);
                     latitude = reader.GetString(5);
                }

                catch
                {

                }
            }

            conn.Close();

            Ips item = new Ips();
            item.Ip = ipTable;
            item.City= city;
            item.Country = country;
            item.Longitude = longitude;
            item.Latitude = latitude;


            return Request.CreateResponse(HttpStatusCode.OK, item, Configuration.Formatters.JsonFormatter);
                        
        }


        // POST: api/Ips
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Ips/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ips/5
        public void Delete(int id)
        {
        }

        
    }
}
