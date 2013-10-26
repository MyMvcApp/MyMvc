using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.ModelsEnd
{
    /// <summary>
    /// 管理员登录日志
    /// </summary>
    public class AdminLoginLog:BaseModel
    {
        public DateTime AdminLoginTime { get; set; }

        public string AdminLoginIP { get; set; }

        public int AdminUserID { get; set; }

        public virtual AdminUser AdminUser { get; set; }
    }
}
