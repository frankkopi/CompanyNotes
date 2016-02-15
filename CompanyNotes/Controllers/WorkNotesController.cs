using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyNotes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CompanyNotes.Controllers
{
    public class WorkNotesController : Controller
    {
        protected ApplicationDbContext db { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }  // User manager - attached to application DB context

        public WorkNotesController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: WorkNotes for an employee
        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Employee = user.Employee;

            //var workNotes = db.WorkNotes.Include(w => w.Case).Include(w => w.Employee);
            var workNotes = db.WorkNotes.Include(w => w.Case).Include(w => w.Employee).Where(e => e.EmployeeId == user.Employee.EmployeeId);

            return View(workNotes.ToList());
        }

        // GET: WorkNotes for a case
        public ActionResult NotesForCase(int caseId)
        {
            var workNotes = db.WorkNotes.Where(w => w.CaseId == caseId).Include(w => w.Employee);
            ViewBag.CaseNumber = db.Cases.Where(c => c.CaseId == caseId).Select(c => c.CaseNumber).FirstOrDefault();

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

            var user = UserManager.FindById(User.Identity.GetUserId());

            WorkNote note = new WorkNote
            {
                WorkNoteId = 0,
                Date = DateTime.Now,
                Caption = null,
                Text = null,
                CaseId = -1,
                EmployeeId = user.Employee.EmployeeId,
                Employee = user.Employee
            };                
            
            return View(note);
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
                return RedirectToAction("NotesForCase", new { caseId = workNote.CaseId} );
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
            return RedirectToAction("NotesForCase", new { caseId = workNote.CaseId });
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
