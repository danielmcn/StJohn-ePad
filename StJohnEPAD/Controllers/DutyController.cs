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
    public class DutyController : Controller
    {
        private SJAContext db = new SJAContext();

        //
        // GET: /Duty/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(db.Duties.ToList());
        }

        //
        // GET: /Duty/Details/5

        public ActionResult Details(int id = 0)
        {
            Duty duty = db.Duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }
            return View(duty);
        }

        //
        // GET: /Duty/Create
        [Authorize(Roles = "Administrator,DutyAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Duty/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator,DutyAdmin")]
        public ActionResult Create(Duty duty)
        {
            if (ModelState.IsValid)
            {
                db.Duties.Add(duty);
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }

            return View(duty);
        }

        //
        // GET: /Duty/Edit/5
        [Authorize(Roles = "Administrator,DutyAdmin")]
        public ActionResult Edit(int id = 0)
        {
            Duty duty = db.Duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }
            return View(duty);
        }

        //
        // POST: /Duty/Edit/5
        [Authorize(Roles = "Administrator,DutyAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Duty duty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            return View(duty);
        }

        //
        // GET: /Duty/Delete/5
        [Authorize(Roles = "Administrator,DutyAdmin")]
        public ActionResult Delete(int id = 0)
        {
            Duty duty = db.Duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }
            return View(duty);
        }

        //
        // POST: /Duty/Delete/5
        [Authorize(Roles = "Administrator,DutyAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Duty duty = db.Duties.Find(id);
            db.Duties.Remove(duty);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
    /// <summary>
    /// By default, ASP.net sends a 401 a code to any unauthorised user, redirecting them to the login page
    /// This isn't an ideal situation, so instead we use this to send them a 403 error
    /// http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-au
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

}