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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fullPath;

        List<ItemWord> words;



        public MainWindow()

        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private async void open(object sender, RoutedEventArgs e)
        {
           
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document
                fullPath = dialog.FileName;
            }
             
            Utilits utilits = new Utilits();


            List<ItemWord> words = new List<ItemWord>();

            ButtonOpen.IsEnabled = false;

            await Task.Run(() =>
            {

                

                Task<List<ItemWord>> result2 = Utilits.DoSortAsync(fullPath);
                words = result2.Result;
                return;
            });

            ButtonOpen.IsEnabled = true;

            dataGrid.ItemsSource = words;

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}
