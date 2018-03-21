using AuthzExample.Helpers;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;

namespace AuthzExample.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
                return new HttpUnauthorizedResult();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType,
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PostLogout()
        {
            return RedirectToAction("Index", "Home");
        }

        [OktaAuthorize]
        public ActionResult Profile() => View();

        [OktaAuthorize(Roles = "Enthusiasts")]
        public ActionResult Enthusiast() => View();

        [OktaAuthorize(Roles = "Admin")]
        public ActionResult Admin() => View();
    }
}