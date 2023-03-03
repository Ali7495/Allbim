using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class PolicyRequestStatusRepository : Repository<PolicyRequestStatus>, IPolicyRequestStatusRepository
    {
        public PolicyRequestStatusRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


    }
}