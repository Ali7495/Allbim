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
    public interface ICompanyAgentPersonRepository : IRepository<CompanyAgentPerson>
    {
        Task<CompanyAgentPerson> GetAgentPersonByCompanyIdAndPersonId(long companyId,long personId, CancellationToken cancellationToken);
        Task<CompanyAgentPerson> GetAgentPersonByCompanyCodeAndPerson(Guid companyCode,long personId, CancellationToken cancellationToken);
    }
}
