using Common.Utilities;
using Models.PageAble;
using Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ISchemaServices
    {
        Task<PagedResult<ShemaVersionViewModel>> GetAllSchema(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
    }
}
