using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using albim.Controllers;
using albim.Result;
using Common.Extensions;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Reminder;
using Services;


namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ReminderController : BaseController
    {
        #region Property

        private readonly IReminderService _reminderService;
        #endregion

        #region Constructor
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }
        #endregion


        #region Reminder
        
        [AllowAnonymous]
        [HttpGet("Period")]
        public async Task<ApiResult<List<ReminderPeriodResultViewModel>>> GetAllReminderPeriod(CancellationToken cancellationToken)
        {
            var model = await _reminderService.getAllPeriod( cancellationToken);

            return model;
        }
        [HttpGet()]
        [AllowAnonymous]
        public async Task<ApiResult<List<ReminderResultViewModel>>> GetAllReminder(CancellationToken cancellationToken)
        {
            var model = await _reminderService.GetAllReminder(cancellationToken);

            return model;
        }
        [HttpPost()]
        public async Task<ApiResult<ReminderResultViewModel>> Create([FromBody] ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _reminderService.Create(viewModel, cancellationToken);

            return model;
        }
        [HttpPut("{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> Update(long id, [FromBody] ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _reminderService.Update(id, viewModel, cancellationToken);
            return model;
        }
        [HttpPut("mine/{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> UpdateMine(long id, [FromBody] ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var model = await _reminderService.UpdateMine(UserID,id, viewModel, cancellationToken);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            var res = await _reminderService.Delete(id, cancellationToken);
            return res.ToString();
        }
        [HttpDelete("mine/{id}")]
        public async Task<ApiResult<string>> DeleteMine(long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var res = await _reminderService.DeleteMine(UserID,id, cancellationToken);
            return res.ToString();
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var model = await _reminderService.detail(id, cancellationToken);
            return model;
        }
        [HttpGet("mine/{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> GetDetailMine(long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var model = await _reminderService.detailMine(UserID,id, cancellationToken);
            return model;
        }

        #endregion

    }
}
