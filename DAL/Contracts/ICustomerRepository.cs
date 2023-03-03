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
    public interface ICustomerRepository : IRepository<PolicyRequest>
    {
        //Task<PagedResult<PolicyRequest>> GetAllCustomersByCompanyCode(Guid Code, PageAbleModel pageAbleModel,
        //    CancellationToken cancellationToken);
        Task<List<Person>> GetAllCustomersByCompanyCode(Guid Code,
            CancellationToken cancellationToken);
    }
}
