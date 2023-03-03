using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRole(long id, CancellationToken cancellationToken);
        Task<List<Role>> GetByParentId(long ParentId, CancellationToken cancellationToken);

    }
}
