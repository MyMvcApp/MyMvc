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
using Newtonsoft.Json;
namespace MyMvc.ControllersEnd.Controllers
{
    public class AdminAuthorityController : BaseEndCRUDController<AdminAuthority,MyMvcContext>
    {
        private IAdminAuthorityRepository adminAuthorityRepository;
        private IAdminModuleRepository adminModuleRepository;
        private IAdminUserRepository adminUserRepository;

        public AdminAuthorityController() 
        {
            MyMvcContext context = new MyMvcContext();
            adminAuthorityRepository = new AdminAuthorityRepository(context);
            adminModuleRepository = new AdminModuleRepository(context);
            adminUserRepository = new AdminUserRepository(context);
            BaseReposity = adminAuthorityRepository;
        }

        [HttpPost]
        public override JsonResult GetDataListByID(string page, string rows, string id)
        {
            Expression<Func<AdminAuthority, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(id) && id != "null")
            {
                int AdminModuleID = Convert.ToInt32(id);
                filter = d => d.AdminModuleID.Equals(AdminModuleID);
            }
            Func<IQueryable<AdminAuthority>, IOrderedQueryable<AdminAuthority>> orderby
                = new Func<IQueryable<AdminAuthority>, IOrderedQueryable<AdminAuthority>>(q => q.OrderByDescending(s => s.ID));
            IPagedList<AdminAuthority> data = adminAuthorityRepository.GetPagedData(filter: filter, orderBy: orderby, pageSize: Convert.ToInt32(rows), pageNumber: Convert.ToInt32(page), includeProperties: "AdminModule");
            //处理一对多的情况需要用Select对查询出来的结果进行处理，不然序列化JSON的时候会报出A circular reference was detected while serializing an object of type这种错误！
            var response = new
            {
                total = data.TotalItemCount,
                rows = data.ToList().Select(p => new
                {
                    ID = p.ID,
                    AdminAuthorityName = p.AdminAuthorityName,
                    AdminAuthorityUrl = p.AdminAuthorityUrl,
                    AdminAuthorityOrder = p.AdminAuthorityOrder,
                    AdminModuleID = p.AdminModuleID,
                    AdminModuleName = p.AdminModule.AdminModuleName
                })
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取AdminUserID对应的所有权限
        /// </summary>
        /// <param name="AdminUserID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAdminUserAuthorityList(string AdminUserID)
        {
            Expression<Func<AdminUser, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(AdminUserID) && AdminUserID != "null")
            {
                int AdminUserIDs = Convert.ToInt32(AdminUserID);
                filter = d => d.ID.Equals(AdminUserIDs);
            }
            Func<IQueryable<AdminUser>, IOrderedQueryable<AdminUser>> orderby
                = new Func<IQueryable<AdminUser>, IOrderedQueryable<AdminUser>>(q => q.OrderByDescending(s => s.AdminAuthoritys.OrderByDescending(t => t.ID)));
            var data = adminUserRepository.GetData(filter: filter, includeProperties: "AdminAuthoritys");
            
            //处理一对多的情况需要用Select对查询出来的结果进行处理，不然序列化JSON的时候会报出A circular reference was detected while serializing an object of type这种错误！
            AdminUser currentAdminUser = data.FirstOrDefault();
            if (currentAdminUser == null) return Json(new { }, JsonRequestBehavior.AllowGet);
            var response = new
            {
                rows = currentAdminUser.AdminAuthoritys.Select(p => new
                {
                    ID = p.ID,
                    AdminAuthorityName = p.AdminAuthorityName,
                    AdminAuthorityUrl = p.AdminAuthorityUrl,
                    AdminAuthorityOrder = p.AdminAuthorityOrder,
                    AdminModuleID = p.AdminModuleID,
                    AdminModuleName = adminModuleRepository.GetByID(p.AdminModuleID).AdminModuleName
                })
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取登录用户的权限
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLoginAuthorityList(AdminUser adminUser) 
        {
            return GetAdminUserAuthorityList(adminUser.ID.ToString());
        }

        /// <summary>
        /// 获取AdminUserID对应的所有权限
        /// </summary>
        /// <param name="AdminUserID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAllAuthority()
        {
            Func<IQueryable<AdminAuthority>, IOrderedQueryable<AdminAuthority>> orderby
                = new Func<IQueryable<AdminAuthority>, IOrderedQueryable<AdminAuthority>>(q => q.OrderByDescending(s => s.ID));
            var data = adminAuthorityRepository.GetData( orderBy: orderby,includeProperties: "AdminModule");
            //处理一对多的情况需要用Select对查询出来的结果进行处理，不然序列化JSON的时候会报出A circular reference was detected while serializing an object of type这种错误！
            var response = new
            {
                rows = data.ToList().Select(p => new
                {
                    ID = p.ID,
                    AdminAuthorityName = p.AdminAuthorityName,
                    AdminAuthorityUrl = p.AdminAuthorityUrl,
                    AdminAuthorityOrder = p.AdminAuthorityOrder,
                    AdminModuleID = p.AdminModuleID,
                    AdminModuleName = p.AdminModule.AdminModuleName
                })
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeliverAuthority(AdminUser adminUser) 
        {
            ResponseResult ret = new ResponseResult();
            try
            {
                Expression<Func<AdminUser, bool>> filter = d => d.ID.Equals(adminUser.ID);
                var oldAdminUser = adminUserRepository.GetData(filter: filter, includeProperties: "AdminAuthoritys").FirstOrDefault();
                var selectedAdminAuthority = new HashSet<int>(adminUser.AdminAuthoritys.Select(p => p.ID));
                var oldAdminAuthority = new HashSet<int>(oldAdminUser.AdminAuthoritys.Select(p => p.ID));
                var allAdminAuthority = adminAuthorityRepository.GetData();
                foreach(var authority in allAdminAuthority)
                {
                    if (selectedAdminAuthority.Contains(authority.ID))
                    {
                        if (!oldAdminAuthority.Contains(authority.ID))
                        {
                            oldAdminUser.AdminAuthoritys.Add(authority);
                        }
                    }
                    else 
                    {
                        if (oldAdminAuthority.Contains(authority.ID)) 
                        {
                            oldAdminUser.AdminAuthoritys.Remove(authority);
                        }
                    }
                }
                adminUserRepository.Update(oldAdminUser);
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                ret.ErroeCode = "error";
                ret.Message = ex.Message;
                return Json(ret);
            }
        }
      
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.AdminModuleID = new SelectList(adminModuleRepository.GetData(), "ID", "AdminModuleName");
            return View();
        }

         [HttpGet]
        public ActionResult AdminUserAuthority() 
        {
            return View();
        }
    }
}
