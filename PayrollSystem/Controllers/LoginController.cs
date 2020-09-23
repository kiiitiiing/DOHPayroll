using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using PayrollSystem.Database;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PayrollSystem.Controllers
{
     public class LoginController : Controller
     {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Signin(string returnUrl)
        {
            if(!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
                return RedirectToAction("JobOrder", "Home", new { id = 0 });
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = DTRDatabase.Instance.VerifyUser(model.UserID, model.Password);

                var isValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.ID, false);
                    var authTicket = new FormsAuthenticationTicket
                        (
                            1,
                            user.Firstname + " " + user.Middlename + " " + user.Lastname,
                            DateTime.Now,
                            DateTime.Now.AddHours(3),
                            false,
                            user.Role
                        );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return RedirectToAction("JobOrder", "Home", new { id = 0 });
                }
                else
                {
                    if (user == null)
                    {
                        ModelState.AddModelError("UserID", "User does not exists");
                        ViewBag.Username = "invalid";
                    }
                    else
                    {
                        ModelState.AddModelError("UserID", "Wrong Password");
                        ViewBag.Password = "invalid";
                    }
                }
            }
            ViewBag.Result = false;
            return View(model);
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Signin");
        }

        /*public string TestLogin(string username, string password)
        {
            Employee user = DTRDatabase.Instance.VerifyUser( username, password);

            var passRes = string.IsNullOrEmpty(user.Password) ? "empty" : BCrypt.Net.BCrypt.Verify(password, user.Password).ToString();


            return user.ID+" || "+ user.Password + "||" + passRes;
        }*/

        #region HELPERS
        /*private async Task LoginAsync(Employee user, bool rememberMe)
        {
            var properties = new AuthenticationProperties
            {
                AllowRefresh = false,
                IsPersistent = rememberMe
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID),
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.GivenName, user.Middlename),
                new Claim(ClaimTypes.Surname, user.Lastname),
            };

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
           // var principal = new ClaimsPrincipal(identity);
            //await HttpContext.(principal, properties);
        }*/
        #endregion
    }
}