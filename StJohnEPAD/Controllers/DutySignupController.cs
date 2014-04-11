using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;
using StJohnEPAD.DAL;

namespace StJohnEPAD.Controllers
{
    public class DutySignupController : Controller
    {
        private SJAContext db = new SJAContext();

        //
        // GET: /DutySignup/

        public ActionResult Index()
        {
            var dutyavailabilty = db.DutyAvailabilty.Include(d => d.Duty).Include(d => d.UserProfile);
            return View(dutyavailabilty.ToList());
        }

        //
        // GET: /DutySignup/Details/5

        public ActionResult Details(int id = 0)
        {
            DutyAvailability dutyavailability = db.DutyAvailabilty.Find(id);
            if (dutyavailability == null)
            {
                return HttpNotFound();
            }
            return View(dutyavailability);
        }

        //
        // GET: /DutySignup/Create

        public ActionResult Create()
        {
            ViewBag.DutyID = new SelectList(db.Duties, "DutyID", "DutyName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /DutySignup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DutyAvailability dutyavailability)
        {
            if (ModelState.IsValid)
            {
                db.DutyAvailabilty.Add(dutyavailability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DutyID = new SelectList(db.Duties, "DutyID", "DutyName", dutyavailability.DutyID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", dutyavailability.UserId);
            return View(dutyavailability);
        }

        //
        // GET: /DutySignup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DutyAvailability dutyavailability = db.DutyAvailabilty.Find(id);
            if (dutyavailability == null)
            {
                return HttpNotFound();
            }
            ViewBag.DutyID = new SelectList(db.Duties, "DutyID", "DutyName", dutyavailability.DutyID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", dutyavailability.UserId);
            return View(dutyavailability);
        }

        //
        // POST: /DutySignup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DutyAvailability dutyavailability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dutyavailability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DutyID = new SelectList(db.Duties, "DutyID", "DutyName", dutyavailability.DutyID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", dutyavailability.UserId);
            return View(dutyavailability);
        }

        //
        // GET: /DutySignup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DutyAvailability dutyavailability = db.DutyAvailabilty.Find(id);
            if (dutyavailability == null)
            {
                return HttpNotFound();
            }
            return View(dutyavailability);
        }

        //
        // POST: /DutySignup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DutyAvailability dutyavailability = db.DutyAvailabilty.Find(id);
            db.DutyAvailabilty.Remove(dutyavailability);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}