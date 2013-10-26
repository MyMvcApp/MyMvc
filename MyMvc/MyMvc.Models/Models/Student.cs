using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyMvc.Models.Models
{
    public class Student : BaseModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public bool Sex { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

    }
}
