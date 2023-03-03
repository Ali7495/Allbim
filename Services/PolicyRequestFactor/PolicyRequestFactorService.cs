using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.PageAble;
using Models.Policy;
using Models.PolicyRequest;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Services.PolicyRequest
{
    public class PolicyRequestFactorService : IPolicyRequestFactorService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly SiteSettings _siteSettings;
        private readonly IRepository<Payment> _paymentService;
        private readonly IPolicyRequestRepository _policyRepository;
        private readonly IPolicyRequestFactorRepository _policyRequestFactorRepository;
        //private readonly IPaymentService _paymentService;
        #endregion

        #region CTOR

        public PolicyRequestFactorService(IMapper mapper, IOptionsSnapshot<SiteSettings> siteSettings, IPolicyRequestRepository policyRepository, IPolicyRequestFactorRepository policyRequestFactorRepository, IRepository<Payment> paymentService)
        {
            _paymentService = paymentService;
            _policyRepository = policyRepository;
            _siteSettings = siteSettings.Value;
            _mapper = mapper;
            _policyRequestFactorRepository = policyRequestFactorRepository;
        }

        #endregion
        public async Task<PolicyRequestFactorViewModel> Create(PolicyRequestFactorViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestFactor policyRequestFactor = new PolicyRequestFactor
            {
                PaymentId = viewModel.PaymentId,

            };

            await _policyRequestFactorRepository.AddAsync(policyRequestFactor, cancellationToken);

            return _mapper.Map<PolicyRequestFactorViewModel>(policyRequestFactor);


        }

        public async Task<bool> Delete(Guid code, long id, CancellationToken cancellationToken)
        {

            DAL.Models.PolicyRequest model = await _policyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (model == null)
                throw new BadRequestException(" خطا در دریافت اطلاعات");
            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByPolicyIdFactorId(id, model.Id, cancellationToken);
            if (factor == null)
                throw new BadRequestException(" خطا در دریافت اطلاعات");
            Payment payment = await _paymentService.GetByIdAsync(cancellationToken, factor.PaymentId);
            var pay = _paymentService.DeleteAsync(payment, cancellationToken);
            await _policyRequestFactorRepository.DeleteAsync(factor, cancellationToken);
            return true;


        }



        public async Task<PagedResult<PolicyRequestFactorViewModel>> GetAll(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<DAL.Models.PolicyRequestFactor> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestFactorRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyRequestFactorRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestFactorViewModel>>(model);
        }

        public Task<PolicyRequestFactorViewModel> GetByCode(Guid code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<PolicyRequestFactorViewModel> GetByCode(Guid code, CancellationToken cancellationToken)
        //{
        //    var model = await _policyRepository.GetByCode(code, cancellationToken);
        //    if (model == null)
        //        throw new BadRequestException(" خطا دردریافت اطلاعات  ");
        //    PolicyRequestFactor data = await _policyRequestFactorRepository.GetFactorsByPolicyId(model.Id, cancellationToken);
        //    return _mapper.Map<PolicyRequestFactorViewModel>(data);
        //}



        public async Task<PolicyRequestFactorViewModel> GetPolicyFactor(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestFactorRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new BadRequestException(" خطا دردریافت اطلاعات  ");

            return _mapper.Map<PolicyRequestFactorViewModel>(model);
        }

        public async Task<PolicyRequestFactorViewModel> Update(long id, PolicyRequestFactorViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestFactorRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new BadRequestException("خطا در دریافت اطلاعات ");
            model.PaymentId = viewModel.PaymentId;

            await _policyRequestFactorRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestFactorViewModel>(model);
        }


    }
}
