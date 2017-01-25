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
    /// <summary>
    /// untuk membuat table-table role & membership
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum documentstatus
    {
        notposted = 0, posted = 1

    }

    public class auditcreation
    {
        public auditcreation()
        {
            createdat = DateTime.Now;
            documentstatus = documentstatus.notposted;
        }
        public string createdby { get; set; }
        public DateTime createdat { get; set; }
        public string modifiedby { get; set; }
        public DateTime modifiedat { get; set;}
        public documentstatus documentstatus { get; set; }


    }

 

   

    public class modul 
    {
        public int modulId { get; set; }
        [Required]
        public string namamodul { get; set; }
        [Required]
        public string namacontroller { get; set; }
        [Required]
        public string alias { get; set; }

    }

    public class userrole 
    {
        public int userroleId { get; set; }
        [Required]
        public string rolename { get; set; }
        public string roledescription { get; set; }

        public virtual List<userroleline> userrolelines { get; set; }

    }

    public class userroleline
    {
        public int userrolelineId { get; set; }

        public virtual userrole userrole { get; set; }
        public virtual ApplicationUser applicationuser { get; set; }
        public string email { get; set; }

    }

    public class akses 
    {
        public int aksesId { get; set; }
        public virtual userrole userrole { get; set; }
        public string aksesdescription { get; set; }
        
        public virtual List<aksesline> akseslines { get; set; }


    }

    public class aksesline 
    {
        public int akseslineId { get; set; }
        public virtual akses akses { get; set; }
        public virtual modul modul { get; set; }

    }

    public class userprofile
    {
        public int userprofileId { get; set; }
        public string fullname { get; set; }
    }

    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// CRM
        /// </summary>
        /// 
        
        public DbSet<accounttype> accounttypes { get; set; }
        public DbSet<leadsource> leadsources { get; set; }
        public DbSet<accountactivitytype> accountactivitytypes { get; set; }
        public DbSet<statuslead> statusleads { get; set; }
        public DbSet<account> accounts { get; set; }
        public DbSet<accountactivityline> accountactivitylines { get; set; }
        public DbSet<pipe> pipes { get; set; }
        public DbSet<stage> stages { get; set; }
        public DbSet<statusopportunity> statusopportunitys { get; set; }
        public DbSet<opportunity> opportunitys { get; set; }
        public DbSet<opportunityline> opportunitylines { get; set; }
        public DbSet<producttype> producttypes { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<quote> quotes { get; set; }
        public DbSet<quoteline> quotelines { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<orderline> orderlines { get; set; }
        public DbSet<invoice> invoices { get; set; }
        public DbSet<invoiceline> invoicelines { get; set; }
        public DbSet<marketinglist> marketinglists { get; set; }
        public DbSet<marketinglistline> marketinglistlines { get; set; }
        public DbSet<campaignactivitytype> campaignactivitytypes { get; set; }
        public DbSet<campaign> campaigns { get; set; }
        public DbSet<campaignline> campaignlines { get; set; }
        public DbSet<statuscase> statuscases { get; set; }
        public DbSet<cases> casess { get; set; }
        public DbSet<casesline> caseslines { get; set; }
        public DbSet<organizationtype> organizationtypes { get; set; }
        public DbSet<organization> organizations { get; set; }

        public DbSet<lead> leads { get; set; }
        public DbSet<modul> moduls { get; set; }

        public DbSet<userrole> userroles { get; set; }

        public DbSet<akses> aksess { get; set; }
        public DbSet<aksesline> akseslines { get; set; }

        public DbSet<userroleline> userrolelines { get; set; }

        public System.Data.Entity.DbSet<aegis.Models.userprofile> userprofiles { get; set; }
        
    }

    
}