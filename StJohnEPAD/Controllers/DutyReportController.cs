using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;
using StJohnEPAD.DAL;
using WebMatrix.WebData;

namespace StJohnEPAD.Controllers
{
    public class DutyReportController : Controller
    {
        private SJAContext db = new SJAContext();


        public ActionResult Index()
        {
            return View(db.PostDutyReport.ToList());
        }

        //


        public ActionResult Details(int id = 0)
        {
            PostDutyReport postdutyreport = db.PostDutyReport.Find(id);
            if (postdutyreport == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            postdutyreport.Attendees = db.ConfirmedDutyHours.Where(x => x.DutyID == id).ToList();
            return View(postdutyreport);
        }


        public ActionResult Edit(int id = 0)
        {
            PostDutyReport postdutyreport = db.PostDutyReport.Find(id);
            ICollection<ConfirmedDutyHours> hours = new LinkedList<ConfirmedDutyHours>();

            if (postdutyreport == null)
            {
                ViewBag.Message = "A report has not yet been created for this duty. Please create one now, or get in touch with the responsible person to do so.";
                ViewBag.MemberGridData = "";

                var users = db.Users.ToList();
                

                foreach (UserProfile up in users)
                {
                    hours.Add(new ConfirmedDutyHours{ UserId = up.UserId });
                }
                ViewBag.allProfiles = hours;

                return View(new PostDutyReport { DutyID = id });
            }



            //Developer note: todo: this is a massive mess!!!! it will need sorted but for now it works
            //And there are more pressing issues.  It's terrible, I know.


            //if it already exists, we need to sort out our duty hours table

            var allUsers = db.Users.Where(x => x.CurrentRole != CurrentRoleEnum.Inactive).ToList();
            var alreadyAdded = db.ConfirmedDutyHours.Where(x => x.DutyID == id);
            List<UserProfile> upList = new List<UserProfile>();
            //now combine these two
            //Check all of the users
            foreach (UserProfile up in allUsers)
            {
                bool needToAdd = true;
                //If they already havea confirmed hours record for this duty, add it to the hours list
                foreach(ConfirmedDutyHours cdh in alreadyAdded)
                    if (up.UserId == cdh.UserId)
                    {
                        hours.Add(cdh);
                        needToAdd = false;
                        break;
                    }
                //otherwise make a new record for them
                if (needToAdd)
                {
                    hours.Add(new ConfirmedDutyHours { UserId = up.UserId });
                }
                upList.Add(up);
            }
            ViewBag.extraUsers = upList;
            ViewBag.allProfiles = hours;
            return View(postdutyreport);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostDutyReport returnedReport)
        {
            if (ModelState.IsValid)
            {
                //Check to see if we need to make a new report
                var currentReport = db.PostDutyReport.Find(returnedReport.DutyID);
                if (currentReport != null)
                {
                    //it already exists, let's just update it
                    currentReport.AnyOtherComments = returnedReport.AnyOtherComments;
                    currentReport.PRFNumbers = returnedReport.AnyOtherComments;
                    currentReport.LastUpdated = DateTime.Now;
                    currentReport.UserId = WebSecurity.CurrentUserId;
                    db.SaveChanges();
                }
                else
                {
                    //otherwise we had better make a new record
                    currentReport = returnedReport;
                    currentReport.LastUpdated = DateTime.Now;
                    currentReport.UserId = WebSecurity.CurrentUserId;
                    db.PostDutyReport.Add(currentReport);
                    db.SaveChanges();
                }


                db.Entry(currentReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = currentReport.DutyID });
            }
            return View(returnedReport);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}