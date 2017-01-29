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
    public class stagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET:
        public JsonResult masters()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.stages.ToList(), JsonRequestBehavior.AllowGet);
        }






        




        // GET: /CRM/stages/
        public ActionResult Index()
        {
            var stages = db.stages.Include(s => s.pipe);
            return View(stages.ToList());
        }

        // GET: /CRM/stages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // GET: /CRM/stages/Create
        public ActionResult Create()
        {
            ViewBag.pipeIdList = new SelectList(db.pipes, "pipeId", "code");
            return View();
        }

        // POST: /CRM/stages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="stageId,code,name,description,pipeId")] stage stage)
        {
            if (ModelState.IsValid)
            {
                db.stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pipeIdList = new SelectList(db.pipes, "pipeId", "code", stage.pipeId);
            return View(stage);
        }

        // GET: /CRM/stages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.pipeIdList = new SelectList(db.pipes, "pipeId", "code", stage.pipeId);
            return View(stage);
        }

        // POST: /CRM/stages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="stageId,code,name,description,pipeId")] stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pipeIdList = new SelectList(db.pipes, "pipeId", "code", stage.pipeId);
            return View(stage);
        }

        // GET: /CRM/stages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage stage = db.stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: /CRM/stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stage stage = db.stages.Find(id);
            db.stages.Remove(stage);
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
