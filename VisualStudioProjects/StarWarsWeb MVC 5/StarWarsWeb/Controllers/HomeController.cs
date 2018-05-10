using Dropbox.Api;
using Dropbox.Api.Files;
using StarWarsWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarWarsWeb.Controllers

{
    public class HomeController : Controller
    {
        static DropboxClient dbx;
        private static byte[] bytesArray;

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
            List<Book> listBook = new List<Book>();

            IList<Metadata> file = Task.Run(() => Run()).Result;

            //Task.Run(Run);
            //task.Wait();
            //IList<Metadata> file = task.Result;

            int count = 0;
            Book book;
            foreach (var item in file)
            {
                count++;
                book = new Book();
                book.id = count;
                book.name = item.Name;
                book.path = item.PathLower;
                listBook.Add(book);
            }

            IEnumerable<StarWarsWeb.Models.Book> booksModel = listBook;



            return View(booksModel);
        }


        public ActionResult KieferUu()
        {

            return View();
        }



        public FileResult DownloadFile(string path)
        {

            string[] substrings = Regex.Split(path, "/");


            Task.Run(() => Download(path)).Wait();
            

            string file_type = "application/fb2";
            string file_name = substrings.Last();
            return File(bytesArray, file_type, file_name);
        }


        static async Task<IList<Metadata>> Run()
        {
            using (dbx = new DropboxClient("S15kjJEg4LAAAAAAAAB612yN2KkNQ8FaCFl8CeWunnbUrx-kN57RjCxIrDqH9e1i"))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();

                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
                // show folders then files

                var list = await dbx.Files.ListFolderAsync("/starwars");

                IList<Metadata> listEnumerator = list.Entries;

                foreach (var item in list.Entries.Where(i => i.IsFolder))
                {
                    Console.WriteLine("D  {0}/", item.Name);
                }

                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
                }
                return listEnumerator;
            }
        }


        static async Task Download(string path)
        {

            DropboxClient dbxDownl;
            using (dbxDownl = new DropboxClient("S15kjJEg4LAAAAAAAAB612yN2KkNQ8FaCFl8CeWunnbUrx-kN57RjCxIrDqH9e1i"))
            {

                using (var response = await dbxDownl.Files.DownloadAsync(path))
                {
                    bytesArray = await response.GetContentAsByteArrayAsync();
                }
            }

        }
    }
}