using LibraryManagement.DAL;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class IssueController : Controller
    {
        public IssueController()
        {
            ViewBag.SoftwareName = SoftwareInfo.SoftwareName;
        }

        public ActionResult Index()
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();

            IssueBundle issueBundle = new IssueBundle();
            issueBundle.Issued = homeDbUtil.GetAllIssue(0);
            issueBundle.Returned = homeDbUtil.GetAllIssue(1);

            ViewBag.BooksList = new SelectList(homeDbUtil.GetAllBooks(1), "id", "title");
            ViewBag.StudentsList = new SelectList(homeDbUtil.GetAllStudents(1), "id", "name");

            return View(issueBundle);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(IssueBundle issueBundle)
        {
            issueBundle.NewIssue.IssueDate = DateTime.Now;
            issueBundle.NewIssue.ReturnedOn = issueBundle.NewIssue.ReturnDate;
            issueBundle.NewIssue.PenaltyAmount = 0;
            issueBundle.NewIssue.Status = 0;

            HomeDbUtil homeDbUtil = new HomeDbUtil();
            int rows = homeDbUtil.insertIssue(issueBundle.NewIssue);
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
        
        public ActionResult Return(int IssueID)
        {
            HomeDbUtil homeDbUtil = new HomeDbUtil();
            if (homeDbUtil.UpdateIssue(IssueID))
            {
                // Success
                Session["Notification"] = 3;
            }
            else
            {
                // Failed
                Session["Notification"] = 4;
            }
            return RedirectToAction("Index");
        }
    }
}