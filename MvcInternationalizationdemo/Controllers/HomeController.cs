using MvcInternationalizationdemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcInternationalizationdemo.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            var cultureValidated = CultureHelper.GetImplementedCulture(culture);
            /* Save culture to a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = cultureValidated;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = cultureValidated;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);
            */
            // Set culture to route
            RouteData.Values["culture"] = cultureValidated;
            return RedirectToAction("Index");
        }
    }
}