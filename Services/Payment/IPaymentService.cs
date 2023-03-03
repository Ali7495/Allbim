using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Payment;
using Models.PaymentGateway;
using Models.PaymentStatus;
using Models.Policy;
using Models.PolicyRequest;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.PolicyRequest
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> Create(PaymentViewModel viewModel, Guid code, CancellationToken cancellationToken);
        Task<PaymentViewModel> GetPolicyFactor(long id, CancellationToken cancellationToken);
        Task<List<PaymentStatusResultViewModel>> GetAllPaymentStatus(CancellationToken cancellationToken);
        Task<PaymentViewModel> GetById(long id, CancellationToken cancellationToken);
        Task<PaymentViewModel> Update(Guid code, long factorId, PaymentViewModel viewmodel, CancellationToken cancellationToken);
        Task<bool> Delete(long factorId, CancellationToken cancellationToken);
        Task<PolicyRequestFactorViewModel> GetByFactorId(Guid code, long id, CancellationToken cancellationToken);
        Task<PagedResult<PaymentFactorViewModel>> GetAllPayments(Guid? companyCode, long? statusId,PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<PaymentFactorViewModel>> GetAllPaymentsMine(long userId, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<GetewayResultViewModel> CreatePaymentGateway(GatewayInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<PagedResult<GetewayResultViewModel>> GetAllPaymentGatewaysPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<List<GetewayResultViewModel>> GetAllPaymentGatewaysList(CancellationToken cancellationToken);
        Task<GetewayResultViewModel> GetPaymentGateWayWithDetails(long id, CancellationToken cancellationToken);
        Task<GetewayResultViewModel> UpdatePaymentGateway(long id, GatewayUpdateInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<bool> DeleteGateway(long id, CancellationToken cancellationToken);


        Task<PaymentStatusResultViewModel> CreatePaymentStatus(PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<PaymentStatusResultViewModel> UpdatePaymentStatus(long id, PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<PaymentStatusResultViewModel> GetPaymentStatus(long id, CancellationToken cancellationToken);
        Task<bool> DeletePaymentStatus(long id, CancellationToken cancellationToken);
    }
}