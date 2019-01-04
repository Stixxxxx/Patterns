using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Weather
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public MainWindow mw;
        public int namberitemcombo;
        public Settings()
        {
            InitializeComponent();
            comboBox.SelectedIndex = Convert.ToInt32(GetKey2("namberitemcombo"));
            cmbxServer.SelectedIndex = Convert.ToInt32(GetKey2("namberitemcmbxserver"));
            
            textBoxlat.Text = GetKey2("lat");
            textBox1long.Text = GetKey2("lon");

            checkBox.IsChecked = Convert.ToBoolean(GetKey2("chechboxischecked"));



        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btn_ClickServer(object sender, RoutedEventArgs e)
        {

            if (checkBox.IsChecked == false)

            {
                SetKey2("chechboxischecked", "false");

                // все по MeteoserviceXML


                if (comboBox.SelectedItem == SPb && cmbxServer.SelectedItem == MeteoserviceXML)

                {

                    mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                    MainWindow.server = "meteoservicexml";
                    MainWindow.city = "sankt-peterburg";

                    SetKey2("namberitemcombo", "0");
                    SetKey2("city", "sankt-peterburg");
                    SetKey2("namberitemcmbxserver", "0");
                    SetKey2("server", "meteoservicexml");

                    mw.refreshMeteoserviceXML();
                }

                else
                {
                    if (comboBox.SelectedItem == Mskw && cmbxServer.SelectedItem == MeteoserviceXML)

                    {

                        mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                        MainWindow.server = "meteoservicexml";
                        MainWindow.city = "moskva";

                        SetKey2("namberitemcombo", "1");
                        SetKey2("city", "moskva");

                        SetKey2("namberitemcmbxserver", "0");
                        SetKey2("server", "meteoservicexml");

                        mw.refreshMeteoserviceXML();
                    }

                    else
                    {
                        if (comboBox.SelectedItem == Mskw && cmbxServer.SelectedItem == MeteoserviceXML)

                        {

                            mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                            MainWindow.server = "meteoservicexml";
                            MainWindow.city = "moskva";

                            SetKey2("namberitemcombo", "1");
                            SetKey2("city", "moskva");

                            SetKey2("namberitemcmbxserver", "0");
                            SetKey2("server", "meteoservicexml");

                            mw.refreshMeteoserviceXML();
                        }


                        else

                        {
                            if (comboBox.SelectedItem == Kv && cmbxServer.SelectedItem == MeteoserviceXML)

                            {

                                mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                MainWindow.server = "meteoservicexml";
                                MainWindow.city = "kiev";

                                SetKey2("namberitemcombo", "2");
                                SetKey2("city", "kiev");

                                SetKey2("namberitemcmbxserver", "0");
                                SetKey2("server", "meteoservicexml");

                                mw.refreshMeteoserviceXML();
                            }

                            // все по MeteoserviceHTML

                            else
                            {

                                if (comboBox.SelectedItem == SPb && cmbxServer.SelectedItem == MeteoserviceHTML)

                                {

                                    mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                    MainWindow.server = "meteoservicexml";
                                    MainWindow.city = "sankt-peterburg";

                                    SetKey2("namberitemcombo", "0");
                                    SetKey2("city", "sankt-peterburg");

                                    SetKey2("namberitemcmbxserver", "1");
                                    SetKey2("server", "meteoservicehtml");

                                    mw.refreshMeteoserviceHTML();
                                }



                                else
                                {


                                    if (comboBox.SelectedItem == Mskw && cmbxServer.SelectedItem == MeteoserviceHTML)

                                    {

                                        mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                        MainWindow.server = "meteoservicehtml";
                                        MainWindow.city = "moskva";

                                        SetKey2("namberitemcombo", "1");
                                        SetKey2("city", "moskva");

                                        SetKey2("namberitemcmbxserver", "1");
                                        SetKey2("server", "meteoservicehtml");

                                        mw.refreshMeteoserviceHTML();
                                    }


                                    else

                                    {
                                        if (comboBox.SelectedItem == Kv && cmbxServer.SelectedItem == MeteoserviceHTML)

                                        {

                                            mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                            MainWindow.server = "meteoservicehtml";
                                            MainWindow.city = "kiev";

                                            SetKey2("namberitemcombo", "2");
                                            SetKey2("city", "kiev");

                                            SetKey2("namberitemcmbxserver", "1");
                                            SetKey2("server", "meteoservicehtml");

                                            mw.refreshMeteoserviceHTML();
                                        }

                                        // Все по openweathermap


                                        if (comboBox.SelectedItem == SPb && cmbxServer.SelectedItem == Openweathermap)

                                        {

                                            mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                            MainWindow.server = "openweathermap";
                                            MainWindow.city = "sankt-peterburg";

                                            SetKey2("namberitemcombo", "0");
                                            SetKey2("city", "sankt-peterburg");
                                            SetKey2("namberitemcmbxserver", "2");
                                            SetKey2("server", "openweathermap");

                                            mw.refreshOpenweathermap();
                                        }

                                        else
                                        {
                                            if (comboBox.SelectedItem == Mskw && cmbxServer.SelectedItem == Openweathermap)

                                            {

                                                mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                                MainWindow.server = "openweathermap";
                                                MainWindow.city = "moskva";

                                                SetKey2("namberitemcombo", "1");
                                                SetKey2("city", "moskva");

                                                SetKey2("namberitemcmbxserver", "2");
                                                SetKey2("server", "openweathermap");

                                                mw.refreshOpenweathermap();
                                            }




                                            else

                                            {
                                                if (comboBox.SelectedItem == Kv && cmbxServer.SelectedItem == Openweathermap)

                                                {

                                                    mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                                                    MainWindow.server = "openweathermap";
                                                    MainWindow.city = "kiev";

                                                    SetKey2("namberitemcombo", "2");
                                                    SetKey2("city", "kiev");

                                                    SetKey2("namberitemcmbxserver", "2");
                                                    SetKey2("server", "openweathermap");

                                                    mw.refreshOpenweathermap();
                                                }






                                            }

                                        }
                                    }
                                }

                            }
                        }

                    }

                }

            }


            else

            {
                if (checkBox.IsChecked == true)

                {

                    SetKey2("chechboxischecked", "true");

                    mw.image.Source = new BitmapImage(new Uri("/image/pogoda10.png", UriKind.Relative));

                    //                                                    MainWindow.server = "openweathermap";
                    MainWindow.lonGeneral = textBoxlat.Text;
                    MainWindow.latGeneral = textBox1long.Text;

                    SetKey2("namberitemcombo", "2");
                    SetKey2("lon", textBox1long.Text);
                    SetKey2("lat", textBoxlat.Text);

                    //                                                  SetKey2("namberitemcmbxserver", "2");
                    //                                                SetKey2("server", "openweathermap");

                    mw.refreshOpenweathermap();
                }






            }




            //---------------
















            this.Close();




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


        public static void SetKey2(string key, string value)
        {
            string keys = loadKeys("settings.ini");

            key = key.ToLower();

            List<String> textList = keys.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].Contains(key + " ")) //если ключ существует
                {
                    textList[i] = key + " " + value;
                    keys = "";
                    for (int j = 0; j < textList.Count; j++)
                        keys += textList[j] + "\r\n";

                    if (File.Exists(GetFullFileName("settings.ini"))) File.Delete(GetFullFileName("settings.ini"));
                    File.WriteAllText(GetFullFileName("settings.ini"), keys);

                    return;
                }
            }

            //если нет
            keys += key + " " + value + "\r\n";
            if (File.Exists(GetFullFileName("settings.ini"))) File.Delete(GetFullFileName("settings.ini"));
            File.WriteAllText(GetFullFileName("settings.ini"), keys);


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

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            textBoxlat.IsEnabled = true;
            textBox1long.IsEnabled = true;

            cmbxServer.IsEnabled = false;
            comboBox.IsEnabled = false;

        }

        private void checkBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textBoxlat.IsEnabled = false;
            textBox1long.IsEnabled = false;

            cmbxServer.IsEnabled = true;
            comboBox.IsEnabled = true;
        }
    }
}
