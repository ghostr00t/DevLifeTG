using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevLifeTG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            ViewBag.Message = "Profile page.";

            return View();
        }
        [Authorize(Users = "richardtwise@gmail.com")]
        public ActionResult Manage()
        {
            ViewBag.Message = "Manage page.";

            return View();
        }
    }
}