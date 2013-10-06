using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyMvc.Models.ModelsEnd;

namespace MyMvc.Controllers.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.UserName.Equals("admin") && model.Password.Equals("123"))
            {
                string md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(model.UserName, "MD5");
                Session.Add(model.UserName, md5);
                return RedirectToAction("Index", "Home");
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ModelState.AddModelError("", "提供的用户名或密码不正确。");
            return View(model);
        }
    }
}
