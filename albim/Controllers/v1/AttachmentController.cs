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

namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class AttachmentController : BaseController
    {
        #region Property

        private readonly IConfiguration _configuration;
        private readonly IAttachmentService _personService;
        private readonly IAttachmentService _attachmentService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public AttachmentController(IConfiguration configuration, IAttachmentService personService,
            IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
            _configuration = configuration;
            _personService = personService;
        }

        #endregion


        #region Attachment Actions

  
        [AllowAnonymous]
        [HttpGet("{filename}")]
        public async Task<FileContentResult> Get([FromRoute] string filename, CancellationToken cancellationToken)
        {
            var result = await _attachmentService.DownloadByFullName(filename, cancellationToken);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("{code}/detail")]
        public async Task<AttachmentResultViewModel> GetAttachmentDetailByCode([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            var result = await _attachmentService.GetAttachmentDetailByCode(code, cancellationToken);
            return result;
        }
        [HttpDelete("{code}")]
        public async Task<bool> DeleteAttachment([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            var result = await _attachmentService.DeleteAttachment(code, cancellationToken);
            return result;
        }
        [HttpPost("")]
        public async Task<AttachmentResultViewModel> CreateAttachment([FromForm]IFormFile file, CancellationToken cancellationToken)
        {
            return await _attachmentService.CreateAttachmentWithDefrentVM(cancellationToken, file);
        }
        [HttpPut("{code}")]
        public async Task<AttachmentInputViewModel> UpdateAttachment([FromRoute] Guid code , IFormFile attachment, CancellationToken cancellationToken)
        {
            return await _attachmentService.UpdateAttachment(code,cancellationToken, attachment);
        }

        #endregion
    }
}