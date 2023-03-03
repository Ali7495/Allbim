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
    public interface IEnumerationRepository : IRepository<Enumeration>
    {
        Task<PagedResult<Enumeration>> GetAllEnumerationPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
