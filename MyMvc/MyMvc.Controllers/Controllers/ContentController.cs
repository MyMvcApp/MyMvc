using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvc.Models.ModelsEnd;

namespace MyMvc.Controllers.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult UserManage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserManage(UserModel user)
        {
            // TODO:数据库的业务逻辑处理
            if (user.userid != 0)
            {
                // TODO:修改处理

            }
            else
            {
                // TODO:添加处理
                
            }

            return Json(1);
        }

    }
}
