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
    public class RegisterTempRepository : Repository<RegisterTemp>, IRegisterTempRepository
    {
        public RegisterTempRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RegisterTemp> GetRegisterTempByCodeAndMobile(string mobile, string code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().FirstOrDefaultAsync(r => r.Code == code && r.Mobile == mobile && r.ExpirationDate >= DateTime.Now, cancellationToken);
        }
    }
}
