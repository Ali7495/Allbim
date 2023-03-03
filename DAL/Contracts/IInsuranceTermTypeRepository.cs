using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IInsuranceTermTypeRepository : IRepository<InsuranceTermType>
    {
        Task<List<InsuranceTermType>> GetInsuranceTermTypesByInsuranceId(long insuranceId, CancellationToken cancellationToken);
    }
}
