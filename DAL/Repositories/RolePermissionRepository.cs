using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<RolePermission>> GetRolesPermissionsAsync(List<long> roles)
        {
            return await Table.AsNoTracking().Include(r=>r.Role).Include(x=>x.Permission).Where(x => roles.Contains(x.RoleId)).ToListAsync();
        }

    }
}
