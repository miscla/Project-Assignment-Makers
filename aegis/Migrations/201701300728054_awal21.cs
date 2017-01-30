namespace aegis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awal21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.products", "code", c => c.String(nullable: false));
            AlterColumn("dbo.products", "name", c => c.String(nullable: false));
            AlterColumn("dbo.products", "description", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.products", "description", c => c.String());
            AlterColumn("dbo.products", "name", c => c.String());
            AlterColumn("dbo.products", "code", c => c.String());
        }
    }
}
