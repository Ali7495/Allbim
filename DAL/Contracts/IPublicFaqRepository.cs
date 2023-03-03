using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;

namespace DAL.Contracts
{
    public interface IPublicFaqRepository : IRepository<Faq>
    {

        Task<PagedResult<Faq>> GetAll(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
