using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class AdminController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            var books = (from book in db.Books
                         select book).ToList();
            return View(books);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var bookDetails = (from book in db.Books
                               where book.BookID == id
                               select book).First();
            return View(bookDetails); 
        }

        // GET: Admin/Add
        public ActionResult Add()
        {
            Book book = new Book();
            return View(book);
        }

        // POST: Admin/Add
        [HttpPost]
        public ActionResult Add(Book book)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Oops", ex);
            }
            return View();
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var bookEdit = (from book in db.Books
                               where book.BookID == id
                               select book).First();
            return View(bookEdit);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var bookEdit = (from book in db.Books
                            where book.BookID == id
                            select book).First();

            try
            {
                // TODO: Add update logic here
                UpdateModel(bookEdit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(bookEdit);
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var bookDelete = (from book in db.Books
                              where book.BookID == id
                              select book).First();
            return View(bookDelete);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var bookDelete = (from book in db.Books
                              where book.BookID == id
                              select book).First();
            try
            {
                // TODO: Add delete logic here
                db.Books.Remove(bookDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(bookDelete);
            }
        }
    }
}
