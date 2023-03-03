using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VehicleBrandRepository : Repository<VehicleBrand>, IVehicleBrandRepository
    {
        public VehicleBrandRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<VehicleBrand>> AllAsync(CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<VehicleBrand>> GetByVehicleTypeId(long vehicleTypeId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.VehicleTypeId == vehicleTypeId).ToListAsync(cancellationToken);
        }
    }
}
