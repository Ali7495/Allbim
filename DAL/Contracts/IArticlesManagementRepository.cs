using Common.Utilities;
using DAL.Models;
using DAL.Repositories;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IArticlesManagementRepository : IRepository<Article>
    {
        Task<Article> GetArticlesByID(long code, CancellationToken cancellationToken);
        Task<Article> GetPageBySlug(string Slug, CancellationToken cancellationToken);
        Task<PagedResult<Article>> GetTopFiveArticles(PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);

        Task<Article> GetPageByIdWithRelation(long id, CancellationToken cancellationToken);
        Task<Article> GetPageById(long id, CancellationToken cancellationToken);
    }
}
