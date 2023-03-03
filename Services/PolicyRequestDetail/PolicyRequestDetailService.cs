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
    public class PolicyRequestDetailService : IPolicyRequestDetailService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly SiteSettings _siteSettings;
        private readonly IRepository<PolicyRequestDetail> _policyRequestDetailRepository;
        #endregion

        #region CTOR

        public PolicyRequestDetailService( IRepository<PolicyRequestDetail> policyRequestDetailRepository,   IMapper mapper, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _siteSettings = siteSettings.Value;
            _mapper = mapper;
            _policyRequestDetailRepository = policyRequestDetailRepository;
        }

        public async Task<PolicyRequestDetailViewModel> Create(PolicyRequestDetailViewModel viewModel, CancellationToken cancellationToken)
        {
            try
            {
                PolicyRequestDetail policyRequestDetail = new PolicyRequestDetail
                {
                    CalculationType = viewModel.CalculationType,
                    Discount = viewModel.Discount,
                    Criteria = viewModel.Criteria,
                    Field = viewModel.Field,
                    InsurerTermId = viewModel.InsurerTermId,
                    PolicyRequestId = viewModel.PolicyRequestId,
                    Type = viewModel.Type,
                    Value = viewModel.Value
                };

                await _policyRequestDetailRepository.AddAsync(policyRequestDetail, cancellationToken);

                return _mapper.Map<PolicyRequestDetailViewModel>(policyRequestDetail); 
            }
            catch (Exception)
            {

                return null;
            }
           

           
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException(" خطا در دریافت اطلاعات");

            await _policyRequestDetailRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<PagedResult<PolicyRequestDetailViewModel>> GetAll(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<DAL.Models.PolicyRequestDetail> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestDetailRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyRequestDetailRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestDetailViewModel>>(model);
        }

        public async Task<PolicyRequestDetailViewModel> GetPolicyDetail(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException(" خطا دردریافت اطلاعات  ");

            return _mapper.Map<PolicyRequestDetailViewModel>(model);
        }

        public async Task<PolicyRequestDetailViewModel> Update(long id, PolicyRequestDetailViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("خطا در دریافت اطلاعات ");
            model.InsurerTermId = viewModel.InsurerTermId;
            model.CalculationType = viewModel.CalculationType;
            model.Criteria = viewModel.Criteria;
            model.Discount = viewModel.Discount;
            model.Field = viewModel.Field;
            model.PolicyRequestId = viewModel.PolicyRequestId;
            model.Type = viewModel.Type;
            model.UserInput = viewModel.UserInput;
            model.Value = viewModel.Value;
            await _policyRequestDetailRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestDetailViewModel>(model);
        }
        #endregion

    }
}
