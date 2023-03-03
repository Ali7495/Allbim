using DAL.Contracts.EnumIRepositories;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.EnumRepositories
{
    public class ThirdMaxFinancialCoverRepository : EnumRepository, IThirdMaxFinancialCoverRepository
    {
        public ThirdMaxFinancialCoverRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {

        }
        public new Task<List<Enumeration>> GetAllAsync(CancellationToken cancellationToken)
        {
            return GetEnumsBytype("ThirdMaxFinancialCover", cancellationToken);
        }
    }
}
