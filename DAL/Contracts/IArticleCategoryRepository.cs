using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
        Task<ArticleCategory> GetByArticleAndCategoryId(long articleId, long categoryId, CancellationToken cancellationToken);
        Task<List<ArticleCategory>> GetByArticleId(long articleId, CancellationToken cancellationToken);
        
    }
}
