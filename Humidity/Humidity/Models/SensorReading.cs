using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class SensorReading
    {
        public int ID { get; set; }
        [Display(Name = "Time Logged")]
        [DataType(DataType.DateTime)]
        public DateTime timeLogged { get; set; }
        [Display(Name = "Duration")]
        [DataType(DataType.Duration)]
        public int duration { get; set; }
        public int deviceID { get; set; }   
       
    }

    
    

}