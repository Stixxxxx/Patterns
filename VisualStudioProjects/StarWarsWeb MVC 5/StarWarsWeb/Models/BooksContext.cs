using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StarWarsWeb.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}