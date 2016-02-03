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
    public class CasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cases
        public ActionResult Index()
        {
            var cases = db.Cases.Include(x => x.Client);

            return View(cases.ToList());
        }

        // GET: Cases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // GET: Cases/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName");
            
            var managers = db.Employees.Where(e => e.WorkTitle.Name == "Site Manager");
            ViewBag.Managers = new SelectList(managers, "FullName", "FullName");

            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CaseId,Address,StartDate,EndDate,IsActive,Manager,ClientId")] Case @case)
        {
            if (ModelState.IsValid)
            {
                int lastCaseNumberInDb = db.Cases.OrderByDescending(c => c.CaseNumber).Select(cn => cn.CaseNumber).FirstOrDefault();

                @case.CaseNumber = @case.setCaseNumber(lastCaseNumberInDb);
                db.Cases.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", @case.ClientId);
            
            return View(@case);
        }

        // GET: Cases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", @case.ClientId);

            var managers = db.Employees.Where(e => e.WorkTitle.Name == "Site Manager");
            ViewBag.Managers = new SelectList(managers, "FullName", "FullName");

            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CaseId,CaseNumber,Address,StartDate,EndDate,IsActive,Manager,ClientId")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", @case.ClientId);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Cases.Find(id);
            db.Cases.Remove(@case);
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
