using DevLifeTG.Models;
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
        public ActionResult Play()
        {
           return View();
        }
        [Authorize(Users = "richardtwise@gmail.com")]
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult SprocTest()
        {
            TravieIOEntities1 entities = new TravieIOEntities1();
            return View(entities.GetTopScore());
        }
    
    }
}