using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMvc.Models.ModelsEnd;
using MyMvc.Context;
namespace MyMvc.IRepository.RepositoryEnd
{
    public interface IAdminUserRepository : IBaseRepository<AdminUser, MyMvcContext>
    {
    }
}
