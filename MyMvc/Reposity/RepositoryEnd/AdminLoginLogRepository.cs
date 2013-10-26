using System.Text;
using System.Threading.Tasks;
using MyMvc.IRepository.RepositoryEnd;
using MyMvc.Repository;
using MyMvc.Models.ModelsEnd;
using MyMvc.Context;

namespace MyMvc.Repository.RepositoryEnd
{
    public class AdminLoginLogRepository : BaseRepository<AdminLoginLog, MyMvcContext>, IAdminLoginLogRepository
    {
        public AdminLoginLogRepository(MyMvcContext context) : base(context) { }
    }
}
