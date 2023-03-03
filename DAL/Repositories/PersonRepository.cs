using Common.Utilities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;
using Models.PageAble;

namespace DAL.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<Person> GetByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Person> GetWithUserAndAddressByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i=> i.Users).Include(i=> i.PersonAddresses).ThenInclude(th=> th.Address).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Person> GetByCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Person> GetByCodeNoTrackingWithDetails(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code)
                .Include(i => i.Users)
                    .ThenInclude(th=> th.UserRoles)
                    .ThenInclude(th=> th.Role)
                .Include(i => i.PersonCompanies)
                    .ThenInclude(x=>x.Company)
                    // .ThenInclude(x=>x.CompanyAgents)
                    // .ThenInclude(x=>x.CompanyAgentPeople)
                .Include(i=> i.PersonAddresses)
                    .ThenInclude(th=> th.Address)
                .Include(x=>x.CompanyAgents)
                    .ThenInclude(x=>x.City)
                    .ThenInclude(x=>x.TownShip)
                    .ThenInclude(x=>x.Province)
                .Include(x=>x.CompanyAgentPeople)
                    .ThenInclude(th=> th.CompanyAgent)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Person> GetByPersonIdwithPrimaryAddressAsync(long personId, CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Id == personId ).Include(i => i.Users).Include(x=>x.PersonAddresses.Where(x=>x.AddressTypeId==1)).ThenInclude(x=>x.Address).ThenInclude(x=>x.City).ThenInclude(x=>x.TownShip).ThenInclude(x=>x.Province).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Person> GetWithUserAndAgentByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Code == code).Include(i => i.Users).Include(i => i.CompanyAgents).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetAllAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.Users).ThenInclude(x=>x.UserRoles).ThenInclude(x=>x.Role)
                .Include(i => i.PersonCompanies).ThenInclude(x => x.Company)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<Person> GetByCodeNoTrackingWithAddress(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i=> i.PersonAddresses).ThenInclude(th=> th.Address).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetAllPersonsWithAgentByCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(th => th.Users).ThenInclude(th => th.UserRoles).ThenInclude(th => th.Role)
                .Include(i=> i.PersonCompanies.Where(p => p.CompanyId == companyId))
                .Include(i=> i.CompanyAgents.Where(p => p.CompanyId == companyId)).ThenInclude(th=> th.City)
                .Include(th => th.PersonAddresses).ThenInclude(th => th.Address).ThenInclude(th => th.City).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetAllWithoutUserAsync(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Users.Count == 0).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Person>> GetAllWithoutUserBySearchTextAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Users.Count == 0 && (p.FirstName.Contains(search_text) || p.LastName.Contains(search_text))).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<Person> GetWithUser(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i => i.Users).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Person>> GetAllBySearchAsync(string search_text, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p=> p.FirstName.Contains(search_text) || p.LastName.Contains(search_text))
                .Include(i => i.Users).ThenInclude(x => x.UserRoles).ThenInclude(x => x.Role)
                .Include(i => i.PersonCompanies).ThenInclude(x => x.Company)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<Person> GetByCodeNoTrackingWithUser(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i=> i.Users).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
