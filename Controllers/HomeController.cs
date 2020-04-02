using LibraryManagement.DAL;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{  
    //[SessionAuthorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.SoftwareName = SoftwareInfo.SoftwareName;
        }

        public ActionResult Index()
        {
            GeneralUtil DbOpObj = new GeneralUtil();

            ViewBag.Books = DbOpObj.Count("books");
            ViewBag.Students = DbOpObj.Count("student");
            ViewBag.Publications = DbOpObj.Count("publication");
            ViewBag.Categories = DbOpObj.Count("category");
            ViewBag.Books_Issued = DbOpObj.Count("issue");
            ViewBag.Books_Return_Pending = DbOpObj.CountByArgs("issue", "status = 0");
            ViewBag.Books_Returned = DbOpObj.CountByArgs("issue", "status = 1");
            ViewBag.Total_Penalties_Collected = DbOpObj.SumByArgs("issue", "amountCollected", "status = 1").ToString("C");

            return View();
        }

        public ActionResult AddPublication()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPublication(Publication pubObj)
        {

            HomeDbUtil db = new HomeDbUtil();
            int rows = db.insertPublication(pubObj);
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
            return RedirectToAction("ManagePublication");
        }
        
        public ActionResult ManagePublication()
        {
            /*
             * Creating object of DAL class `HomeDbUtil`
             */
            HomeDbUtil dbObj = new HomeDbUtil();

            /*
             * List class of type `Publication` model
             * List is a dynamic generic collection array 
             */
            List<Publication> details = dbObj.GetAllPublications();
            return View(details);
        }

        public ActionResult EditPublication(int id)
        {
            HomeDbUtil dbObj = new HomeDbUtil();

            Publication pubObj = dbObj.GetPublicationByID(id);

            return View(pubObj);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditPublication(Publication pubObj)
        {
            HomeDbUtil dbObj = new HomeDbUtil();

            int rows = dbObj.updatePublication(pubObj);
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
            return RedirectToAction("ManagePublication");
        }

        // Manage Category methods starts here
        public ActionResult AddCategory()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCategory(Category catObj)
        {

            HomeDbUtil db = new HomeDbUtil();
            int rows = db.insertCategory(catObj);
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
            return RedirectToAction("ManageCategory");
        }
        
        public ActionResult ManageCategory()
        {
            /*
             * Creating object of DAL class `HomeDbUtil`
             */
            HomeDbUtil dbObj = new HomeDbUtil();

            List<Category> details = dbObj.GetAllCategory();
            return View(details);
        }

        public ActionResult EditCategory(int id)
        {
            HomeDbUtil dbObj = new HomeDbUtil();

            Category catObj = dbObj.GetCategoryByID(id);

            return View(catObj);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditCategory(Category catObj)
        {
            HomeDbUtil dbObj = new HomeDbUtil();

            int rows = dbObj.updateCategory(catObj);
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
            return RedirectToAction("ManageCategory");
        }

    }
}