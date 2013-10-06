using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvc.Controllers.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }
    }
}
