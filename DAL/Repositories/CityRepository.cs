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
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<City>> GetAllWithProvince(CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.TownShip).ThenInclude(x => x.Province).ToListAsync();
        }
    }
}
