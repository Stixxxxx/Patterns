using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfApp1.Error;

namespace CallBook
{
    class Log
    {

        public static void addToLog(string text)
        {


        


            DateTime localDate = DateTime.Now;
      
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Log.txt", true))
            {
                file.WriteLine(localDate + " | " + text);

          
            }
            
        }


        

             
    }
    
}