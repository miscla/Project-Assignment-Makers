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
    public class casesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.casess.ToList(), JsonRequestBehavior.AllowGet);
        }


        
        // GET: 
        public JsonResult lines(int? id)
        { 
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.caseslines.Where(x => x.cases.casesId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, casesline line)
        {
                cases header = db.casess.Find(Convert.ToInt32(fkidx));
                line.cases = header;
                db.caseslines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,casesline line)
        {
            if (ModelState.IsValid)
            {
                cases  header = db.casess.Find(Convert.ToInt32(fkidx));
                line.cases  = header;
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
                casesline data = db.caseslines.Find(Convert.ToInt32(key));
                db.caseslines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/cases/
        public ActionResult Index()
        {
            var casess = db.casess.Include(c => c.account).Include(c => c.product).Include(c => c.statuscase);
            return View(casess.ToList());
        }

        // GET: /CRM/cases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cases cases = db.casess.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            return View(cases);
        }

        // GET: /CRM/cases/Create
        public ActionResult Create()
        {
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code");
            ViewBag.productIdList = new SelectList(db.products, "productId", "code");
            ViewBag.statuscaseIdList = new SelectList(db.statuscases, "statuscaseId", "code");
            return View();
        }

        // POST: /CRM/cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="casesId,code,name,description,reporteddate,accountId,productId,statuscaseId,DetailForm_pkidx_caseslineId,DetailForm_actiondate,DetailForm_actiondescription")] cases cases)
        {
            if (ModelState.IsValid)
            {
                db.casess.Add(cases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", cases.accountId);
            ViewBag.productIdList = new SelectList(db.products, "productId", "code", cases.productId);
            ViewBag.statuscaseIdList = new SelectList(db.statuscases, "statuscaseId", "code", cases.statuscaseId);
            return View(cases);
        }

        // GET: /CRM/cases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cases cases = db.casess.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", cases.accountId);
            ViewBag.productIdList = new SelectList(db.products, "productId", "code", cases.productId);
            ViewBag.statuscaseIdList = new SelectList(db.statuscases, "statuscaseId", "code", cases.statuscaseId);
            return View(cases);
        }

        // POST: /CRM/cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="casesId,code,name,description,reporteddate,accountId,productId,statuscaseId,DetailForm_pkidx_caseslineId,DetailForm_actiondate,DetailForm_actiondescription")] cases cases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", cases.accountId);
            ViewBag.productIdList = new SelectList(db.products, "productId", "code", cases.productId);
            ViewBag.statuscaseIdList = new SelectList(db.statuscases, "statuscaseId", "code", cases.statuscaseId);
            return View(cases);
        }

        // GET: /CRM/cases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cases cases = db.casess.Find(id);
            if (cases == null)
            {
                return HttpNotFound();
            }
            return View(cases);
        }

        // POST: /CRM/cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cases cases = db.casess.Find(id);
            db.casess.Remove(cases);
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
