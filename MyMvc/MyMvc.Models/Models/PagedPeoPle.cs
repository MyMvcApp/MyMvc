using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.Models
{
    public class PagedPeoPle
    {
        public int PagedPeoPleID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public bool Sex { get; set; }
    }
}
