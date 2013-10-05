using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MyMvc.ControllerTemplate
{
    //带有分页的页面模版接口
    public interface IControlerTemplatePaged<TEntity> : IControlerCUD<TEntity>,IControllerRPaged
        where TEntity:class
    {

    }
}
