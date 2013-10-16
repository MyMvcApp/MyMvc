﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using MyMvc.Models.Models;
using MyMvc.Repository.Repository;
using MyMvc.Context;
using MyMvc.ControllerTemplate;
using System.Linq.Expressions;
using PagedList;
using MyMvc.Helper;
namespace MyMvc.Controllers.Common
{
    public class PagedPeoPleController : BaseController, IControlerTemplatePaged<PagedPeoPle>
    {
       private PagedPeoPleRepository pagedPeoPleRepository;
       public PagedPeoPleController() 
       {
           pagedPeoPleRepository = new PagedPeoPleRepository(new MyMvcContext());
       }

       public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int pageSize=1)
       {
           ViewBag.CurrentSort = sortOrder;
           ViewBag.AgeSortParm = String.IsNullOrEmpty(sortOrder) ? "Age desc" : "";
           if (Request.HttpMethod == "GET")
           {
               searchString = currentFilter;
           }
           else
           {
               page = 1;
           }
           ViewBag.CurrentFilter = searchString;
           Func<IQueryable<PagedPeoPle>, IOrderedQueryable<PagedPeoPle>> orderBy = null;
           switch (sortOrder)
           {
               case "Age desc":
                   orderBy = new Func<IQueryable<PagedPeoPle>, IOrderedQueryable<PagedPeoPle>>(q => q.OrderByDescending(s => s.Age));
                   break;
               default:
                   orderBy = new Func<IQueryable<PagedPeoPle>, IOrderedQueryable<PagedPeoPle>>(q => q.OrderBy(s => s.Age));
                   break;
           }
           int pageNumber = (page ?? 1);
           Expression<Func<PagedPeoPle, bool>> filter = null;
           if (!String.IsNullOrWhiteSpace(searchString))
               filter = d => d.Name.ToUpper().Contains(searchString.ToUpper());
           IPagedList<PagedPeoPle> PagedPeoPles = pagedPeoPleRepository.GetPagedData(
             filter: filter,
             orderBy: orderBy,
             pageSize: pageSize,
             pageNumber: pageNumber);
           return View(PagedPeoPles);
       }

       //
       // GET: /PagedPeoPle/Details/5

       public ActionResult Details(int id = 0)
       {
           PagedPeoPle pagedPeoPle = pagedPeoPleRepository.GetByID(id);
           if (pagedPeoPle == null)
           {
               return HttpNotFound();
           }
           return View(pagedPeoPle);
       }

       //
       // GET: /PagedPeoPle/Create

       public ActionResult Create()
       {
           return View();
       }

       //
       // POST: /PagedPeoPle/Create

       [HttpPost]
       public ActionResult Create(PagedPeoPle pagedPeoPle)
       {
           if (ModelState.IsValid)
           {
               pagedPeoPleRepository.Create(pagedPeoPle);
               return RedirectToAction("Index");
           }
           return View(pagedPeoPle);
       }

       //
       // GET: /PagedPeoPle/Edit/5

       public ActionResult Edit(int id = 0)
       {
           PagedPeoPle pagedPeoPle = pagedPeoPleRepository.GetByID(id);
           if (pagedPeoPle == null)
           {
               return HttpNotFound();
           }
           return View(pagedPeoPle);
       }

       //
       // POST: /PagedPeoPle/Edit/5

       [HttpPost]
       public ActionResult Edit(PagedPeoPle pagedPeoPle)
       {
           if (ModelState.IsValid)
           {
               pagedPeoPleRepository.Update(pagedPeoPle);
               return RedirectToAction("Index");
           }
           return View(pagedPeoPle);
       }

       //
       // GET: /PagedPeoPle/Delete/5

       public ActionResult Delete(int id = 0)
       {
           PagedPeoPle pagedPeoPle = pagedPeoPleRepository.GetByID(id);
           if (pagedPeoPle == null)
           {
               return HttpNotFound();
           }
           return View(pagedPeoPle);
       }

       //
       // POST: /PagedPeoPle/Delete/5

       [HttpPost, ActionName("Delete")]
       public ActionResult DeleteConfirmed(int id)
       {
           PagedPeoPle pagedPeoPle = pagedPeoPleRepository.GetByID(id);
           pagedPeoPleRepository.Delete(pagedPeoPle);
           return RedirectToAction("Index");
       }

       protected override void Dispose(bool disposing)
       {
           pagedPeoPleRepository.Dispose();
           base.Dispose(disposing);
       }

        [HttpPost]
       public JsonResult PagedPeoPleManage(PagedPeoPle pagedPeoPle) 
       {
           ResponseResult ret = new ResponseResult();
           if (!ModelState.IsValid) return Json(Validate()); 
            // TODO:数据库的业务逻辑处理
               try
               {
                   if (pagedPeoPle.PagedPeoPleID != 0)
                   {
                       // TODO:修改处理
                       pagedPeoPleRepository.Update(pagedPeoPle);
                   }
                   else
                   {
                       pagedPeoPleRepository.Create(pagedPeoPle);
                   }
                   ret.Status = "success";
                   return Json(ret);
               }
               catch (Exception ex)
               {
                   throw ex;
               }
       }

        [HttpPost]
        public JsonResult DelPagedPeoPleById(int id)
        {
            try
            {
                ResponseResult ret = new ResponseResult();
                pagedPeoPleRepository.Delete(id);
                ret.Status = "success";
                return Json(ret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetPagedPeoPleList(string page, string rows)
        {
            Func<IQueryable<PagedPeoPle>, IOrderedQueryable<PagedPeoPle>> orderby
                = new Func<IQueryable<PagedPeoPle>, IOrderedQueryable<PagedPeoPle>>(q => q.OrderBy(s => s.PagedPeoPleID));
            return Json(pagedPeoPleRepository.GetPagedData(orderBy: orderby, pageSize: Convert.ToInt32(rows), pageNumber: Convert.ToInt32(page)));
        }

        [HttpGet]
        public ActionResult PagedPeopleEnd() 
        {
            return View();
        }

    }
}
