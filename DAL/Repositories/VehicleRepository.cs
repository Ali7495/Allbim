using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Vehicle>> GetByVehicleBrandId(long vehicleBrandId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.VehicleBrandId == vehicleBrandId).ToListAsync(cancellationToken);
        }

        public async Task<Vehicle> GetWithRuleCategoryAndBrand(long vehicleId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(v => v.Id == vehicleId).Include(i => i.VehicleBrand).Include(i => i.VehicleRuleCategory).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Vehicle> GetWithVehicleRuleCategory(long vehicleId, CancellationToken cancellationToken)
        {
            return await Table.Include(x=>x.VehicleRuleCategory).FirstAsync(x=>x.Id==vehicleId,cancellationToken);
        }
        
        public async Task<Vehicle> GetWithRuleCategoryAndBrandAndType(long vehicleId, CancellationToken cancellationToken)
        {
            return await Table.Where(v => v.Id == vehicleId)
                .Include(i => i.VehicleBrand).ThenInclude(x=>x.VehicleType)
                .Include(i => i.VehicleRuleCategory)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
