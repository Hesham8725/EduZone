using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduZone.Models;

namespace EduZone.Controllers
{
    public class EducatorTestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EducatorTest
        public ActionResult Index()
        {
            return View(db.GetEducators.ToList());
        }

        // GET: EducatorTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Educator educator = db.GetEducators.Find(id);
            if (educator == null)
            {
                return HttpNotFound();
            }
            return View(educator);
        }

        // GET: EducatorTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducatorTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AcademicDegree,AccountID")] Educator educator)
        {
            if (ModelState.IsValid)
            {
                db.GetEducators.Add(educator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(educator);
        }

        // GET: EducatorTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Educator educator = db.GetEducators.Find(id);
            if (educator == null)
            {
                return HttpNotFound();
            }
            return View(educator);
        }

        // POST: EducatorTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AcademicDegree,AccountID")] Educator educator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(educator);
        }

        // GET: EducatorTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Educator educator = db.GetEducators.Find(id);
            if (educator == null)
            {
                return HttpNotFound();
            }
            return View(educator);
        }

        // POST: EducatorTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Educator educator = db.GetEducators.Find(id);
            db.GetEducators.Remove(educator);
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
