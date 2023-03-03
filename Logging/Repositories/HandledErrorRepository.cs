using DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;
using Logging.LogModels;
using Logging.Contracts;
using Common.Utilities;
using DAL;
using Models.PageAble;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Logging.Repositories
{
    public class HandledErrorRepository : LoggingRepository<HandledErrorLog>, IHandledErrorRepository
    {
        public HandledErrorRepository(AlbimLogDbContext dbContext)
           : base(dbContext)
        {
        }
        public async Task<PagedResult<HandledErrorLog>> GetPageableResult(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
