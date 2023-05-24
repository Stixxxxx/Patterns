 
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    internal class Utilits
    {

        public void AddToLogFile(string text, DateTime dateTimeNow) 
        {
            try
            {
                ////Pass the filepath and filename to the StreamWriter Constructor
                //StreamWriter sw = new StreamWriter("./file.txt");
                ////Write a line of text
                //sw.WriteLine(text);
                ////Write a second line of text
              
                ////Close the file
                //sw.Close();



                string fileName = "./log.txt";
                StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.Append));
                sw.WriteLine(text);
                sw.WriteLine(dateTimeNow);
                sw.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

        public string GetStringFeatherSertolovo()
        {

            string lat = "60.08";
            string lon = "30.12";


            string sURL;
            //sURL = @"https://api.openweathermap.org/data/2.5/weather?lat=60.08&lon=30.12&appid=c5ba134efa510143f5193f7d8e1f5bc7";
            sURL = @"http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=c5ba134efa510143f5193f7d8e1f5bc7&units=metric";


            var client = new RestClient(sURL);
          
            var request = new RestRequest();
            request.AddHeader("Key", "Value");
            RestResponse response = client.Execute(request);

            try
            {
            

                return response.Content;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return response.Content;
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");

            }


            


            //DateTime dayToday = DateTime.Now;


            ////проверка на существование файла с сегодняшними тиксами
            //string path = @"Tiks for weather.txt";

            //bool fileExist = File.Exists(path);
            //if (fileExist)
            //{
            //    string pathTiks = @"weather.txt";
            //    // асинхронное чтение
            //    using (StreamReader reader = new StreamReader(pathTiks))
            //    {
            //        string text = reader.ReadToEndAsync().Result;

            //        if (text.Contains(dayToday.Date.Ticks.ToString()))
            //        {


            //            text = text.Replace(dayToday.Date.Ticks.ToString(),"");

            //            return text;
            //        }
            //        return String.Empty; //?????;
            //    }

            //}
            //else
            //{




            //    string lat = "60.08";
            //    string lon = "30.12";


            //    string sURL;
            //    //sURL = @"https://api.openweathermap.org/data/2.5/weather?lat=60.08&lon=30.12&appid=c5ba134efa510143f5193f7d8e1f5bc7";
            //    sURL = @"http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=c5ba134efa510143f5193f7d8e1f5bc7&units=metric";




            //    var client = new RestClient(sURL);
            //    //  client.Timeout = -1;
            //    var request = new RestRequest();
            //    request.AddHeader("Key", "Value");
            //    RestResponse response = client.Execute(request);

            //    try
            //    {
            //        ////Pass the filepath and filename to the StreamWriter Constructor
            //        //StreamWriter sw = new StreamWriter("./file.txt");
            //        ////Write a line of text
            //        //sw.WriteLine(text);
            //        ////Write a second line of text

            //        ////Close the file
            //        //sw.Close();

            //        string fileName = "./weather.txt";
            //        StreamWriter sw = new StreamWriter(File.Open(fileName, FileMode.CreateNew));
            //        sw.WriteLine(response.Content);
            //        sw.WriteLine(dayToday.Date.Ticks);
            //        StreamWriter sw2 = new StreamWriter(File.Open("Tiks for weather.txt", FileMode.Append));
            //        sw2.WriteLine(dayToday.Date.Ticks);

            //        sw.Close();
            //        sw2.Close();

            //        return response.Content;

            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("Exception: " + e.Message);
            //        return response.Content;
            //    }
            //    finally
            //    {
            //        //Console.WriteLine("Executing finally block.");

            //    }


            //    return String.Empty;


            return String.Empty;

        }


        public string GetAnecdot()
        {
            string param = "pid=515a9uvd06d42c4l6hms&method=getRandItem&charset=cp1251&uts=1490050901";

            string KEY = "d7745e54024560817ef2f438a166bc4a808bb514609cb069dbfac7e2dfd3d512";

            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(param + KEY));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }



            var client = new RestClient("http://anecdotica.ru/api?" + param + "&hash=" + sBuilder.ToString());

            var request = new RestRequest();

            request.AddHeader("KEY", KEY);
            RestResponse response = client.Execute(request);


            //string test = {"result":{"error":0,"errMsg":""},"item":{"text":"Психотерапевт — это врач, который теряет сознание при виде крови.","note":""}};



         

            JObject json = JObject.Parse(response.Content);


            var main = json["item"];
            var text = main["text"];



            try
            {


                return text.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return response.Content;
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");

            }


            return text.ToString();

        }


        public string GetJam()
        {
           


            var client = new RestClient("https://static-maps.yandex.ru/1.x/?ll=30.22,60.12&size=650,450&z=13&l=map,trf,skl&pt=37.620070,55.753630,pmwtm1~37.64,55.76363,pmwtm99");

            var request = new RestRequest();

           
            RestResponse response = client.Execute(request);


            //string test = {"result":{"error":0,"errMsg":""},"item":{"text":"Психотерапевт — это врач, который теряет сознание при виде крови.","note":""}};





            JObject json = JObject.Parse(response.Content);


            var main = json["item"];
            var text = main["text"];



            try
            {


                return text.ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return response.Content;
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");

            }


            return text.ToString();

        }

    }
    
}
