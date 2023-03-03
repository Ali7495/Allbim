using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Insurance;
using Models.Product;
using Models.QueryParams;
using Services.PipeLine;
using System;
using Models.PolicyRequest;

namespace Services.PolicyRequestStatus
{
    public interface IPolicyRequestStatusService
    {
        Task<List<PolicyRequestStatusViewModel>> GetPolicyRequestStatus(CancellationToken cancellationToken);
    }
}