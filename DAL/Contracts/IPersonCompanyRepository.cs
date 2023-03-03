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
    public interface IPersonCompanyRepository : IRepository<PersonCompany>
    {
        public Task<PersonCompany> GetByPersonId(long personId, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonIdAndCompanyId(long personId, long companyId, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonCodeAndCompanyCode(Guid personCode, Guid companyCode, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonCodeAndCompanyId(Guid personCode, long companyId, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonIdAndCompanyCode(long personId, Guid companyCode, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonIdNoTracking(long personId, CancellationToken cancellationToken);

        public Task<PersonCompany> GetByPersonIdAndParentIdNoTracking(long personId, long parentId, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetUsersByCompanyId(long CompanyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonsWithoutUserAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonsWithoutUserBySearchTextAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonByCompany(long CompanyId, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonByParentId(long parentId, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);

        Task<PersonCompany> GetByParentIdAndPersonCodeAsync(long parentId, Guid personCode, CancellationToken cancellationToken);

        Task<PersonCompany> GetByPersonCodeAndCompanyCodeAndParentId(Guid personCode, Guid companyCode,long parentId, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonsWithoutUserByParentIdAsync(long parentId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetAllPersonsWithoutUserByParentIdBySearchTextAsync(long parentId, string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<Person>> GetUsersByParentId(long parentId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PersonCompany> GetUserByParentAndUserId(long parentId, long userId, CancellationToken cancellationToken);
        Task<PersonCompany> GetByPersonIdWithPerson(long personId, CancellationToken cancellationToken);
    }
}
