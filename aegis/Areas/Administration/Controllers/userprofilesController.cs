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
//tambahan
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace aegis.Areas.Administration.Controllers
{
    [Authorize]
    public class userprofilesController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public userprofilesController()
        {
        }

        public userprofilesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Administration/userprofiles/
        public ActionResult Index() 
        {
            List<ApplicationUser> listUser = new List<ApplicationUser>();
            List<UserViewModel> fUser = new List<UserViewModel>();
            listUser = db.Users.ToList();

            foreach (var item in listUser)
            {
                UserViewModel f = new UserViewModel();
                f.Email = item.Email;
                f.Id = item.Id;
                fUser.Add(f);
            }
            
            return View(fUser);
        }

        // GET: /Administration/userprofiles/Details/5
        public ActionResult Details(string id)
        {

            ApplicationUser userprofile = db.Users.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // GET: /Administration/userprofiles/Create
        public ActionResult Create()
        {
            return View();
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var db = new ApplicationDbContext();
                    var userroleline = db.userrolelines.Where(s => s.email.Equals(model.Email)).FirstOrDefault();
                    Session["aegis-role"] = userroleline.userrole.rolename;
                    Session["aegis-email"] = model.Email;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "userprofiles");
        }

        //
        // POST: /Administration/userprofiles/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //
        // POST: /Administration/userprofiles/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //destroy all session
            Session.Abandon();
            return RedirectToAction("Index", new { area = "Administration" });
        }

        // GET: /Administration/userprofiles/Edit/5
        public ActionResult Edit(string id)
        {
            
            ApplicationUser userprofile = db.Users.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Administration/userprofiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Email")] ApplicationUser userprofile)
        {
            if (ModelState.IsValid)
            {
                var appusr = db.Users.Find(userprofile.Id);
                appusr.Email = userprofile.Email;
                appusr.UserName = userprofile.Email;
                //db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }



        // GET: /Administration/userprofiles/Reset/5
        public ActionResult Reset(string id)
        {

            ApplicationUser userprofile = db.Users.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }

            UserViewModel uvm = new UserViewModel();
            uvm.Id = userprofile.Id;
            uvm.Email = userprofile.Email;

            return View(uvm);
        }

        // POST: /Administration/userprofiles/Reset/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset([Bind(Include = "Id,NewPassword")] UserViewModel userprofile)
        {
            if (ModelState.IsValid)
            {
                var appusr = db.Users.Find(userprofile.Id);
                string code = UserManager.GeneratePasswordResetToken(userprofile.Id);
                IdentityResult result = UserManager.ResetPassword(userprofile.Id, code, userprofile.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                
            }
            return View(userprofile);
        }




        // GET: /Administration/userprofiles/Delete/5
        public ActionResult Delete(string id)
        {
            
            ApplicationUser userprofile = db.Users.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Administration/userprofiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser userprofile = db.Users.Find(id);
            db.Users.Remove(userprofile);
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
