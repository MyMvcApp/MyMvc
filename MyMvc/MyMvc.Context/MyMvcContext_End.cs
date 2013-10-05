using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyMvc.Models.ModelsEnd;

namespace MyMvc.Context
{
    //后端基础模块的表
    public partial class MyMvcContext
    {
        public DbSet<AdminUser> AdminUser { get; set; }
    }
}
