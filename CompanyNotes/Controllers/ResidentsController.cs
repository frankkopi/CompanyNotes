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
    public class ResidentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Residents
        //public ActionResult Index()
        //{
        //    var residents = db.Residents.Include(r => r.Case);
        //    return View(residents.ToList());
        //}

        // GET: Residents
        public ActionResult ResidentsForCase(int caseId, int caseNumber)
        {
            var residentsForCurrentCase = db.Residents.Where(r => r.CaseId == caseId);

            if (!residentsForCurrentCase.Any())
            {
                ViewBag.CaseNumber = caseNumber;
                return View("NoResidentsFound");
            }

            return View(residentsForCurrentCase);
        }


        // GET: Residents/Create
        public ActionResult Create()
        {
            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "CaseNumber");
            return View();
        }

        // POST: Residents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResidentId,FirstName,LastName,Address,Email,Phone,CaseId")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                db.Residents.Add(resident);
                db.SaveChanges();
                return RedirectToAction("ResidentsForCase", new { caseId = resident.CaseId });
            }

            //ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address", resident.CaseId);
            //return View(resident);
            return RedirectToAction("ResidentsForCase", new { caseId = resident.CaseId });
        }

        // GET: Residents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Residents.Find(id);
            if (resident == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "CaseNumber", resident.CaseId);
            return View(resident);
        }

        // POST: Residents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResidentId,FirstName,LastName,Address,Email,Phone,CaseId")] Resident resident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ResidentsForCase", new { caseId = resident.CaseId });
            }
            //ViewBag.CaseId = new SelectList(db.Cases, "CaseId", "Address", resident.CaseId);
            //return View(resident);
            return RedirectToAction("ResidentsForCase", new { caseId = resident.CaseId });
        }

        // GET: Residents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resident resident = db.Residents.Find(id);
            if (resident == null)
            {
                return HttpNotFound();
            }
            return View(resident);
        }

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resident resident = db.Residents.Find(id);
            db.Residents.Remove(resident);
            db.SaveChanges();
            return RedirectToAction("ResidentsForCase", new { caseId = resident.CaseId});
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
