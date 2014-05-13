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
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace StJohnEPAD.Controllers
{
    [Authorize]
    public class DutyController : Controller
    {
        private SJAContext db = new SJAContext();

        /// <summary>
        /// GET: /Duty/
        /// Return a list of the duties in the system, either those in the future
        /// or all of them depending on whether the viewAll parameter is used
        /// </summary>
        /// <param name="viewAll">
        ///     Indicate if we want to view all duties, not just those upcoming
        ///     This feature will only work if the user has suitable permissions to do so
        /// </param>
        /// <returns>
        ///     A View containing a List of Duty objects, where there duty date exceed the current
        ///     date
        ///     If viewAll == true, and user == admin, we will return ALL duties in the system
        /// </returns>
        public ActionResult Index(string viewAll)
        {
            if (User.IsInRole("Administrator") || User.IsInRole("DutyAdmin"))
                if(viewAll != null)
                if (viewAll.Equals("true"))
                {
                    ViewBag.ViewCurrentOrAllLink = "allMode";
                    return View(db.Duties.ToList().OrderBy(x=>x.DutyDate));
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

            return View(db.Duties.Where(x => EntityFunctions.TruncateTime(x.DutyDate) >= EntityFunctions.TruncateTime(DateTime.Now)).Include(x => x.DutySignups).OrderBy(x=> x.DutyDate).ToList());
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
                    ViewBag.UserAvailability = "No Response";

                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Available):
                    ViewBag.UserAvailability = "Available";
                    break;
                case (StJohnEPAD.Models.DutyResponseValue.Unavailable):
                    ViewBag.UserAvailability = "Unvailable";
                    break;
                default:
                    ViewBag.UserAvailability = "No Response";
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(duty).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(duty);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Duty)entry.Entity;
                var databaseValues = (Duty)entry.GetDatabaseValues().ToObject();

                if (databaseValues.DutyName != clientValues.DutyName)
                    ModelState.AddModelError("DutyName", "Current value: "
                        + databaseValues.DutyName);
                if (databaseValues.DutyDate != clientValues.DutyDate)
                    ModelState.AddModelError("DutyDate", "Current value: "
                        + databaseValues.DutyDate);
                if (databaseValues.DutyStartTime != clientValues.DutyStartTime)
                    ModelState.AddModelError("DutyStartTime", "Current value: "
                        + databaseValues.DutyStartTime);
                if (databaseValues.DutyEndTime != clientValues.DutyEndTime)
                    ModelState.AddModelError("DutyEndTime", "Current value: "
                        + databaseValues.DutyEndTime);
                if (databaseValues.DutyLocation != clientValues.DutyLocation)
                    ModelState.AddModelError("DutyLocation", "Current value: "
                        + databaseValues.DutyLocation);
                if (databaseValues.DutyDescription != clientValues.DutyDescription)
                    ModelState.AddModelError("DutyDescription", "Current value: "
                        + databaseValues.DutyDescription);
                if (databaseValues.DutyAdditionalNotes != clientValues.DutyAdditionalNotes)
                    ModelState.AddModelError("DutyAdditionalNotes", "Current value: "
                        + databaseValues.DutyAdditionalNotes);
                if (databaseValues.DutyOrganiser != clientValues.DutyOrganiser)
                    ModelState.AddModelError("DutyOrganiser", "Current value: "
                        + databaseValues.DutyOrganiser);
                if (databaseValues.DutyOrganiserPhoneNumber != clientValues.DutyOrganiserPhoneNumber)
                    ModelState.AddModelError("DutyOrganiserPhoneNumber", "Current value: "
                        + databaseValues.DutyOrganiserPhoneNumber);
                if (databaseValues.DutyOrganiserEmailAddress != clientValues.DutyOrganiserEmailAddress)
                    ModelState.AddModelError("DutyOrganiserEmailAddress", "Current value: "
                        + databaseValues.DutyOrganiserEmailAddress);
                if (databaseValues.DutyLocationLong != clientValues.DutyLocationLong)
                    ModelState.AddModelError("DutyLocationLong", "Current value: "
                        + databaseValues.DutyLocationLong);
                if (databaseValues.DutyLocationLat != clientValues.DutyLocationLat)
                    ModelState.AddModelError("DutyLocationLat", "Current value: "
                        + databaseValues.DutyLocationLat);
                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");
                duty.RowVersion = databaseValues.RowVersion;
            }
            catch (DataException /*dataException*/)
            {
                
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
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