using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AgentSelectionTypeRepository : EnumRepository, IAgentSelectionTypeRepository
    {
        public AgentSelectionTypeRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }
        public Task<List<Enumeration>> Get(CancellationToken cancellationToken)
        {
            return GetEnumsBytype("AgentSelectionType", cancellationToken);
        }
    }
}
