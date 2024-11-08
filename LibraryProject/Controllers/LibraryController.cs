using LibraryProject.DAL;
using LibraryProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class LibraryController : Controller
    {
        private readonly StudentContext library_context;
        public LibraryController()
        {
            library_context = new StudentContext();
        }

        public ActionResult Index()
        {
            var libraries = library_context.Libraries.Include("Student").Include("Book").ToList();
            return View(libraries);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> libraryList = new List<SelectListItem>();
            foreach (Student ab in library_context.Students)
            {
                libraryList.Add(new SelectListItem()
                {
                    Text=ab.Name,
                    Value=ab.StudentId.ToString()
                });
            }
            List<SelectListItem>libraryList1 = new List<SelectListItem>();
            foreach(Book c in library_context.Books)
            {
                libraryList1.Add(new SelectListItem()
                {
                    Text=c.Name,
                    Value=c.BookId.ToString()
                 });

            }
            ViewBag.StudentId = libraryList;
            ViewBag.BookId=libraryList1;
            return View();

        }
        [HttpPost]
        public ActionResult Create(Library library)
        {
            
            if (!ModelState.IsValid)
            {
                
                ViewBag.StudentId = new SelectList(library_context.Students, "StudentId", "Name");
                ViewBag.BookId = new SelectList(library_context.Books, "BookId", "Name");
                return View(library); 
            }

            
            if (library.StartDate == default(DateTime))
            {
                library.StartDate = DateTime.Now;
            }

            if (library.EndDate == default(DateTime))
            {
                library.EndDate = DateTime.Now.AddDays(14);
            }

            
            library_context.Libraries.Add(library);
            library_context.SaveChanges();

            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Library l = library_context.Libraries.Find(Id);
            List<SelectListItem>std=new List<SelectListItem>();
            foreach(Student mg in library_context.Students)
            {
                if (mg.StudentId == l.StudentId)
                {
                    std.Add(new SelectListItem() 
                    {
                        Text = mg.Name,
                        Value=mg.StudentId.ToString(),Selected=true
                    });

                }
                else
                {
                    std.Add(new SelectListItem() 
                    {
                        Text = mg.Name,
                        Value=mg.StudentId.ToString()
                    });

                }
            }
            List<SelectListItem>l1=new List<SelectListItem>();
            foreach (Book bo in library_context.Books)
            {
                if (bo.BookId == l.BookId)
                {
                    l1.Add(new SelectListItem()
                    {
                        Text = bo.Name,
                        Value = bo.BookId.ToString(),
                        Selected = true
                    });

                }
                else
                {
                    l1.Add(new SelectListItem() 
                    {
                        Text = bo.Name,
                        Value = bo.BookId.ToString()
                    });

                }
            }
            ViewBag.StudentId = std;
            ViewBag.BookId = l1;
            return View();
         
        }
        [HttpPost]
        public ActionResult Edit(Library l)
        {
            Library x = library_context.Libraries.Find(l.LibraryId);
            x.StudentId = l.StudentId;
            x.BookId = l.BookId;
            x.StartDate = l.StartDate;
            x.EndDate = l.EndDate;
            library_context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            Library lb = library_context.Libraries.Find(Id);
            library_context.Libraries.Remove(lb);
            library_context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}