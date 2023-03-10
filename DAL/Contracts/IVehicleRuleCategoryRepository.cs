using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IVehicleRuleCategoryRepository : IRepository<VehicleRuleCategory>
    {
        Task<List<VehicleRuleCategory>> GetAllVehicleRuleCategories(CancellationToken cancellationToken);
    }
}
