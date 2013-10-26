using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyMvc.Models.ModelsEnd;
using MyMvc.Helper;
using MyMvc.ControllerTemplate;
using MyMvc.IRepository;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
namespace MyMvc.Controllers.Common
{
    public class BaseEndCRUDController<TEntity, TContext> : BaseEndController, IControllerEndCRUD<TEntity, TContext>
        where TEntity:MyMvc.Models.BaseModel
        where TContext : DbContext
    {
        public IBaseRepository<TEntity, TContext> BaseReposity { get; set; }

        [HttpPost]
        public virtual JsonResult CreateOrUpdate(TEntity T) 
        {
            ResponseResult ret = new ResponseResult();
            if (!ModelState.IsValid) return Json(Validate());
            try
            {
                if (T.ID != 0)
                {
                    BaseReposity.Update(T);
                }
                else
                {
                    BaseReposity.Create(T);
                }
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    ret.ErroeCode = "RowVersionError";
                    ret.Message = "数据已修改请刷新后重试！";
                }
                else {
                    if (ex.Message.IndexOf("Value cannot be null.") > -1)
                    {
                        ret.ErroeCode = "RowVersionError";
                        ret.Message = "数据已修改请刷新后重试！";
                    }
                    else
                    {
                        ret.ErroeCode = "error";
                        ret.Message = ex.Message;
                    }
                }
                return Json(ret);
            }
        }

        [HttpPost]
        public virtual JsonResult Create(TEntity T)
        {
            ResponseResult ret = new ResponseResult();
            if (!ModelState.IsValid) return Json(Validate());
            try
            {
                BaseReposity.Create(T);
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    ret.ErroeCode = "RowVersionError";
                    ret.Message = "数据已修改请刷新后重试！";
                }
                else
                {
                    if (ex.Message.IndexOf("Value cannot be null.") > -1)
                    {
                        ret.ErroeCode = "RowVersionError";
                        ret.Message = "数据已修改请刷新后重试！";
                    }
                    else
                    {
                        ret.ErroeCode = "error";
                        ret.Message = ex.Message;
                    }
                }
                return Json(ret);
            }
        }

        [HttpPost]
        public virtual JsonResult Update(TEntity T)
        {
            ResponseResult ret = new ResponseResult();
            if (!ModelState.IsValid) return Json(Validate());
            try
            {
                BaseReposity.Create(T);
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    ret.ErroeCode = "RowVersionError";
                    ret.Message = "数据已修改请刷新后重试！";
                }
                else
                {
                    if (ex.Message.IndexOf("Value cannot be null.") > -1)
                    {
                        ret.ErroeCode = "RowVersionError";
                        ret.Message = "数据已修改请刷新后重试！";
                    }
                    else
                    {
                        ret.ErroeCode = "error";
                        ret.Message = ex.Message;
                    }
                }
                return Json(ret);
            }
        }

        [HttpPost]
        public virtual JsonResult Delete(TEntity T) 
        {
            ResponseResult ret = new ResponseResult();
            try
            {
                BaseReposity.Delete(T.ID);
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    ret.ErroeCode = "RowVersionError";
                    ret.Message = "数据已修改请刷新后重试！";
                }
                else
                {
                    if (ex.Message.IndexOf("Value cannot be null.") > -1)
                    {
                        ret.ErroeCode = "RowVersionError";
                        ret.Message = "数据已修改请刷新后重试！";
                    }
                    else {
                        ret.ErroeCode = "error";
                        ret.Message = ex.Message;
                    }
                }
                return Json(ret);
            }
        }

        [HttpPost]
        public virtual JsonResult GetDataList(string page, string rows) 
        {
            try
            {
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby
                       = new Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>(q => q.OrderBy(s => s.ID));
                return BaseReposity.GetPagedJsonData(orderBy: orderby, pageSize: Convert.ToInt32(rows), pageNumber: Convert.ToInt32(page));
            }
            catch (Exception ex) 
            {
                return Json(new ResponseResult { ErroeCode="error",Message=ex.Message},JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual JsonResult GetDataListByID(string page, string rows, string id)
        {
            try
            {
                Expression<Func<TEntity, bool>> filter = null;
                if (!string.IsNullOrWhiteSpace(id) && id != "null")
                {
                    filter = d => d.ID.Equals(Convert.ToInt32(id));
                }
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby
                       = new Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>(q => q.OrderBy(s => s.ID));
                return BaseReposity.GetPagedJsonData(filter:filter, orderBy: orderby, pageSize: Convert.ToInt32(rows), pageNumber: Convert.ToInt32(page));
             }
            catch (Exception ex) 
            {
                return Json(new ResponseResult { ErroeCode="error",Message=ex.Message},JsonRequestBehavior.AllowGet);
            }
        }
    }
}
