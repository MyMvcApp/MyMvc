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

namespace MyMvc.Controllers.Controllers
{
    public class ContentController : BaseController
    {
        private AdminUserRepository adminUserRepository;
        public ContentController()
        {
            adminUserRepository = new AdminUserRepository(new MyMvcContext());
        }

        public ActionResult UserManage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserManage(AdminUser user)
        {
            // TODO:数据库的业务逻辑处理
            try
            {
                Result ret = new Result();

                if (user.AdminUserID != 0)
                {
                    // TODO:修改处理
                    adminUserRepository.Update(user);
                }
                else
                {
                    // TODO:添加处理
                    user.AdminPwd = StringHelper.GetMD5Hash("123");// 新增用户默认密码
                    adminUserRepository.Create(user);
                }

                ret.code = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetUserList(string page, string rows)
        {
            Func<IQueryable<AdminUser>, IOrderedQueryable<AdminUser>> orderby
                = new Func<IQueryable<AdminUser>, IOrderedQueryable<AdminUser>>(q => q.OrderByDescending(s => s.AdminUserID));

            return Json(adminUserRepository.GetPagedData(null, orderby));
        }

        [HttpPost]
        public JsonResult DelUserById(int id)
        {
            try
            {
                Result ret = new Result();

                adminUserRepository.Delete(id);
                ret.code = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult UpdatePwd(UpdatePwdParm parm)
        {
            try
            {
                Result ret = new Result();

                if (Session["admin"] != null)
                {
                    string adminName = Session["admin"].ToString();
                    string pwd = StringHelper.GetMD5Hash(parm.AdminPwd);

                    Expression<Func<AdminUser, bool>> filter = null;
                    if (!String.IsNullOrWhiteSpace(adminName))
                        filter = d => d.AdminName.ToUpper().Equals(adminName.ToUpper())
                            && d.AdminPwd.ToUpper().Equals(pwd);

                    IEnumerable<AdminUser> data = adminUserRepository.GetData(filter: filter);
                    if (data !=  null && data.Count() > 0)
                    {
                        AdminUser user = adminUserRepository.GetData(filter: filter).First(m => m.AdminName.Equals(adminName));
                        user.AdminPwd = StringHelper.GetMD5Hash(parm.AdminNewPwd);
                        adminUserRepository.Update(user);
                        ret.code = "success";
                    }
                    else
                    {
                        ret.code = "fail";
                        ret.message = "原始密码不正确";
                    }
                }

                return Json(ret);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
