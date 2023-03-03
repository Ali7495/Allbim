using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.Center;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InsurerRepository : Repository<Insurer>, IInsurerRepository
    {
        public InsurerRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<List<Insurer>> GetAllInsurersByInsuranceId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Company)
                .Include(i => i.InsurerTerms)
                .ThenInclude(th => th.InsurerTermDetails)
                .Include(i=> i.InsurerTerms)
                .ThenInclude(th=> th.InsuranceTermType)
                .Where(w => w.InsuranceId == id).ToListAsync(cancellationToken);
        }

        

        public async Task<List<Insurer>> GetAllInsurerWithCompanyCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Company.Code == code).Include(i => i.Company).Include(i => i.Insurance).ToListAsync(cancellationToken);
        }

        public async Task<List<Insurer>> GetByInsuranceSlug(string slug, CancellationToken cancellationToken)
        {
            //return Table.Include(i => i.Insurers).ThenInclude(c => c.Select(s => new { s.Company , s.InsurerTerms}).ToList()).Include(i => i.InsuranceSteps).Include(i => i.InsuranceCentralRules).FirstOrDefaultAsync();
            return await Table.AsNoTracking().Include(i => i.Company)
                .Include(x=>x.Insurance)
                .Where(x=>x.Insurance.Slug==slug)
                .ToListAsync();
        }
        public async Task<Insurer> GetInsurerById(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(w => w.Id == id).Include(i => i.Article).ThenInclude(th => th.Author).Include(i => i.Article).ThenInclude(th => th.Comments).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Insurer> GetInsurerWithInsuranceById(long id, CancellationToken cancellationToken)
        {
            // return await Table.Include(x=>x.Company).Include(i => i.Insurance).ThenInclude(t => t.InsuranceSteps).Include(n=> n.Insurance).ThenInclude(th=> th.InsuranceCentralRules).Include(i=> i.InsurerTerms).FirstOrDefaultAsync(f => f.Id == id,cancellationToken);
            return await Table.AsNoTracking().Include(x=>x.Company).Include(i => i.Insurance).ThenInclude(t => t.InsuranceSteps).Include(n=> n.Insurance).Include(i=> i.InsurerTerms).ThenInclude(th=> th.InsuranceTermType).FirstOrDefaultAsync(f => f.Id == id,cancellationToken);
        }

        public async Task<Insurer> GetWithInsuranceIdAndCompanyCode(Guid code, long id, CancellationToken cancellationToken)
        {
            return await Table.Include(i=> i.Insurance).Include(i=> i.Company).Include(i=> i.Article).ThenInclude(th=> th.Comments).ThenInclude(th=> th.Author).Include(i=> i.Article).ThenInclude(th=> th.Author).FirstOrDefaultAsync(i => i.Company.Code == code && i.InsuranceId == id,cancellationToken);
        }    
        public async Task<Insurer> GetWithInsuranceIdAndCompanyCodeNoTracking(Guid code, long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i=> i.Insurance).Include(i=> i.Company).Include(i=> i.Article).ThenInclude(th=> th.Comments).ThenInclude(th=> th.Author).Include(i=> i.Article).ThenInclude(th=> th.Author).FirstOrDefaultAsync(i => i.Company.Code == code && i.InsuranceId == id,cancellationToken);
        }

        public async Task<List<Insurer>> GetAllInsurersWithTermsByInsuranceId(long insuranceId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.InsuranceId == insuranceId)
                .Include(i=> i.Company)
                .Include(i => i.InsurerTerms).ThenInclude(th => th.InsuranceTermType)
                .Include(i => i.InsurerTerms).ThenInclude(th => th.InsurerTermDetails)
                .ToListAsync(cancellationToken);
        }
    }
}

