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
    public class organizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.organizations.ToList(), JsonRequestBehavior.AllowGet);
        }






        




        // GET: /CRM/organizations/
        public ActionResult Index()
        {
            var organizations = db.organizations.Include(o => o.organizationtype);
            return View(organizations.ToList());
        }

        // GET: /CRM/organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organization organization = db.organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // GET: /CRM/organizations/Create
        public ActionResult Create()
        {
            ViewBag.organizationtypeIdList = new SelectList(db.organizationtypes, "organizationtypeId", "code");
            return View();
        }

        // POST: /CRM/organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="organizationId,code,name,description,fulladdress,phone,email,website,organizationtypeId")] organization organization)
        {
            if (ModelState.IsValid)
            {
                db.organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.organizationtypeIdList = new SelectList(db.organizationtypes, "organizationtypeId", "code", organization.organizationtypeId);
            return View(organization);
        }

        // GET: /CRM/organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organization organization = db.organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.organizationtypeIdList = new SelectList(db.organizationtypes, "organizationtypeId", "code", organization.organizationtypeId);
            return View(organization);
        }

        // POST: /CRM/organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="organizationId,code,name,description,fulladdress,phone,email,website,organizationtypeId")] organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.organizationtypeIdList = new SelectList(db.organizationtypes, "organizationtypeId", "code", organization.organizationtypeId);
            return View(organization);
        }

        // GET: /CRM/organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            organization organization = db.organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: /CRM/organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            organization organization = db.organizations.Find(id);
            db.organizations.Remove(organization);
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
