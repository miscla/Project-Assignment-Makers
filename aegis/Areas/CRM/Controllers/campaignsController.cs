using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aegis.Models;

namespace aegis.Areas.CRM.Controllers
{

    [Authorize]
    public class campaignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.campaigns.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string getddlcampaignactivitytypeId()
        {
            
            var alllist = db.campaignactivitytypes.ToList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in alllist)
            {
                sb.Append(item.campaignactivitytypeId.ToString() + ":" + item.code + ";");
            }
            var result = sb.ToString();
            var length = result.Length;
            result = result.Remove(length - 1, 1);
            return result;//contoh: "FE: FedEx; IN: InTime; TN: TNT";
        }

        
        // GET: 
        public JsonResult lines(int? id)
        { 
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.campaignlines.Where(x => x.campaign.campaignId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, campaignline line)
        {
                campaign header = db.campaigns.Find(Convert.ToInt32(fkidx));
                line.campaign = header;
                db.campaignlines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,campaignline line)
        {
            if (ModelState.IsValid)
            {
                campaign  header = db.campaigns.Find(Convert.ToInt32(fkidx));
                line.campaign  = header;
                db.Entry(line).State = EntityState.Modified;
                db.SaveChanges();
                return "";
            }

            return "";
        }

        // POST
        [HttpPost]
        public String LineDelete(string key)
        {
            if (ModelState.IsValid)
            {
                campaignline data = db.campaignlines.Find(Convert.ToInt32(key));
                db.campaignlines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/campaigns/
        public ActionResult Index()
        {
            var campaigns = db.campaigns.Include(c => c.marketinglist);
            return View(campaigns.ToList());
        }

        // GET: /CRM/campaigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            campaign campaign = db.campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: /CRM/campaigns/Create
        public ActionResult Create()
        {
            ViewBag.marketinglistIdList = new SelectList(db.marketinglists, "marketinglistId", "code");
            return View();
        }

        // POST: /CRM/campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="campaignId,code,name,description,startdate,enddate,marketinglistId,DetailForm_pkidx_campaignlineId,DetailForm_ddl_campaignactivitytypeId,DetailForm_activitydate,DetailForm_description")] campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.campaigns.Add(campaign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.marketinglistIdList = new SelectList(db.marketinglists, "marketinglistId", "code", campaign.marketinglistId);
            return View(campaign);
        }

        // GET: /CRM/campaigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            campaign campaign = db.campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            ViewBag.marketinglistIdList = new SelectList(db.marketinglists, "marketinglistId", "code", campaign.marketinglistId);
            return View(campaign);
        }

        // POST: /CRM/campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="campaignId,code,name,description,startdate,enddate,marketinglistId,DetailForm_pkidx_campaignlineId,DetailForm_ddl_campaignactivitytypeId,DetailForm_activitydate,DetailForm_description")] campaign campaign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.marketinglistIdList = new SelectList(db.marketinglists, "marketinglistId", "code", campaign.marketinglistId);
            return View(campaign);
        }

        // GET: /CRM/campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            campaign campaign = db.campaigns.Find(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: /CRM/campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            campaign campaign = db.campaigns.Find(id);
            db.campaigns.Remove(campaign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
