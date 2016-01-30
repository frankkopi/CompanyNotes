using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyNotes.Models;
using CompanyNotes.ViewModels;

namespace CompanyNotes.Controllers
{
    public class InternalEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InternalEmployees
        public ActionResult Index()
        {
            var internalEmployees = db.Employees.OfType<InternalEmployee>().ToList();
            return View(internalEmployees);
        }

        // GET: InternalEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee internalEmployee = db.Employees.Find(id);
            if (internalEmployee == null)
            {
                return HttpNotFound();
            }
            return View(internalEmployee);
        }


        // GET: InternalEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee internalEmployee = db.Employees.Find(id);
            if (internalEmployee == null)
            {
                return HttpNotFound();
            }

            // prepopulate roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            ViewBag.WorkTitleId = new SelectList(db.WorkTitles, "WorkTitleId", "Name", internalEmployee.WorkTitleId);

            return View(internalEmployee);
        }

        // POST: InternalEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstMidName,LastName,Phone,Email,Address,WorkTitleId,HireDate")] InternalEmployee internalEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(internalEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(internalEmployee);
        }

        // GET: InternalEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee internalEmployee = db.Employees.Find(id);
            if (internalEmployee == null)
            {
                return HttpNotFound();
            }
            return View(internalEmployee);
        }

        // POST: InternalEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee internalEmployee = db.Employees.Find(id);
            db.Employees.Remove(internalEmployee);
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
