using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace StarWars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _pathApp;
        private string _pathBooks;
        private string[] _listBooks;
        private string resultSearch;
        private  ListViewItem itemSearch;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _pathApp = Environment.CurrentDirectory;
            _pathBooks = _pathApp + "\\Star Wars Expanded Universe";
            _listBooks = Directory.GetFiles(_pathBooks);

            foreach (var pathBook in _listBooks)
            {
                var arrayBookPath = pathBook.Split('\\');
                var nameBook = arrayBookPath.Last();
                var item = new ListViewItem {Content = nameBook , Tag = pathBook};
                item.MouseDoubleClick += ItemOnMouseDoubleClick;
                item.Selected += ItemSelect;
                ListBooksLv.Items.Add(item);
            }
        }

        private void ItemSelect(object sender, RoutedEventArgs e)
        {
            var book = (ListViewItem)sender;
            var pathBook = book.Tag.ToString();
            
            string base64 = GetBase64Cover(pathBook);
            if (!string.IsNullOrEmpty(base64))
            {
                byte[] imageData = ConvertBase64ToByteArray(base64);
                BitmapImage image = ConvertByteArrayToBitmapImage(imageData);
                CoverImage.Source = image;
            }
            else
            {
                var test = new BitmapImage(new Uri("defaultCover.jpg", UriKind.Relative));

                CoverImage.Source = test;
            }
        }

        private void ItemOnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var book = (ListViewItem)ListBooksLv.SelectedItem;
            var pathBook = book.Tag.ToString();
            Process.Start(pathBook);

            string base64 = GetBase64Cover(pathBook);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBooksLv.Items.Clear();
            string search = SearchTb.Text;

            foreach (var pathBook in _listBooks)
            {
                var arrayBookPath = pathBook.Split('\\');
                var nameBook = arrayBookPath.Last();

                if (nameBook.Contains(search))
                {
                    resultSearch = nameBook;
                    itemSearch = new ListViewItem { Content = nameBook, Tag = pathBook };
                    itemSearch.MouseDoubleClick += ItemOnMouseDoubleClick;
                    itemSearch.Selected += ItemSelect;
                    ListBooksLv.Items.Add(itemSearch);
                }
            }
        }


        private static string GetBase64Cover(string pathBook)
        {
            XmlNodeType type;
            string base64 = string.Empty;
            try
            {
            XmlTextReader xtwpath = new XmlTextReader(pathBook);
            while (xtwpath.Read())
            {
                type = xtwpath.NodeType;
                if (type == XmlNodeType.Element)
                {
                    if (xtwpath.Name == "binary")
                    {
                        xtwpath.Read();
                        base64 = xtwpath.Value;
                    }
                }
            }
            return base64;
            }
            catch (Exception e)
            {
             
            }
            return string.Empty;
        }

        static void SaveImageBook(string base64, string fileName)
        {
            if (string.IsNullOrEmpty(base64)) return;

            var buffer = System.Convert.FromBase64String(base64);
            using (var file = File.Create(fileName))
            {
                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }
        }

        static byte[] ConvertBase64ToByteArray(string base64)
        {
            if (string.IsNullOrEmpty(base64));
            var buffer = System.Convert.FromBase64String(base64);
            return buffer;
        }

        static BitmapImage ConvertByteArrayToBitmapImage(byte[] imageData)
        {

            BitmapImage image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            return image;
        }
    }
}
