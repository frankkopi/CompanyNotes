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
    public class WorkNotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkNotes
        public ActionResult Index()
        {
            var workNotes = db.WorkNotes.Include(w => w.Case).Include(w => w.Employee);
            return View(workNotes.ToList());
        }

        // GET: WorkNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkNote workNote = db.WorkNotes.Find(id);
            if (workNote == null)
            {
                return HttpNotFound();
            }
            return View(workNote);
        }

        // GET: WorkNotes/Create
        public ActionResult Create()
        {
            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: WorkNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkNoteId,Date,Caption,Text,CaseId,EmployeeId")] WorkNote workNote)
        {
            if (ModelState.IsValid)
            {
                db.WorkNotes.Add(workNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address", workNote.CaseId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", workNote.EmployeeId);
            return View(workNote);
        }

        // GET: WorkNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkNote workNote = db.WorkNotes.Find(id);
            if (workNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address", workNote.CaseId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", workNote.EmployeeId);
            return View(workNote);
        }

        // POST: WorkNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkNoteId,Date,Caption,Text,CaseId,EmployeeId")] WorkNote workNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address", workNote.CaseId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", workNote.EmployeeId);
            return View(workNote);
        }

        // GET: WorkNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkNote workNote = db.WorkNotes.Find(id);
            if (workNote == null)
            {
                return HttpNotFound();
            }
            return View(workNote);
        }

        // POST: WorkNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkNote workNote = db.WorkNotes.Find(id);
            db.WorkNotes.Remove(workNote);
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
