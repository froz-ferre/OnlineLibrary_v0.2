using OnlineLibrary.Helpers;
using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities(); // database variable
        //private int pageSize = 3; // amount of books on one page

        //public ActionResult Index(int pageNum = 0, string sort = "")
        //{
        //    ViewData["pageNum"] = pageNum; // current page
        //    ViewData["itemsCount"] = db.Books.Count(); // common amount of pages
        //    ViewData["pageSize"] = pageSize;


        //    var books = (from book in db.Books
        //                 orderby book.Title
        //                 select book).Skip(pageSize * pageNum).Take(pageSize).ToList();

        //    SelectList sort_param = new SelectList(new string[] { "author", "title"});
        //    ViewBag.Books = sort_param;
        //    return View(books);
        //}

        //GET: Home
        public ActionResult Index(string sort = "Title", short? able = null/*, int page = 0*/)
        {
            //ViewData["page"] = page; // current page
            //ViewData["itemsCount"] = db.Books.Count(); // common amount of pages
            //ViewData["pageSize"] = pageSize;

            SelectList sort_param = new SelectList(new string[] { "Author", "Title" });
            ViewBag.Books = sort_param;

            ViewBag.Sort = sort;
            // LINQuery .Skip(pageSize * pageNum).Take(pageSize)
            var books = (from book in db.Books select book).ToList();

            switch (sort)
            {
                case "Title":   // sort by title
                    books = (from book in db.Books
                             orderby book.Title
                             where book.Availible == able || able == null
                             select book)/*.Skip(pageSize * page).Take(pageSize)*/.ToList();
                    return View(books);
                case "Author":  // sort by authors
                    books = (from book in db.Books
                             orderby book.Authors
                             where book.Availible == able || able == null
                             select book)/*.Skip(pageSize * page).Take(pageSize)*/.ToList();
                    return View(books);
                default: return View(books);
            }
        }


        // GET: Details/id
        public ActionResult Details(int id)
        {
            if (Request.Cookies["AUTH"] == null)
                return Redirect("/Account/Login");
            // LINQuery
            var bookDetails = (from book in db.Books
                               where book.BookID == id
                               select book).First();
            return View(bookDetails);
        }

        // GET: BookHistory/id
        public ActionResult BookHistory(int id)
        {
            var bookHistory = (from history in db.Histories
                               where history.BookID == id
                               select history).ToList();
            return View(bookHistory);
        }

        // POST: TakeBook/id
        //[HttpPost]
        public ActionResult TakeBook(int id)
        {
            var book = (from b in db.Books
                        where b.BookID == id
                        select b).First();

            History history = new History();

            if (Request.Cookies["AUTH"] == null)
                return Redirect("/Account/Login");
            else if (book.Availible == 0)
                return Redirect("/Home/Details/" + id);
            else
            {
                string email = Request.Cookies["AUTH"].Value;
                var mail = new Mail();
                var user = (from u in db.Users
                            where u.Email == email
                            select u.UserID).First();

                history.UserID = user; 
                history.BookID = id;
                history.TakeDate = DateTime.Now;
                history.BackDate = DateTime.MinValue;

                book.Availible = 0;
                

                try
                {
                    UpdateModel(book);
                    db.Histories.Add(history);
                    db.SaveChanges();
                    mail.SendMessage(email);
                    return Redirect("/Home/Details/" + id);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Oops", ex);
                    return Redirect("Index");
                }

            }

        }

        public ActionResult ReturnBook (int id)
        {
            var book = (from b in db.Books
                        where b.BookID == id
                        select b).First();
            var tmp_history = (from h in db.Histories
                           where h.BackDate == DateTime.MinValue
                           select h);
            var history = tmp_history.Where(h => h.BookID == id).First();

            book.Availible = 1;
            history.BackDate = DateTime.Now;
            try
            {
                UpdateModel(book);
                UpdateModel(history);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Oops", ex);
            }
            return Redirect("/Home/Details/" + id);
        }
    }
}