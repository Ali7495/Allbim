using Common.Utilities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<Address> GetAddressByCode(Guid code,  CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Code == code).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Address> GetAddressByCodeNoTracking(Guid code,  CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Code == code).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
