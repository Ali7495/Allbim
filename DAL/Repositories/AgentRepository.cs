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
    public class AgentRepository : Repository<CompanyAgent>, IAgentRepository
    {
        public AgentRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CompanyAgent>> GetAllByCompanyCodeAsync(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Company).Include(i => i.Person).ThenInclude(th => th.Users).Include(i=> i.City).ThenInclude(th=> th.TownShip).ThenInclude(th=> th.Province).Where(a => a.Company.Code == code).ToListAsync(cancellationToken);
        }

        public async Task<CompanyAgent> GetByCompanyAndPersonCodeAsync(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            return await Table.Include(i => i.Company).Include(i => i.Person).ThenInclude(th => th.Users).Include(i => i.City).ThenInclude(th => th.TownShip).ThenInclude(th => th.Province).Where(a => a.Company.Code == code && a.Person.Code == personCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CompanyAgent> GetByCompanyAndPersonCodeAsyncNoTracking(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(a => a.Company.Code == code && a.Person.Code == personCode).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CompanyAgent> GetByIdNoTracking(long companyAgentId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(a => a.Id == companyAgentId).FirstOrDefaultAsync();
        }

        public async Task<CompanyAgent> GetByPersonIdAndCompanyId(long personId, long companyId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(a => a.PersonId == personId && a.CompanyId == companyId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<CompanyAgent>> GetCompanyAgents(long companyId, long roleId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(a=> a.CompanyId == companyId && a.Person.Users.FirstOrDefault().UserRoles.FirstOrDefault().RoleId == roleId).Include(i => i.Company).Include(i => i.Person).ThenInclude(th => th.Users).Include(i => i.City).ThenInclude(th => th.TownShip).ThenInclude(th => th.Province).GetNewPagedAsync(cancellationToken,pageAbleModel,null,null);
        }

        public async Task<CompanyAgent> GetLonelyByPersonAndCompanyCodeAsync(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(a => a.Company.Code == code && a.Person.Code == personCode).FirstOrDefaultAsync(cancellationToken);
        }

    }
}
