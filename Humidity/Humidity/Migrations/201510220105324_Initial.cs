namespace Humidity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        deviceTypeID = c.Int(nullable: false),
                        location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SensorReadings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        timeLogged = c.DateTime(nullable: false),
                        duration = c.Int(nullable: false),
                        deviceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SensorReadingValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        sensorReadingID = c.Int(nullable: false),
                        readingValue = c.String(),
                        readingValueType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.SensorReadingValues");
            DropTable("dbo.SensorReadings");
            DropTable("dbo.Devices");
        }
    }
}
