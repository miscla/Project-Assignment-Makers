namespace aegis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awal2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.accounts", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.accounts", "email", c => c.String());
        }
    }
}
