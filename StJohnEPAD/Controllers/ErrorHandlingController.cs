using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Controllers
{
    public class ErrorHandlingController : Controller
    {
        /// <summary>
        /// By default, ASP.net sends a 401 a code to any unauthorised user, redirecting them to the login page
        /// This isn't an ideal situation, so instead we use this to send them a 403 error
        /// http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-au
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
        {
            protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

    }
}
