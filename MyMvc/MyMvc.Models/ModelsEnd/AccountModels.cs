using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyMvc.Models.ModelsEnd
{
    public class LoginModel
    {
        [Display(Name = "帐号")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
