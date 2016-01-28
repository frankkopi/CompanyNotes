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
    public class ExternalEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExternalEmployees
        public ActionResult Index()
        {
            //var employees = db.Employees.Include(e => e.Subcontractor);
            var employees = db.Employees.OfType<ExternalEmployee>().Include(e => e.Subcontractor);

            return View(employees.ToList());
        }

        // GET: ExternalEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee externalEmployee = db.Employees.Find(id);
            ExternalEmployee externalEmployee = db.Employees.OfType<ExternalEmployee>().FirstOrDefault(e => e.EmployeeId == id);
            if (externalEmployee == null)
            {
                return HttpNotFound();
            }
            return View(externalEmployee);
        }


        // GET: ExternalEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee externalEmployee = db.Employees.Find(id);
            ExternalEmployee externalEmployee = db.Employees.OfType<ExternalEmployee>().FirstOrDefault(e => e.EmployeeId == id);
            if (externalEmployee == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SubcontractorId = new SelectList(db.Subcontractors, "SubcontractorId", "Name", externalEmployee.SubcontractorId);
            ViewBag.SubcontractorId = new SelectList(db.Subcontractors, "SubcontractorId", "Name", externalEmployee.SubcontractorId);
            ViewBag.WorkTitleId = new SelectList(db.WorkTitles, "WorkTitleId", "Name", externalEmployee.WorkTitleId);

            return View(externalEmployee);
        }

        // POST: ExternalEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstMidName,LastName,Phone,Email,Address,WorkTitleId,SubcontractorId")] ExternalEmployee externalEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(externalEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubcontractorId = new SelectList(db.Subcontractors, "SubcontractorId", "Name", externalEmployee.SubcontractorId);
            return View(externalEmployee);
        }

        // GET: ExternalEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee externalEmployee = db.Employees.Find(id);
            if (externalEmployee == null)
            {
                return HttpNotFound();
            }
            return View(externalEmployee);
        }

        // POST: ExternalEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee externalEmployee = db.Employees.Find(id);
            db.Employees.Remove(externalEmployee);
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
