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
    public class userrolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Administration/userroles/
        public async Task<ActionResult> Index()
        {
            return View(await db.userroles.ToListAsync());
        }

        // GET: /Administration/userroles/Users
        [HttpGet]
        
        public JsonResult Users()
        {
            List<ApplicationUser> listUser = new List<ApplicationUser>();
            listUser = db.Users.ToList();
            return Json(listUser, JsonRequestBehavior.AllowGet);
        }

        // POST: /Administration/userroles/CreateLine
        [HttpPost]
        public List<String> CreateLine(UserRoleLineForm pForms)
        {
            List<String> response = new List<string>();
            response.Add("Suskes dipanggil");

            int fkid = Convert.ToInt32(pForms.userroleId);
            var userrole = db.userroles.Find(fkid);

            db.userrolelines.RemoveRange(db.userrolelines.Where(x => x.userrole.userroleId == fkid));

            for (int i = 0; i < pForms.users.Length; i++)
            {
                if (!string.IsNullOrEmpty(pForms.users[i]))
                {
                    var userroleline = new userroleline();
                    userroleline.userrole = userrole;

                    var userapp = db.Users.Find(pForms.users[i]);
                    userroleline.applicationuser = userapp;
                    userroleline.email = userapp.Email;

                    db.userrolelines.Add(userroleline);
                }

            }

            db.SaveChanges();


            return response;
        }

        // GET: /Administration/userroles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userrole userrole = await db.userroles.FindAsync(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // GET: /Administration/userroles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Administration/userroles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="userroleId,rolename,roledescription")] userrole userrole)
        {
            if (ModelState.IsValid)
            {
                db.userroles.Add(userrole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userrole);
        }

        // GET: /Administration/userroles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userrole userrole = await db.userroles.FindAsync(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /Administration/userroles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="userroleId,rolename,roledescription")] userrole userrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userrole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userrole);
        }

        // GET: /Administration/userroles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userrole userrole = await db.userroles.FindAsync(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /Administration/userroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            userrole userrole = await db.userroles.FindAsync(id);
            db.userroles.Remove(userrole);
            await db.SaveChangesAsync();
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
