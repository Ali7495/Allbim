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
    public class CentralRuleTypeRepository : Repository<CentralRuleType>, ICentralRuleTypeRepository
    {
        public CentralRuleTypeRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CentralRuleType>> GetCentralRuleTypesByInsuranceId(long insuranceId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(t => t.InsuranceField.InsuranceId == insuranceId).Include(i => i.InsuranceField).ToListAsync(cancellationToken);
        }
    }
}
