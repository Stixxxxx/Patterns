using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfApp1.Error;

namespace WpfApp1.ErrorWind
{
    class ErrorWithThread
    {
        LogWindow dialogLW;
        string text;

        public ErrorWithThread(string text_, LogWindow dialogLW_)
        {

            dialogLW = dialogLW_;
            text = text_;
        }

        public void LogWindow() { 

        dialogLW.tbError.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { dialogLW.tbError.Text += text; }));

        }
    }
}
