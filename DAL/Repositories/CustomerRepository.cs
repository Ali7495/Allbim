using Common.Extensions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<PolicyRequest>, ICustomerRepository
    {
        public CustomerRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }
        

        public async Task<List<Person>> GetAllCustomersByCompanyCode(Guid Code,
            CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Where(c => c.Insurer.Company.Code == Code)
                .Include(i => i.RequestPerson)
                .Include(i => i.Insurer)
                .Select(x=> x.RequestPerson).Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}