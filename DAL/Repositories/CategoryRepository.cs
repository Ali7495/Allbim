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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetBySlug(string slug, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.Slug == slug).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<Category>> GetAllCategories(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c=> c.IsDeleted == false).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<Category>> GetArticlesByCategorySlug(string slug, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.Slug == slug)
                .Include(i => i.ArticleCategories).ThenInclude(th => th.Article)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
