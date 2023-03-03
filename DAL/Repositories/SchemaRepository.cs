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
    public class SchemaRepository : Repository<SchemaVersion>, ISchemaRepository
    {
        public SchemaRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<SchemaVersion>> GetAllSchema(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().OrderByDescending(o=> o.Id).GetNewPagedAsync(cancellationToken,pageAbleModel,null,null);
        }
    }
}
