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
    public class organizationtypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.organizationtypes.ToList(), JsonRequestBehavior.AllowGet);
        }






        




        // GET: /CRM/organizationtypes/
        public ActionResult Index()
        {
            return View(db.organizationtypes.ToList());
        }

        // GET: /CRM/organizationtypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organizationtype organizationtype = db.organizationtypes.Find(id);
            if (organizationtype == null)
            {
                return HttpNotFound();
            }
            return View(organizationtype);
        }

        // GET: /CRM/organizationtypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CRM/organizationtypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="organizationtypeId,code,name,description")] organizationtype organizationtype)
        {
            if (ModelState.IsValid)
            {
                db.organizationtypes.Add(organizationtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organizationtype);
        }

        // GET: /CRM/organizationtypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organizationtype organizationtype = db.organizationtypes.Find(id);
            if (organizationtype == null)
            {
                return HttpNotFound();
            }
            return View(organizationtype);
        }

        // POST: /CRM/organizationtypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="organizationtypeId,code,name,description")] organizationtype organizationtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizationtype);
        }

        // GET: /CRM/organizationtypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organizationtype organizationtype = db.organizationtypes.Find(id);
            if (organizationtype == null)
            {
                return HttpNotFound();
            }
            return View(organizationtype);
        }

        // POST: /CRM/organizationtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            organizationtype organizationtype = db.organizationtypes.Find(id);
            db.organizationtypes.Remove(organizationtype);
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
