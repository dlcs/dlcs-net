using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iiifly.Models;

namespace iiifly.Controllers
{
    public class SpaceMappingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpaceMappings
        public ActionResult Index()
        {
            return View(db.SpaceMappings.ToList());
        }

        // GET: SpaceMappings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpaceMapping spaceMapping = db.SpaceMappings.Find(id);
            if (spaceMapping == null)
            {
                return HttpNotFound();
            }
            return View(spaceMapping);
        }

        // GET: SpaceMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpaceMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpaceMappingId,DlcsSpace,SpaceId")] SpaceMapping spaceMapping)
        {
            if (ModelState.IsValid)
            {
                spaceMapping.SpaceMappingId = Guid.NewGuid();
                db.SpaceMappings.Add(spaceMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spaceMapping);
        }

        // GET: SpaceMappings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpaceMapping spaceMapping = db.SpaceMappings.Find(id);
            if (spaceMapping == null)
            {
                return HttpNotFound();
            }
            return View(spaceMapping);
        }

        // POST: SpaceMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpaceMappingId,DlcsSpace,SpaceId")] SpaceMapping spaceMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spaceMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spaceMapping);
        }

        // GET: SpaceMappings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpaceMapping spaceMapping = db.SpaceMappings.Find(id);
            if (spaceMapping == null)
            {
                return HttpNotFound();
            }
            return View(spaceMapping);
        }

        // POST: SpaceMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SpaceMapping spaceMapping = db.SpaceMappings.Find(id);
            db.SpaceMappings.Remove(spaceMapping);
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
