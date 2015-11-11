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
    public class ClimateController : Controller
    {
        private BiblerConnectDBContext db = new BiblerConnectDBContext();

        // GET: Climate
        public ActionResult Index()
        {
            var mostRecentQuery = from sr in db.SensorReadings
                                  orderby sr.timeLogged descending
                                  select sr;
            IQueryable < SensorReadingValue > values = from srv in db.SensorReadingValues
                                                       where (
                                                       mostRecentQuery
                                                       ).FirstOrDefault().ID == srv.sensorReadingID
                                                       select srv;
            

            return View(new ClimateReading { reading = mostRecentQuery.First<SensorReading>(), values = values });
        }

        // GET: Climate/Details/5
        public ActionResult Details(int? id)
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

        // GET: Climate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Climate/Create
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

        // GET: Climate/Edit/5
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

        // POST: Climate/Edit/5
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

        // GET: Climate/Delete/5
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

        // POST: Climate/Delete/5
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
