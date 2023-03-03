using Common.Utilities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class ResourceOperationRepository : Repository<ResourceOperation>, IResourceOperationRepository
    {
        public ResourceOperationRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<ResourceOperation>> GetAllByResource(string ResourceName, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.Permission)
                .Include(x => x.Resource)
                .Where(x => x.Resource.Name == ResourceName)
                .ToListAsync();
        }
        public async Task<List<ResourceOperation>> GetAllByResourceAndPermissionIds(string ResourceName,List<long> permissionIds, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.Permission)
                .Include(x => x.Resource)
                .Where(x => x.Resource.Name == ResourceName)
                .Where(w => w.PermissionId== null || permissionIds.Contains(w.PermissionId.Value))
                .ToListAsync();
        }
    }
}
