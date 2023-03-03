using Common.Utilities;
using DAL.Models;
using Models.InsuranceCentralRule;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICentralRulesRepository : IRepository<InsuranceCentralRule>
    {
        Task<List<InsuranceCentralRule>> GetByInsuranceSlug(string slug, CancellationToken cancellationToken);
        Task<InsuranceCentralRule> GetRuleByInsuranceIdAndId(long insuranceId, long ruleId, CancellationToken cancellationToken);
        Task<PagedResult<InsuranceCentralRule>> GetAllCentalRules(long insuranceId, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);
    }
}
