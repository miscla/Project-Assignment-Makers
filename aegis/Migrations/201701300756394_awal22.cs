namespace aegis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awal22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.accounts", "code", c => c.String(nullable: false));
            AlterColumn("dbo.accounts", "name", c => c.String(nullable: false));
            AlterColumn("dbo.accounts", "telephone", c => c.String(nullable: false));
            AlterColumn("dbo.accounts", "address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.accounts", "address", c => c.String());
            AlterColumn("dbo.accounts", "telephone", c => c.String());
            AlterColumn("dbo.accounts", "name", c => c.String());
            AlterColumn("dbo.accounts", "code", c => c.String());
        }
    }
}
