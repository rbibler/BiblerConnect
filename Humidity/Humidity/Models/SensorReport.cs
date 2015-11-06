using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class SensorReport
    {
       public IQueryable<SensorReadingValue> query { get; set; }
       public SensorReading reading { get; set; }
    }
}