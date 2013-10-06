using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMvc.Models.ModelsEnd
{
    public class UserModel
    {
        [Display(Name = "员工ID")]
        public int userid { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "职位")]
        public string Job { get; set; }

        [Display(Name = "电话")]
        public string Phone { get; set; }

        [Display(Name = "性别")]
        public string Sex { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }
    }
}