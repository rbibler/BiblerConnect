using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class ReadingsForDevice
    {
        public Devices device { get; set; }
        public IQueryable<SensorReading> readings { get; set; }
    }
}