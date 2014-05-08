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

namespace StJohnEPAD.Controllers
{
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
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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