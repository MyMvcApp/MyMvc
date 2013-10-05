using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using MyMvc.Models.Models;
using MyMvc.Repository;
using MyMvc.Context;
using MyMvc.ControllerTemplate;
using System.Linq.Expressions;
using PagedList;
namespace MyMvc.Controllers.Controllers
{
    public class AdminUserController:BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
