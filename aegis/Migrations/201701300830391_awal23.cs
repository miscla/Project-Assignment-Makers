namespace aegis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awal23 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.campaigns", "code", c => c.String(nullable: false));
            AlterColumn("dbo.campaigns", "name", c => c.String(nullable: false));
            AlterColumn("dbo.campaigns", "description", c => c.String(nullable: false));
            AlterColumn("dbo.marketinglists", "code", c => c.String(nullable: false));
            AlterColumn("dbo.marketinglists", "name", c => c.String(nullable: false));
            AlterColumn("dbo.marketinglists", "description", c => c.String(nullable: false));
            AlterColumn("dbo.cases", "code", c => c.String(nullable: false));
            AlterColumn("dbo.cases", "name", c => c.String(nullable: false));
            AlterColumn("dbo.cases", "description", c => c.String(nullable: false));
            AlterColumn("dbo.invoices", "code", c => c.String(nullable: false));
            AlterColumn("dbo.invoices", "name", c => c.String(nullable: false));
            AlterColumn("dbo.invoices", "description", c => c.String(nullable: false));
            AlterColumn("dbo.invoices", "billingaddress", c => c.String(nullable: false));
            AlterColumn("dbo.leads", "code", c => c.String(nullable: false));
            AlterColumn("dbo.leads", "firstname", c => c.String(nullable: false));
            AlterColumn("dbo.leads", "lastname", c => c.String(nullable: false));
            AlterColumn("dbo.leads", "email", c => c.String(nullable: false));
            AlterColumn("dbo.organizations", "code", c => c.String(nullable: false));
            AlterColumn("dbo.organizations", "name", c => c.String(nullable: false));
            AlterColumn("dbo.organizations", "phone", c => c.String(nullable: false));
            AlterColumn("dbo.organizations", "email", c => c.String(nullable: false));
            AlterColumn("dbo.organizations", "website", c => c.String(nullable: false));
            AlterColumn("dbo.organizationtypes", "code", c => c.String(nullable: false));
            AlterColumn("dbo.organizationtypes", "name", c => c.String(nullable: false));
            AlterColumn("dbo.orders", "code", c => c.String(nullable: false));
            AlterColumn("dbo.orders", "name", c => c.String(nullable: false));
            AlterColumn("dbo.orders", "description", c => c.String(nullable: false));
            AlterColumn("dbo.orders", "address", c => c.String(nullable: false));
            AlterColumn("dbo.orders", "shipaddress", c => c.String(nullable: false));
            AlterColumn("dbo.quotes", "code", c => c.String(nullable: false));
            AlterColumn("dbo.quotes", "name", c => c.String(nullable: false));
            AlterColumn("dbo.quotes", "description", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.quotes", "description", c => c.String());
            AlterColumn("dbo.quotes", "name", c => c.String());
            AlterColumn("dbo.quotes", "code", c => c.String());
            AlterColumn("dbo.orders", "shipaddress", c => c.String());
            AlterColumn("dbo.orders", "address", c => c.String());
            AlterColumn("dbo.orders", "description", c => c.String());
            AlterColumn("dbo.orders", "name", c => c.String());
            AlterColumn("dbo.orders", "code", c => c.String());
            AlterColumn("dbo.organizationtypes", "name", c => c.String());
            AlterColumn("dbo.organizationtypes", "code", c => c.String());
            AlterColumn("dbo.organizations", "website", c => c.String());
            AlterColumn("dbo.organizations", "email", c => c.String());
            AlterColumn("dbo.organizations", "phone", c => c.String());
            AlterColumn("dbo.organizations", "name", c => c.String());
            AlterColumn("dbo.organizations", "code", c => c.String());
            AlterColumn("dbo.leads", "email", c => c.String());
            AlterColumn("dbo.leads", "lastname", c => c.String());
            AlterColumn("dbo.leads", "firstname", c => c.String());
            AlterColumn("dbo.leads", "code", c => c.String());
            AlterColumn("dbo.invoices", "billingaddress", c => c.String());
            AlterColumn("dbo.invoices", "description", c => c.String());
            AlterColumn("dbo.invoices", "name", c => c.String());
            AlterColumn("dbo.invoices", "code", c => c.String());
            AlterColumn("dbo.cases", "description", c => c.String());
            AlterColumn("dbo.cases", "name", c => c.String());
            AlterColumn("dbo.cases", "code", c => c.String());
            AlterColumn("dbo.marketinglists", "description", c => c.String());
            AlterColumn("dbo.marketinglists", "name", c => c.String());
            AlterColumn("dbo.marketinglists", "code", c => c.String());
            AlterColumn("dbo.campaigns", "description", c => c.String());
            AlterColumn("dbo.campaigns", "name", c => c.String());
            AlterColumn("dbo.campaigns", "code", c => c.String());
        }
    }
}
