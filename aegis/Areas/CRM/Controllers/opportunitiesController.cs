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
    public class opportunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.opportunitys.ToList(), JsonRequestBehavior.AllowGet);
        }


        
        // GET: 
        public JsonResult lines(int? id)
        { 
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.opportunitylines.Where(x => x.opportunity.opportunityId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, opportunityline line)
        {
                opportunity header = db.opportunitys.Find(Convert.ToInt32(fkidx));
                line.opportunity = header;
                db.opportunitylines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,opportunityline line)
        {
            if (ModelState.IsValid)
            {
                opportunity  header = db.opportunitys.Find(Convert.ToInt32(fkidx));
                line.opportunity  = header;
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
                opportunityline data = db.opportunitylines.Find(Convert.ToInt32(key));
                db.opportunitylines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/opportunities/
        public ActionResult Index()
        {
            var opportunitys = db.opportunitys.Include(o => o.account).Include(o => o.stage).Include(o => o.statusopportunity);
            return View(opportunitys.ToList());
        }

        // GET: /CRM/opportunities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            opportunity opportunity = db.opportunitys.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            return View(opportunity);
        }

        // GET: /CRM/opportunities/Create
        public ActionResult Create()
        {
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code");
            ViewBag.stageIdList = new SelectList(db.stages, "stageId", "code");
            ViewBag.statusopportunityIdList = new SelectList(db.statusopportunitys, "statusopportunityId", "code");
            return View();
        }

        // POST: /CRM/opportunities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="opportunityId,stageId,statusopportunityId,accountId,code,name,description,value,winningprobability,forecastclosedate,DetailForm_pkidx_opportunitylineId,DetailForm_activitydate,DetailForm_description")] opportunity opportunity)
        {
            if (ModelState.IsValid)
            {
                db.opportunitys.Add(opportunity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", opportunity.accountId);
            ViewBag.stageIdList = new SelectList(db.stages, "stageId", "code", opportunity.stageId);
            ViewBag.statusopportunityIdList = new SelectList(db.statusopportunitys, "statusopportunityId", "code", opportunity.statusopportunityId);
            return View(opportunity);
        }

        // GET: /CRM/opportunities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            opportunity opportunity = db.opportunitys.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", opportunity.accountId);
            ViewBag.stageIdList = new SelectList(db.stages, "stageId", "code", opportunity.stageId);
            ViewBag.statusopportunityIdList = new SelectList(db.statusopportunitys, "statusopportunityId", "code", opportunity.statusopportunityId);
            return View(opportunity);
        }

        // POST: /CRM/opportunities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="opportunityId,stageId,statusopportunityId,accountId,code,name,description,value,winningprobability,forecastclosedate,DetailForm_pkidx_opportunitylineId,DetailForm_activitydate,DetailForm_description")] opportunity opportunity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opportunity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", opportunity.accountId);
            ViewBag.stageIdList = new SelectList(db.stages, "stageId", "code", opportunity.stageId);
            ViewBag.statusopportunityIdList = new SelectList(db.statusopportunitys, "statusopportunityId", "code", opportunity.statusopportunityId);
            return View(opportunity);
        }

        // GET: /CRM/opportunities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            opportunity opportunity = db.opportunitys.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }
            return View(opportunity);
        }

        // POST: /CRM/opportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            opportunity opportunity = db.opportunitys.Find(id);
            db.opportunitys.Remove(opportunity);
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
