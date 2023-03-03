using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetBySlug(string slug, CancellationToken cancellationToken);
        Task<PagedResult<Category>> GetAllCategories(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<Category>> GetArticlesByCategorySlug(string slug, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
