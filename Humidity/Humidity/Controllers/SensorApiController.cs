using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Humidity.Models;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Humidity.Controllers
{
    public class SensorApiController : ApiController
    {
        private BiblerConnectDBContext db = new BiblerConnectDBContext();
        public IEnumerable<Models.SensorReading> GetAllSensorReadings()
        {
            return db.SensorReadings.ToList();
        }

        [ResponseType(typeof(SensorReading))]
        [ActionName("Reading")]
        public async Task<IHttpActionResult> PostSensorReading()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dynamic obj = await Request.Content.ReadAsAsync<JObject>();
            var dt = DateTime.Now;
            SensorReading sensorReading = new SensorReading();
            int numReadings = (int)obj["numReadings"];
            sensorReading.timeLogged = dt;
            sensorReading.duration = (int)obj["duration"];
            sensorReading.deviceID = (int) obj["deviceID"];
            db.SensorReadings.Add(sensorReading);
            db.SaveChanges();
            JArray types = obj["readingType"];
            JArray values = obj["readingValue"];
            for(int i = 0; i < numReadings; i++)
            {
                SensorReadingValue value = new SensorReadingValue();
                value.sensorReadingID = sensorReading.ID;
                value.readingValueType = (int) types[i];
                value.readingValue = (string) values[i];
                db.SensorReadingValues.Add(value);
            }
            db.SaveChanges();
            return CreatedAtRoute("ControllerAndAction", new { id = sensorReading.ID }, sensorReading);
        }

        [ResponseType(typeof(SensorReading))]
        [ActionName("Reading")]
        public IHttpActionResult GetSensorReading(SensorReading sensorReading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dt = DateTime.Now;
            sensorReading.timeLogged = dt;
            db.SensorReadings.Add(sensorReading);
            db.SaveChanges();

            return CreatedAtRoute("ControllerAndAction", new { id = sensorReading.ID }, sensorReading);
        }

        [ResponseType(typeof(SensorReadingValue))]
        [ActionName("Value")]
        public IHttpActionResult PostSensorReadingValue(SensorReadingValue sensorReadingValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SensorReadingValues.Add(sensorReadingValue);
            db.SaveChanges();

            return CreatedAtRoute("ControllerAndAction", new { id = sensorReadingValue.ID }, sensorReadingValue);
        }
    }
}
