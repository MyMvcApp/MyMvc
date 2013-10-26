using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.Models
{
    public class PagedPeoPle:BaseModel
    {
        [Required(ErrorMessage="人员的名称不能为空！")]
        public string Name { get; set; }
        [Required(ErrorMessage = "人员的年龄不能为空！")]
        public int Age { get; set; }

        public bool Sex { get; set; }
    }
}
