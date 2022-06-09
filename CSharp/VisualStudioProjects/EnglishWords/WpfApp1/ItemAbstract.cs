using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    abstract class ItemAbstract : IItem
    {
        public int id { get; set; }

        public void PrintId() {

            Console.WriteLine(id);
        }
        
    }
}
