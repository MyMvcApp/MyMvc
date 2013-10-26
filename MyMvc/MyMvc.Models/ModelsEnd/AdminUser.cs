using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.ModelsEnd
{
    public class AdminUser:BaseModel
    {
        [MaxLength(50)]
        public string AdminName { get; set; }
        [MaxLength(50)]
        public string AdminPwd { get; set; }

        public string RealName { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string LastLoginIp { get; set; }
        /// <summary>
        /// 0超级管理员1普通管理员
        /// </summary>
        public int AdminType { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginAmount { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telephone { get; set; }

        public string QQ { get; set; }

        /// <summary>
        /// 管理员登录日志
        /// </summary>
        public virtual ICollection<AdminLoginLog> AdminLoginLogs { get; set; }

        public virtual ICollection<AdminAuthority> AdminAuthoritys { get; set; }

    }

    public class UpdatePwdParm
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string AdminPwd { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string AdminNewPwd { get; set; }
    }
}
