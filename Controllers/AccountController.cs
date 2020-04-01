using LibraryManagement.DAL;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    
    public class AccountController : Controller
    {
        public AccountController()
        {
            ViewBag.SoftwareName = SoftwareInfo.SoftwareName;
        }

        [IsSessionAuthorize]
        public ActionResult Login()
        {
            return View();
        }

        //Post method of Login
        [IsSessionAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Admin adminObj)
        {
            /*
             * Creating object of DAL class AccountDbUtil
             */
            AccountDbUtil db = new AccountDbUtil();

            /*
             * Passing parameters to Login method of Account class
             * @param Email
             * @param Password
             * 
             * @return object
             */

            /*
             * Creating object of Model class UserInfo
             */
            Admin result = new Admin();


            result = db.Login(adminObj.Email, adminObj.Password);

            if (result.ID != 0)
            {
                /*
                 * if user exist then set Session and redirect to Home page
                 */
                Session["userID"] = result.ID;
                Session["userName"] = result.Name;
                Session["userEmail"] = result.Email;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                /*
                 * if user does not exist then display error message
                 */
                ViewBag.Error = @"
                    <div class=""alert alert-danger wow shake"" role=""alert"" data-wow-delay=""1s"">
                      Incorrect Email or Password!
                    </div>
                ";
                return View();
            }
        }
        
        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}