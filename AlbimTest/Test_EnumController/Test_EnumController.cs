using albim.Result;
using Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Net;

namespace AlbimTest.Test_EnumController
{
    public class Test_EnumController : IClassFixture<WebApplicationFactory<albim.Startup>>
    {
        #region Props
        private readonly HttpClient _client;
        
        #endregion
        #region Ctor
        public Test_EnumController(WebApplicationFactory<albim.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
        #endregion
        // #region Get Methode Tests
        // #region ThirdMaxFinancialCover Get Test 
        // [Fact]
        // public async Task Check200StatusCode_ThirdMaxFinancialCover()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdMaxFinancialCover");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ThirdMaxFinancialCoverViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_ThirdMaxFinancialCover()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdMaxFinancialCover");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ThirdMaxFinancialCoverViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_ThirdMaxFinancialCover()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdMaxFinancialCover");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region ThirdInsuranceCreditMonth Get Test 
        // [Fact]
        // public async Task Check200StatusCode_ThirdInsuranceCreditMonth()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdInsuranceCreditMonth");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ThirdInsuranceCreditMonthViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_ThirdInsuranceCreditMonth()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdInsuranceCreditMonth");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<ThirdInsuranceCreditMonthViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_ThirdInsuranceCreditMonth()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/ThirdInsuranceCreditMonth");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region BodyNoDamageDiscountYear Get Test 
        // [Fact]
        // public async Task Check200StatusCode_BodyNoDamageDiscountYear()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/BodyNoDamageDiscountYear");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<BodyNoDamageDiscountYearOutPutViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_BodyNoDamageDiscountYear()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/BodyNoDamageDiscountYear");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<BodyNoDamageDiscountYearOutPutViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_BodyNoDamageDiscountYear()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/BodyNoDamageDiscountYear");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region InsurerTermsTypes Get Test 
        // [Fact]
        // public async Task Check200StatusCode_InsurerTermsTypes()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/InsurerTermsTypes");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_InsurerTermsTypes()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/InsurerTermsTypes");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_InsurerTermsTypes()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/InsurerTermsTypes");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region PriceFluctuation Get Test 
        // [Fact]
        // public async Task Check200StatusCode_PriceFluctuation()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/PriceFluctuation");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // //[Fact]
        // //public async Task CheckDataIsNotNull_PriceFluctuation()
        // //{
        // //    var response = await _client.GetAsync("api/v1/enum/PriceFluctuation");
        // //    var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        // //    Assert.NotNull(Result.Data);
        // //}
        // //[Fact]
        // //public async Task CheckResultHasDataFeildOrNot_PriceFluctuation()
        // //{
        // //    var response = await _client.GetAsync("api/v1/enum/PriceFluctuation");
        // //    string Result = await response.Content.ReadAsStringAsync();
        // //    Assert.Contains("data", Result);
        // //}
        // #endregion
        // #region getNoBodyDamageDiscountEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getNoBodyDamageDiscountEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getNoBodyDamageDiscountEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getNoBodyDamageDiscountEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getNoBodyDamageDiscountEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getNoBodyDamageDiscountEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getNoBodyDamageDiscountEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region getThirdDiscountOnInsuranceEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getThirdDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getThirdDiscountOnInsuranceEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getThirdDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getThirdDiscountOnInsuranceEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getThirdDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getThirdDiscountOnInsuranceEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region getDriverDiscountOnInsuranceEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getDriverDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDiscountOnInsuranceEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getDriverDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDiscountOnInsuranceEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getDriverDiscountOnInsuranceEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDiscountOnInsuranceEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region getDriverDamageEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getDriverDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDamageEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getDriverDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDamageEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getDriverDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDriverDamageEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region getFinancialDamageEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getFinancialDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getFinancialDamageEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getFinancialDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getFinancialDamageEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getFinancialDamageEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getFinancialDamageEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #region getDamageToLifeEnums Get Test 
        // [Fact]
        // public async Task Check200StatusCode_getDamageToLifeEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDamageToLifeEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.Equal(ApiResultStatusCode.Success, Result.StatusCode);
        // }
        // [Fact]
        // public async Task CheckDataIsNotNull_getDamageToLifeEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDamageToLifeEnums");
        //     var Result = JsonConvert.DeserializeObject<ApiResult<List<EnumViewModel>>>(await response.Content.ReadAsStringAsync());
        //     Assert.NotNull(Result.Data);
        // }
        // [Fact]
        // public async Task CheckResultHasDataFeildOrNot_getDamageToLifeEnums()
        // {
        //     var response = await _client.GetAsync("api/v1/enum/getDamageToLifeEnums");
        //     string Result = await response.Content.ReadAsStringAsync();
        //     Assert.Contains("data", Result);
        // }
        // #endregion
        // #endregion
    }
}
