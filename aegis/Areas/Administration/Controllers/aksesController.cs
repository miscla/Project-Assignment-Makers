using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aegis.Models;

namespace aegis.Areas.Administration.Controllers
{
    [Authorize]
    public class aksesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Administration/akses/
        public async Task<ActionResult> Index()
        {
            return View(await db.aksess.ToListAsync());
        }

        // GET: /Administration/akses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akses akses = await db.aksess.FindAsync(id);
            if (akses == null)
            {
                return HttpNotFound();
            }
            return View(akses);
        }

        // GET: /Administration/akses/Create
        public ActionResult Create()
        {
            AksesForm af = new AksesForm();
            af.userroles = new SelectList(db.userroles.ToList(), "userroleId", "rolename");
            return View(af);
        }

        // POST: /Administration/akses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="aksesId,aksesdescription,userroleId")] AksesForm akses)
        {
            if (ModelState.IsValid)
            {
                akses newakses = new Models.akses();
                newakses.aksesdescription = akses.aksesdescription;

                userrole refuserrole = db.userroles.Find(akses.userroleId);
                newakses.userrole = refuserrole;

                db.aksess.Add(newakses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(akses);
        }

        // POST: /Administration/userroles/CreateLine
        [HttpPost]
        public List<String> CreateLine(AksesLineForm pForms)
        {
            List<String> response = new List<string>();
            response.Add("Suskes dipanggil");

            int fkid = Convert.ToInt32(pForms.aksesId);
            var akses = db.aksess.Find(fkid);

            db.akseslines.RemoveRange(db.akseslines.Where(x => x.akses.aksesId == fkid));

            for (int i = 0; i < pForms.moduls.Length; i++)
            {
                if (!string.IsNullOrEmpty(pForms.moduls[i]))
                {
                    var line = new aksesline();
                    line.akses = akses;

                    var modul = db.moduls.Find(Convert.ToInt32(pForms.moduls[i]));
                    line.modul = modul;

                    db.akseslines.Add(line);
                }

            }

            db.SaveChanges();


            return response;
        }

        // GET: /Administration/akses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akses akses = await db.aksess.FindAsync(id);
            if (akses == null)
            {
                return HttpNotFound();
            }
            return View(akses);
        }

        // POST: /Administration/akses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="aksesId,aksesdescription")] akses akses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(akses).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(akses);
        }

        // GET: /Administration/akses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            akses akses = await db.aksess.FindAsync(id);
            if (akses == null)
            {
                return HttpNotFound();
            }
            return View(akses);
        }

        // POST: /Administration/akses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            akses akses = await db.aksess.FindAsync(id);
            db.aksess.Remove(akses);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /Administration/akses/Moduls
        [HttpGet]

        public JsonResult Moduls()
        {
            List<modul> list = new List<modul>();
            list = db.moduls.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
