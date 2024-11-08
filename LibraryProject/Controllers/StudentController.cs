using LibraryProject.DAL;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class StudentController : Controller
    {
        StudentContext _context = null;
        public StudentController()
        {
            _context = new StudentContext();
        }
        public ActionResult Index()
        {
           List<Student>Slist = _context.Students.ToList();
            return View(Slist);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {

                HttpPostedFileBase photoFile = Request.Files["PhotoFile"];
                if (photoFile != null && photoFile.ContentLength > 0)
                {
                    string ImagePath = "/Images/" + photoFile.FileName;
                    s.Photo = ImagePath;
                    photoFile.SaveAs(Server.MapPath(ImagePath));
                }


                HttpPostedFileBase videoFile = Request.Files["VideoFile"];
                if (videoFile != null && videoFile.ContentLength > 0)
                {
                    string VideoPath = "/Images/" + videoFile.FileName;
                    s.Video = VideoPath;
                    videoFile.SaveAs(Server.MapPath(VideoPath));
                }
                _context.Students.Add(s);
                _context.SaveChanges();
        
                List<Student> slist = _context.Students.ToList();
                return View("Index", slist);
            }
            else
            {
                return View("Create", s);
            }
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Student s = _context.Students.Find(Id);
            return View(s);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if (ModelState.IsValid)
            {
                Student x = _context.Students.Find(s.StudentId);
                x.Name = s.Name;
                x.Class = s.Class;
                if (Request.Files["PhotoFile"] != null)
                {
                    HttpPostedFileBase Photofile = Request.Files["PhotoFile"];
                    if (Photofile != null && Photofile.ContentLength > 0)
                    {
                        string ImagePath = "/Images/" + Photofile.FileName;
                        x.Photo = ImagePath;
                        Photofile.SaveAs(Server.MapPath(ImagePath));
                    }
                }
                if (Request.Files["VideoFile"] != null)
                {
                    HttpPostedFileBase Videofile = Request.Files["VideoFile"];
                    if (Videofile != null && Videofile.ContentLength > 0)
                    {
                        string VideoPath = "/Images/" + Videofile.FileName;
                        x.Video = VideoPath;
                        Videofile.SaveAs(Server.MapPath(VideoPath));
                    }
                }
                _context.SaveChanges();
                return View("Index", _context.Students.ToList());
            }
            else
            {
                return View("Edit", s);
            }
        }
        public ActionResult Delete(int id)
        {
            Student p = _context.Students.Find(id);
            _context.Students.Remove(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}