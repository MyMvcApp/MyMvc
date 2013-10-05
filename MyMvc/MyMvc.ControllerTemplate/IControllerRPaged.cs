using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace MyMvc.ControllerTemplate
{
    public interface IControllerRPaged
    {
        //GET带过滤，排序，分页条件,默认显示10条记录
        ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int pageSize=10);
    }
}
