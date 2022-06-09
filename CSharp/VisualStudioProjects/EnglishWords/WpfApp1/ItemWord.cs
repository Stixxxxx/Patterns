using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class ItemWord : ItemAbstract
    {


        public int id { get; set; }
        public string? Word { get; set; }
        public  bool IsUsed { get; set; }
        public bool IsAddedRepeat { get; set; }
        public int Count { get; set; }

        public string? Meaning { get; set; }

    }
}
