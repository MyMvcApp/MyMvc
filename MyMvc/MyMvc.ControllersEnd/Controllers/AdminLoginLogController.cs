using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MyMvc.Context;
using MyMvc.Helper;
using MyMvc.Models.ModelsEnd;
using MyMvc.Repository.RepositoryEnd;
using MyMvc.Controllers.Common;
using PagedList;
using MyMvc.IRepository.RepositoryEnd;
namespace MyMvc.ControllersEnd.Controllers
{
    public class AdminLoginLogController : BaseEndCRUDController<AdminLoginLog,MyMvcContext>
    {
        private IAdminLoginLogRepository adminLoginLogRepository;

        public AdminLoginLogController()
        {
            adminLoginLogRepository = new AdminLoginLogRepository(new MyMvcContext());
            BaseReposity = adminLoginLogRepository;
        }

        [HttpPost]
        public override JsonResult GetDataListByID(string page, string rows, string id)
        {
            Expression<Func<AdminLoginLog, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(id) && id != "null")
            {
                int AdminUserID = Convert.ToInt32(id);
                filter = d => d.AdminUserID.Equals(AdminUserID);
            }
            Func<IQueryable<AdminLoginLog>, IOrderedQueryable<AdminLoginLog>> orderby
                = new Func<IQueryable<AdminLoginLog>, IOrderedQueryable<AdminLoginLog>>(q => q.OrderByDescending(s => s.AdminUserID));
            IPagedList<AdminLoginLog> data = adminLoginLogRepository.GetPagedData(filter: filter, orderBy: orderby, pageSize: Convert.ToInt32(rows), pageNumber: Convert.ToInt32(page), includeProperties: "AdminUser");
            var response = new
            {
                total = data.TotalItemCount,
                rows = data.ToList().Select(p => new
                {
                    ID = p.ID,
                    AdminUserID = p.AdminUserID,
                    AdminName = p.AdminUser.AdminName,
                    AdminLoginTime = p.AdminLoginTime,
                    AdminLoginIP = p.AdminLoginIP
                })
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
