using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using LibraryProject.DAL;
using LibraryProject.Models;
using System.Runtime.Remoting.Messaging;

namespace LibraryProject.Controllers
{
    public class BookController : Controller
    {
        private readonly StudentContext _context;
        public BookController() : base()
        {
            _context = new StudentContext();
        }
        public ActionResult Index()
        {
            List<Book>Blist = _context.Books.ToList();
            return View(Blist);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book b)
        {
           
            if (ModelState.IsValid)
            {
                _context.Books.Add(b);
                _context.SaveChanges();
                List<Book> books = _context.Books.ToList();
                return View("Index", books);
            }
            else
            {
                return View("Create", b);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book b=_context.Books.Find(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Book b)
        {
            if (ModelState.IsValid)
            {
                Book x = _context.Books.Find(b.BookId);
                x.Name = b.Name;
                x.Author = b.Author;
                x.Publications = b.Publications;
                x.Year = b.Year;
                _context.SaveChanges();
                return View("Index", _context.Books.ToList());
            }
            else
            {
                return View("Edit", b);
            }
        }
        public ActionResult Delete(int id)
        {
            Book k= _context.Books.Find(id);
            _context.Books.Remove(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}