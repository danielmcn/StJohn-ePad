using StJohnEPAD.DAL;
using StJohnEPAD.Models;
using StJohnEPAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace StJohnEPAD.Controllers
{
    [Authorize]
    public class MyDutySignupController : Controller
    {
        //First things first: get our database
        private SJAContext db = new SJAContext();
        
        
        //
        // GET: /MyDutySignup/
        [Authorize]
        public ActionResult Index()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            
            //first get our existing results
            var signupResult =
                from duties in db.Duties
                join signups in db.DutyAvailabilty
                on duties.DutyID equals signups.DutyID
                where signups.UserId == userId
                select signups;
                
            //then get a list of all duties
            var dutyResult =
                from duties in db.Duties
                select duties;

            //now join the 2#
            var fullResult =
                (from duties in dutyResult
                 join signups in signupResult
                 on duties.DutyID equals signups.DutyID
                 select signups);

            return View(fullResult.ToList());
        }

        //
        // GET: /MyDutySignup/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MyDutySignup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyDutySignup/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MyDutySignup/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MyDutySignup/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MyDutySignup/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MyDutySignup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
