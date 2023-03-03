using albim.Controllers;
using albim.Result;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Models.PageAble;
using Models.Payment;
using Models.PaymentGateway;
using Models.PaymentStatus;
using Services.PolicyRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models.QueryParams;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<PaymentFactorViewModel>>> GetAllPayments(Guid? companyCode, long? statusId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PaymentFactorViewModel> result = await _paymentService.GetAllPayments(companyCode, statusId, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("mine")]
        public async Task<ApiResult<PagedResult<PaymentFactorViewModel>>> GetAllPaymentsMine( [FromQuery] PaymentParamsViewModel paymentParamsViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            PagedResult<PaymentFactorViewModel> result = await _paymentService.GetAllPaymentsMine(userId, paymentParamsViewModel.statusId, paymentParamsViewModel, cancellationToken);
            return result;
        }

        










        #region Gateway

        [HttpPost("paymentGateway")]
        public async Task<ApiResult<GetewayResultViewModel>> CreateGateWay(GatewayInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await _paymentService.CreatePaymentGateway(inputViewModel, cancellationToken);
        }

        [HttpGet("paymentGateway")]
        public async Task<ApiResult<PagedResult<GetewayResultViewModel>>> GetAllGateWayPaging([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _paymentService.GetAllPaymentGatewaysPaging(pageAbleResult, cancellationToken);
        }

        [HttpGet("paymentGateway/list")]
        public async Task<ApiResult<List<GetewayResultViewModel>>> GetAllGateWayList(CancellationToken cancellationToken)
        {
            return await _paymentService.GetAllPaymentGatewaysList(cancellationToken);
        }

        [HttpGet("paymentGateway/{gatewayId}")]
        public async Task<ApiResult<GetewayResultViewModel>> GetAllGateWayList(long gatewayId, CancellationToken cancellationToken)
        {
            return await _paymentService.GetPaymentGateWayWithDetails(gatewayId,cancellationToken);
        }

        [HttpPut("paymentGateway/{gatewayId}")]
        public async Task<ApiResult<GetewayResultViewModel>> UpdateGateWay(long gatewayId, GatewayUpdateInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await _paymentService.UpdatePaymentGateway(gatewayId, inputViewModel, cancellationToken);
        }

        [HttpDelete("paymentGateway/{gatewayId}")]
        public async Task<bool> DeleteGateway(long gatewayId, CancellationToken cancellationToken)
        {
            return await _paymentService.DeleteGateway(gatewayId, cancellationToken);
        }

        #endregion



        #region 
        [HttpPost("paymentStatus")]
        public async Task<ApiResult<PaymentStatusResultViewModel>> CreatePaymentStatus(PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await _paymentService.CreatePaymentStatus(inputViewModel, cancellationToken);
        }


        [HttpGet("paymentStatus/list")]
        public async Task<ApiResult<List<PaymentStatusResultViewModel>>> GetAllPaymentStatus(CancellationToken cancellationToken)
        {
            List<PaymentStatusResultViewModel> result = await _paymentService.GetAllPaymentStatus(cancellationToken);
            return result;
        }

        [HttpGet("paymentStatus/{statusId}")]
        public async Task<ApiResult<PaymentStatusResultViewModel>> GetPaymentStatus(long statusId, CancellationToken cancellationToken)
        {
            PaymentStatusResultViewModel result = await _paymentService.GetPaymentStatus(statusId, cancellationToken);
            return result;
        }


        [HttpPut("paymentStatus/{statusId}")]
        public async Task<ApiResult<PaymentStatusResultViewModel>> GetAllPaymentStatus(long statusId, PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            PaymentStatusResultViewModel result = await _paymentService.UpdatePaymentStatus(statusId, inputViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("paymentStatus/{statusId}")]
        public async Task<bool> DeletePaymentStatus(long statusId, CancellationToken cancellationToken)
        {
            bool result = await _paymentService.DeletePaymentStatus(statusId, cancellationToken);
            return result;
        }

        #endregion
    }
}
