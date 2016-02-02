using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using iiifly.Models;

namespace iiifly.Controllers
{
    public class ImageSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImageSets
        public ActionResult Index()
        {
            return View(db.ImageSets.ToList());
        }

        // GET: ImageSets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSet imageSet = db.ImageSets.Find(id);
            if (imageSet == null)
            {
                return HttpNotFound();
            }
            return View(imageSet);
        }

        // GET: ImageSets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationUserId,Label,Description")] ImageSet imageSet)
        {
            if (ModelState.IsValid)
            {
                db.ImageSets.Add(imageSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imageSet);
        }

        // GET: ImageSets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSet imageSet = db.ImageSets.Find(id);
            if (imageSet == null)
            {
                return HttpNotFound();
            }
            return View(imageSet);
        }

        // POST: ImageSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApplicationUserId,Label,Description")] ImageSet imageSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageSet);
        }

        // GET: ImageSets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageSet imageSet = db.ImageSets.Find(id);
            if (imageSet == null)
            {
                return HttpNotFound();
            }
            return View(imageSet);
        }

        // POST: ImageSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ImageSet imageSet = db.ImageSets.Find(id);
            db.ImageSets.Remove(imageSet);
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
