using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aegis.Models
{
    public class Initializer
    {
        public void run(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            string rolename = "Admin";
            string password = "123456";
            string email = "admin@aegis.co.id";
            string emailbudget = "budget@aegis.co.id";
            string emailcrm = "crm@aegis.co.id";
            string emailga = "ga@aegis.co.id";

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(rolename))
            {
                var roleresult = RoleManager.Create(new IdentityRole(rolename));
            }

            //Create User=Admin with password=aegis
            var user = new ApplicationUser();
            user.UserName = email;
            user.Email = email;
            var adminresult = UserManager.Create(user, password);

            var userbudget = new ApplicationUser();
            userbudget.UserName = emailbudget;
            userbudget.Email = emailbudget;
            UserManager.Create(userbudget, password);

            var usercrm = new ApplicationUser();
            usercrm.UserName = emailcrm;
            usercrm.Email = emailcrm;
            UserManager.Create(usercrm, password);

            var userga = new ApplicationUser();
            userga.UserName = emailga;
            userga.Email = emailga;
            UserManager.Create(userga, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, rolename);
            }

           
            initModul(context);
            initRole(context);

            initAdminRole(context);
            initBudgetRole(context);
            initCRMRole(context);
            initGARole(context);

            initAkses(context);
            initAksesLine(context);

            

           
            initPipeline(context);
            initStage(context);
            initStatusOpportunity(context);
            initOpportunity(context);

            var crm = new InitializerCRM(context);
        }

        private DateTime generateStartDate(Random gen)
        {
            DateTime start = new DateTime(2016, 12, 1);
            DateTime end = new DateTime(2016, 12, 30);
            int range = (end - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private DateTime generateEndDate(Random gen)
        {
            DateTime start = new DateTime(2017, 1, 1);
            DateTime end = new DateTime(2017, 12, 1);
            int range = (end - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private int getRandomIndex(Random gen, int min, int max)
        {
            int randomIndex = gen.Next(min, max);
            return randomIndex;
        }

        private decimal getRandomValue(Random gen)
        {
            decimal[] numbers = new decimal[5] { 500000000, 350000000, 480000000, 680000000, 430000000 };
            int randomIndex = gen.Next(0, 5);
            decimal randomNumber = numbers[randomIndex];
            return randomNumber;
        }

        private int getRandomProgress(Random gen)
        {
            int[] numbers = new int[5] { 80, 70, 60, 40, 20 };
            int randomIndex = gen.Next(0, 5);
            int randomNumber = numbers[randomIndex];
            return randomNumber;
        }
        


    

        private void initRole(ApplicationDbContext context)
        {
            var admin = new userrole();

            admin.rolename = "ADMIN";
            context.userroles.Add(admin);

            var crmstaff = new userrole();

            crmstaff.rolename = "CRM_STAFF";
            context.userroles.Add(crmstaff);

            var budgetstaff = new userrole();

            budgetstaff.rolename = "BUDGETING_STAFF";
            context.userroles.Add(budgetstaff);

            var gastaff = new userrole();

            gastaff.rolename = "GA_STAFF";
            context.userroles.Add(gastaff);

            context.SaveChanges();
        }

        private void initModul(ApplicationDbContext context)
        {
            
          

            var CRM = "CRM";

            var product = new modul();
            product.namamodul = CRM;
            product.namacontroller = "products";
            product.alias = "Product";
            context.moduls.Add(product);

            var account = new modul();
            account.namamodul = CRM;
            account.namacontroller = "accounts";
            account.alias = "Account";
            context.moduls.Add(account);

            var opportunity = new modul();
            opportunity.namamodul = CRM;
            opportunity.namacontroller = "opportunities";
            opportunity.alias = "Opportunity";
            context.moduls.Add(opportunity);

           

            var quote = new modul();
            quote.namamodul = CRM;
            quote.namacontroller = "quotes";
            quote.alias = "Quote";
            context.moduls.Add(quote);

            var order = new modul();
            order.namamodul = CRM;
            order.namacontroller = "orders";
            order.alias = "Order";
            context.moduls.Add(order);

            var invoice = new modul();
            invoice.namamodul = CRM;
            invoice.namacontroller = "invoices";
            invoice.alias = "Invoice";
            context.moduls.Add(invoice);


            var marketinglist = new modul();
            marketinglist.namamodul = CRM;
            marketinglist.namacontroller = "marketinglists";
            marketinglist.alias = "Marketing List";
            context.moduls.Add(marketinglist);

            var campaign = new modul();
            campaign.namamodul = CRM;
            campaign.namacontroller = "campaigns";
            campaign.alias = "Campaign";
            context.moduls.Add(campaign);

            var cases = new modul();
            cases.namamodul = CRM;
            cases.namacontroller = "casess";
            cases.alias = "Cases";
            context.moduls.Add(cases);



            var producttype = new modul();
            producttype.namamodul = CRM;
            producttype.namacontroller = "producttypes";
            producttype.alias = "Product Type";
            context.moduls.Add(producttype);

           

            var organizationtype = new modul();
            organizationtype.namamodul = CRM;
            organizationtype.namacontroller = "organizationtypes";
            organizationtype.alias = "Organization Type";
            context.moduls.Add(organizationtype);

            var organization = new modul();
            organization.namamodul = CRM;
            organization.namacontroller = "organizations";
            organization.alias = "Organization";
            context.moduls.Add(organization);

            var lead = new modul();
            lead.namamodul = CRM;
            lead.namacontroller = "leads";
            lead.alias = "Lead";
            context.moduls.Add(lead);

            

            var pipeline = new modul();
            pipeline.namamodul = CRM;
            pipeline.namacontroller = "pipes";
            pipeline.alias = "Pipe";
            context.moduls.Add(pipeline);

            var stage = new modul();
            stage.namamodul = CRM;
            stage.namacontroller = "stages";
            stage.alias = "Stage";
            context.moduls.Add(stage);

            var statusopportunity = new modul();
            statusopportunity.namamodul = CRM;
            statusopportunity.namacontroller = "statusopportunities";
            statusopportunity.alias = "Status Opportunity";
            context.moduls.Add(statusopportunity);

           


            var Administration = "Administration";

            var user = new modul();
            user.namamodul = Administration;
            user.namacontroller = "userprofiles";
            user.alias = "User";
            context.moduls.Add(user);

            var role = new modul();
            role.namamodul = Administration;
            role.namacontroller = "userroles";
            role.alias = "Role";
            context.moduls.Add(role);

            var modul = new modul();
            modul.namamodul = Administration;
            modul.namacontroller = "moduls";
            modul.alias = "Modul";
            context.moduls.Add(modul);

            var akses = new modul();
            akses.namamodul = Administration;
            akses.namacontroller = "akses";
            akses.alias = "Akses";
            context.moduls.Add(akses);

            

            context.SaveChanges();
        }

        private void initAdminRole(ApplicationDbContext context)
        {
            var adminrole = context.userroles.Where(s => s.rolename.Equals("ADMIN")).FirstOrDefault();
            var appuser = context.Users.Where(s => s.UserName.Equals("admin@aegis.co.id")).FirstOrDefault();
            if (adminrole != null && appuser != null)
            {
                var roleline = new userroleline();
                roleline.userrole = adminrole;
                roleline.applicationuser = appuser;
                roleline.email = appuser.Email;

                context.userrolelines.Add(roleline);
                context.SaveChanges();
            }
        }

        private void initBudgetRole(ApplicationDbContext context)
        {
            var role = context.userroles.Where(s => s.rolename.Equals("BUDGETING_STAFF")).FirstOrDefault();
            var appuser = context.Users.Where(s => s.UserName.Equals("budget@aegis.co.id")).FirstOrDefault();
            if (role != null && appuser != null)
            {
                var roleline = new userroleline();
                roleline.userrole = role;
                roleline.applicationuser = appuser;
                roleline.email = appuser.Email;

                context.userrolelines.Add(roleline);
                context.SaveChanges();
            }
        }

        private void initCRMRole(ApplicationDbContext context)
        {
            var role = context.userroles.Where(s => s.rolename.Equals("CRM_STAFF")).FirstOrDefault();
            var appuser = context.Users.Where(s => s.UserName.Equals("crm@aegis.co.id")).FirstOrDefault();
            if (role != null && appuser != null)
            {
                var roleline = new userroleline();
                roleline.userrole = role;
                roleline.applicationuser = appuser;
                roleline.email = appuser.Email;

                context.userrolelines.Add(roleline);
                context.SaveChanges();
            }
        }

        private void initGARole(ApplicationDbContext context)
        {
            var role = context.userroles.Where(s => s.rolename.Equals("GA_STAFF")).FirstOrDefault();
            var appuser = context.Users.Where(s => s.UserName.Equals("ga@aegis.co.id")).FirstOrDefault();
            if (role != null && appuser != null)
            {
                var roleline = new userroleline();
                roleline.userrole = role;
                roleline.applicationuser = appuser;
                roleline.email = appuser.Email;

                context.userrolelines.Add(roleline);
                context.SaveChanges();
            }
        }

        private void initAkses(ApplicationDbContext context)
        {
            var crm_staff = context.userroles.Where(s => s.rolename.Equals("CRM_STAFF")).FirstOrDefault();
            var budgeting_staff = context.userroles.Where(s => s.rolename.Equals("BUDGETING_STAFF")).FirstOrDefault();
            var ga_staff = context.userroles.Where(s => s.rolename.Equals("GA_STAFF")).FirstOrDefault();

            var crm_akses = new akses();
            crm_akses.userrole = crm_staff;
            crm_akses.aksesdescription = "CRM_STAFF";
            context.aksess.Add(crm_akses);

            var budgeting_akses = new akses();
            budgeting_akses.userrole = budgeting_staff;
            budgeting_akses.aksesdescription = "BUDGETING_STAFF";
            context.aksess.Add(budgeting_akses);

            var ga_akses = new akses();
            ga_akses.userrole = ga_staff;
            ga_akses.aksesdescription = "GA_STAFF";
            context.aksess.Add(ga_akses);

            context.SaveChanges();
        }

        private void initAksesLine(ApplicationDbContext context)
        {
            
            var crm_role = context.userroles.Where(s => s.rolename.Equals("CRM_STAFF")).FirstOrDefault();
            var crm_akses = context.aksess.Where(s => s.userrole.userroleId.Equals(crm_role.userroleId)).FirstOrDefault();
            List<modul> crmmoduls = context.moduls.Where(s => s.namamodul.Equals("CRM")).ToList();
            foreach (var item in crmmoduls)
            {
                var line = new aksesline();
                line.akses = crm_akses;
                line.modul = item;
                context.akseslines.Add(line);
            }

            var productivity_role = context.userroles.Where(s => s.rolename.Equals("GA_STAFF")).FirstOrDefault();
            var productivity_akses = context.aksess.Where(s => s.userrole.userroleId.Equals(productivity_role.userroleId)).FirstOrDefault();
            List<modul> productivitymoduls = context.moduls.Where(s => s.namamodul.Equals("Productivity")).ToList();
            foreach (var item in productivitymoduls)
            {
                var line = new aksesline();
                line.akses = productivity_akses;
                line.modul = item;
                context.akseslines.Add(line);
            }

            var budget_role = context.userroles.Where(s => s.rolename.Equals("BUDGETING_STAFF")).FirstOrDefault();
            var budget_akses = context.aksess.Where(s => s.userrole.userroleId.Equals(budget_role.userroleId)).FirstOrDefault();
            List<modul> budgetmoduls = context.moduls.Where(s => s.namamodul.Equals("Budget")).ToList();
            foreach (var item in budgetmoduls)
            {
                var line = new aksesline();
                line.akses = budget_akses;
                line.modul = item;
                context.akseslines.Add(line);
            }

            context.SaveChanges();
        }
        
        

        private void initPipeline(ApplicationDbContext context)
        {
            List<pipe> pipelines = new List<pipe>()
            {
                new pipe() {code="STD", name="Standard", description="Standard" },
                new pipe() {code="FT", name="Fast Track", description="Fast Track" }
            };

            context.pipes.AddRange(pipelines);
            context.SaveChanges();
        }

        private void initStage(ApplicationDbContext context)
        {
            var std = context.pipes.Where(s => s.code.Equals("STD")).FirstOrDefault();
            var ft = context.pipes.Where(s => s.code.Equals("FT")).FirstOrDefault();

            List<stage> stds = new List<stage>()
            {
                new stage() {code="STD1", name="Initial Contact", pipe=std },
                new stage() {code="STD2", name="Qualification", pipe=std },
                new stage() {code="STD3", name="Meeting", pipe=std },
                new stage() {code="STD4", name="Proposal", pipe=std },
                new stage() {code="STD5", name="Close", pipe=std }           
            };

            context.stages.AddRange(stds);

            List<stage> fts = new List<stage>()
            {
                new stage() {code="FT1", name="Initial Contact", pipe=ft },
                new stage() {code="FT2", name="Proposal", pipe=ft },
                new stage() {code="FT3", name="Close", pipe=ft }
            };

            context.stages.AddRange(fts);

            context.SaveChanges();
        }

        private void initStatusOpportunity(ApplicationDbContext context)
        {
            List<statusopportunity> status = new List<statusopportunity>()
            {
                new statusopportunity() {code="S0", name="Not Contacted Yet" },
                new statusopportunity() {code="S1", name="Contacted" },
                new statusopportunity() {code="S2", name="Won" },
                new statusopportunity() {code="S3", name="Loose" }
            };


            context.statusopportunitys.AddRange(status);

            context.SaveChanges();
        }

        private void initOpportunity(ApplicationDbContext context)
        {
            var std1 = context.stages.Where(x => x.code.Equals("STD1")).FirstOrDefault();
            var std2 = context.stages.Where(x => x.code.Equals("STD2")).FirstOrDefault();
            var std3 = context.stages.Where(x => x.code.Equals("STD3")).FirstOrDefault();
            var std4 = context.stages.Where(x => x.code.Equals("STD4")).FirstOrDefault();
            var std5 = context.stages.Where(x => x.code.Equals("STD5")).FirstOrDefault();
            var ft1 = context.stages.Where(x => x.code.Equals("FT1")).FirstOrDefault();
            var ft2 = context.stages.Where(x => x.code.Equals("FT2")).FirstOrDefault();
            var ft3 = context.stages.Where(x => x.code.Equals("FT3")).FirstOrDefault();

            List<stage> stages = new List<stage>();
            stages.Add(std1);
            stages.Add(std2);
            stages.Add(std3);
            stages.Add(std4);
            stages.Add(std5);
            stages.Add(ft1);
            stages.Add(ft2);
            stages.Add(ft3);
            Random genStage = new Random();

            var st0 = context.statusopportunitys.Where(x => x.code.Equals("S0")).FirstOrDefault();
            var st1 = context.statusopportunitys.Where(x => x.code.Equals("S1")).FirstOrDefault();
            var st2 = context.statusopportunitys.Where(x => x.code.Equals("S2")).FirstOrDefault();
            var st3 = context.statusopportunitys.Where(x => x.code.Equals("S3")).FirstOrDefault();
            var st4 = context.statusopportunitys.Where(x => x.code.Equals("S4")).FirstOrDefault();

            List<statusopportunity> status = new List<statusopportunity>();
            status.Add(st0);
            status.Add(st1);
            status.Add(st2);
            status.Add(st3);
            status.Add(st4);
            Random genStatus = new Random();

            List<decimal> values = new List<decimal>();
            values.Add(450000000);
            values.Add(350000000);
            values.Add(250000000);
            values.Add(650000000);
            values.Add(430000000);
            values.Add(370000000);
            values.Add(410000000);
            values.Add(550000000);
            Random genValues = new Random();


            List<int> winnings = new List<int>();
            winnings.Add(80);
            winnings.Add(90);
            winnings.Add(70);
            winnings.Add(40);
            winnings.Add(30);
            winnings.Add(20);
            Random genWinnings = new Random();

            List<DateTime> closeds = new List<DateTime>();
            closeds.Add(DateTime.Now.AddMonths(1));
            closeds.Add(DateTime.Now.AddMonths(2));
            closeds.Add(DateTime.Now.AddMonths(3));
            closeds.Add(DateTime.Now.AddMonths(4));
            closeds.Add(DateTime.Now.AddMonths(5));
            closeds.Add(DateTime.Now.AddMonths(6));
            closeds.Add(DateTime.Now.AddMonths(7));
            Random genCloseds = new Random();

            List<opportunity> opps = new List<opportunity>();
            for (int i = 0; i < 50; i++)
            {
                var op = new opportunity();
                op.stage = stages[getRandomIndex(genStage, 0, 7)];
                op.statusopportunity = status[getRandomIndex(genStatus, 0, 4)];
                op.code = "OPP-" + (i + 1).ToString().PadLeft(3, '0');
                op.name = op.code;
                op.value = values[getRandomIndex(genValues, 0, 7)];
                op.winningprobability = winnings[getRandomIndex(genWinnings, 0, 5)];
                op.forecastclosedate = closeds[getRandomIndex(genCloseds, 0, 6)];
                opps.Add(op);
            }


            context.opportunitys.AddRange(opps);
            context.SaveChanges();


        }



    }



    
}