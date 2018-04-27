using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarWarsWeb.Models
{
    public class Book
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public byte[] imageByte { get; set; }

    }
}