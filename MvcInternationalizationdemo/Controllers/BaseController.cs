#region using
using MvcInternationalizationdemo.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
#endregion

namespace MvcInternationalizationdemo.Controllers
{
    /// <summary>
    /// Ref: http://afana.me/post/aspnet-mvc-internationalization.aspx
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            /* Read culture from Cookie
             string cultureName = null;
            var cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                // Retrieve culture from HTTP Accept-Language header
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                              ? Request.UserLanguages.First() : null;
            }
            */

            // Read culture from Route
            var cultureName = RouteData.Values["culture"] as string;
            if(string.IsNullOrWhiteSpace(cultureName))
            {
                // Retrieve culture from HTTP Accept-Language header
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                              ? Request.UserLanguages.First() : null;
            }

            // Validate culture name
            var cultureNameValidated = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            if (cultureNameValidated != cultureName)
            {
                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureNameValidated.ToLowerInvariant(); // lower case too
                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            // Modify current thread's cultures    
            var ci = new System.Globalization.CultureInfo(cultureNameValidated);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            return base.BeginExecuteCore(callback, state);
        }
    }
}