using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using sp18Team7Final.DAL;
using System.Net.Mail;
using System.Net;
using sp18Team7Final.Models;
using System.Collections.Generic;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.EntityFramework;

namespace sp18Team7Final.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        private AppDbContext db = new AppDbContext();

        public class EmailMessaging
        {
            public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("team7sp18@utexas.edu", "abc.1234"),
                    EnableSsl = true
                };

                String finalMessage = emailBody + "\n\n Thank you for choosing Longhorn Cinema!";

                MailAddress senderEmail = new MailAddress("team7sp18@utexas.edu", "Team 7: Longhorn Cinema");

                MailMessage mm = new MailMessage();
                mm.Subject = "Team 7 - " + emailSubject;
                mm.Sender = senderEmail;
                mm.From = senderEmail;
                mm.To.Add(new MailAddress(toEmailAddress));
                mm.Body = finalMessage;
                client.Send(mm);
            }
        }

        public AccountsController()
        {
        }

        //NOTE: This creates a user manager and a sign-in manager every time someone creates a request to this controller
        public AccountsController(AppUserManager userManager, ApplicationSignInManager signInManager )
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

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // NOTE:  This is the logic for the login page
        // GET: /Accounts/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            AuthenticationManager.SignOut(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        //
        // POST: /Accounts/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            String email = model.Email;
            AppUser i = db.Users.FirstOrDefault(x => x.Email == email);
            var user = await UserManager.FindAsync(model.Email, model.Password);
            var roles = await UserManager.GetRolesAsync(user.Id);
            if (roles.Contains("Fired") == true)
            {
                TempData["msg"] = "<script>alert('You are no longer an employee, please register an an account with a different email address in order to use the site as a customer :)');</script>";
                return Redirect(Request.UrlReferrer.ToString());
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

       
        //
        // GET: /Accounts/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // NOTE: Here is your logic for registering a new user
        // POST: /Accounts/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add fields to user here so they will be saved to do the database
                bool date = (model.Birthday.AddYears(13) < DateTime.Today);
                if (date == false)
                {
                    TempData["msg"] = "<script>alert('You must be at least 13 years old to make an account.');</script>";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                if(User.IsInRole("Manager") == true)
                {
                    bool employeedate = (model.Birthday.AddYears(18) < DateTime.Today);
                    if (employeedate == false)
                    {
                        TempData["msg"] = "<script>alert('Employees must be at least 18 years old.');</script>";
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                var user = new AppUser {
                    UserName = model.Email,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PopcornPoints = 0,
                                     
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO:  Once you get roles working, you may want to add users to roles upon creation
                //UserManager.AddToRole(user.Id, "Customer");
                if(User.IsInRole("Manager") == true)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Employee");
                }
                else
                {
                    await UserManager.AddToRoleAsync(user.Id, "Customer");
                }


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    EmailMessaging.SendEmail(model.Email, "Thank you for creating an account!", "now keep working");
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET: Accounts/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.GetUserId();
            AppUser user = db.Users.Find(id);

            //populate the view model
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.PopcornPoints = user.PopcornPoints;
            ivm.Name = user.FirstName + " " + user.LastName;
            ivm.Address = user.Address;
            ivm.Birthday = user.Birthday;
            ivm.PhoneNumber = user.PhoneNumber;
            return View(ivm);
        }
        [Authorize(Roles = "Employee, Manager")]
        public ActionResult ManageCustomers()
        {
            String currentuserID = User.Identity.GetUserId();
            AppUser currentuser = db.Users.Find(currentuserID);
            bool manager = currentuser.Roles.Any(x => x.RoleId == "96bcfd56-7522-4c8b-8d1e-b635bb886f90");
            var query = from userp in db.Users select userp;
            UserManager.IsInRole(currentuserID, "Manager");
            List<AppUser> customers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("c4623b95-e149-4ae8-b65f-7850b8af8cbb")).ToList();
            List<AppUser> allusers = new List<AppUser>();
            if (manager == true)
            {
                List<AppUser> employees = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("bbd4bb79-697f-4ce3-83e2-b27f8da82788")).ToList();
                allusers = customers.Concat(employees).ToList();
            }

            return View(allusers);
        }

        public ActionResult Edit(String id)
        {
            AppUser thisuser = db.Users.Find(id);
            return View(thisuser);
        }

        [Authorize(Roles = "Employee, Manager")]
        public ActionResult ChangeCustomerPassword(String id)
        {
            AppUser thisuser = db.Users.Find(id);
            ViewBag.UserID = id;
            ChangeCustomerPasswordViewModel ivm = new ChangeCustomerPasswordViewModel();
            ivm.UserID = id;
            return View(ivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeCustomerPassword(ChangeCustomerPasswordViewModel model, String id)
        {
            String userid = model.UserID;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            String userID = model.UserID;
            UserManager.RemovePassword(id);
            UserManager.AddPassword(id, model.NewPassword);
            return RedirectToAction("ManageCustomers", "Accounts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Address,Birthday,PhoneNumber,UserName,Id,FirstName,LastName,Email,PopcornPoints")] AppUser user)
        {
            if(ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCustomers");
            }
            return View(user);
        }

        //Logic for change password
        // GET: /Accounts/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangeAddress()
        {
            return View();
        }

        //
        // POST: /Accounts/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAddress(ChangeAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            String user;
            user = HttpContext.User.Identity.GetUserName();
            AppUser f = db.Users.FirstOrDefault(x => x.UserName == user);
            f.Address = model.NewAddress;
            db.SaveChanges();
            return RedirectToAction("Index", "Accounts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePhoneNumber(ChangePhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            String user;
            user = HttpContext.User.Identity.GetUserName();
            AppUser f = db.Users.FirstOrDefault(x => x.UserName == user);
            f.PhoneNumber = model.NewPhoneNumber;
            db.SaveChanges();
            return RedirectToAction("Index", "Accounts");
        }

        //

        // POST: /Accounts/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region 

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}