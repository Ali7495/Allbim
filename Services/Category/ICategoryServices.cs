using Common.Utilities;
using Models.Category;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryServices
    {
        Task<CategoryResultViewModel> CreateNewCategory(CategoryInputViewModel model, CancellationToken cancellationToken);
        Task<CategoryResultViewModel> GetCategory(string slug, CancellationToken cancellationToken);
        Task<CategoryResultViewModel> GetCategoryById(long id, CancellationToken cancellationToken);
        Task<PagedResult<CategoryResultViewModel>> GetAllCategoriesPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<List<CategoryResultViewModel>> GetAllCategories(CancellationToken cancellationToken);
        Task<CategoryResultViewModel> UpdateCategory(long id, CategoryInputViewModel model, CancellationToken cancellationToken);
        Task<bool> DeleteCategory(long id, CancellationToken cancellationToken);
        Task<PagedResult<CategoryResultViewModel>> GetArticleByCategorySlug(string slug, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
    }
}
