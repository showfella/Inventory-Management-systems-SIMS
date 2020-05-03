namespace InventorySystem.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedstatustostringobject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateAdminUpdated", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "status", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "DateAdminUpdated");
        }
    }
}
