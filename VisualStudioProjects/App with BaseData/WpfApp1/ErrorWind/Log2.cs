using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfApp1.Error;

namespace WpfApp1.ErrorWind
{
    class Log2
    {

        LogWindow dialogLW;
        
        DateTime localDate = DateTime.Now;

        Thread thread;

        public Log2(LogWindow dialogLW_, Thread thread_)
        {
            dialogLW = dialogLW_;
            thread = thread_;

        }


        public LogWindow ShowWindow(string text)
        {
                     
            

            string error = localDate + " | " + text +"\n";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Log.txt", true))
            {
                file.WriteLine(error);
            }

            Thread thread = new Thread(ShowWindow2);
            thread.SetApartmentState(ApartmentState.STA);
            
            thread.IsBackground = true;
            bool result = thread.TrySetApartmentState(ApartmentState.MTA);
            thread.Start();

            Thread.Sleep(1000);
            dialogLW.tbError.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { dialogLW.tbError.Text += "Управление потоком из класса \n"; }));
                                   

            return dialogLW;
            
        }
        
        private void ShowWindow2()
        {
            dialogLW = new LogWindow();

            dialogLW.Show();

            dialogLW.tbError.Text = "Лог\n";

            System.Windows.Threading.Dispatcher.Run();
                        
        }
        
    }


}

