using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMvc.IRepository.Repository;
using MyMvc.Repository;
using MyMvc.Models.Models;
using MyMvc.Context;
namespace MyMvc.Repository.Repository
{
    public class StudentRepository:BaseRepository<Student,MyMvcContext>,IStudentRepository
    {
        public StudentRepository(MyMvcContext context) : base(context) { }
    }
}
