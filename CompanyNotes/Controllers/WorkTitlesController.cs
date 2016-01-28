using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyNotes.Models;

namespace CompanyNotes.Controllers
{
    public class WorkTitlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkTitles
        public ActionResult Index()
        {
            return View(db.WorkTitles.ToList());
        }

        // GET: WorkTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTitle workTitle = db.WorkTitles.Find(id);
            if (workTitle == null)
            {
                return HttpNotFound();
            }
            return View(workTitle);
        }

        // GET: WorkTitles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkTitleId,Name")] WorkTitle workTitle)
        {
            if (ModelState.IsValid)
            {
                db.WorkTitles.Add(workTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workTitle);
        }

        // GET: WorkTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTitle workTitle = db.WorkTitles.Find(id);
            if (workTitle == null)
            {
                return HttpNotFound();
            }
            return View(workTitle);
        }

        // POST: WorkTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkTitleId,Name")] WorkTitle workTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workTitle);
        }

        // GET: WorkTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTitle workTitle = db.WorkTitles.Find(id);
            if (workTitle == null)
            {
                return HttpNotFound();
            }
            return View(workTitle);
        }

        // POST: WorkTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkTitle workTitle = db.WorkTitles.Find(id);
            db.WorkTitles.Remove(workTitle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
