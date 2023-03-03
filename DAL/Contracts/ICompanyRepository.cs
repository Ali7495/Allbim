using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using Models.Center;

namespace DAL.Contracts
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetByCode(Guid code,  CancellationToken cancellationToken);
        Task<Company> GetByCodeNoTracking(Guid code,  CancellationToken cancellationToken);
        Task<Company> GetByIdNoTracking(long id, CancellationToken cancellationToken);
    }
}