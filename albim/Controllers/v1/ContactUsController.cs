using albim.Controllers;
using albim.Result;
using Common.Exceptions;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ContactUs;
using Services.Contactus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]

    public class ContactUsController : BaseController
    {
        #region Property
        private readonly IContactUsService _contactUsService;
        #endregion
        #region Constructor
        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }
        #endregion

        #region Contact US Actions
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<ContactUsFrontResultViewModel>> Create([FromBody] ContactUsInputPostViewModel ContactUsInputPostViewModel, CancellationToken cancellationToken)
        {
            return await _contactUsService.create(ContactUsInputPostViewModel, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            var Result = await _contactUsService.Delete(id, cancellationToken);
            return Result.ToString();
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<ContactUsDashboardResultViewModel>> Update(long id, [FromBody] ContactUsInputViewModel ContactUsEditeViewModel, CancellationToken cancellationToken)
        {
            return await _contactUsService.Update(id, ContactUsEditeViewModel, cancellationToken);
        }
        [HttpPut("{id}/answer")]
        public async Task<ApiResult<ContactUsDashboardResultViewModel>> Answer(long id, [FromBody] ContactUsInputPutViewModel ContactUsEditeViewModel, CancellationToken cancellationToken)
        {
            return await _contactUsService.Answer(id, ContactUsEditeViewModel, cancellationToken);
        }
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ContactUsDashboardResultViewModel>>> GetAll([FromQuery]  PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _contactUsService.all(pageAbleResult, cancellationToken);
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<ContactUsDashboardResultViewModel>> GetById(long id, CancellationToken cancellationToken)
        {
            return await _contactUsService.GetById(id, cancellationToken);
        }

        #endregion
    }
}
