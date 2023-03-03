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

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class ArticleSectionController : BaseController
    {
        #region Property
        private readonly IArticlesManagementService _articlesmanagementservice;
        #endregion

        public ArticleSectionController(IArticlesManagementService articlesManagementService)
        {
            _articlesmanagementservice = articlesManagementService;
        }

        [HttpGet("{id}/Article")]
        public async Task<ApiResult<List<ArticleResultViewModel>>> GetAllWithoutPaging(int id, CancellationToken cancellationToken)
        {
            return await _articlesmanagementservice.GetBySection(id, cancellationToken);
        }


    }
}
