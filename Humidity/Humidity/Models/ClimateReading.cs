using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class ClimateReading
    {
        public SensorReading reading { get; set; }
        public IQueryable<SensorReadingValue> values { get; set; }
    }
}