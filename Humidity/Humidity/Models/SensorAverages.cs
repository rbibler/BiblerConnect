using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class SensorAverages
    {
        public IQueryable<SensorReading> readings { get; set; }
        public float[] humidityArray { get; set; }
        public float[] tempArray { get; set; }
    }
}