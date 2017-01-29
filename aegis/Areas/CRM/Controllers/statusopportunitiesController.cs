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
    public class statusopportunitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.statusopportunitys.ToList(), JsonRequestBehavior.AllowGet);
        }






        




        // GET: /CRM/statusopportunities/
        public ActionResult Index()
        {
            return View(db.statusopportunitys.ToList());
        }

        // GET: /CRM/statusopportunities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusopportunity statusopportunity = db.statusopportunitys.Find(id);
            if (statusopportunity == null)
            {
                return HttpNotFound();
            }
            return View(statusopportunity);
        }

        // GET: /CRM/statusopportunities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CRM/statusopportunities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="statusopportunityId,code,name,description")] statusopportunity statusopportunity)
        {
            if (ModelState.IsValid)
            {
                db.statusopportunitys.Add(statusopportunity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusopportunity);
        }

        // GET: /CRM/statusopportunities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusopportunity statusopportunity = db.statusopportunitys.Find(id);
            if (statusopportunity == null)
            {
                return HttpNotFound();
            }
            return View(statusopportunity);
        }

        // POST: /CRM/statusopportunities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="statusopportunityId,code,name,description")] statusopportunity statusopportunity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusopportunity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusopportunity);
        }

        // GET: /CRM/statusopportunities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statusopportunity statusopportunity = db.statusopportunitys.Find(id);
            if (statusopportunity == null)
            {
                return HttpNotFound();
            }
            return View(statusopportunity);
        }

        // POST: /CRM/statusopportunities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            statusopportunity statusopportunity = db.statusopportunitys.Find(id);
            db.statusopportunitys.Remove(statusopportunity);
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
