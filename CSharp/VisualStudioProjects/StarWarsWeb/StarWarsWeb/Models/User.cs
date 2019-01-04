using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarWarsWeb.Models
{
    public class User
    {
        public int id { get; set; }
        public string ip { get; set; }
        public string country { get; set; }
        public string browser { get; set; }
        public DateTime time { get; set; }
    }
}