using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class SensorReadingValue
    {
        public int ID { get; set; }
        public int sensorReadingID { get; set; }
        public string readingValue { get; set; }
        public int readingValueType { get; set; }
    }
}