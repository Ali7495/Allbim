using albim.Controllers;
using albim.Result;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.PageAble;
using Models.Schema;
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
    public class SchemaController : BaseController
    {
        private readonly ISchemaServices _schemaServices;
        public SchemaController(ISchemaServices schemaServices)
        {
            _schemaServices = schemaServices;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ShemaVersionViewModel>>> GetAllSchemaVersions([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _schemaServices.GetAllSchema(pageAbleResult, cancellationToken);
        }
    }
}
