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
    public class InsuranceTermTypeRepository : Repository<InsuranceTermType>, IInsuranceTermTypeRepository
    {
        public InsuranceTermTypeRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<InsuranceTermType>> GetInsuranceTermTypesByInsuranceId(long insuranceId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(t => t.InsuranceField.InsuranceId == insuranceId).Include(i => i.InsuranceField).ToListAsync(cancellationToken);
        }
    }
}
