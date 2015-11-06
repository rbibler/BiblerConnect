using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Humidity.Models;

namespace Humidity.Controllers
{
    public class SensorReadingValuesController : Controller
    {
        private BiblerConnectDBContext db = new BiblerConnectDBContext();

        // GET: SensorReadingValues
        public ActionResult Index()
        {
            return View(db.SensorReadingValues.ToList());
        }

        // GET: SensorReadingValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReadingValue sensorReadingValue = db.SensorReadingValues.Find(id);
            if (sensorReadingValue == null)
            {
                return HttpNotFound();
            }
            return View(sensorReadingValue);
        }

        // GET: SensorReadingValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SensorReadingValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,sensorReadingID,readingValue,readingValueType")] SensorReadingValue sensorReadingValue)
        {
            if (ModelState.IsValid)
            {
                db.SensorReadingValues.Add(sensorReadingValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensorReadingValue);
        }

        // GET: SensorReadingValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReadingValue sensorReadingValue = db.SensorReadingValues.Find(id);
            if (sensorReadingValue == null)
            {
                return HttpNotFound();
            }
            return View(sensorReadingValue);
        }

        // POST: SensorReadingValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,sensorReadingID,readingValue,readingValueType")] SensorReadingValue sensorReadingValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorReadingValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensorReadingValue);
        }

        // GET: SensorReadingValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReadingValue sensorReadingValue = db.SensorReadingValues.Find(id);
            if (sensorReadingValue == null)
            {
                return HttpNotFound();
            }
            return View(sensorReadingValue);
        }

        // POST: SensorReadingValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorReadingValue sensorReadingValue = db.SensorReadingValues.Find(id);
            db.SensorReadingValues.Remove(sensorReadingValue);
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
