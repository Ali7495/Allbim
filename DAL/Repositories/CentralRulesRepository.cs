using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.InsuranceCentralRule;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CentralRulesRepository : Repository<InsuranceCentralRule>, ICentralRulesRepository
    {
        public CentralRulesRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<InsuranceCentralRule>> GetAllCentalRules(long insuranceId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.CentralRuleType.InsuranceField.Insurance.Id == insuranceId)
                .Include(i => i.CentralRuleType).ThenInclude(th => th.InsuranceField).ThenInclude(th => th.Insurance)
                .GetNewPagedAsync(cancellationToken,pageAbleModel,null,null);
        }

        public async Task<List<InsuranceCentralRule>> GetByInsuranceSlug(string slug, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.CentralRuleType.InsuranceField.Insurance.Slug == slug).Include(i=> i.CentralRuleType).ToListAsync(cancellationToken);
        }

        public async Task<InsuranceCentralRule> GetRuleByInsuranceIdAndId(long insuranceId, long ruleId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.Id == ruleId && c.CentralRuleType.InsuranceField.Insurance.Id == insuranceId)
                .Include(i=> i.CentralRuleType).ThenInclude(th=> th.InsuranceField).ThenInclude(th=> th.Insurance)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
