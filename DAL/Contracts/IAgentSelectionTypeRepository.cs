using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IAgentSelectionTypeRepository : IRepository<Enumeration>
    {
        public Task<List<Enumeration>> Get(CancellationToken cancellationToken);
    }
}
