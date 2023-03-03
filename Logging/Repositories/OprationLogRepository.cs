using Common.Utilities;
using DAL;
using Logging.Contracts;
using Logging.LogModels;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logging.Repositories
{
    public class OprationLogRepository : LoggingRepository<OperationLog>, IOprationLogRepository
    {
        public OprationLogRepository(AlbimLogDbContext dbContext)
           : base(dbContext)
        {
        }
        public async Task<PagedResult<OperationLog>> GetPageableResult(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}