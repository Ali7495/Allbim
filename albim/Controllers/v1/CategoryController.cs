using albim.Controllers;
using albim.Result;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Category;
using Models.PageAble;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class CategoryController : BaseController
    {
        #region properties

        private readonly ICategoryServices _categoryServices;

        #endregion


        #region ctor

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        #endregion

        [HttpPost("")]
        public async Task<ApiResult<CategoryResultViewModel>> Create(CategoryInputViewModel model, CancellationToken cancellationToken)
        {
            return await _categoryServices.CreateNewCategory(model, cancellationToken);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ApiResult<CategoryResultViewModel>> GetBySlug(string slug, CancellationToken cancellationToken)
        {
            return await _categoryServices.GetCategory(slug, cancellationToken);
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryResultViewModel>> GetById(long id, CancellationToken cancellationToken)
        {
            return await _categoryServices.GetCategoryById(id, cancellationToken);
        }

        
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<CategoryResultViewModel>>> GetAllPaging([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _categoryServices.GetAllCategoriesPaging(pageAbleResult, cancellationToken);
        }

        
        [HttpGet("list")]
        public async Task<ApiResult<List<CategoryResultViewModel>>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryServices.GetAllCategories(cancellationToken);
        }


        [HttpPut("{id}")]
        public async Task<ApiResult<CategoryResultViewModel>> Update(long id, CategoryInputViewModel model, CancellationToken cancellationToken)
        {
            return await _categoryServices.UpdateCategory(id, model, cancellationToken);
        }

        
        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            return await _categoryServices.DeleteCategory(id, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("slug/{slug}/article")]
        public async Task<ApiResult<PagedResult<CategoryResultViewModel>>> GetCategoryArticles(string slug, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _categoryServices.GetArticleByCategorySlug(slug, pageAbleResult, cancellationToken);
        }
    }
}
