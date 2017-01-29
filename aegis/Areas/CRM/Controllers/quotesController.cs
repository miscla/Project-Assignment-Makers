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
    public class quotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.quotes.ToList(), JsonRequestBehavior.AllowGet);
        }

        public string getddlproductId()
        {
            
            var alllist = db.products.ToList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in alllist)
            {
                sb.Append(item.productId.ToString() + ":" + item.code + ";");
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
            return Json(db.quotelines.Where(x => x.quote.quoteId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, quoteline line)
        {
                quote header = db.quotes.Find(Convert.ToInt32(fkidx));
                line.quote = header;
                db.quotelines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,quoteline line)
        {
            if (ModelState.IsValid)
            {
                quote  header = db.quotes.Find(Convert.ToInt32(fkidx));
                line.quote  = header;
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
                quoteline data = db.quotelines.Find(Convert.ToInt32(key));
                db.quotelines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/quotes/
        public ActionResult Index()
        {
            var quotes = db.quotes.Include(q => q.account);
            return View(quotes.ToList());
        }

        // GET: /CRM/quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quote quote = db.quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: /CRM/quotes/Create
        public ActionResult Create()
        {
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code");
            return View();
        }

        // POST: /CRM/quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="quoteId,code,name,description,accountId,estimatedstartdate,estimatedenddate,DetailForm_pkidx_quotelineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] quote quote)
        {
            if (ModelState.IsValid)
            {
                db.quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", quote.accountId);
            return View(quote);
        }

        // GET: /CRM/quotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quote quote = db.quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", quote.accountId);
            return View(quote);
        }

        // POST: /CRM/quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="quoteId,code,name,description,accountId,estimatedstartdate,estimatedenddate,DetailForm_pkidx_quotelineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", quote.accountId);
            return View(quote);
        }

        // GET: /CRM/quotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            quote quote = db.quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: /CRM/quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            quote quote = db.quotes.Find(id);
            db.quotes.Remove(quote);
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
