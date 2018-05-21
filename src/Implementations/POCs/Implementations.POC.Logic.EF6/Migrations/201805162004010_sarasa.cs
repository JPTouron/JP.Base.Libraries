namespace Implementations.POC.Logic.EF6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sarasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Employers", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employers", "Version");
            DropColumn("dbo.Employees", "Version");
        }
    }
}
