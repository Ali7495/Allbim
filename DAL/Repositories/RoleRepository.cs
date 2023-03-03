using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Role> GetRole(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(t => t.RolePermissions).FirstOrDefaultAsync(r => r.Id == id,cancellationToken);
        }

        public async Task<List<Role>> GetByParentId(long ParentId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.ParentId == ParentId).ToListAsync();
        }
    }
}
