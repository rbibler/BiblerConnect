using System.Data.Entity;

namespace Humidity.Models
{
    public class Devices
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int deviceTypeID { get; set; }
        public string location { get; set; }
    }
    
}