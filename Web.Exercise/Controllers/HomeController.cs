using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Exercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Auth", new { area = "UserOp" });
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

        /// <summary>
        /// 404页面
        /// </summary>
        /// <param name="aspxerrprpath"></param>
        /// <returns></returns>
        public ActionResult Error404(string aspxerrprpath = "Error")
        {
            return View("~/Views/Home/Error404.cshtml", null, aspxerrprpath);
        }
    }
}