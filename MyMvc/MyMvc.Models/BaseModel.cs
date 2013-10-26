using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MyMvc.Models
{
    /// <summary>
    /// 基类表
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用户自定义字段1
        /// </summary>
        public string UserDefine1 { get; set; }
        /// <summary>
        /// 用户自定义字段2
        /// </summary>
        public string UserDefine2 { get; set; }
        /// <summary>
        /// 用户自定义字段3
        /// </summary>
        public string UserDefine3 { get; set; }
        /// <summary>
        /// 用户自定义字段4
        /// </summary>
        public string UserDefine4 { get; set; }
        /// <summary>
        /// 用户自定义字段5
        /// </summary>
        public string UserDefine5 { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
