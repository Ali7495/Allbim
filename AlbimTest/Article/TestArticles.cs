using albim;
using Albim.Controllers.v1;
using Microsoft.AspNetCore.Hosting;
using Models.Articles;
using Models.Insurance;
using Moq;
using Services.ArticleManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services;
using Xunit;

namespace AlbimTest.Blogs
{
    public class TestArticles
    {
        #region Property
        private readonly IArticlesManagementService _iArticlesManagementService;
        #endregion
        #region Constructor
        public TestArticles(IArticlesManagementService articlesManagementService)
        {
            _iArticlesManagementService = articlesManagementService;
        }
        #endregion
       // [Fact]
        public void ArticleService_SuccessResult()
        {
            // var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            //
            // CancellationToken cancellationToken = new CancellationToken();
            // var RepositoryStop = new Mock<IArticlesManagementService>();
            // var commentService = new Mock<ICommentServices>();
            // ArticlesController articlesController = new ArticlesController(RepositoryStop,commentService);
            // var resultGet = articlesController.GetDetail(7, cancellationToken);
            // Assert.True(resultGet.Result.IsSuccess);
        }
    }
}
