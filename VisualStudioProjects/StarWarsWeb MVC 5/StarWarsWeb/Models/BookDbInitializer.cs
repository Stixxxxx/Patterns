using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Dropbox.Api;
using StarWarsWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarWarsWeb.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<BooksContext>
    {
        protected override void Seed(BooksContext db)
        {

            base.Seed(db);
        }
    }
}