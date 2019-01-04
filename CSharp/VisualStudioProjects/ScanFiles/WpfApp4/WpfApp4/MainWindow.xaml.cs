using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource CancellationToken;
        Task ScanTask;
        public MainWindow()
        {
            InitializeComponent();
         

        }

        public string ReLoad(string path)
        {
            var directories = new List<DirectoryInfo> { new DirectoryInfo(path) };

            while (directories.Count > 0)
            {
                if (CancellationToken.IsCancellationRequested)
                    return "cancel";

                var curent = directories.First();
                try
                {
                    directories.AddRange(curent.GetDirectories());
                    curent
                        .GetFiles()
                        .ToList()
                        .ForEach(x => Dispatcher.Invoke(() => lbFilDir.Items.Add(x.FullName)));
                }
                catch { }
                finally
                {
                    directories.Remove(curent);
                }
            }

            return "good";
        }

        void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            CancellationToken.Cancel();
        }

        private void Button_Click_Go(object sender, RoutedEventArgs e)
        {
            
            CancellationToken = new CancellationTokenSource();
            ScanTask = new Task(() => ReLoad("c:"), CancellationToken.Token);
            Loaded += (s, a) => ScanTask.Start();
            
        }
    }
}
