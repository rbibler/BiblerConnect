using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Humidity.Models
{
    public class BiblerConnectDBContext : DbContext
    {
        public BiblerConnectDBContext()
        {
            Database.SetInitializer<BiblerConnectDBContext>(null);
        }
        public DbSet<Devices> Devices { get; set; }

        public System.Data.Entity.DbSet<Humidity.Models.SensorReading> SensorReadings { get; set; }

        public System.Data.Entity.DbSet<Humidity.Models.SensorReadingValue> SensorReadingValues { get; set; }

        public System.Data.Entity.DbSet<Humidity.Models.User> Users { get; set; }

    }
}