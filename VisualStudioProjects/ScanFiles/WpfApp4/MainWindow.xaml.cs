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

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "D:\\";
        
        string[] fullfilesPath;

        string[] fulldirPath;

        public MainWindow()
        {
            InitializeComponent();
            
        }
                

        public void ReLoad(string path)
        {

            //try
            //{ 

            //var directories = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            //foreach (var d in directories)
            //{
            //    lbFilDir.Items.Add(d);
            //}

            //}

            //catch
            //{

            //}


            //fullfilesPath = Directory.GetFiles(path, "*.*");
            //fulldirPath = Directory.GetDirectories(path);

            //for (int ii = 0; ii < fullfilesPath.Length; ii++)

            //{ 

            //    for (int i = 0; i < fullfilesPath.Length; i++)
            //{ 
            //    lbFilDir.Items.Add(fullfilesPath[i]);

            //}

            //for (int i = 0; i < fulldirPath.Length; i++)
            //{

            //    ReLoad(fulldirPath[0]);

            //}

            //    ReLoad(fulldirPath+"\\..");

            //}

            IEnumerable<string> a = GetFileList("*.*", path);


        }


        public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
        {
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(rootFolderPath);
            string[] tmp;
            while (pending.Count > 0)
            {
                rootFolderPath = pending.Dequeue();
                try
                {
                    tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                for (int i = 0; i < tmp.Length; i++)
                {
                    yield return tmp[i];
                }
                tmp = Directory.GetDirectories(rootFolderPath);
                for (int i = 0; i < tmp.Length; i++)
                {
                    pending.Enqueue(tmp[i]);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


          ReLoad(path);

        }
    }

}
