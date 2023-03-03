using DAL.Contracts.EnumIRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.EnumRepositories
{
    public class DriverRepository : EnumRepository, IDriverRepository
    {
        public DriverRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Enumeration>> Get(CancellationToken cancellation)
        {
            return GetEnumsBytype("DriverDamage", cancellation);
        }
    }
}
