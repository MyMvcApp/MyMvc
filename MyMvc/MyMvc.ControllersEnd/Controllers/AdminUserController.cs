using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyMvc.ControllersEnd;
using MyMvc.ModelsEnd;
using MyMvc.Repository;
namespace MyMvc.Controllers.ControllersEnd
{
    public class AdminUserController:BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
