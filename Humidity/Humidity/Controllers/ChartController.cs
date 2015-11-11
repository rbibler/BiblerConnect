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
    public class ChartController : Controller
    {
        private BiblerConnectDBContext db = new BiblerConnectDBContext();

        // GET: Chart
        public ActionResult Index()
        {
            DateTime now = DateTime.Now;
            DateTime yesterday = now.Add(new TimeSpan(-24, 0, 0)).ToUniversalTime();
           
            var reading = from sr in db.SensorReadings
                          where sr.timeLogged > yesterday
                          && sr.ID % 10 == 0
                          orderby sr.timeLogged ascending
                          select sr;

            return View(new SensorAverages { readings = reading, humidityArray = getAverages(yesterday, 1), tempArray = getAverages(yesterday, 2)});
        }

        private float[] getAverages(DateTime yesterday, int type)
        {
            var average = from srv in db.SensorReadingValues
                          join sr in db.SensorReadings
                          on srv.sensorReadingID equals sr.ID
                          where sr.timeLogged > yesterday &&
                          srv.readingValueType == type
                          orderby sr.timeLogged ascending
                          select srv;
            int count = 0;
            int index = 0;
            float[] averages = new float[average.Count() / 10];
            float avg = 0;
            foreach (var item in average)
            {
                avg += float.Parse(item.readingValue);
                if (count > 0 && count % 10 == 0)
                {
                    averages[index++] = avg / 10;
                    avg = 0;
                }
                count++;
            }
            return averages;
        }

        // GET: Chart/Details/5
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

        // GET: Chart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chart/Create
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

        // GET: Chart/Edit/5
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

        // POST: Chart/Edit/5
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

        // GET: Chart/Delete/5
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

        // POST: Chart/Delete/5
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
