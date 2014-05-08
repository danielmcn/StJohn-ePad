using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;
using StJohnEPAD.ViewModels;
using StJohnEPAD.DAL;
using WebMatrix.WebData;

namespace StJohnEPAD.Controllers
{
    [Authorize]
    public class DutyController : Controller
    {
        private SJAContext db = new SJAContext();

        //
        // GET: /Duty/

        public ActionResult Index(string viewAll)
        {
            if (User.IsInRole("Administrator") || User.IsInRole("DutyAdmin"))
                if(viewAll != null)
                if (viewAll.Equals("true"))
                {
                    ViewBag.ViewCurrentOrAllLink = "allMode";
                    return View(db.Duties.ToList());
                }
            ViewBag.ViewCurrentOrAllLink = "currentMode";

            /*
             * //todo: this might be workable (conditional include for duty singups)
             * //http://blogs.msdn.com/b/alexj/archive/2009/10/13/tip-37-how-to-do-a-conditional-include.aspx
            var dbquery = 
               from duties in db.Duties 
               select new { 
                  duties,
                  signups = from signup in duties.DutySignups
                            where signup.UserId == WebSecurity.CurrentUserId && signup.DutyID == duties.DutyID
                            select signup
               };
            var duties2 = dbquery.AsEnumerable().ToList();
            var beep = 2;
             */

            return View(db.Duties.Where(x => x.DutyDate >= DateTime.Now).Include(x => x.DutySignups).ToList());
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
            
            
            //Todo: put into a viewmodel?
            List<String> drvList = new List<String>();
            
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DutyResponseValue? drv in Enum.GetValues(typeof(DutyResponseValue)))
            {
                items.Add(new SelectListItem { Text = drv.ToString(), Value = drv.ToString() });
                drvList.Add(drv.ToString());
            }
            //ViewBag.itemList = new SelectList(items);
            ViewBag.itemList = items;
            //ViewBag.itemList = drvList;

            var response = db.DutyAvailabilty.Where(x => x.UserId == WebSecurity.CurrentUserId && x.DutyID == duty.DutyID).FirstOrDefault();
            StJohnEPAD.Models.DutyResponseValue? value;
            if(response != null)
                value = response.DutyAvailabilityResponse;
            else
                 value = DutyResponseValue.NoResponse;

            ViewBag.selectedItem = value.Value.ToString();


            switch (value)
            {
                case (StJohnEPAD.Models.DutyResponseValue.NoResponse):
                    //ViewBag.UserAvailability = "No Response";

                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Available):
                    //ViewBag.UserAvailability = "Available";
                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Unavailable):
                    //ViewBag.UserAvailability = "Unvailable";
                    break;
                default:
                    //ViewBag.UserAvailability = "No Response";
                    break;
            }
            
            if(User.IsInRole("Administrator") || User.IsInRole("DutyAdmin"))
            {
                return View(db.Duties.Include(x => x.DutySignups).Where(x => x.DutyID == id).FirstOrDefault());
            }
            else
            {
                return View(duty);
            }
        }
        
        [HttpPost]
        public ActionResult Details(FormCollection form, int id = 0)
        {
            Duty duty = db.Duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }

            //Add or update the availabilty

            var result = Request.Form.GetValues(0);
            DutyResponseValue? response;
            if (result[0].ToString() == "Available")
                response = DutyResponseValue.Available;
            else if (result[0].ToString() == "Unavailable")
                response = DutyResponseValue.Unavailable;
            else
                response = DutyResponseValue.NoResponse;
                
            //DutyResponseValue? response = (DutyResponseValue?)Enum.Parse(typeof(DutyResponseValue?), result[0].ToString());
            // = (DutyResponseValue?)2;

            var currAvailability = db.DutyAvailabilty.Where(x => x.DutyID == id && x.UserId == WebSecurity.CurrentUserId).FirstOrDefault();
            
            if (currAvailability != null)
            {
                //Already exists, update it
                currAvailability.DutyAvailabilityResponse = response;
                db.Entry(currAvailability).State = EntityState.Modified;
                db.SaveChanges();
                //save to db
            }
            else
            {
                //Create a new record
                currAvailability = new DutyAvailability { DutyAvailabilityResponse = response, UserId = WebSecurity.CurrentUserId, DutyID = id };
                db.DutyAvailabilty.Add(currAvailability);
                db.SaveChanges();
            }

            //Todo: put into a viewmodel?
            DutyResponseValue? value = currAvailability.DutyAvailabilityResponse;

            switch (value)
            {
                case (StJohnEPAD.Models.DutyResponseValue.NoResponse):
                    ViewBag.UserAvailability = "No Response";
                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Available):
                    ViewBag.UserAvailability = "Available";
                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Unavailable):
                    ViewBag.UserAvailability = "Unavailable";
                    break;
                default:
                    ViewBag.UserAvailability = "No Response";
                    break;
            }
            if (User.IsInRole("Administrator") || User.IsInRole("DutyAdmin"))
            {
                return View(db.Duties.Include(x => x.DutySignups).Where(x => x.DutyID == id).FirstOrDefault());
            }
            else
            {
                return View(duty);
            }
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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