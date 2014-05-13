using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;
using StJohnEPAD.DAL;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data.Entity.Infrastructure;

namespace StJohnEPAD.Controllers
{
    [Authorize(Roles="Administrator")]
    public class EquipmentController : Controller
    {
        private SJAContext db = new SJAContext();
        private string imageSaveLocation = "~/Uploads";

        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            if(db.Equipment == null)
                return View();
            return View(db.Equipment.ToList());
            //return View(db.Equipment.ToList());
        }

        //
        // GET: /Equipment/Details/5

        public ActionResult Details(int id = 0)
        {
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // GET: /Equipment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Equipment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Equipment equipment, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                    if(image.ContentLength > 0)
                    {
                        //var fileName1 = Path.GetFileName(image.FileName);
                        var extension = Path.GetExtension(image.FileName);
                        var fileName = Guid.NewGuid().ToString() + extension;
                        equipment.ImageFilename = fileName;
                        var path = Path.Combine(Server.MapPath(imageSaveLocation), fileName);
                        image.SaveAs(path);
                    }

                db.Equipment.Add(equipment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(equipment);
        }
        public bool ThumbnailCallback()
        {
            return false;
        }

        //
        // GET: /Equipment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // POST: /Equipment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Equipment equipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(equipment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(equipment);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Equipment)entry.Entity;
                var databaseValues = (Equipment)entry.GetDatabaseValues().ToObject();

                if (databaseValues.EquipmentName != clientValues.EquipmentName)
                    ModelState.AddModelError("EquipmentName", "Current value: "
                        + databaseValues.EquipmentName);
                if (databaseValues.EquipmentLastCheck != clientValues.EquipmentLastCheck)
                    ModelState.AddModelError("EquipmentLastCheck", "Current value: "
                        + databaseValues.EquipmentLastCheck);
                if (databaseValues.EquipmentNextCheck != clientValues.EquipmentNextCheck)
                    ModelState.AddModelError("EquipmentNextCheck", "Current value: "
                        + databaseValues.EquipmentNextCheck);
                if (databaseValues.EquipmentDescription != clientValues.EquipmentDescription)
                    ModelState.AddModelError("EquipmentDescription", "Current value: "
                        + databaseValues.EquipmentDescription);
                if (databaseValues.EquipmentNotes != clientValues.EquipmentNotes)
                    ModelState.AddModelError("EquipmentNotes", "Current value: "
                        + databaseValues.EquipmentNotes);
                if (databaseValues.ImageFilename != clientValues.ImageFilename)
                    ModelState.AddModelError("ImageFilename", "Current value: "
                        + databaseValues.ImageFilename);
                if (databaseValues.EquipmentCheckInBy != clientValues.EquipmentCheckInBy)
                    ModelState.AddModelError("EquipmentCheckInBy", "Current value: "
                        + databaseValues.EquipmentCheckInBy);
                if (databaseValues.EquipmentCheckInDate != clientValues.EquipmentCheckInDate)
                    ModelState.AddModelError("EquipmentCheckInDate", "Current value: "
                        + databaseValues.EquipmentCheckInDate);
                if (databaseValues.EquipmentCheckOutBy != clientValues.EquipmentCheckOutBy)
                    ModelState.AddModelError("EquipmentCheckOutBy", "Current value: "
                        + databaseValues.EquipmentCheckOutBy);
                if (databaseValues.EquipmentCheckOutDate
                    != clientValues.EquipmentCheckOutDate)
                    ModelState.AddModelError("EquipmentCheckOutDate", "Current value: "
                        + databaseValues.EquipmentCheckOutDate);


                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");
                equipment.RowVersion = databaseValues.RowVersion;
            }
            catch (DataException /*dataException*/)
            {

                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(equipment);
        }

        //
        // GET: /Equipment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        //
        // POST: /Equipment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipment.Find(id);
            db.Equipment.Remove(equipment);
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