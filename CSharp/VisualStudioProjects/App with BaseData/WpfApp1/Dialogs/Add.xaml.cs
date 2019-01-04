using System;
using System.Collections.Generic;
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
using WpfApp1.DataBase;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mv = this.Owner as MainWindow;
            DataBaseMobilePhone add = new DataBaseMobilePhone();
            add.Add(tbxModel.Text, tbxManufacturer.Text, tbxPrice.Text);
            mv.ReadDataBaseMobilePhone();
            

            Close();
        }
    }
}
