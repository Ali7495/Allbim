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
    public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ArticleCategory> GetByArticleAndCategoryId(long articleId, long categoryId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(ac => ac.ArticleId == articleId && ac.CategoryId == categoryId && ac.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ArticleCategory>> GetByArticleId(long articleId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(ac => ac.ArticleId == articleId ).ToListAsync(cancellationToken);
        }
    }
}
