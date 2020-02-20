using LibraryManagement.DAL;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    //[SessionAuthorize]
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            /*
             *Creating object of DAL class `HomeDbUtil`
             */
            HomeDbUtil dbObj = new HomeDbUtil();

            List<Student> details = new List<Student>();
            details = dbObj.GetAllStudents();
            return View(details);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}