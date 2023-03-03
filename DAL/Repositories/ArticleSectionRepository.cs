using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ArticleSectionRepository : Repository<ArticleSection>, IArticleSectionRepository
    {
        public ArticleSectionRepository(AlbimDbContext dbContext)
           : base(dbContext)
        {
        }
        public async Task<List<ArticleSection>> GetAllArticleByIDFromArticleSection(CancellationToken cancellationToken, long ArticleId)
        {
            return await Table.AsNoTracking().Where(x => x.ArticleId == ArticleId).ToListAsync(cancellationToken);
        }

        public async Task<List<ArticleSection>> GetArticleidByArticleSectionID(CancellationToken cancellationToken, int SectionID)
        {
            return await Table.AsNoTracking().Include(x=>x.Article).ThenInclude(x=>x.ArticleMetaKeys).Where(x => x.SectionId == SectionID).ToListAsync(cancellationToken);
        }

        public async Task<ArticleSection> GetByCode(int code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Id == code).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
