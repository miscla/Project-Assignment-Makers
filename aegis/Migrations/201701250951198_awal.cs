namespace aegis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accountactivitylines",
                c => new
                    {
                        accountactivitylineId = c.Int(nullable: false, identity: true),
                        activitydate = c.DateTime(nullable: false),
                        accountactivitytypeId = c.Int(nullable: false),
                        description = c.String(),
                        account_accountId = c.Int(),
                    })
                .PrimaryKey(t => t.accountactivitylineId)
                .ForeignKey("dbo.accounts", t => t.account_accountId)
                .ForeignKey("dbo.accountactivitytypes", t => t.accountactivitytypeId, cascadeDelete: true)
                .Index(t => t.accountactivitytypeId)
                .Index(t => t.account_accountId);
            
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        accountId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        telephone = c.String(),
                        email = c.String(),
                        address = c.String(),
                        accounttypeId = c.Int(nullable: false),
                        leadsourceId = c.Int(nullable: false),
                        statusleadId = c.Int(nullable: false),
                        DetailForm_pkidx_accountactivitylineId = c.String(),
                        DetailForm_ddl_accountactivitytypeId = c.Int(nullable: false),
                        DetailForm_activitydate = c.DateTime(nullable: false),
                        DetailForm_description = c.String(),
                    })
                .PrimaryKey(t => t.accountId)
                .ForeignKey("dbo.accounttypes", t => t.accounttypeId, cascadeDelete: true)
                .ForeignKey("dbo.leadsources", t => t.leadsourceId, cascadeDelete: true)
                .ForeignKey("dbo.statusleads", t => t.statusleadId, cascadeDelete: true)
                .Index(t => t.accounttypeId)
                .Index(t => t.leadsourceId)
                .Index(t => t.statusleadId);
            
            CreateTable(
                "dbo.accounttypes",
                c => new
                    {
                        accounttypeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.accounttypeId);
            
            CreateTable(
                "dbo.leadsources",
                c => new
                    {
                        leadsourceId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.leadsourceId);
            
            CreateTable(
                "dbo.statusleads",
                c => new
                    {
                        statusleadId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.statusleadId);
            
            CreateTable(
                "dbo.accountactivitytypes",
                c => new
                    {
                        accountactivitytypeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.accountactivitytypeId);
            
            CreateTable(
                "dbo.akseslines",
                c => new
                    {
                        akseslineId = c.Int(nullable: false, identity: true),
                        akses_aksesId = c.Int(),
                        modul_modulId = c.Int(),
                    })
                .PrimaryKey(t => t.akseslineId)
                .ForeignKey("dbo.akses", t => t.akses_aksesId)
                .ForeignKey("dbo.moduls", t => t.modul_modulId)
                .Index(t => t.akses_aksesId)
                .Index(t => t.modul_modulId);
            
            CreateTable(
                "dbo.akses",
                c => new
                    {
                        aksesId = c.Int(nullable: false, identity: true),
                        aksesdescription = c.String(),
                        userrole_userroleId = c.Int(),
                    })
                .PrimaryKey(t => t.aksesId)
                .ForeignKey("dbo.userroles", t => t.userrole_userroleId)
                .Index(t => t.userrole_userroleId);
            
            CreateTable(
                "dbo.userroles",
                c => new
                    {
                        userroleId = c.Int(nullable: false, identity: true),
                        rolename = c.String(nullable: false),
                        roledescription = c.String(),
                    })
                .PrimaryKey(t => t.userroleId);
            
            CreateTable(
                "dbo.userrolelines",
                c => new
                    {
                        userrolelineId = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        applicationuser_Id = c.String(maxLength: 128),
                        userrole_userroleId = c.Int(),
                    })
                .PrimaryKey(t => t.userrolelineId)
                .ForeignKey("dbo.AspNetUsers", t => t.applicationuser_Id)
                .ForeignKey("dbo.userroles", t => t.userrole_userroleId)
                .Index(t => t.applicationuser_Id)
                .Index(t => t.userrole_userroleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.moduls",
                c => new
                    {
                        modulId = c.Int(nullable: false, identity: true),
                        namamodul = c.String(nullable: false),
                        namacontroller = c.String(nullable: false),
                        alias = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.modulId);
            
            CreateTable(
                "dbo.campaignactivitytypes",
                c => new
                    {
                        campaignactivitytypeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.campaignactivitytypeId);
            
            CreateTable(
                "dbo.campaignlines",
                c => new
                    {
                        campaignlineId = c.Int(nullable: false, identity: true),
                        campaignactivitytypeId = c.Int(nullable: false),
                        activitydate = c.DateTime(nullable: false),
                        description = c.String(),
                        campaign_campaignId = c.Int(),
                    })
                .PrimaryKey(t => t.campaignlineId)
                .ForeignKey("dbo.campaigns", t => t.campaign_campaignId)
                .ForeignKey("dbo.campaignactivitytypes", t => t.campaignactivitytypeId, cascadeDelete: true)
                .Index(t => t.campaignactivitytypeId)
                .Index(t => t.campaign_campaignId);
            
            CreateTable(
                "dbo.campaigns",
                c => new
                    {
                        campaignId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        startdate = c.DateTime(nullable: false),
                        enddate = c.DateTime(nullable: false),
                        marketinglistId = c.Int(nullable: false),
                        DetailForm_pkidx_campaignlineId = c.String(),
                        DetailForm_ddl_campaignactivitytypeId = c.Int(nullable: false),
                        DetailForm_activitydate = c.DateTime(nullable: false),
                        DetailForm_description = c.String(),
                    })
                .PrimaryKey(t => t.campaignId)
                .ForeignKey("dbo.marketinglists", t => t.marketinglistId, cascadeDelete: true)
                .Index(t => t.marketinglistId);
            
            CreateTable(
                "dbo.marketinglists",
                c => new
                    {
                        marketinglistId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        startdate = c.DateTime(nullable: false),
                        enddate = c.DateTime(nullable: false),
                        DetailForm_pkidx_marketinglistlineId = c.String(),
                        DetailForm_ddl_accountId = c.Int(nullable: false),
                        DetailForm_description = c.String(),
                    })
                .PrimaryKey(t => t.marketinglistId);
            
            CreateTable(
                "dbo.marketinglistlines",
                c => new
                    {
                        marketinglistlineId = c.Int(nullable: false, identity: true),
                        accountId = c.Int(nullable: false),
                        description = c.String(),
                        marketinglist_marketinglistId = c.Int(),
                    })
                .PrimaryKey(t => t.marketinglistlineId)
                .ForeignKey("dbo.accounts", t => t.accountId, cascadeDelete: true)
                .ForeignKey("dbo.marketinglists", t => t.marketinglist_marketinglistId)
                .Index(t => t.accountId)
                .Index(t => t.marketinglist_marketinglistId);
            
            CreateTable(
                "dbo.caseslines",
                c => new
                    {
                        caseslineId = c.Int(nullable: false, identity: true),
                        actiondate = c.DateTime(nullable: false),
                        actiondescription = c.String(),
                        cases_casesId = c.Int(),
                    })
                .PrimaryKey(t => t.caseslineId)
                .ForeignKey("dbo.cases", t => t.cases_casesId)
                .Index(t => t.cases_casesId);
            
            CreateTable(
                "dbo.cases",
                c => new
                    {
                        casesId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        reporteddate = c.DateTime(nullable: false),
                        accountId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        statuscaseId = c.Int(nullable: false),
                        DetailForm_pkidx_caseslineId = c.String(),
                        DetailForm_actiondate = c.DateTime(),
                        DetailForm_actiondescription = c.String(),
                    })
                .PrimaryKey(t => t.casesId)
                .ForeignKey("dbo.accounts", t => t.accountId, cascadeDelete: true)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.statuscases", t => t.statuscaseId, cascadeDelete: true)
                .Index(t => t.accountId)
                .Index(t => t.productId)
                .Index(t => t.statuscaseId);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        productId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        costprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        salesprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        producttypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.productId)
                .ForeignKey("dbo.producttypes", t => t.producttypeId, cascadeDelete: true)
                .Index(t => t.producttypeId);
            
            CreateTable(
                "dbo.producttypes",
                c => new
                    {
                        producttypeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.producttypeId);
            
            CreateTable(
                "dbo.statuscases",
                c => new
                    {
                        statuscaseId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.statuscaseId);
            
            CreateTable(
                "dbo.invoicelines",
                c => new
                    {
                        invoicelineId = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        unitprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        invoice_invoiceId = c.Int(),
                    })
                .PrimaryKey(t => t.invoicelineId)
                .ForeignKey("dbo.invoices", t => t.invoice_invoiceId)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.invoice_invoiceId);
            
            CreateTable(
                "dbo.invoices",
                c => new
                    {
                        invoiceId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        accountId = c.Int(nullable: false),
                        invoicedate = c.DateTime(nullable: false),
                        billingaddress = c.String(),
                        DetailForm_pkidx_invoicelineId = c.String(),
                        DetailForm_ddl_productId = c.Int(nullable: false),
                        DetailForm_qty = c.String(),
                        DetailForm_unitprice = c.String(),
                    })
                .PrimaryKey(t => t.invoiceId)
                .ForeignKey("dbo.accounts", t => t.accountId, cascadeDelete: true)
                .Index(t => t.accountId);
            
            CreateTable(
                "dbo.leads",
                c => new
                    {
                        leadId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        firstname = c.String(),
                        lastname = c.String(),
                        organizationId = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        email = c.String(),
                        phone = c.String(),
                        pic_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.leadId)
                .ForeignKey("dbo.organizations", t => t.organizationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.pic_Id)
                .Index(t => t.organizationId)
                .Index(t => t.pic_Id);
            
            CreateTable(
                "dbo.organizations",
                c => new
                    {
                        organizationId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        fulladdress = c.String(),
                        phone = c.String(),
                        email = c.String(),
                        website = c.String(),
                        organizationtypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.organizationId)
                .ForeignKey("dbo.organizationtypes", t => t.organizationtypeId, cascadeDelete: true)
                .Index(t => t.organizationtypeId);
            
            CreateTable(
                "dbo.organizationtypes",
                c => new
                    {
                        organizationtypeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.organizationtypeId);
            
            CreateTable(
                "dbo.opportunitylines",
                c => new
                    {
                        opportunitylineId = c.Int(nullable: false, identity: true),
                        activitydate = c.DateTime(nullable: false),
                        description = c.String(),
                        opportunity_opportunityId = c.Int(),
                    })
                .PrimaryKey(t => t.opportunitylineId)
                .ForeignKey("dbo.opportunities", t => t.opportunity_opportunityId)
                .Index(t => t.opportunity_opportunityId);
            
            CreateTable(
                "dbo.opportunities",
                c => new
                    {
                        opportunityId = c.Int(nullable: false, identity: true),
                        stageId = c.Int(),
                        statusopportunityId = c.Int(),
                        accountId = c.Int(),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        winningprobability = c.Int(nullable: false),
                        forecastclosedate = c.DateTime(nullable: false),
                        DetailForm_pkidx_opportunitylineId = c.String(),
                        DetailForm_activitydate = c.DateTime(nullable: false),
                        DetailForm_description = c.String(),
                    })
                .PrimaryKey(t => t.opportunityId)
                .ForeignKey("dbo.accounts", t => t.accountId)
                .ForeignKey("dbo.stages", t => t.stageId)
                .ForeignKey("dbo.statusopportunities", t => t.statusopportunityId)
                .Index(t => t.stageId)
                .Index(t => t.statusopportunityId)
                .Index(t => t.accountId);
            
            CreateTable(
                "dbo.stages",
                c => new
                    {
                        stageId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        pipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.stageId)
                .ForeignKey("dbo.pipes", t => t.pipeId, cascadeDelete: true)
                .Index(t => t.pipeId);
            
            CreateTable(
                "dbo.pipes",
                c => new
                    {
                        pipeId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.pipeId);
            
            CreateTable(
                "dbo.statusopportunities",
                c => new
                    {
                        statusopportunityId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.statusopportunityId);
            
            CreateTable(
                "dbo.orderlines",
                c => new
                    {
                        orderlineId = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        unitprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        order_orderId = c.Int(),
                    })
                .PrimaryKey(t => t.orderlineId)
                .ForeignKey("dbo.orders", t => t.order_orderId)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.order_orderId);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        accountId = c.Int(nullable: false),
                        orderdate = c.DateTime(nullable: false),
                        shipdate = c.DateTime(nullable: false),
                        address = c.String(),
                        shipaddress = c.String(),
                        DetailForm_pkidx_orderlineId = c.String(),
                        DetailForm_ddl_productId = c.Int(nullable: false),
                        DetailForm_qty = c.String(),
                        DetailForm_unitprice = c.String(),
                    })
                .PrimaryKey(t => t.orderId)
                .ForeignKey("dbo.accounts", t => t.accountId, cascadeDelete: true)
                .Index(t => t.accountId);
            
            CreateTable(
                "dbo.quotelines",
                c => new
                    {
                        quotelineId = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        unitprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quote_quoteId = c.Int(),
                    })
                .PrimaryKey(t => t.quotelineId)
                .ForeignKey("dbo.products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.quotes", t => t.quote_quoteId)
                .Index(t => t.productId)
                .Index(t => t.quote_quoteId);
            
            CreateTable(
                "dbo.quotes",
                c => new
                    {
                        quoteId = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        name = c.String(),
                        description = c.String(),
                        accountId = c.Int(nullable: false),
                        estimatedstartdate = c.DateTime(nullable: false),
                        estimatedenddate = c.DateTime(nullable: false),
                        DetailForm_pkidx_quotelineId = c.String(),
                        DetailForm_ddl_productId = c.Int(nullable: false),
                        DetailForm_qty = c.String(),
                        DetailForm_unitprice = c.String(),
                    })
                .PrimaryKey(t => t.quoteId)
                .ForeignKey("dbo.accounts", t => t.accountId, cascadeDelete: true)
                .Index(t => t.accountId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.userprofiles",
                c => new
                    {
                        userprofileId = c.Int(nullable: false, identity: true),
                        fullname = c.String(),
                    })
                .PrimaryKey(t => t.userprofileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.quotelines", "quote_quoteId", "dbo.quotes");
            DropForeignKey("dbo.quotes", "accountId", "dbo.accounts");
            DropForeignKey("dbo.quotelines", "productId", "dbo.products");
            DropForeignKey("dbo.orderlines", "productId", "dbo.products");
            DropForeignKey("dbo.orderlines", "order_orderId", "dbo.orders");
            DropForeignKey("dbo.orders", "accountId", "dbo.accounts");
            DropForeignKey("dbo.opportunities", "statusopportunityId", "dbo.statusopportunities");
            DropForeignKey("dbo.opportunities", "stageId", "dbo.stages");
            DropForeignKey("dbo.stages", "pipeId", "dbo.pipes");
            DropForeignKey("dbo.opportunitylines", "opportunity_opportunityId", "dbo.opportunities");
            DropForeignKey("dbo.opportunities", "accountId", "dbo.accounts");
            DropForeignKey("dbo.leads", "pic_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.leads", "organizationId", "dbo.organizations");
            DropForeignKey("dbo.organizations", "organizationtypeId", "dbo.organizationtypes");
            DropForeignKey("dbo.invoicelines", "productId", "dbo.products");
            DropForeignKey("dbo.invoicelines", "invoice_invoiceId", "dbo.invoices");
            DropForeignKey("dbo.invoices", "accountId", "dbo.accounts");
            DropForeignKey("dbo.cases", "statuscaseId", "dbo.statuscases");
            DropForeignKey("dbo.cases", "productId", "dbo.products");
            DropForeignKey("dbo.products", "producttypeId", "dbo.producttypes");
            DropForeignKey("dbo.caseslines", "cases_casesId", "dbo.cases");
            DropForeignKey("dbo.cases", "accountId", "dbo.accounts");
            DropForeignKey("dbo.campaignlines", "campaignactivitytypeId", "dbo.campaignactivitytypes");
            DropForeignKey("dbo.campaigns", "marketinglistId", "dbo.marketinglists");
            DropForeignKey("dbo.marketinglistlines", "marketinglist_marketinglistId", "dbo.marketinglists");
            DropForeignKey("dbo.marketinglistlines", "accountId", "dbo.accounts");
            DropForeignKey("dbo.campaignlines", "campaign_campaignId", "dbo.campaigns");
            DropForeignKey("dbo.akseslines", "modul_modulId", "dbo.moduls");
            DropForeignKey("dbo.akses", "userrole_userroleId", "dbo.userroles");
            DropForeignKey("dbo.userrolelines", "userrole_userroleId", "dbo.userroles");
            DropForeignKey("dbo.userrolelines", "applicationuser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.akseslines", "akses_aksesId", "dbo.akses");
            DropForeignKey("dbo.accountactivitylines", "accountactivitytypeId", "dbo.accountactivitytypes");
            DropForeignKey("dbo.accounts", "statusleadId", "dbo.statusleads");
            DropForeignKey("dbo.accounts", "leadsourceId", "dbo.leadsources");
            DropForeignKey("dbo.accounts", "accounttypeId", "dbo.accounttypes");
            DropForeignKey("dbo.accountactivitylines", "account_accountId", "dbo.accounts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.quotes", new[] { "accountId" });
            DropIndex("dbo.quotelines", new[] { "quote_quoteId" });
            DropIndex("dbo.quotelines", new[] { "productId" });
            DropIndex("dbo.orders", new[] { "accountId" });
            DropIndex("dbo.orderlines", new[] { "order_orderId" });
            DropIndex("dbo.orderlines", new[] { "productId" });
            DropIndex("dbo.stages", new[] { "pipeId" });
            DropIndex("dbo.opportunities", new[] { "accountId" });
            DropIndex("dbo.opportunities", new[] { "statusopportunityId" });
            DropIndex("dbo.opportunities", new[] { "stageId" });
            DropIndex("dbo.opportunitylines", new[] { "opportunity_opportunityId" });
            DropIndex("dbo.organizations", new[] { "organizationtypeId" });
            DropIndex("dbo.leads", new[] { "pic_Id" });
            DropIndex("dbo.leads", new[] { "organizationId" });
            DropIndex("dbo.invoices", new[] { "accountId" });
            DropIndex("dbo.invoicelines", new[] { "invoice_invoiceId" });
            DropIndex("dbo.invoicelines", new[] { "productId" });
            DropIndex("dbo.products", new[] { "producttypeId" });
            DropIndex("dbo.cases", new[] { "statuscaseId" });
            DropIndex("dbo.cases", new[] { "productId" });
            DropIndex("dbo.cases", new[] { "accountId" });
            DropIndex("dbo.caseslines", new[] { "cases_casesId" });
            DropIndex("dbo.marketinglistlines", new[] { "marketinglist_marketinglistId" });
            DropIndex("dbo.marketinglistlines", new[] { "accountId" });
            DropIndex("dbo.campaigns", new[] { "marketinglistId" });
            DropIndex("dbo.campaignlines", new[] { "campaign_campaignId" });
            DropIndex("dbo.campaignlines", new[] { "campaignactivitytypeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.userrolelines", new[] { "userrole_userroleId" });
            DropIndex("dbo.userrolelines", new[] { "applicationuser_Id" });
            DropIndex("dbo.akses", new[] { "userrole_userroleId" });
            DropIndex("dbo.akseslines", new[] { "modul_modulId" });
            DropIndex("dbo.akseslines", new[] { "akses_aksesId" });
            DropIndex("dbo.accounts", new[] { "statusleadId" });
            DropIndex("dbo.accounts", new[] { "leadsourceId" });
            DropIndex("dbo.accounts", new[] { "accounttypeId" });
            DropIndex("dbo.accountactivitylines", new[] { "account_accountId" });
            DropIndex("dbo.accountactivitylines", new[] { "accountactivitytypeId" });
            DropTable("dbo.userprofiles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.quotes");
            DropTable("dbo.quotelines");
            DropTable("dbo.orders");
            DropTable("dbo.orderlines");
            DropTable("dbo.statusopportunities");
            DropTable("dbo.pipes");
            DropTable("dbo.stages");
            DropTable("dbo.opportunities");
            DropTable("dbo.opportunitylines");
            DropTable("dbo.organizationtypes");
            DropTable("dbo.organizations");
            DropTable("dbo.leads");
            DropTable("dbo.invoices");
            DropTable("dbo.invoicelines");
            DropTable("dbo.statuscases");
            DropTable("dbo.producttypes");
            DropTable("dbo.products");
            DropTable("dbo.cases");
            DropTable("dbo.caseslines");
            DropTable("dbo.marketinglistlines");
            DropTable("dbo.marketinglists");
            DropTable("dbo.campaigns");
            DropTable("dbo.campaignlines");
            DropTable("dbo.campaignactivitytypes");
            DropTable("dbo.moduls");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.userrolelines");
            DropTable("dbo.userroles");
            DropTable("dbo.akses");
            DropTable("dbo.akseslines");
            DropTable("dbo.accountactivitytypes");
            DropTable("dbo.statusleads");
            DropTable("dbo.leadsources");
            DropTable("dbo.accounttypes");
            DropTable("dbo.accounts");
            DropTable("dbo.accountactivitylines");
        }
    }
}
