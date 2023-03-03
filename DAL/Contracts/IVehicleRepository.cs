using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        public Task<List<Vehicle>> GetByVehicleBrandId(long vehicleBrandId, CancellationToken cancellationToken);
        public Task<Vehicle> GetWithVehicleRuleCategory(long vehicleId, CancellationToken cancellationToken);
        public Task<Vehicle> GetWithRuleCategoryAndBrand(long vehicleId, CancellationToken cancellationToken);
        public Task<Vehicle> GetWithRuleCategoryAndBrandAndType(long vehicleId, CancellationToken cancellationToken);
    }
}