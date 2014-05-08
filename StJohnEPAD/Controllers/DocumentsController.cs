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
using WebMatrix.WebData;

namespace StJohnEPAD.Controllers
{
    [Authorize(Roles="Administrator")]
    public class DocumentsController : Controller
    {
        private SJAContext db = new SJAContext();
        //Location to save the files
        private readonly string documentSaveLocation = "~/Uploads/Documents";

        //
        // GET: /Document/

        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Document/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Document document, HttpPostedFileBase uploadedDocument)
        {
            if (ModelState.IsValid)
            {
                if (uploadedDocument != null)
                    if (uploadedDocument.ContentLength > 0)
                    {
                        //Get the variables we need
                        var extension = Path.GetExtension(uploadedDocument.FileName);
                        var fileName = Guid.NewGuid().ToString() + extension;
                        
                        //Set the properties in the model object
                        document.DocumentGUID = fileName;
                        document.DocumentLocation = documentSaveLocation.TrimStart(new Char[] { '~', '/' });
                        
                        //Save the document to the location
                        var path = Path.Combine(Server.MapPath(documentSaveLocation), fileName);
                        
                        uploadedDocument.SaveAs(path);
                    }
                document.UserId= WebSecurity.CurrentUserId;
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        //
        // GET: /Document/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        //
        // GET: /Document/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Document/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            //Todo: this could be dangerous? don't know enough about the method
            //System.IO.File.Move("~/" + document.DocumentLocation + "/" + document.DocumentGUID, "~/" + document.DocumentLocation + "/Deleted" + document.DocumentGUID);
            System.IO.File.Move(Server.MapPath(Url.Content("~/" + document.DocumentLocation + "/") + document.DocumentGUID), 
                Server.MapPath(Url.Content("~/" + document.DocumentLocation + "/Deleted/") + document.DocumentGUID));
            db.Documents.Remove(document);
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