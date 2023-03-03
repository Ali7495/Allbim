using System;
using albim.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.User;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.SmsService;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Upload;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UploadController : BaseController
    {
        #region Property
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _configuration;


        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public UploadController(IConfiguration configuration , IUploadService uploadService)
        {
            _uploadService = uploadService;
            _configuration = configuration;

        }
        #endregion



        [HttpPost("")]
        public async Task<ApiResult<UploadViewModel>> CreateFile(IFormFile file, CancellationToken cancellationToken)
        {
            return await _uploadService.CreateFile(cancellationToken, file);
        }
        
        [HttpDelete("")]
        public async Task<ApiResult<string>> DeleteFile([FromQuery]UploadViewModel uploadViewModel, CancellationToken cancellationToken)
        {
            var res= await _uploadService.DeleteFileFromPublic(uploadViewModel.url,cancellationToken);
            return res.ToString();
        }
        
        
    }
}
