using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ISchemaRepository : IRepository<SchemaVersion>
    {
        Task<PagedResult<SchemaVersion>> GetAllSchema(PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);
    }
}
