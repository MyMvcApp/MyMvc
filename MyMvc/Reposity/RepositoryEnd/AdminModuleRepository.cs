using System.Text;
using System.Threading.Tasks;
using MyMvc.IRepository.RepositoryEnd;
using MyMvc.Repository;
using MyMvc.Models.ModelsEnd;
using MyMvc.Context;

namespace MyMvc.Repository.RepositoryEnd
{
    public class AdminModuleRepository : BaseRepository<AdminModule, MyMvcContext>, IAdminModuleRepository
    {
        public AdminModuleRepository(MyMvcContext context) : base(context) { }
    }
}
