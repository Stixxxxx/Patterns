using Dropbox.Api;
using StarWarsWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarWarsWeb.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BooksContext db = new BooksContext();

        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Коллекция книг Star Wars.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Мои контакты";

            return View();
        }

        public ActionResult Books()
        {
            db.Books
            string[] pathBook = Directory.GetFiles(Server.MapPath("~/Books/"));

            List <StarWarsWeb.Models.Book> Books = new List<StarWarsWeb.Models.Book>();

            int count=0;
            StarWarsWeb.Models.Book book;
            string nameBook;
            string[] arrayBookPath;
            foreach (var item in pathBook)
            {
                count++; 
                book = new StarWarsWeb.Models.Book();
                //book.id = count;
                arrayBookPath = item.Split('\\');
                nameBook = arrayBookPath.Last();
                book.name = nameBook;
                book.path = item;

                Books.Add(book);
                db.Books.Add(book);
                
            }
            db.SaveChanges();

            IEnumerable<StarWarsWeb.Models.Book> BooksModel = Books;

            //BooksModel = db.Books;


            ViewBag.Message = "Your contact page.";

            return View(BooksModel);
        }
               
       

        
        public FileResult DownloadFile(int idBook)
        {
            string[] pathBook = Directory.GetFiles(Server.MapPath("~/Books/"));
            string path = pathBook[idBook];
            // Тип файла - content-type
            string file_type = "application/fb2";
            var arrayBookPath = path.Split('\\');
            string nameBook = arrayBookPath.Last();

            return File(path, file_type, nameBook);
        }

       public  static async Task Run()
        {
            using (var dbx = new DropboxClient("S15kjJEg4LAAAAAAAAB612yN2KkNQ8FaCFl8CeWunnbUrx-kN57RjCxIrDqH9e1i"))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }
    }
}