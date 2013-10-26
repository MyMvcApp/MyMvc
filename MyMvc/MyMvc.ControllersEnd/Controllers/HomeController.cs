using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvc.Controllers.Common;
using Newtonsoft.Json;
namespace MyMvc.ControllersEnd.Controllers
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
                    ViewBag.User = JsonConvert.SerializeObject(new { AdminUserID = CurrentEndUser.ID,
                                                                     AdminName = CurrentEndUser.AdminName,
                                                                     RealName = CurrentEndUser.RealName,
                                                                     AdminType = CurrentEndUser.AdminType
                    });
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
