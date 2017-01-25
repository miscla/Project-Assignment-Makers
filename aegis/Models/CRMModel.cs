using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aegis.Models
{
    public class accounttype
    {
        [Display(Name = "Account Type")]
        public int accounttypeId { get; set; }

        [Display(Name = "Account Type Code")]
        public string code { get; set; }

        [Display(Name = "Account Type Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

    }

    public class leadsource
    {
        [Display(Name = "Lead Source")]
        public int leadsourceId { get; set; }

        [Display(Name = "Lead Source Code")]
        public string code { get; set; }

        [Display(Name = "Lead Source Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }


    public class statuslead
    {
        [Display(Name = "Lead Status")]
        public int statusleadId { get; set; }

        [Display(Name = "Lead Status Code")]
        public string code { get; set; }

        [Display(Name = "Lead Status Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }

    public class account
    {

        public account()
        {
            DetailForm_activitydate = DateTime.Now;
        }
            
        public int accountId { get; set; }

        [Display(Name = "Account Code")]
        public string code { get; set; }

        [Display(Name = "Account Name")]
        public string name { get; set; }

        public string telephone { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        [Display(Name = "Account Type")]
        public int accounttypeId { get; set; }

        public virtual accounttype accounttype { get; set; }

        [Display(Name = "Lead Source")]
        public int leadsourceId { get; set; }

        public virtual leadsource leadsource { get; set; }

        [Display(Name = "Lead Status")]
        public int statusleadId { get; set; }

        public virtual statuslead statuslead { get; set; }

        public virtual List<accountactivityline> accountactivityline { get; set; }

        
        public string DetailForm_pkidx_accountactivitylineId { get; set; }
        
        public int DetailForm_ddl_accountactivitytypeId { get; set; }
       
        public DateTime DetailForm_activitydate { get; set; }
        
        public string DetailForm_description { get; set; }

    }

    public class accountactivityline
    {
        public int accountactivitylineId { get; set; }

        public account account { get; set; }

        [Display(Name = "Activity Date")]
        public DateTime activitydate { get; set; }

        [Display(Name = "Activity Type")]
        public int accountactivitytypeId { get; set; }

        public virtual accountactivitytype accountactivitytype { get; set; }

        public string description { get; set; }

    }

    public class accountactivitytype
    {
        [Display(Name = "Activity Type")]
        public int accountactivitytypeId { get; set; }

        [Display(Name = "Account Activity Code")]
        public string code { get; set; }

        [Display(Name = "Account Activity Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }


    public class pipe
    {
        [Display(Name = "Pipe")]
        public int pipeId { get; set; }
        [Display(Name = "Pipe Code")]
        public string code { get; set; }
        [Display(Name = "Pipe Name")]
        public string name { get; set; }
        public string description { get; set; }

    }

    public class stage
    {
        [Display(Name = "Stage")]
        public int stageId { get; set; }

        [Display(Name = "Stage Code")]
        public string code { get; set; }

        [Display(Name = "Stage Name")]
        public string name { get; set; }
        public string description { get; set; }

        [Display(Name = "Pipe")]
        public int pipeId { get; set; }
        public virtual pipe pipe { get; set; }


    }

    public class statusopportunity
    {
        [Display(Name = "Status Opportunity")]
        public int statusopportunityId { get; set; }

        [Display(Name = "Status Code")]
        public string code { get; set; }

        [Display(Name = "Status Name")]
        public string name { get; set; }
        public string description { get; set; }

    }

    public class opportunity
    {
        public opportunity()
        {
            DetailForm_activitydate = DateTime.Now;
        }

        public int opportunityId { get; set; }
        [Display(Name = "Stage")]
        public int? stageId { get; set; }
        public virtual stage stage { get; set; }
        [Display(Name = "Status")]
        public int? statusopportunityId { get; set; }
        public virtual statusopportunity statusopportunity { get; set; }
        [Display(Name = "Account")]
        public int? accountId { get; set; }
        public virtual account account { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public int winningprobability { get; set; }
        public DateTime forecastclosedate { get; set; }

        public virtual List<opportunityline> opportunityline { get; set; }
        public string DetailForm_pkidx_opportunitylineId { get; set; }
        public DateTime DetailForm_activitydate { get; set; }

        public string DetailForm_description { get; set; }

    }

    public class opportunityline
    {
        public int opportunitylineId { get; set; }
        public opportunity opportunity { get; set; }
        public DateTime activitydate { get; set; }
        public string description { get; set; }
    }

    public class producttype
    {
        [Display(Name = "Product Type")]
        public int producttypeId { get; set; }

        [Display(Name = "Product Type Code")]
        public string code { get; set; }

        [Display(Name = "Product Type Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }

    public class product
    {
        [Display(Name = "Product")]
        public int productId { get; set; }

        [Display(Name = "Product Code")]
        public string code { get; set; }

        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        public decimal costprice { get; set; }
        public decimal salesprice { get; set; }
        public int producttypeId { get; set; }
        public virtual producttype producttype { get; set; }
    }

    public class quote
    {
        [Display(Name = "Quote")]
        public int quoteId { get; set; }

        [Display(Name = "Quote Code")]
        public string code { get; set; }

        [Display(Name = "Quote Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Account")]
        public int accountId { get; set; }
        public virtual account account { get; set; }
        [Display(Name = "Est Start Date")]
        public DateTime estimatedstartdate { get; set; }
        [Display(Name = "Est End Date")]
        public DateTime estimatedenddate { get; set; }
        public virtual List<quoteline> quoteline { get; set; }
        public string DetailForm_pkidx_quotelineId { get; set; }
        public int DetailForm_ddl_productId { get; set; }
        public string DetailForm_qty { get; set; }
        public string DetailForm_unitprice { get; set; }
    }

    public class quoteline
    {
        public int quotelineId { get; set; }
        public quote quote { get; set; }
        [Display(Name = "Product")]
        public int productId { get; set; }
        public product product { get; set; }
        public decimal qty { get; set; }
        public decimal unitprice { get; set; }
        
        public decimal netamount { get { return qty * unitprice; } }
    }


    public class order
    {
        [Display(Name = "Order")]
        public int orderId { get; set; }

        [Display(Name = "Order Code")]
        public string code { get; set; }

        [Display(Name = "Order Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Account")]
        public int accountId { get; set; }
        public virtual account account { get; set; }
        [Display(Name = "Order Date")]
        public DateTime orderdate { get; set; }
        [Display(Name = "Requested Ship Date")]
        public DateTime shipdate { get; set; }
        public string address { get; set; }
        public string shipaddress { get; set; }
        public virtual List<orderline> orderline { get; set; }
        public string DetailForm_pkidx_orderlineId { get; set; }
        public int DetailForm_ddl_productId { get; set; }
        public string DetailForm_qty { get; set; }
        public string DetailForm_unitprice { get; set; }
    }

    public class orderline
    {
        public int orderlineId { get; set; }
        public order order { get; set; }
        [Display(Name = "Product")]
        public int productId { get; set; }
        public product product { get; set; }
        public decimal qty { get; set; }
        public decimal unitprice { get; set; }

        public decimal netamount { get { return qty * unitprice; } }
    }


    public class invoice
    {
        [Display(Name = "Invoice")]
        public int invoiceId { get; set; }

        [Display(Name = "Invoice Code")]
        public string code { get; set; }

        [Display(Name = "Invoice Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Account")]
        public int accountId { get; set; }
        public virtual account account { get; set; }
        [Display(Name = "Invoice Date")]
        public DateTime invoicedate { get; set; }
        [Display(Name = "Billing Address")]
        public string billingaddress { get; set; }
        public virtual List<invoiceline> invoiceline { get; set; }
        public string DetailForm_pkidx_invoicelineId { get; set; }
        public int DetailForm_ddl_productId { get; set; }
        public string DetailForm_qty { get; set; }
        public string DetailForm_unitprice { get; set; }
    }

    public class invoiceline
    {
        public int invoicelineId { get; set; }
        public invoice invoice { get; set; }
        [Display(Name = "Product")]
        public int productId { get; set; }
        public product product { get; set; }
        public decimal qty { get; set; }
        public decimal unitprice { get; set; }

        public decimal netamount { get { return qty * unitprice; } }
    }


    public class marketinglist
    {
        [Display(Name = "Marketing List")]
        public int marketinglistId { get; set; }

        [Display(Name = "Marketing List Code")]
        public string code { get; set; }

        [Display(Name = "Marketing List Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime startdate { get; set; }
        [Display(Name = "End Date")]
        public DateTime enddate { get; set; }
        public virtual List<marketinglistline> marketinglistline { get; set; }
        public string DetailForm_pkidx_marketinglistlineId { get; set; }
        public int DetailForm_ddl_accountId { get; set; }
        public string DetailForm_description { get; set; }
    }

    public class marketinglistline
    {
        public int marketinglistlineId { get; set; }
        public marketinglist marketinglist { get; set; }
        [Display(Name = "Account")]
        public int accountId { get; set; }
        public virtual account account { get; set; }
        public string description { get; set; }
    }

    public class campaignactivitytype
    {
        [Display(Name = "Campaign Activity Type")]
        public int campaignactivitytypeId { get; set; }

        [Display(Name = "Activity Code")]
        public string code { get; set; }

        [Display(Name = "Activity Name")]
        public string name { get; set; }
        public string description { get; set; }

    }

    public class campaign
    {
        [Display(Name = "Campaign")]
        public int campaignId { get; set; }

        [Display(Name = "Campaign Code")]
        public string code { get; set; }

        [Display(Name = "Campaign Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime startdate { get; set; }
        [Display(Name = "End Date")]
        public DateTime enddate { get; set; }
        [Display(Name = "Marketing List")]
        public int marketinglistId { get; set; }
        public virtual marketinglist marketinglist { get; set; }
        public virtual List<campaignline> campaignline { get; set; }
        public string DetailForm_pkidx_campaignlineId { get; set; }
        public int DetailForm_ddl_campaignactivitytypeId { get; set; }
        public DateTime DetailForm_activitydate { get; set; }
        public string DetailForm_description { get; set; }
    }

    
    public class campaignline
    {
        public int campaignlineId { get; set; }
        public campaign campaign { get; set; }
        [Display(Name = "Activity Type")]
        public int campaignactivitytypeId { get; set; }
        public virtual campaignactivitytype campaignactivitytype { get; set; }
        public DateTime activitydate { get; set; }
        public string description { get; set; }
    }

    public class statuscase
    {
        [Display(Name = "Case Status")]
        public int statuscaseId { get; set; }

        [Display(Name = "Case Status Code")]
        public string code { get; set; }

        [Display(Name = "Case Status Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }

    public class cases
    {
        [Display(Name = "Cases")]
        public int casesId { get; set; }

        [Display(Name = "Cases Code")]
        public string code { get; set; }

        [Display(Name = "Cases Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Reported Date")]
        public DateTime reporteddate { get; set; }
        [Display(Name = "Account")]
        public int accountId { get; set; }
        public account account { get; set; }
        [Display(Name = "Product")]
        public int productId { get; set; }
        public product product { get; set; }
        [Display(Name = "Case Status")]
        public int statuscaseId { get; set; }
        public virtual statuscase statuscase { get; set; }
        public virtual List<casesline> casesline { get; set; }
        public string DetailForm_pkidx_caseslineId { get; set; }
        public DateTime? DetailForm_actiondate { get; set; }
        public string DetailForm_actiondescription { get; set; }
    }


    public class casesline
    {
        public int caseslineId { get; set; }
        public cases cases { get; set; }
        public DateTime actiondate { get; set; }
        public string actiondescription { get; set; }
    }

    public class organizationtype
    {
        public int organizationtypeId { get; set; }

        public string code { get; set; }

        public string name { get; set; }
        public string description { get; set; }


    }

    public class organization
    {
        public int organizationId { get; set; }

        public string code { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public string fulladdress { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public int organizationtypeId { get; set; }
        public virtual organizationtype organizationtype { get; set; }


    }

    public class lead
    {
        public int leadId { get; set; }

        public string code { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
        public int organizationId { get; set; }
        public virtual organization organization { get; set; }

        public int rating { get; set; }

        public virtual ApplicationUser pic { get; set; }
        public string email { get; set; }
        public string phone { get; set; }



    }



    public class InitializerCRM
    {
        public InitializerCRM(ApplicationDbContext context)
        {
            InitAccountType(context);
            InitLeadSource(context);
            InitAccountActivityType(context);
            InitStatusLead(context);
            InitProductType(context);
            InitCampaignActivityType(context);
            InitCaseStatus(context);
        }

        private void InitAccountType(ApplicationDbContext context)
        {
            List<accounttype> entities = new List<accounttype>
            {
                new accounttype() { code = "customer", name = "customer" },
                new accounttype() { code = "contact", name = "contact" },
                new accounttype() { code = "lead", name = "lead" },
                new accounttype() { code = "competitor", name = "competitor" },
            };

            context.accounttypes.AddRange(entities);
            context.SaveChanges();
        }

        private void InitLeadSource(ApplicationDbContext context)
        {
            List<leadsource> entities = new List<leadsource>
            {
                new leadsource() { code = "web", name = "web" },
                new leadsource() { code = "email", name = "email" },
                new leadsource() { code = "media", name = "media" },
                new leadsource() { code = "direct", name = "direct" },
            };

            context.leadsources.AddRange(entities);
            context.SaveChanges();
        }

        private void InitAccountActivityType(ApplicationDbContext context)
        {
            List<accountactivitytype> entities = new List<accountactivitytype>
            {
                new accountactivitytype() { code = "telp", name = "telp" },
                new accountactivitytype() { code = "email", name = "email" },
                new accountactivitytype() { code = "visit", name = "visit" },
            };

            context.accountactivitytypes.AddRange(entities);
            context.SaveChanges();
        }

        private void InitStatusLead(ApplicationDbContext context)
        {
            List<statuslead> entities = new List<statuslead>
            {
                new statuslead() { code = "open", name = "open" },
                new statuslead() { code = "qualified", name = "qualified" },
                new statuslead() { code = "disqualified", name = "disqualified" },
            };

            context.statusleads.AddRange(entities);
            context.SaveChanges();
        }

        private void InitProductType(ApplicationDbContext context)
        {
            List<producttype> entities = new List<producttype>
            {
                new producttype() { code = "Item", name = "Item" },
                new producttype() { code = "Service", name = "Service" },
            };

            context.producttypes.AddRange(entities);
            context.SaveChanges();
        }

        private void InitCampaignActivityType(ApplicationDbContext context)
        {
            List<campaignactivitytype> entities = new List<campaignactivitytype>
            {
                new campaignactivitytype() { code = "Phone", name = "Phone" },
                new campaignactivitytype() { code = "Email", name = "Email" },
            };

            context.campaignactivitytypes.AddRange(entities);
            context.SaveChanges();
        }

        private void InitCaseStatus(ApplicationDbContext context)
        {
            List<statuscase> entities = new List<statuscase>
            {
                new statuscase() { code = "Close", name = "Close" },
                new statuscase() { code = "Cancel", name = "Cancel" },
            };

            context.statuscases.AddRange(entities);
            context.SaveChanges();
        }

    }


    


}