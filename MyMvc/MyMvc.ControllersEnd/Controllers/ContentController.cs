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
using MyMvc.IRepository.RepositoryEnd;
namespace MyMvc.ControllersEnd.Controllers
{
    public class ContentController : BaseEndCRUDController<AdminUser, MyMvcContext>
    {
        private IAdminUserRepository adminUserRepository;
        public ContentController()
        {
            adminUserRepository = new AdminUserRepository(new MyMvcContext());
            BaseReposity = adminUserRepository;
        }

        public ActionResult UserManage()
        {
            return View();
        }

        [HttpPost]
        public override JsonResult CreateOrUpdate(AdminUser user)
        {
            ResponseResult ret = new ResponseResult();
            try
            {
                if (user.ID != 0)
                {
                    // TODO:修改处理
                    adminUserRepository.Update(user);
                }
                else
                {
                    // TODO:添加处理
                    user.AdminPwd = WebHelper.GetMD5Hash("123");// 新增用户默认密码
                    adminUserRepository.Create(user);
                }

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

        [HttpPost]
        public JsonResult UpdatePwd(UpdatePwdParm parm)
        {
            try
            {
                ResponseResult ret = new ResponseResult();

                if (Session["admin"] != null)
                {
                    string adminName = Session["admin"].ToString();
                    string pwd = WebHelper.GetMD5Hash(parm.AdminPwd);

                    Expression<Func<AdminUser, bool>> filter = null;
                    if (!String.IsNullOrWhiteSpace(adminName))
                        filter = d => d.AdminName.ToUpper().Equals(adminName.ToUpper())
                            && d.AdminPwd.ToUpper().Equals(pwd);

                    IEnumerable<AdminUser> data = adminUserRepository.GetData(filter: filter);
                    if (data !=  null && data.Count() > 0)
                    {
                        AdminUser user = data.FirstOrDefault();
                        user.AdminPwd = WebHelper.GetMD5Hash(parm.AdminNewPwd);
                        adminUserRepository.Update(user);
                        ret.Status = "success";
                    }
                    else
                    {
                        ret.Status = "fail";
                        ret.Message = "原始密码不正确";
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
