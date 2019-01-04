using MaxMind.Db;
using MaxMind.GeoIP2;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RefreshDB
{
    class Program
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "postgres";
        private static string Password = "Po_Fa14730d";
        private static string Port = "5432";

        static string cityString;

        static void Main(string[] args)
        {

            Console.WriteLine();

            Console.WriteLine("Plaease download database \"GeoLite2 City\" from site https://dev.maxmind.com/geoip/geoip2/geolite2/.");
            Console.WriteLine();
            Console.WriteLine("Place file \"GeoLite2-City.mmdb\" in the same folder " +
                                "where is file \"RefreshDB.exe\"");
            Console.WriteLine();
            Console.WriteLine("Push button \"Enter\"");
            Console.ReadLine();

            Console.WriteLine();

            string connString =
             String.Format(
                 "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4};",
                 Host,
                 User,
                 DBname,
                 Port,
                 Password);

            Console.WriteLine("DATABASE");
            Console.WriteLine();

            //string conn_param = "Server=localhost;Port=5432;User Id=postgres;Password=Po_Fa14730d;Database=postgres;";
            //string sql = "CREATE TABLE inventory (id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER);";
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            Console.WriteLine("Opening connection");
            conn.Open();



            var command = conn.CreateCommand();
            command.CommandText = "DROP TABLE IF EXISTS ips;";
            command.ExecuteNonQuery();
            Console.WriteLine("Finished dropping table (if existed)");

            command.CommandText = "CREATE TABLE ips (id serial PRIMARY KEY, ip VARCHAR(50), city VARCHAR(50), " +
                "country VARCHAR(50), longitude VARCHAR(50), latitude VARCHAR(50));";
            command.ExecuteNonQuery();
            Console.WriteLine("Finished creating table");
            Console.WriteLine();

            Console.WriteLine("");
            Console.WriteLine("Push button \"Enter\" for refresh database");
            Console.WriteLine("Filling database will begin from 5.18.97.179 ip. You can change this in source of the programm");
            Console.ReadLine();

            using (var reader = new DatabaseReader("GeoLite2-City.mmdb"))
            {

                for (byte i = 5; i < 255; i++)
                {
                    for (byte k = 18; k < 255; k++)
                    {
                        for (byte l = 97; l < 255; l++)
                        {
                            for (byte m = 179; m < 255; m++)
                            {
                                string ipCircle = $"{i}.{k}.{l}.{m}";
                                Console.WriteLine(ipCircle);

                                //byte[] ipArray = new byte[] { 5, 18, 97, 179};

                                //System.Net.IPAddress ipCicleIn = new System.Net.IPAddress(ipArray);


                                //using (var reader = new Reader("GeoLite2-City.mmdb"))
                                //{

                                //    var data = reader.Find<Dictionary<string, object>>(ipCicleIn);


                                //    var city = data["city"];
                                //    //  var city2 = city["0"];


                                //    //var test = city.GetType().GetProperties(1).GetValue(1);
                                //  var country = data["country"];

                                //    //                                    var tewt = country[""];
                                //}


                                // Replace "City" with the appropriate method for your database, e.g.,
                                // "Country".
                                var city = reader.City(ipCircle);                             
                                                      /*
                                                      Console.WriteLine(city.Country.IsoCode); // 'US'
                                                      Console.WriteLine(city.Country.Names["zh-CN"]); // '美国'
                                                      Console.WriteLine(city.MostSpecificSubdivision.Name); // 'Minnesota'
                                                      Console.WriteLine(city.MostSpecificSubdivision.IsoCode); // 'MN'
                                                      Console.WriteLine(city.Postal.Code); // '55455'
                                                      */
                                try
                                {
                                    cityString = city.City.Name;
                                    if (city.City.Name != null && city.City.Name.Contains("\'"))
                                    { 
                                        cityString = city.City.Name.Remove(city.City.Name.IndexOf('\''), 1);
                                    }
                                    //countryString = city.Country.Name.Remove(;
                                    //longitudeString = city.Location.Longitude.ToString();
                                    //latitudeString  = city.Location.Latitude.ToString();


                                    command.CommandText =
     String.Format(
         @"INSERT INTO ips (ip, city, country, longitude, latitude) VALUES ({0}, {1}, {2}, {3}, {4});",
         $"'{ipCircle}'",
         $"'{cityString}'",
         $"'{city.Country.Name}'",
         $"'{city.Location.Longitude}'",
         $"'{city.Location.Latitude}'");

                                    int nRows = command.ExecuteNonQuery();
                                    //Console.WriteLine(String.Format("Number of rows inserted={0}", nRows));

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }
                    }
                }
            }

            conn.Close();
            Console.WriteLine("Closing connection");




            //byte[] ip = new byte[] { 5, 18, 97, 179 };
            //System.Net.IPAddress ipClass = new System.Net.IPAddress(ip);
            //using (var reader = new Reader("GeoLite2-City.mmdb"))
            //{
            //    var data = reader.Find<Dictionary<string, object>>(ipClass);
            //}


            Console.ReadLine();
        }
    }
}
