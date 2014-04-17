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

namespace StJohnEPAD.Controllers
{
    public class UsersController : Controller
    {
        private SJAContext db = new SJAContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            /*
            var viewModel = new SingleUserTrainingRecordViewModel();
            viewModel.User = db.Users.Find(id);
            if (viewModel.User == null)
            {
                return HttpNotFound();
            }

            viewModel.TrainingRecords = db.TrainingRecord.Where(x => x.UserId == id);
             * 
             * return View(viewModel);
            */

            UserProfile user = (UserProfile)db.Users.Include(u => u.Qualifications).Where(u => u.UserId == id).FirstOrDefault();
            
            if(user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.Users.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(userprofile).State = EntityState.Modified;
                var currentProfile = db.Users.FirstOrDefault(u => u.UserId == userprofile.UserId);
                currentProfile.EmailAddress = userprofile.EmailAddress;
                currentProfile.TelephoneNumber = userprofile.TelephoneNumber;
                //currentProfile.Qualifications = new LinkedList<TrainingRecord>();
                //currentProfile.Qualifications.Add(new TrainingRecord { TrainingType = "TESTTYPE", TrainingDate = new DateTime(year: 2011, month: 1, day: 1) });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}