using System.Text;
using System.Threading.Tasks;
using MyMvc.IRepository.Repository;
using MyMvc.Repository;
using MyMvc.Models.Models;
using MyMvc.Context;
namespace MyMvc.Repository.Repository
{
    public class PagedPeoPleRepository : BaseRepository<PagedPeoPle, MyMvcContext>, IPagedPeoPleRepository
    {
        public PagedPeoPleRepository(MyMvcContext context): base(context)
        {
        
        }
    }
}
