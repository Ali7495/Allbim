using DAL.Contracts.EnumIRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.EnumRepositories
{
    public class AgentSelectRepository : EnumRepository, IAgentSelectRepository
    {
        public AgentSelectRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Enumeration>> Get(CancellationToken cancellationToken)
        {
            return await GetEnumsBytype("AgentSelectionType", cancellationToken);
        }
    }
}
