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
    public class CompanyAgentPersonRepository : Repository<CompanyAgentPerson>, ICompanyAgentPersonRepository
    {
        public CompanyAgentPersonRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CompanyAgentPerson> GetAgentPersonByCompanyIdAndPersonId(long companyId, long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.CompanyAgent)
                .Where(x => x.CompanyAgent.CompanyId == companyId && x.PersonId == personId).FirstOrDefaultAsync();
            
        }

        public async Task<CompanyAgentPerson> GetAgentPersonByCompanyCodeAndPerson(Guid companyCode, long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.CompanyAgent).ThenInclude(x => x.Company)
                .Where(x => x.CompanyAgent.Company.Code == companyCode && x.PersonId == personId).FirstOrDefaultAsync();
        }
    }
}
