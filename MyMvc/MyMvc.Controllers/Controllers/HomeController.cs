using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Controllers.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (Session["admin"] != null)
                {
                    ViewBag.LoginName = Session["admin"];
                    return View();
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Top()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Bottom()
        {
            return View();
        }
    }
}
