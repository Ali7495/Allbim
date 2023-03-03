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
    public class VehicleRuleCategoryRepository : Repository<VehicleRuleCategory>, IVehicleRuleCategoryRepository
    {
        public VehicleRuleCategoryRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<VehicleRuleCategory>> GetAllVehicleRuleCategories(CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
