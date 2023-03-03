using Common.Extensions;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<List<Menu>> GetMenusByPremission(List<long> UserRolesPermissionsArray, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(w => w.PermissionId== null || UserRolesPermissionsArray.Contains(w.PermissionId.Value)).OrderBy(x=>x.Order).ToListAsync();
        }
    }
}
