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
    public class accountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.accounts.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string getddlaccountactivitytypeId()
        {
            
            var alllist = db.accountactivitytypes.ToList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in alllist)
            {
                sb.Append(item.accountactivitytypeId.ToString() + ":" + item.code + ";");
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
            return Json(db.accountactivitylines.Where(x => x.account.accountId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, accountactivityline line)
        {
                account header = db.accounts.Find(Convert.ToInt32(fkidx));
                line.account = header;
                db.accountactivitylines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,accountactivityline line)
        {
            if (ModelState.IsValid)
            {
                account  header = db.accounts.Find(Convert.ToInt32(fkidx));
                line.account  = header;
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
                accountactivityline data = db.accountactivitylines.Find(Convert.ToInt32(key));
                db.accountactivitylines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/accounts/
        public ActionResult Index()
        {
            var accounts = db.accounts.Include(a => a.accounttype).Include(a => a.leadsource).Include(a => a.statuslead);
            return View(accounts.ToList());
        }

        // GET: /CRM/accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: /CRM/accounts/Create
        public ActionResult Create()
        {
            ViewBag.accounttypeIdList = new SelectList(db.accounttypes, "accounttypeId", "code");
            ViewBag.leadsourceIdList = new SelectList(db.leadsources, "leadsourceId", "code");
            ViewBag.statusleadIdList = new SelectList(db.statusleads, "statusleadId", "code");
            return View();
        }

        // POST: /CRM/accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="accountId,code,name,telephone,email,address,accounttypeId,leadsourceId,statusleadId,DetailForm_pkidx_accountactivitylineId,DetailForm_ddl_accountactivitytypeId,DetailForm_activitydate,DetailForm_description")] account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accounttypeIdList = new SelectList(db.accounttypes, "accounttypeId", "code", account.accounttypeId);
            ViewBag.leadsourceIdList = new SelectList(db.leadsources, "leadsourceId", "code", account.leadsourceId);
            ViewBag.statusleadIdList = new SelectList(db.statusleads, "statusleadId", "code", account.statusleadId);
            return View(account);
        }

        // GET: /CRM/accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.accounttypeIdList = new SelectList(db.accounttypes, "accounttypeId", "code", account.accounttypeId);
            ViewBag.leadsourceIdList = new SelectList(db.leadsources, "leadsourceId", "code", account.leadsourceId);
            ViewBag.statusleadIdList = new SelectList(db.statusleads, "statusleadId", "code", account.statusleadId);
            return View(account);
        }

        // POST: /CRM/accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="accountId,code,name,telephone,email,address,accounttypeId,leadsourceId,statusleadId,DetailForm_pkidx_accountactivitylineId,DetailForm_ddl_accountactivitytypeId,DetailForm_activitydate,DetailForm_description")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accounttypeIdList = new SelectList(db.accounttypes, "accounttypeId", "code", account.accounttypeId);
            ViewBag.leadsourceIdList = new SelectList(db.leadsources, "leadsourceId", "code", account.leadsourceId);
            ViewBag.statusleadIdList = new SelectList(db.statusleads, "statusleadId", "code", account.statusleadId);
            return View(account);
        }

        // GET: /CRM/accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: /CRM/accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
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
