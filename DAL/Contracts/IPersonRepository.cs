using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;


namespace DAL.Contracts
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetByCode(Guid code, CancellationToken cancellationToken);
        Task<Person> GetByCodeNoTracking(Guid code, CancellationToken cancellationToken);
        Task<Person> GetByCodeNoTrackingWithDetails(Guid code, CancellationToken cancellationToken);
        Task<Person> GetWithUserAndAgentByCode(Guid code, CancellationToken cancellationToken);
        Task<Person> GetByPersonIdwithPrimaryAddressAsync(long personId, CancellationToken cancellationToken);
        Task<PagedResult<Person>> GetAllAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<Person>> GetAllPersonsWithAgentByCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<Person> GetWithUserAndAddressByCode(Guid code, CancellationToken cancellationToken);
        Task<PagedResult<Person>> GetAllWithoutUserAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<Person>> GetAllWithoutUserBySearchTextAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<Person> GetWithUser(Guid code, CancellationToken cancellationToken);
        Task<PagedResult<Person>> GetAllBySearchAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<Person> GetByCodeNoTrackingWithUser(Guid code, CancellationToken cancellationToken);
    }
}