using albim.Controllers;
using albim.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Articles;
using Services.ArticleManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class PageController : BaseController
    {
        #region Property
        private readonly IBlogService _blogService;
        #endregion
        #region Constructor
        public PageController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        #endregion
        [HttpGet("slug/{slug}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetArticleBySlug(string slug, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.GetArticleBySlug(slug, cancellationToken);
            return SlugArticle;
        }
        
        [HttpGet("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetById(long id, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.GetArticleById(id, cancellationToken);
            return SlugArticle;
        }

        
        
        [HttpPost("")]
        public async Task<ApiResult<ArticleResultViewModel>> CreatePage([FromBody] PageInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.create(articlesInputViewModel, cancellationToken);
            return SlugArticle;
        }
        
        
        
        
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ArticleSummaryViewModel>>> GetAll( [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.allPages(pageAbleResult, cancellationToken);
            return SlugArticle;
        }
        
        
        
        
        [HttpPut("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> UpdatePage(long id,[FromBody] PageInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.Update(id,articlesInputViewModel, cancellationToken);
            return SlugArticle;
        }
        
        
        
        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> DeletePage(long id, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.Delete(id, cancellationToken);
            return SlugArticle.ToString();
        }
        
        
        
        
        
        
    }
}
