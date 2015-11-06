using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Humidity.Models;

namespace Humidity.Controllers
{
    public class SensorReadingsController : Controller
    {
        private BiblerConnectDBContext db = new BiblerConnectDBContext();

        // GET: SensorReadings
        public ActionResult Index()
        {
            return View(db.SensorReadings.ToList());
        }

        // GET: SensorReadings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReading sensorReading = db.SensorReadings.Find(id);
            var allReadings = from o in db.SensorReadingValues
                              join a in db.SensorReadings
                              on o.sensorReadingID equals a.ID
                              where o.sensorReadingID == id
                              select o;
            var ret = new SensorReport { query = allReadings, reading = sensorReading };
            return View(ret);
        }

        // GET: SensorReadings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SensorReadings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,timeLogged,duration,deviceID")] SensorReading sensorReading)
        {
            if (ModelState.IsValid)
            {
                db.SensorReadings.Add(sensorReading);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensorReading);
        }

        // GET: SensorReadings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReading sensorReading = db.SensorReadings.Find(id);
            if (sensorReading == null)
            {
                return HttpNotFound();
            }
            return View(sensorReading);
        }

        // POST: SensorReadings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,timeLogged,duration,deviceID")] SensorReading sensorReading)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorReading).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensorReading);
        }

        // GET: SensorReadings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorReading sensorReading = db.SensorReadings.Find(id);
            if (sensorReading == null)
            {
                return HttpNotFound();
            }
            return View(sensorReading);
        }

        // POST: SensorReadings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorReading sensorReading = db.SensorReadings.Find(id);
            db.SensorReadings.Remove(sensorReading);
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
