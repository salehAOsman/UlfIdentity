using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UlfIdentity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Admin")] //we assign role that we created to About we go to "_LoginPartial.cshtml" to add few sentences like "I am admin" 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //we can add here this validation to make every one register or log in to see app
        [Authorize(Roles ="Commen")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}