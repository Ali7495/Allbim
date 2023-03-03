using albim.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Issue;
using Services;
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
    public class IssueSessionController : BaseController
    {
        #region Fields

        private readonly IIssueSessionService _issueSessionService;

        #endregion

        #region CTOR

        public IssueSessionController(IIssueSessionService issueSessionService)
        {
            _issueSessionService = issueSessionService;
        }

        #endregion

        [HttpGet("")]
        public async Task<List<IssueSessionDataViewModel>> GetIssueSessions(CancellationToken cancellationToken)
        {
            return await _issueSessionService.GetIssueSessionData(cancellationToken);
        }

    }
}
