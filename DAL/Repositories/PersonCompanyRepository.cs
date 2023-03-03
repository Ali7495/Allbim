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
    public class PersonCompanyRepository : Repository<PersonCompany>, IPersonCompanyRepository
    {
        public PersonCompanyRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PersonCompany> GetByPersonCodeAndCompanyCode(Guid personCode, Guid companyCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Code == personCode && p.Company.Code == companyCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonCodeAndCompanyId(Guid personCode, long companyId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Code == personCode && p.CompanyId == companyId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonId(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId).Include(x=> x.Company).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonIdWithPerson(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId).Include(x => x.Company).Include(i=> i.Person).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonIdAndCompanyCode(long personId, Guid companyCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId && p.Company.Code == companyCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonIdAndCompanyId(long personId, long companyId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId && p.CompanyId == companyId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonIdNoTracking(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetUsersByCompanyId(long CompanyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.CompanyId == CompanyId && p.Person.Users.FirstOrDefault() != null).Include(i => i.Person).ThenInclude(th => th.Users).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetAllPersonsWithoutUserAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Users.Count == 0).Select(s=> s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetAllPersonsWithoutUserBySearchTextAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Users.Count == 0 && (p.Person.FirstName.Contains(search_text) || p.Person.LastName.Contains(search_text))).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
        public async Task<PagedResult<Person>> GetAllPersonByCompany(long CompanyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x=>x.CompanyId==CompanyId).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PersonCompany> GetByPersonIdAndParentIdNoTracking(long personId, long parentId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId && p.ParentId == parentId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetAllPersonByParentId(long parentId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.ParentId == parentId).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PersonCompany> GetByParentIdAndPersonCodeAsync(long parentId, Guid personCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.ParentId == parentId && x.Person.Code == personCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonCompany> GetByPersonCodeAndCompanyCodeAndParentId(Guid personCode, Guid companyCode, long parentId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.ParentId == parentId && x.Person.Code == personCode && x.Company.Code == companyCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetAllPersonsWithoutUserByParentIdAsync(long parentId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Users.Count == 0 && p.PersonId == parentId).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetAllPersonsWithoutUserByParentIdBySearchTextAsync(long parentId, string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Person.Users.Count == 0 && p.PersonId == parentId && (p.Person.FirstName.Contains(search_text) || p.Person.LastName.Contains(search_text))).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetUsersByParentId(long parentId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.ParentId == parentId && p.Person.Users.FirstOrDefault() != null).Include(i => i.Person).ThenInclude(th => th.Users).Select(s => s.Person).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PersonCompany> GetUserByParentAndUserId(long parentId, long userId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.ParentId == parentId && p.Person.Users.FirstOrDefault(u => u.Id == userId) != null).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
