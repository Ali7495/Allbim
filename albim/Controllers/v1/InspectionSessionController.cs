using albim.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Inspection;
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
    public class InspectionSessionController : BaseController
    {
        #region Fields

        private readonly IInspectionServices _inspectionServices;

        #endregion

        #region ctor

        public InspectionSessionController(IInspectionServices inspectionServices)
        {
            _inspectionServices = inspectionServices;
        }

        #endregion

        [HttpGet("")]
        public async Task<List<InspectionDataViewModel>> GetInspectionSessions(CancellationToken cancellationToken)
        {
            return await _inspectionServices.GetInspectionData(cancellationToken);
        }
    }
}
