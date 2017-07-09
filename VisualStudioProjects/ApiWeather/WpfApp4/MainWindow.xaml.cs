using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string lat = Lat.Text;
            string lon = Long.Text;


            string sURL;
            sURL = @"http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=c5ba134efa510143f5193f7d8e1f5bc7&units=metric";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

try { 

            var a = objReader.ReadToEnd();

            JObject json = JObject.Parse(a);


            var main = json["main"];
            var b = json["weather"];
            var bb = b[0];
            var bbb = bb["description"];

            var k = json["sys"];
            var kk = k["country"];

            var temp = main["temp"];


            var c = json.GetValue("name");
            var d = json.GetValue("name");

            Result.Text = "Населенный пункт:\n" + c.ToString() + "\n\nСтрана:\n" + kk.ToString() + "\n\nСостояние:\n" + bbb.ToString() + "\n\nТемпература:\n" + temp.ToString()+"* (по цельсию)";

            }
            catch (Exception eee)
            {

                MessageBox.Show(eee.Message);
            }


        }


       



    }
}
