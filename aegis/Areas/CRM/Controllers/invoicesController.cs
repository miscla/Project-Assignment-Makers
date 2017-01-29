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
    public class invoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.invoices.ToList(), JsonRequestBehavior.AllowGet);
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
            return Json(db.invoicelines.Where(x => x.invoice.invoiceId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, invoiceline line)
        {
                invoice header = db.invoices.Find(Convert.ToInt32(fkidx));
                line.invoice = header;
                db.invoicelines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,invoiceline line)
        {
            if (ModelState.IsValid)
            {
                invoice  header = db.invoices.Find(Convert.ToInt32(fkidx));
                line.invoice  = header;
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
                invoiceline data = db.invoicelines.Find(Convert.ToInt32(key));
                db.invoicelines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/invoices/
        public ActionResult Index()
        {
            var invoices = db.invoices.Include(i => i.account);
            return View(invoices.ToList());
        }

        // GET: /CRM/invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: /CRM/invoices/Create
        public ActionResult Create()
        {
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code");
            return View();
        }

        // POST: /CRM/invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="invoiceId,code,name,description,accountId,invoicedate,billingaddress,DetailForm_pkidx_invoicelineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", invoice.accountId);
            return View(invoice);
        }

        // GET: /CRM/invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", invoice.accountId);
            return View(invoice);
        }

        // POST: /CRM/invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="invoiceId,code,name,description,accountId,invoicedate,billingaddress,DetailForm_pkidx_invoicelineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", invoice.accountId);
            return View(invoice);
        }

        // GET: /CRM/invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: /CRM/invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            invoice invoice = db.invoices.Find(id);
            db.invoices.Remove(invoice);
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
