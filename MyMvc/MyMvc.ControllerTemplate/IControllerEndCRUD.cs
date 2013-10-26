using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyMvc.IRepository;
using MyMvc.Models;
using System.Data;
using System.Data.Entity;
using System.Web;
namespace MyMvc.ControllerTemplate
{
    public interface IControllerEndCRUD<TEntity,TContext>
        where TEntity:BaseModel
        where TContext : DbContext
       
    {
        IBaseRepository<TEntity, TContext> BaseReposity { get; set; }

        [HttpPost]
        JsonResult CreateOrUpdate(TEntity T);

        [HttpPost]
        JsonResult Create(TEntity T);

        [HttpPost]
        JsonResult Update(TEntity T);

         [HttpPost]
        JsonResult Delete(TEntity T);

         [HttpPost]
         JsonResult GetDataList(string page, string rows);

         [HttpPost]
         JsonResult GetDataListByID(string page, string rows, string id);

    }
}
