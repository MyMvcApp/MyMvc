using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.ModelsEnd
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    public class AdminAuthority:BaseModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string AdminAuthorityName { get; set; }
        /// <summary>
        /// 权限对应的访问地址
        /// </summary>
        public string AdminAuthorityUrl { get; set; }
        /// <summary>
        /// 权限顺序
        /// </summary>
        public int AdminAuthorityOrder { get; set; }

        public int AdminModuleID { get; set; }

        public AdminModule AdminModule { get; set; }

        public virtual ICollection<AdminUser> AdminUsers { get; set; }
    }
}
