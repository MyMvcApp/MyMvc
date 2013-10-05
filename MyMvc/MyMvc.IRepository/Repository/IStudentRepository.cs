using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMvc.Models.Models;
using MyMvc.Context;
namespace MyMvc.IRepository.Repository
{
    public interface IStudentRepository:IBaseRepository<Student,MyMvcContext>
    {

    }
}
