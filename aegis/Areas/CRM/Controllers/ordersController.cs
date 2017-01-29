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
    public class ordersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.orders.ToList(), JsonRequestBehavior.AllowGet);
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
            return Json(db.orderlines.Where(x => x.order.orderId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        
        // POST
        [HttpPost]
        public String LineCreate(string fkidx, orderline line)
        {
                order header = db.orders.Find(Convert.ToInt32(fkidx));
                line.order = header;
                db.orderlines.Add(line);
                db.SaveChanges();
                return "";
        }


        
        // POST
        [HttpPost]
        public String LineEdit(string fkidx,orderline line)
        {
            if (ModelState.IsValid)
            {
                order  header = db.orders.Find(Convert.ToInt32(fkidx));
                line.order  = header;
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
                orderline data = db.orderlines.Find(Convert.ToInt32(key));
                db.orderlines.Remove(data);
                db.SaveChanges();
                return "";
            }

            return "";
        }





        




        // GET: /CRM/orders/
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.account);
            return View(orders.ToList());
        }

        // GET: /CRM/orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /CRM/orders/Create
        public ActionResult Create()
        {
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code");
            return View();
        }

        // POST: /CRM/orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="orderId,code,name,description,accountId,orderdate,shipdate,address,shipaddress,DetailForm_pkidx_orderlineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", order.accountId);
            return View(order);
        }

        // GET: /CRM/orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", order.accountId);
            return View(order);
        }

        // POST: /CRM/orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="orderId,code,name,description,accountId,orderdate,shipdate,address,shipaddress,DetailForm_pkidx_orderlineId,DetailForm_ddl_productId,DetailForm_qty,DetailForm_unitprice")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accountIdList = new SelectList(db.accounts, "accountId", "code", order.accountId);
            return View(order);
        }

        // GET: /CRM/orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /CRM/orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
