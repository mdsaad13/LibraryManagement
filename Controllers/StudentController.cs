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
        public StudentController()
        {
            ViewBag.SoftwareName = SoftwareInfo.SoftwareName;
        }

        // GET: Student
        public ActionResult Index()
        {
            /*
             *Creating object of DAL class `HomeDbUtil`
             */
            HomeDbUtil dbObj = new HomeDbUtil();

            List<Student> details = dbObj.GetAllStudents();
            return View(details);
        }

        public ActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Student student)
        {
            student.BookIssuedCount = 0;
            HomeDbUtil db = new HomeDbUtil();
            int rows = db.insertStudent(student);
            if (rows > 0)
            {
                /*
                 * if insert success
                 */
                Session["Notification"] = 1;
            }
            else
            {
                /*
                 * if insert failure
                 */
                Session["Notification"] = 2;

            }
            return RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();

            Student student = homeDbUtil.GetStudentByID(id);

            return View(student);
        }

        public ActionResult Edit(int id)
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();

            Student student = homeDbUtil.GetStudentByID(id);

            return View(student);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            HomeDbUtil dbObj = new HomeDbUtil();

            int rows = dbObj.updateStudent(student);
            if (rows > 0)
            {
                /*
                 * if update success
                 */
                Session["Notification"] = 3;
            }
            else
            {
                /*
                 * if update failure
                 */
                Session["Notification"] = 4;
            }
            return RedirectToAction("Index");
        }
    }
}