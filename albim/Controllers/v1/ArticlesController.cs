using albim.Result;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Attachment;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System;
using System.IO;
using albim.Controllers;
using Common.Extensions;
using Models.Articles;
using Services.ArticleManagement;
using Models.Comment;
using Models.Article;
using Models.PageAble;
using Common.Exceptions;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class ArticlesController : BaseController
    {
        #region Property
        private readonly IArticlesManagementService _articlesmanagementservice;
        private readonly ICommentServices _commentServices;
        #endregion
        #region Constructor
        public ArticlesController(IArticlesManagementService articlesManagementService, ICommentServices commentServices)
        {
            _commentServices = commentServices;
            _articlesmanagementservice = articlesManagementService;
        }
        #endregion
        #region Article Actions
        [HttpPost("")]
        public async Task<ApiResult<ArticleResultViewModel>> Create([FromBody] ArticlesInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            ArticleResultViewModel ArticalData = await _articlesmanagementservice.create(articlesInputViewModel, cancellationToken);
            return ArticalData;
        }


        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            var res = await _articlesmanagementservice.Delete(id, cancellationToken);
            return res.ToString();
        }
        [HttpDelete("mine/{id}")]
        public async Task<ApiResult<string>> DeleteMine(long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var res = await _articlesmanagementservice.DeleteMine(userId,id, cancellationToken);
            return res.ToString();
        }

        [HttpDelete("{articleId}/meta/{articleMetaKeyId}")]
        public async Task<ApiResult<string>> Delete(long articleId, long articleMetaKeyId, CancellationToken cancellationToken)
        {
            var res = await _articlesmanagementservice.DeleteArticleMetakey(articleId, articleMetaKeyId, cancellationToken);
            return res.ToString();
        }
        [HttpPut("{articleId}/meta/{articleMetaKeyId}")]
        public async Task<ApiResult<ArticleMetaKeyOutputViewModel>> Update(long articleId, long articleMetaKeyId, [FromBody] ArticleMetaKeyViewModel ArticleMetakeyinputViewModel, CancellationToken cancellationToken)
        {
            ArticleMetaKeyOutputViewModel Result = await _articlesmanagementservice.UpdateArticleMetakey(articleId, articleMetaKeyId, ArticleMetakeyinputViewModel, cancellationToken);
            return Result;
        }
        [HttpPut("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> Update(long id, [FromBody] ArticlesInputViewModel BlogsEditeViewModel, CancellationToken cancellationToken)
        {
            ArticleResultViewModel ArticalData = await _articlesmanagementservice.Update(id, BlogsEditeViewModel, cancellationToken);
            return ArticalData;
        }
        [HttpPut("mine/{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> UpdateMine(long id, [FromBody] ArticlesInputViewModel BlogsEditeViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _articlesmanagementservice.UpdateMine(userId,id, BlogsEditeViewModel, cancellationToken);
        }

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ArticleSummaryViewModel>>> GetAll([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var ArticlesList = await _articlesmanagementservice.all(pageAbleResult, cancellationToken);
            return ArticlesList;
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var company = await _articlesmanagementservice.detailV2(id, cancellationToken);
            return company;
        }
        [HttpGet("mine/{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetDetailMine(long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var company = await _articlesmanagementservice.detailMine(userId,id, cancellationToken);
            return company;
        }


        [HttpDelete("{id}/category/{categoryId}")]
        public async Task<bool> DeleteArticleCategory(long id, long categoryId, CancellationToken cancellationToken)
        {
            return await _articlesmanagementservice.DeleteArticleCategory(id, categoryId, cancellationToken);
        }

        [HttpGet("latest")]
        public async Task<ApiResult<PagedResult<ArticleLatestResultViewModel>>> GetTopFive([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _articlesmanagementservice.GetTopFiveArticles(pageAbleResult, cancellationToken);
        }
        #endregion

        #region Comment

        [HttpPost("{id}/comment")]
        public async Task<ApiResult<CommentResultViewModel>> PostComment(long id, [FromBody] CommentInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long? userId = long.Parse(HttpContext.User.Identity.GetUserId());
            return await _commentServices.PostNewComment(id, userId, viewModel, cancellationToken);
        }

        [HttpGet("{id}/comment")]
        public async Task<ApiResult<List<CommentResultViewModel>>> GetArticleComments(long id, CancellationToken cancellationToken)
        {
            return await _commentServices.GetArticleComments(id, cancellationToken);
        }
        [HttpGet("mine/{id}/comment")]
        public async Task<ApiResult<List<CommentResultViewModel>>> GetArticleCommentsMine(long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            return await _commentServices.GetArticleCommentsMine(userId,id, cancellationToken);
        }

        #endregion
    }
}
