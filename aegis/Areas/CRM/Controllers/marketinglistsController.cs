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
    public class marketinglistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.marketinglists.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string getddlaccountId()
        {
            
            var alllist = db.accounts.ToList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in alllist)
            {
                sb.Append(item.accountId.ToString() + ":" + item.code + ";");
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
            return Json(db.marketinglistlines.Where(x => x.marketinglist.marketinglistId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, marketinglistline line)
        {
                marketinglist header = db.marketinglists.Find(Convert.ToInt32(fkidx));
                line.marketinglist = header;
                db.marketinglistlines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,marketinglistline line)
        {
            if (ModelState.IsValid)
            {
                marketinglist  header = db.marketinglists.Find(Convert.ToInt32(fkidx));
                line.marketinglist  = header;
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
                marketinglistline data = db.marketinglistlines.Find(Convert.ToInt32(key));
                db.marketinglistlines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/marketinglists/
        public ActionResult Index()
        {
            return View(db.marketinglists.ToList());
        }

        // GET: /CRM/marketinglists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marketinglist marketinglist = db.marketinglists.Find(id);
            if (marketinglist == null)
            {
                return HttpNotFound();
            }
            return View(marketinglist);
        }

        // GET: /CRM/marketinglists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CRM/marketinglists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="marketinglistId,code,name,description,startdate,enddate,DetailForm_pkidx_marketinglistlineId,DetailForm_ddl_accountId,DetailForm_description")] marketinglist marketinglist)
        {
            if (ModelState.IsValid)
            {
                db.marketinglists.Add(marketinglist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marketinglist);
        }

        // GET: /CRM/marketinglists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marketinglist marketinglist = db.marketinglists.Find(id);
            if (marketinglist == null)
            {
                return HttpNotFound();
            }
            return View(marketinglist);
        }

        // POST: /CRM/marketinglists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="marketinglistId,code,name,description,startdate,enddate,DetailForm_pkidx_marketinglistlineId,DetailForm_ddl_accountId,DetailForm_description")] marketinglist marketinglist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marketinglist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marketinglist);
        }

        // GET: /CRM/marketinglists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marketinglist marketinglist = db.marketinglists.Find(id);
            if (marketinglist == null)
            {
                return HttpNotFound();
            }
            return View(marketinglist);
        }

        // POST: /CRM/marketinglists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            marketinglist marketinglist = db.marketinglists.Find(id);
            db.marketinglists.Remove(marketinglist);
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
