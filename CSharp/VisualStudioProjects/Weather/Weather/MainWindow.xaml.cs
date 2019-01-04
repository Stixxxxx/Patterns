using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

//распарсить документом хмл
namespace Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

 

public partial class MainWindow : Window
    {
        //забить все в ини
        Point sPosition;
        public static string city = GetKey2("city");
        public static string server = GetKey2("server");
        public static string lonGeneral = GetKey2("lon");
        public static string latGeneral = GetKey2("lat");
        public static string chechboxischecked = GetKey2("chechboxischecked");

        public static string cityMeteoserviceXML;
        public static string cityMeteoserviceXMLforInterface;


        public MainWindow()
        {
            InitializeComponent();

            double w = SystemParameters.PrimaryScreenWidth;
            double h = SystemParameters.PrimaryScreenHeight;
            this.Left = w - this.Width;
            this.Top = 0;


            image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

            if (chechboxischecked == "false")
            { 
            if (server == "meteoservicehtml")

            {
                
                refreshMeteoserviceHTML();

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(60000);
                timer.Tick += new EventHandler(refreshasyn);
                timer.Start();

            }

            else

            if (server == "meteoservicexml")
            { 
            refreshMeteoserviceXML();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(60000);
            timer.Tick += new EventHandler(refreshasyn);
            timer.Start();
            }

            else

            if (server == "openweathermap")
            {
                refreshOpenweathermap();

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(60000);
                timer.Tick += new EventHandler(refreshasyn);
                timer.Start();

            }
            }

            else

            {
                if (chechboxischecked == "true")
                {
                    refreshOpenweathermap();

                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(60000);
                    timer.Tick += new EventHandler(refreshasyn);
                    timer.Start();

                }



            }



        }


        public void refreshMeteoserviceXML()
        {
            
            string town = string.Empty; 
              string temp = string.Empty;
                string osadki = string.Empty;
                string pressure = string.Empty;

            if (city == "sankt-peterburg")
            {
                cityMeteoserviceXML = "69";

            }
            if (city == "moskva")
            {
                cityMeteoserviceXML = "37";

            }

            if (city == "kiev")
            {
                town = city;
                //Error Error = new Error();
                //Error.textBlock.Text = "нет данных по гододу";
                //Error.ShowDialog();



            }
            
              

            if (cityMeteoserviceXML == "69")
            {

                cityMeteoserviceXMLforInterface = "Погода в Санкт-Петербурге";

                // Создаем экземпляр класса
                XmlDocument xmlDoc = new XmlDocument();

            // Загружаем XML-документ из файла
            xmlDoc.Load("http://xml.meteoservice.ru/export/gismeteo/point/" + cityMeteoserviceXML + ".xml");
                string g = xmlDoc.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].Attributes[0].Value;
                
            // Загружаем XML-документ из строки
            // xmlDoc.LoadXML(s1);

                // Получаем всех детей корневого элемента
                // xmlDoc.DocumentElement - корневой элемент


                foreach (XmlNode table in xmlDoc.DocumentElement)
            {
                if (table.Name == "REPORT")
                {
                        foreach (XmlNode table2 in table)
                        {
                            if (table2.Name == "TOWN")
                            {
                                foreach (XmlNode table3 in table2)
                                {
                                    if (table3.Name == "FORECAST")
                                    {
                                        foreach (XmlNode table4 in table3)
                                        {
                                            if (table4.Name == "TEMPERATURE")
                                            {
                                                temp = "+" + table4.Attributes[0].Value;

                                                break;
                                            }
                                          }
                                        break;
                                    }

                                }


                            }

                        }
                    }
                              
                }


                foreach (XmlNode table in xmlDoc.DocumentElement)
                {
                    if (table.Name == "REPORT")
                    {
                        foreach (XmlNode table2 in table)
                        {
                            if (table2.Name == "TOWN")
                            {
                                foreach (XmlNode table3 in table2)
                                {
                                    if (table3.Name == "FORECAST")
                                    {
                                        foreach (XmlNode table4 in table3)
                                        {
                                            if (table4.Name == "PRESSURE")
                                            {
                                                pressure = table4.Attributes[0].Value +" мм. рт.ст.";

                                                break;
                                            }
                                        }
                                        break;
                                    }

                                }


                            }

                        }
                    }

                }


                foreach (XmlNode table in xmlDoc.DocumentElement)
                {
                    if (table.Name == "REPORT")
                    {
                        foreach (XmlNode table2 in table)
                        {
                            if (table2.Name == "TOWN")
                            {
                                foreach (XmlNode table3 in table2)
                                {
                                    if (table3.Name == "FORECAST")
                                    {
                                        foreach (XmlNode table4 in table3)
                                        {
                                            if (table4.Name == "PHENOMENA")
                                            {

                                                string osadkinumber = table4.Attributes[0].Value;

                                                if (osadkinumber == "1" || osadkinumber == "2" || osadkinumber == "3")

                                                {
                                                    osadki = "значительная облачность";
                                                }

                                                else
                                                {
                                                    if (osadkinumber == "0")
                                                    {

                                                        osadki = "пасмурно (есть просветы)";

                                                    }


                                                }

                                                break;
                                            }
                                        }
                                        break;
                                    }

                                }


                            }

                        }
                    }

                }


            }

            else 
                if (cityMeteoserviceXML == "37")
                {

                cityMeteoserviceXMLforInterface = "Погода в Москве";
                    // Создаем экземпляр класса
                    XmlDocument xmlDoc = new XmlDocument();

                    // Загружаем XML-документ из файла
                    xmlDoc.Load("http://xml.meteoservice.ru/export/gismeteo/point/" + cityMeteoserviceXML + ".xml");
                    //string g = xmlDoc.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].Attributes[0].Value;

                    // Загружаем XML-документ из строки
                    // xmlDoc.LoadXML(s1);

                    // Получаем всех детей корневого элемента
                    // xmlDoc.DocumentElement - корневой элемент
                    foreach (XmlNode table in xmlDoc.DocumentElement)
                    {
                        if (table.Name == "REPORT")
                        {

                            foreach (XmlNode table2 in table)
                            {
                                if (table2.Name == "TOWN")
                                {
                                    foreach (XmlNode table3 in table2)
                                    {
                                        if (table3.Name == "FORECAST")
                                        {
                                            foreach (XmlNode table4 in table3)
                                            {
                                                if (table4.Name == "TEMPERATURE")
                                                {
                                                temp = "+" + table4.Attributes[0].Value;

                                                    break;

                                                }

                                            }
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                     
                    }

                foreach (XmlNode table in xmlDoc.DocumentElement)
                {
                    if (table.Name == "REPORT")
                    {
                        foreach (XmlNode table2 in table)
                        {
                            if (table2.Name == "TOWN")
                            {
                                foreach (XmlNode table3 in table2)
                                {
                                    if (table3.Name == "FORECAST")
                                    {
                                        foreach (XmlNode table4 in table3)
                                        {
                                            if (table4.Name == "PRESSURE")
                                            {
                                                pressure = table4.Attributes[0].Value + " мм. рт.ст.";

                                                break;
                                            }
                                        }
                                        break;
                                    }

                                }


                            }

                        }
                    }

                }

                foreach (XmlNode table in xmlDoc.DocumentElement)
                {
                    if (table.Name == "REPORT")
                    {
                        foreach (XmlNode table2 in table)
                        {
                            if (table2.Name == "TOWN")
                            {
                                foreach (XmlNode table3 in table2)
                                {
                                    if (table3.Name == "FORECAST")
                                    {
                                        foreach (XmlNode table4 in table3)
                                        {
                                            if (table4.Name == "PHENOMENA")
                                            {

                                                string osadkinumber = table4.Attributes[0].Value;

                                                if (osadkinumber == "1" || osadkinumber == "2")

                                                {
                                                    osadki = "значительная облачность";
                                                }

                                                else
                                                {
                                                    if (osadkinumber == "0")
                                                    {

                                                        osadki = "пасмурно (есть просветы)";

                                                    }

                                                        
                                                            }

                                                break;
                                            }
                                        }
                                        break;
                                    }

                                }


                            }

                        }
                    }

                }


            }


            


            TBCityResult.Text = cityMeteoserviceXMLforInterface;
            TBtempResult.Text = temp;
            TBCloudsResult.Text = osadki;
            TBPressureResult.Text = pressure;
            TBCloudnessResult.Text = "-";

            InterFase();

        }


        public void refreshMeteoserviceHTML()
        {


            WebRequest request = WebRequest.Create(@"http://www.meteoservice.ru/weather/now/" + city + ".html"); //переделать в ксмл http://xml.meteoservice.ru/export/gismeteo/point/37.xml

            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();

                    string town = new Regex(@"<h1>(?<town>.*)</h1>").Match(data).Groups["town"].Value;
                    string temp = new Regex(@"<span class=""temperature"">(?<temp>[^<]+)&deg").Match(data).Groups["temp"].Value;
                    string osadki = new Regex(@"<td class=""title"">Облачность:</td>[^<]*?<td>(?<osadki>[^<]+)</td>").Match(data).Groups["osadki"].Value;
                    string pressure = new Regex(@"<td class=""title"">Атмосферное давление:</td>[^<]*?<td>(?<pressure>[^<]+)</td>").Match(data).Groups["pressure"].Value;

                    TBCityResult.Text = town;
                    TBtempResult.Text = temp;
                    TBCloudsResult.Text = osadki;
                    TBPressureResult.Text = pressure;
                    TBCloudnessResult.Text = "-";

                    InterFase();
                    
                }
            }
        }


        public void refreshOpenweathermap()
        {
            string town = string.Empty;
            string temp = string.Empty;

            string osadki = string.Empty;
            string pressure = string.Empty;

            string cloudness = string.Empty;

            if (chechboxischecked == "false")
            {

                if (city == "sankt-peterburg")
                {

                    string lat = "59.93";
                    string lon = "30.29";

                    // json
                    var Gentral = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/find?lat=" + lat + "&lon=" + lon + "&APPID=c5ba134efa510143f5193f7d8e1f5bc7&units=metric");


                    // Город

                    //var GentralTestCity = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/weather?lat=35&lon=139&APPID=c5ba134efa510143f5193f7d8e1f5bc7");
                    //System.Json.JsonValue ParsGentraltest = JsonValue.Parse(GentralTestCity);

                    System.Json.JsonValue ParsGentral = JsonValue.Parse(Gentral);

                    var CityName = ParsGentral.ValueOrDefault("city");

                    System.Json.JsonValue cityPars = JsonValue.Parse(CityName.ToString());

                    var NameValue = CityName["name"];


                    if (NameValue.ToString() == "\"Novaya Gollandiya\"")

                    {
                        town = "Погода в Санкт-Петербурге";
                    }

                    // Температура


                    System.Json.JsonValue ParsGentralTemp = JsonValue.Parse(Gentral);

                    var List0 = ParsGentralTemp.ValueOrDefault("list");
                    var List1 = List0[0];

                    System.Json.JsonValue tempPars = JsonValue.Parse(List1.ToString());

                    var TempValue1 = tempPars["main"];

                    System.Json.JsonValue tempPars2 = JsonValue.Parse(TempValue1.ToString());

                    var TempValue2 = tempPars2["temp"];

                    temp = "+" + TempValue2.ToString();


                    // Оссадки

                    System.Json.JsonValue ParsGentralOsadki = JsonValue.Parse(Gentral);

                    var ListOsadki0 = ParsGentralOsadki.ValueOrDefault("list");
                    var ListOsadki1 = ListOsadki0[0];

                    System.Json.JsonValue OsadkiPars = JsonValue.Parse(ListOsadki1.ToString());

                    string OsadkiValue1 = OsadkiPars["weather"].ToString();

                    string osadkiTemp2 = new Regex(@"""description"":""(?<osadkiTemp2>[^<]+)"",").Match(OsadkiValue1).Groups["osadkiTemp2"].Value; // на будущее наклонную черту в регексе \ менять на "

                    osadki = osadkiTemp2;


                    // Давление

                    System.Json.JsonValue ParsGentralPressure = JsonValue.Parse(Gentral);

                    var ListPressure0 = ParsGentralPressure.ValueOrDefault("list");
                    var ListPressure1 = ListPressure0[0];

                    System.Json.JsonValue PressurePars = JsonValue.Parse(ListPressure1.ToString());

                    var PressureValue1 = PressurePars["main"];

                    System.Json.JsonValue PressurePars2 = JsonValue.Parse(PressureValue1.ToString());

                    var PressureValue2 = PressurePars2["pressure"];

                    pressure = Math.Round(Double.Parse(Convert.ToString(PressureValue2).Replace('.', ',')) * 0.75006375541921, 0).ToString() + " мм. рт. ст.";

                    // Облачность


                    System.Json.JsonValue ParsGentralOsadky = JsonValue.Parse(Gentral);

                    var List00 = ParsGentralOsadky.ValueOrDefault("list");
                    var List11 = List00[0];

                    System.Json.JsonValue cloudnessPars = JsonValue.Parse(List11.ToString());

                    var CloudnessValue1 = cloudnessPars["clouds"];

                    System.Json.JsonValue cloudnessPars2 = JsonValue.Parse(CloudnessValue1.ToString());

                    var cloudnessPars3 = cloudnessPars2["all"];


                    cloudness = cloudnessPars3.ToString();


                }

                else

                {

                    if (city == "moskva")
                    {

                        string lat = "55.75";
                        string lon = "37.62";

                        // json
                        var Gentral = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/find?lat=" + lat + "&lon=" + lon + "&APPID=c5ba134efa510143f5193f7d8e1f5bc7&units=metric");


                        // Город

                        //var GentralTestCity = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/weather?lat=35&lon=139&APPID=c5ba134efa510143f5193f7d8e1f5bc7");
                        //System.Json.JsonValue ParsGentraltest = JsonValue.Parse(GentralTestCity);

                        System.Json.JsonValue ParsGentral = JsonValue.Parse(Gentral);

                        var CityName = ParsGentral.ValueOrDefault("city");

                        System.Json.JsonValue cityPars = JsonValue.Parse(CityName.ToString());

                        var NameValue = CityName["name"];


                        if (NameValue.ToString() == "\"Moscow\"")



                        {
                            town = "Погода в Москве";
                        }

                        // Температура


                        System.Json.JsonValue ParsGentralTemp = JsonValue.Parse(Gentral);

                        var List0 = ParsGentralTemp.ValueOrDefault("list");
                        var List1 = List0[0];

                        System.Json.JsonValue tempPars = JsonValue.Parse(List1.ToString());

                        var TempValue1 = tempPars["main"];

                        System.Json.JsonValue tempPars2 = JsonValue.Parse(TempValue1.ToString());

                        var TempValue2 = tempPars2["temp"];

                        temp = "+" + TempValue2.ToString();


                        // Оссадки

                        System.Json.JsonValue ParsGentralOsadki = JsonValue.Parse(Gentral);

                        var ListOsadki0 = ParsGentralOsadki.ValueOrDefault("list");
                        var ListOsadki1 = ListOsadki0[0];

                        System.Json.JsonValue OsadkiPars = JsonValue.Parse(ListOsadki1.ToString());

                        string OsadkiValue1 = OsadkiPars["weather"].ToString();

                        string osadkiTemp2 = new Regex(@"""description"":""(?<osadkiTemp2>[^<]+)"",").Match(OsadkiValue1).Groups["osadkiTemp2"].Value; // на будущее наклонную черту в регексе \ менять на "

                        osadki = osadkiTemp2;


                        // Давление

                        System.Json.JsonValue ParsGentralPressure = JsonValue.Parse(Gentral);

                        var ListPressure0 = ParsGentralPressure.ValueOrDefault("list");
                        var ListPressure1 = ListPressure0[0];

                        System.Json.JsonValue PressurePars = JsonValue.Parse(ListPressure1.ToString());

                        var PressureValue1 = PressurePars["main"];

                        System.Json.JsonValue PressurePars2 = JsonValue.Parse(PressureValue1.ToString());

                        var PressureValue2 = PressurePars2["pressure"];

                        pressure = Math.Round(Double.Parse(Convert.ToString(PressureValue2).Replace('.', ',')) * 0.75006375541921, 0).ToString() + " мм. рт. ст.";

                        // Облачность


                        System.Json.JsonValue ParsGentralOsadky = JsonValue.Parse(Gentral);

                        var List00 = ParsGentralOsadky.ValueOrDefault("list");
                        var List11 = List00[0];

                        System.Json.JsonValue cloudnessPars = JsonValue.Parse(List11.ToString());

                        var CloudnessValue1 = cloudnessPars["clouds"];

                        System.Json.JsonValue cloudnessPars2 = JsonValue.Parse(CloudnessValue1.ToString());

                        var cloudnessPars3 = cloudnessPars2["all"];


                        cloudness = cloudnessPars3.ToString();


                    }

                    else

                    {

                        if (city == "kiev")
                        {

                            string lat = "50.45";
                            string lon = "30.5";

                            // json
                            var Gentral = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/find?lat=" + lat + "&lon=" + lon + "&APPID=c5ba134efa510143f5193f7d8e1f5bc7&units=metric");


                            // Город

                            //var GentralTestCity = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/weather?lat=35&lon=139&APPID=c5ba134efa510143f5193f7d8e1f5bc7");
                            //System.Json.JsonValue ParsGentraltest = JsonValue.Parse(GentralTestCity);

                            System.Json.JsonValue ParsGentral = JsonValue.Parse(Gentral);

                            var CityName = ParsGentral.ValueOrDefault("city");

                            System.Json.JsonValue cityPars = JsonValue.Parse(CityName.ToString());

                            var NameValue = CityName["name"];

                            string sdf = NameValue.ToString();

                            if (NameValue.ToString() == "\"Pushcha-Voditsa\"")

                            {
                                town = "Погода в Киеве";
                            }

                            // Температура


                            System.Json.JsonValue ParsGentralTemp = JsonValue.Parse(Gentral);

                            var List0 = ParsGentralTemp.ValueOrDefault("list");
                            var List1 = List0[0];

                            System.Json.JsonValue tempPars = JsonValue.Parse(List1.ToString());

                            var TempValue1 = tempPars["main"];

                            System.Json.JsonValue tempPars2 = JsonValue.Parse(TempValue1.ToString());

                            var TempValue2 = tempPars2["temp"];

                            temp = "+" + TempValue2.ToString();


                            // Оссадки

                            System.Json.JsonValue ParsGentralOsadki = JsonValue.Parse(Gentral);

                            var ListOsadki0 = ParsGentralOsadki.ValueOrDefault("list");
                            var ListOsadki1 = ListOsadki0[0];

                            System.Json.JsonValue OsadkiPars = JsonValue.Parse(ListOsadki1.ToString());

                            string OsadkiValue1 = OsadkiPars["weather"].ToString();

                            string osadkiTemp2 = new Regex(@"""description"":""(?<osadkiTemp2>[^<]+)"",").Match(OsadkiValue1).Groups["osadkiTemp2"].Value; // на будущее наклонную черту в регексе \ менять на "

                            osadki = osadkiTemp2;


                            // Давление

                            System.Json.JsonValue ParsGentralPressure = JsonValue.Parse(Gentral);

                            var ListPressure0 = ParsGentralPressure.ValueOrDefault("list");
                            var ListPressure1 = ListPressure0[0];

                            System.Json.JsonValue PressurePars = JsonValue.Parse(ListPressure1.ToString());

                            var PressureValue1 = PressurePars["main"];

                            System.Json.JsonValue PressurePars2 = JsonValue.Parse(PressureValue1.ToString());

                            var PressureValue2 = PressurePars2["pressure"];

                            pressure = Math.Round(Double.Parse(Convert.ToString(PressureValue2).Replace('.', ',')) * 0.75006375541921, 0).ToString() + " мм. рт. ст.";

                            // Облачность


                            System.Json.JsonValue ParsGentralOsadky = JsonValue.Parse(Gentral);

                            var List00 = ParsGentralOsadky.ValueOrDefault("list");
                            var List11 = List00[0];

                            System.Json.JsonValue cloudnessPars = JsonValue.Parse(List11.ToString());

                            var CloudnessValue1 = cloudnessPars["clouds"];

                            System.Json.JsonValue cloudnessPars2 = JsonValue.Parse(CloudnessValue1.ToString());

                            var cloudnessPars3 = cloudnessPars2["all"];


                            cloudness = cloudnessPars3.ToString();


                        }






                    }

                    
                }
            }

            else
            {
                if (chechboxischecked == "true")

                {


                    string lat = latGeneral;
                    string lon = lonGeneral;

                    // json
                    var Gentral = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/find?lat=" + lat + "&lon=" + lon + "&APPID=c5ba134efa510143f5193f7d8e1f5bc7&units=metric");


                    // Город

                    //var GentralTestCity = new System.Net.WebClient().DownloadString("http://api.openweathermap.org/data/2.5/forecast/weather?lat=35&lon=139&APPID=c5ba134efa510143f5193f7d8e1f5bc7");
                    //System.Json.JsonValue ParsGentraltest = JsonValue.Parse(GentralTestCity);

                    System.Json.JsonValue ParsGentral = JsonValue.Parse(Gentral);

                    var CityName = ParsGentral.ValueOrDefault("city");

                    System.Json.JsonValue cityPars = JsonValue.Parse(CityName.ToString());

                    var NameValue = CityName["name"];

                    

                    

                    
                        town = "Погода в " + NameValue.ToString();


                    // Температура


                    System.Json.JsonValue ParsGentralTemp = JsonValue.Parse(Gentral);

                    var List0 = ParsGentralTemp.ValueOrDefault("list");
                    var List1 = List0[0];

                    System.Json.JsonValue tempPars = JsonValue.Parse(List1.ToString());

                    var TempValue1 = tempPars["main"];

                    System.Json.JsonValue tempPars2 = JsonValue.Parse(TempValue1.ToString());

                    var TempValue2 = tempPars2["temp"];

                    temp = "+" + TempValue2.ToString();


                    // Оссадки

                    System.Json.JsonValue ParsGentralOsadki = JsonValue.Parse(Gentral);

                    var ListOsadki0 = ParsGentralOsadki.ValueOrDefault("list");
                    var ListOsadki1 = ListOsadki0[0];

                    System.Json.JsonValue OsadkiPars = JsonValue.Parse(ListOsadki1.ToString());

                    string OsadkiValue1 = OsadkiPars["weather"].ToString();

                    string osadkiTemp2 = new Regex(@"""description"":""(?<osadkiTemp2>[^<]+)"",").Match(OsadkiValue1).Groups["osadkiTemp2"].Value; // на будущее наклонную черту в регексе \ менять на "

                    osadki = osadkiTemp2;


                    // Давление

                    System.Json.JsonValue ParsGentralPressure = JsonValue.Parse(Gentral);

                    var ListPressure0 = ParsGentralPressure.ValueOrDefault("list");
                    var ListPressure1 = ListPressure0[0];

                    System.Json.JsonValue PressurePars = JsonValue.Parse(ListPressure1.ToString());

                    var PressureValue1 = PressurePars["main"];

                    System.Json.JsonValue PressurePars2 = JsonValue.Parse(PressureValue1.ToString());

                    var PressureValue2 = PressurePars2["pressure"];

                    pressure = Math.Round(Double.Parse(Convert.ToString(PressureValue2).Replace('.', ',')) * 0.75006375541921, 0).ToString() + " мм. рт. ст.";

                    // Облачность


                    System.Json.JsonValue ParsGentralOsadky = JsonValue.Parse(Gentral);

                    var List00 = ParsGentralOsadky.ValueOrDefault("list");
                    var List11 = List00[0];

                    System.Json.JsonValue cloudnessPars = JsonValue.Parse(List11.ToString());

                    var CloudnessValue1 = cloudnessPars["clouds"];

                    System.Json.JsonValue cloudnessPars2 = JsonValue.Parse(CloudnessValue1.ToString());

                    var cloudnessPars3 = cloudnessPars2["all"];


                    cloudness = cloudnessPars3.ToString();

                }
           }







            TBCityResult.Text = town;
                    TBtempResult.Text = temp;
            TBCloudnessResult.Text = cloudness;
                    TBCloudsResult.Text = osadki;
                    TBPressureResult.Text = pressure;

                    InterFase();

            
        }



        private void InterFase()
        {
            if (TBCloudsResult.Text == "малооблачно")
            {
                image.Source = new BitmapImage(new Uri("/image/pogoda9.png", UriKind.Relative));
            }

            else
            {
                if (TBCloudsResult.Text == "пасмурно (есть просветы)" || TBCloudsResult.Text == "scattered clouds")
                {
                    image.Source = new BitmapImage(new Uri("/image/pogoda2.png", UriKind.Relative));

                }

                else
                {
                    if (TBCloudsResult.Text == "пасмурно (без просветов)" || TBCloudsResult.Text == "значительная облачность")
                    {
                        image.Source = new BitmapImage(new Uri("/image/pogoda24.png", UriKind.Relative));

                    }

                    else
                    {
                        if (TBCloudsResult.Text == "облачно (6 баллов)" || TBCloudsResult.Text == "облачно (4 балла)" || TBCloudsResult.Text == "few clouds")
                        {
                            image.Source = new BitmapImage(new Uri("/image/pogoda24.png", UriKind.Relative));

                        }

                        else
                        {
                            if (TBCloudsResult.Text == "ясно" || TBCloudsResult.Text == "clear sky")
                            {
                                image.Source = new BitmapImage(new Uri("/image/pogoda20.png", UriKind.Relative));

                            }

                            else

                            if (TBCloudsResult.Text == "moderate rain")
                            {
                                image.Source = new BitmapImage(new Uri("/image/pogoda11.png", UriKind.Relative));

                            }
                            else

                            if (TBCloudsResult.Text == "light rain")
                            {
                                image.Source = new BitmapImage(new Uri("/image/pogoda11.png", UriKind.Relative));

                            }


                        }
                    }

                }
            }
        }

        public async void refreshasyn(Object sender, EventArgs args)
        {
            if (server == "meteoservicehtml")

            {

                refreshMeteoserviceHTML();
                
            }

            else

           if (server == "meteoservicexml")
            {
                refreshMeteoserviceXML();
                
            }


        }

        private void Window_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            sPosition = e.GetPosition(this);

        }

        private void Window_PreviewMouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);
                this.Left -= sPosition.X - p.X; //DELTA 
                this.Top -= sPosition.Y - p.Y;
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings Window = new Settings();
            Window.mw = this;
            Window.Owner = this;
            Window.ShowDialog();
        }

        private static string GetFullFileName(string keyName)
        {
            String strAppDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + "\\Settings\\";
            System.IO.Directory.CreateDirectory(strAppDir);
            return System.IO.Path.Combine(strAppDir, keyName);
        }


        private static string loadKeys(string name)
        {
            if (File.Exists(GetFullFileName(name)))
                return File.ReadAllText(GetFullFileName(name));
            else
                return ""; //empty string if no key

        }

        public static string GetKey2(string key)
        {
            string keys = loadKeys("settings.ini");

            key = key.ToLower();

            string result = "";

            List<String> textList = keys.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].Contains(key + " "))
                {
                    result = textList[i].Substring(key.Length + 1);

                    return result;
                }
            }

            return "";
            //
        }

    }
}
