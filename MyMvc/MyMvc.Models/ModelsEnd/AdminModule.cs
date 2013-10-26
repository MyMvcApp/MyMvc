using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.ModelsEnd
{
    /// <summary>
    /// 功能模块
    /// </summary>
    public class AdminModule : BaseModel
    {
        public string AdminModuleName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AdminAuthority> AdminAuthoritys { get; set; }
    }
}
