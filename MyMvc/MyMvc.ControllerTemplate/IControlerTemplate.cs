using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MyMvc.ControllerTemplate
{
    //不带分页的页面模版接口
    public interface IControlerTemplate<TEntity> : IControlerCUD<TEntity>, IControllerR
        where TEntity:class
    {
    }
}
