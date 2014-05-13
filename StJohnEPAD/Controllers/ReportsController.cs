using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Equipment()
        {
            return View();
        }

        public ActionResult Training()
        {
            return View();
        }

        public ActionResult Duties()
        {
            return View();
        }

        public ActionResult CoverCalculator()
        {
            return View();
        }

    }
}
