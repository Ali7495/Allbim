using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using albim.Controllers;
using albim.Result;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Center;
using Services;


namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CenterController : BaseController
    {
        #region Property

        private readonly ICenterService _centerService;
        #endregion

        #region Constructor
        public CenterController(ICenterService roleService)
        {
            _centerService = roleService;
        }
        #endregion


        #region Center

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ApiResult<List<CenterResultViewModel>>> Get(CancellationToken cancellationToken)
        {
            var model = await _centerService.GetAll( cancellationToken);

            return model;
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<CenterResultViewModel>> Get([FromRoute]long id,CancellationToken cancellationToken)
        {
            var model = await _centerService.Get(id,cancellationToken);

            return model;
        }

        [HttpPost()]
        public async Task<ApiResult<CenterResultViewModel>> Create([FromBody] CenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _centerService.Create(viewModel, cancellationToken);

            return model;
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<CenterResultViewModel>> Update([FromRoute]long id, [FromBody] CenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _centerService.Update(id, viewModel, cancellationToken);

            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete([FromRoute]long id, CancellationToken cancellationToken)
        {
            bool result = await _centerService.Delete(id, cancellationToken);

            return result.ToString();
        }

        #endregion


    
    }
}
