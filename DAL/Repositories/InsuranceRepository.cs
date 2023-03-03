using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InsuranceRepository : Repository<Insurance>, IInsuranceRepository
    {
        public InsuranceRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Insurance> GetInsurance(string slug, CancellationToken cancellationToken)
        {
            //return Table.Include(i => i.Insurers).ThenInclude(c => c.Select(s => new { s.Company , s.InsurerTerms}).ToList()).Include(i => i.InsuranceSteps).Include(i => i.InsuranceCentralRules).FirstOrDefaultAsync();
            //return await Table.Include(i => i.Insurers)
            //    .ThenInclude(t=> t.Company)
            //    .Include(i => i.Insurers)
            //    .ThenInclude(t => t.InsurerTerms)
            //    .Include(i=> i.InsuranceSteps.OrderBy(o=> o.StepOrder))
            //    .FirstAsync(w => w.Slug == slug);

            return await Table
                .Include(i => i.InsuranceSteps.OrderBy(o => o.StepOrder))
                .FirstAsync(w => w.Slug == slug);
        }

        public async Task<Insurance> GetInsurancesWithDetailBySlug(string slug, CancellationToken cancellationToken)
        {
            // return Table.Include(i => i.Insurers).ThenInclude(c => c.Select(s => new { s.Company , s.InsurerTerms}).ToList()).Include(i => i.InsuranceSteps).Include(i => i.InsuranceCentralRules).FirstOrDefaultAsync();
            return await Table.AsNoTracking().Include(i => i.Insurers)
                .ThenInclude(t => t.Company)
                .Include(i => i.Insurers)
                .ThenInclude(t => t.InsurerTerms)
                .Include(x => x.InsuranceSteps)
                .Include(i => i.InsuranceSteps.OrderBy(o => o.StepOrder))
                .FirstAsync(w => w.Slug == slug);
        }


        public async Task<Insurance> GetBySlugAndInsurer(string slug, long insurerId,
            CancellationToken cancellationToken)
        {
            //return Table.Include(i => i.Insurers).ThenInclude(c => c.Select(s => new { s.Company , s.InsurerTerms}).ToList()).Include(i => i.InsuranceSteps).Include(i => i.InsuranceCentralRules).FirstOrDefaultAsync();
            return await Table.Include(i => i.Insurers)
                .ThenInclude(t => ((Insurer) t).Company)
                .Include(i => i.Insurers.Where(x => x.Id == insurerId))
                .ThenInclude(t => t.InsurerTerms)
                .Include(i => i.InsuranceSteps.OrderBy(o => o.StepOrder))
                .FirstAsync(w => w.Slug == slug);
        }

        public async Task<Insurance> GetInsurerWithRelation(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.InsuranceFields)
                .Include(i => i.InsuranceSteps)
                .Include(i => i.Insurers)
                .Include(i => i.Reminders)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

    
        

        public async Task<List<Insurance>> GetInsuranceDetails(string slug, CancellationToken cancellationToken)
        {
            return await Table.Where(w => w.Slug == slug)
                .Include(i => i.Insurers).ThenInclude(th => th.Company)
                .Include(x => x.InsuranceFaqs)
                .ToListAsync(cancellationToken);
        }

        public async Task<Insurance> GetInsuranceById(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(w => w.Id == id).Include(i => i.Insurers).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Insurance> GetInsurancesWithStepsBySlug(string slug, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Slug == slug).Include(i => i.InsuranceSteps.OrderBy(o=> o.StepOrder)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Insurance> GetInsuranceByIdNoTracking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}