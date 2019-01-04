using CallBook;
using System;
using System.Collections;
using System.Collections.Generic;
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
using WpfApp1.DataBase;
using WpfApp1.Dialogs;
using WpfApp1.Error;
using WpfApp1.ErrorWind;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogWindow dialogLW = new LogWindow();
        Log2 Window;
        Thread thread;

        public MainWindow()
        {
            InitializeComponent();

            DataBaseAuth init = new DataBaseAuth();
            init.Init();

            DataBaseMobilePhone initDbMbp = new DataBaseMobilePhone();
            initDbMbp.Init();


            ReadDataBaseMobilePhone();

            lvMobilePhone.Visibility = Visibility.Hidden;
            //lvMobilePhone.ContextMenu.
            //Правильная версия

            

            Window = new Log2(dialogLW, thread);
            dialogLW = Window.ShowWindow("Ошибка");
            
            Thread.Sleep(3000);
            dialogLW.tbError.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { dialogLW.tbError.Text += "Получил управление потоком \n"; }));

            Thread.Sleep(3000);

            ErrorWithThread errwiththread = new ErrorWithThread("Главное окно загружено\nНажмите кнопку \"ДОБАВИТЬ\" для демонстрации работы консоли\n", dialogLW);
            errwiththread.LogWindow();

        }

        public void ReadDataBaseMobilePhone()
        {
            

            DataBaseMobilePhone readDbMbp = new DataBaseMobilePhone();
            List<MobilePhone> MobilePhoneList = readDbMbp.GetDataBase();
            List<MobilePhone> items = new List<MobilePhone>();

            for (int i = 0; i < MobilePhoneList.Count; i++)

            {

                int id = MobilePhoneList[i].id;
                string model = MobilePhoneList[i].model;
                string manufacturer = MobilePhoneList[i].manufacturer;
                string price = MobilePhoneList[i].price;



                items.Add(new MobilePhone() { id = MobilePhoneList[i].id, model = MobilePhoneList[i].model, manufacturer = MobilePhoneList[i].manufacturer, price = MobilePhoneList[i].price });


            }

            lvMobilePhone.ItemsSource = items;
            
            //lvMobilePhone.ItemsSource = MobilePhoneList;
                        
        }

        private void Button_Click_Auth(object sender, RoutedEventArgs e)
        {
            
            Auth dialog = new Auth();
            tblAuth.Text = Auth.isAuth.ToString();
            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();
            
            if (Auth.isAuth)
            {
                tblAuth.Text = "Вы авторизованы в базе MySQL";
            }
            else
            {
                tblAuth.Text = "Вы не авторизованы в базе MySQL";
            }
            
        }

        private void Button_Click_Regis(object sender, RoutedEventArgs e)
        {
            
            Regist dialog = new Regist();
            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ErrorWithThread errwiththread = new ErrorWithThread("Из главного потока нажали кнапочку\n", dialogLW);
            errwiththread.LogWindow();


            Add dialog = new Add();
            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMobilePhone.SelectedItem != null)
            {
                MobilePhone list = new MobilePhone();

                list = (MobilePhone)lvMobilePhone.SelectedItem;

                int id = list.id;


                DataBaseMobilePhone dbMF = new DataBaseMobilePhone();
                dbMF.Remove(id);

                ReadDataBaseMobilePhone();


            }

        }
    }
}
