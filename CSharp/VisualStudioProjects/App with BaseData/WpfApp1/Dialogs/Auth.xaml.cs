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
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public static bool isAuth = false;
        

        public bool AuthForMV
        {
            get
            {
                return isAuth;
            }
        }

        public Auth()
        {
            InitializeComponent();
            tblError.Text = "Вы не авторизованы в базе MySQL";
            

        }

        private void btnOk_Click_Ok(object sender, RoutedEventArgs e)
        {


            DataBaseAuth Authorisation = new DataBaseAuth();
            isAuth = Authorisation.Authorisation(tbxLogin.Text, pbPass.Password);

            if (isAuth) {

                MainWindow mv = this.Owner as MainWindow;
                imgOk.Visibility = Visibility.Visible;
                tblError.Text = "Вы авторизованы (база MySQL)";
                mv.tblAuth.Text = "Вы авторизованы (база MySQL)";
                mv.lvMobilePhone.Visibility = Visibility.Visible;

            }
            else 
            {
                MainWindow mv = this.Owner as MainWindow;
                imgOk.Visibility = Visibility.Hidden;
                tblError.Text = "Вы не авторизованы MySQL(база MySQL)";
                mv.tblAuth.Text = "Вы не авторизованы MySQL(база MySQL)";
                

            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            if (isAuth)
            {
                MainWindow mv = this.Owner as MainWindow;
                mv.tblAuth.Text = "Вы авторизованы (база MySQL)";
                mv.lvMobilePhone.Visibility = Visibility.Visible;
                
            }
            else
            {
                MainWindow mv = this.Owner as MainWindow;
                mv.tblAuth.Text = "Вы не авторизованы (база MySQL)";
                


            }
            

            Close();
        }
    }
}
