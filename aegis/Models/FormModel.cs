using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//tambahan
using System.Web.Mvc;

namespace aegis.Models
{
    public class NavigationItemForm
    {
        public int modulId { get; set; }
        public string namacontroller { get; set; }
        public string alias { get; set; }

    }
    public class NavigationForm
    {
        public string namamodul { get; set; }
        public List<NavigationItemForm> navigationitems { get; set; }

    }

    public class TimeSheetLineForm
    {
        public string timesheetheaderId { get; set; }
        public string[] tasks { get; set; }
        public string[] descriptions { get; set; }
        public string[] hourspents { get; set; }
    }

    public class UserRoleLineForm
    {
        public string userroleId { get; set; }
        public string[] users { get; set; }
    }

    public class AksesLineForm
    {
        public string aksesId { get; set; }
        public string[] moduls { get; set; }
    }

    public class AksesForm 
    {
        public int aksesId { get; set; }
        public userrole userrole { get; set; }
        public string aksesdescription { get; set; }

        public int userroleId { get; set; }
        public virtual List<aksesline> akseslines { get; set; }
        public SelectList userroles { get; set; }
    }
}