using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MyMvc.ControllerTemplate
{
    public interface IControlerCUD<TEntity> 
        where TEntity:class
    {
        //GET
         ActionResult Details(int id = 0);
        //GET
         ActionResult Create();
        [HttpPost]
         ActionResult Create(TEntity entity);
        [HttpPost]
         ActionResult Delete(int id);
        //GET
         ActionResult Edit(int id = 0);
        //POST
         [HttpPost]
         ActionResult Edit(TEntity entity);
    }
}
