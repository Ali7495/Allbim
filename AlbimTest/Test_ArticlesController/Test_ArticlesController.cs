using albim.Result;
using Common;
using Common.Utilities;
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

namespace AlbimTest.Test_ArticlesController
{
    public class Test_ArticlesController : IClassFixture<WebApplicationFactory<albim.Startup>>
    {
        #region Props
        private readonly HttpClient _client;

        #endregion
        #region Ctor
        public Test_ArticlesController(WebApplicationFactory<albim.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
        #endregion
        // #region Get Methode Tests
        // #region GetAll Test 
        // [Fact]
        // public async Task Check200StatusCode_GetAll()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?page=1&pageSize=3");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<PagedResult<ArticleSummaryViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task HttpCheckOKStatusCode_GetAll()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?page=1&pageSize=3");
        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_GetAll()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?page=1&pageSize=3");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<PagedResult<ArticleSummaryViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data.Results);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_GetAll()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?page=1&pageSize=3");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region GetDetailes Test 
        // [Fact]
        // public async Task Check200StatusCode_GetDetailes()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<ArticlesDetailsOutputViewModel>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task HttpCheckOKStatusCode_GetDetailes()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7");
        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_GetDetailes()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<ArticlesDetailsOutputViewModel>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_GetDetailes()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region GetArticleComments Test 
        // [Fact]
        // public async Task Check200StatusCode_GetArticleComments()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7/comment");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<ArticlesDetailsOutputViewModel>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task HttpCheckOKStatusCode_GetArticleComments()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7/comment");
        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_GetArticleComments()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7/comment");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<ArticlesDetailsOutputViewModel>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_GetArticleComments()
        // {
        //     var response = await _client.GetAsync("api/v1/articles?id=7/comment");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #endregion


    }
}
