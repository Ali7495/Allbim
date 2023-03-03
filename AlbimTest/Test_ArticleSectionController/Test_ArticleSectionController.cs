using albim.Result;
using Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Models.Articles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlbimTest.Test_ArticleSectionController
{
    public class Test_ArticleSectionController : IClassFixture<WebApplicationFactory<albim.Startup>>
    {
        #region Props
        private readonly HttpClient _client;

        #endregion
        #region Ctor
        public Test_ArticleSectionController(WebApplicationFactory<albim.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
        #endregion

        #region Get Methode Tests
        // #region GetAll Test 
        // [Fact]
        // public async Task Check200StatusCode_GetAllWithoutPaging()
        // {
        //     var response = await _client.GetAsync("api/v1/article-section/1/Article");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ArticleResultViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task HttpCheckOKStatusCode_GetAllWithoutPaging()
        // {
        //     var response = await _client.GetAsync("api/v1/article-section/1/Article");
        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_GetAllWithoutPaging()
        // {
        //     var response = await _client.GetAsync("api/v1/article-section/1/Article");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ArticleResultViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_GetAllWithoutPaging()
        // {
        //     var response = await _client.GetAsync("api/v1/article-section/1/Article");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        #endregion

    }
}
