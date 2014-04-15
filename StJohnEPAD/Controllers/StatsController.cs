using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Controllers
{
    [Authorize(Roles="Administrator")]
    public class StatsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "View stats about the division";

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
