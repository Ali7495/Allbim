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
    public class EnumRepository : Repository<Enumeration>, IEnumRepository 
    {
        public EnumRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {

        }
        public Task<List<Enumeration>> GetEnumsBytype(string type, CancellationToken cancellationToken)
        {
            return Table.Where(e => e.CategoryName == type).OrderBy(x=>x.Order).ToListAsync(cancellationToken);
        }
    }
}
