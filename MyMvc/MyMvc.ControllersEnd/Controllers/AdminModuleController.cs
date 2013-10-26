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
using MyMvc.Models.ModelsEnd;
using MyMvc.Repository.RepositoryEnd;
using System.Web.Security;
using MyMvc.Helper;
using MyMvc.Controllers.Common;
using MyMvc.IRepository.RepositoryEnd;
namespace MyMvc.ControllersEnd.Controllers
{
    public class AdminModuleController : BaseEndCRUDController<AdminModule, MyMvcContext>
    {
        private IAdminModuleRepository adminModuleRepository;

        public AdminModuleController() 
        {
            adminModuleRepository = new AdminModuleRepository(new MyMvcContext());
            BaseReposity = adminModuleRepository;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
