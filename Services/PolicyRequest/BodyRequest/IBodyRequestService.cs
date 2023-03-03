using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Policy;
using Models.PolicyRequest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Insurance;
using Models.Product;
using Models.QueryParams;
using Services.PipeLine;
using System;
using Models.PolicyRequestIssue;
using Models.PolicyRequestSupplement;
using Models.PolicyRequestInspection;
using Models.PolicyRequestPaymentInfo;
using Models.BodySupplementInfo;

namespace Services.PolicyRequest
{
    public interface IBodyRequestService
    {

        #region PaymentInfo

        Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetails(Guid code,
            CancellationToken cancellationToken);
        Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetailsMine(long userId, Guid code,
            CancellationToken cancellationToken);

        #endregion


    }
}