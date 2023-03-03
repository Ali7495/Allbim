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
    public class ArticlesManagementRepository : Repository<Article>, IArticlesManagementRepository
    {
        public ArticlesManagementRepository(AlbimDbContext dbContext)
           : base(dbContext)
        {
        }
        public async Task<Article> GetArticlesByID(long ID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.Author)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Category)
                .Include(i => i.ArticleMetaKeys)
                .Where(p => p.Id == ID).Where(x=>x.ArticleTypeId==2) // articleTypeId==2 : is article
                .SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<Article> GetPageBySlug(string Slug, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.Author)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Category)
                .Include(i => i.ArticleMetaKeys)
                .Where(p => p.Slug == Slug).Where(x=>x.ArticleTypeId==1) // articleTypeId==2 : is page
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Article>> GetTopFiveArticles(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.Author)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Category)
                .Include(i => i.ArticleMetaKeys)
                .Where(x=>x.ArticleTypeId==2) // articleTypeId==2 : is article
                .OrderByDescending(o => o.CreatedDateTime).Take(5)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
        
        
        
        public async Task<Article> GetPageByIdWithRelation( long id,CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Author)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Category)
                .Include(i => i.ArticleMetaKeys)
                .Where(p => p.Id == id).Where(x=>x.ArticleTypeId==1) // articleTypeId==1 : is page
                .SingleOrDefaultAsync(cancellationToken);
        }
        
        public async Task<Article> GetPageById(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Author)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Category)
                .Include(i => i.ArticleMetaKeys)
                .Where(p => p.Id == id).Where(x=>x.ArticleTypeId==1) // articleTypeId==1 : is page
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
