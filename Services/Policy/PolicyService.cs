using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.PageAble;
using Models.Policy;
using System;
using System.Threading;
using System.Threading.Tasks;
using Policyy = DAL.Models.Policy;

namespace Services.Policy
{
    public class PolicyService : IPolicyService
    {
        #region Fields
        private readonly IMapper _mapper;

        private readonly IRepository<Policyy> _policyRepository;
        private readonly IRepository<PolicyHolder> _policyHolderRepository;
        private readonly IRepository<PolicyDetail> _policyDetailRepository;
        private readonly IRepository<Insurer> _insurerRepository;
        private readonly IRepository<PolicyHolderCompany> _policyHolderCompanyRepository;
        private readonly IRepository<PolicyHolderPerson> _policyHolderPersonRepository;
        #endregion

        #region CTOR

        public PolicyService(IRepository<Policyy> policyRepository, IRepository<PolicyDetail> policyDetailRepository, IRepository<Insurer> insurerRepository, IRepository<PolicyHolder> policyHolderRepository, IRepository<PolicyHolderCompany> policyHolderCompanyRepository, IRepository<PolicyHolderPerson> policyHolderPersonRepository, IMapper mapper)
        {
            _mapper = mapper;
            _policyHolderPersonRepository = policyHolderPersonRepository;
            _policyHolderCompanyRepository = policyHolderCompanyRepository;
            _policyHolderRepository = policyHolderRepository;
            _policyRepository = policyRepository;
            _policyDetailRepository = policyDetailRepository;
            _insurerRepository = insurerRepository;
        }
        #endregion

        #region Create
        public async Task<PolicyViewModel> CreatePolicyAsync(PolicyViewModel policyViewModel, CancellationToken cancellationToken)
        {
            var insurerIdIsValid = await _insurerRepository.GetByIdAsync(cancellationToken, policyViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر");

            Policyy policy = new Policyy
            {
                Code = policyViewModel.Code,
                Title = policyViewModel.Title,
                PolicyNumber = policyViewModel.PolicyNumber,
                Description = policyViewModel.Description,
                CreatedBy = policyViewModel.CreatedBy,
                InsurerId = policyViewModel.InsurerId
            };

            await _policyRepository.AddAsync(policy, cancellationToken);

            return _mapper.Map<PolicyViewModel>(policy); ;
        }
        public async Task<PolicyDetailViewModel> CreatePolicyDetailAsync(PolicyDetailViewModel policyDetailViewModel, CancellationToken cancellationToken)
        {
            var policyIdIsValid = await _policyRepository.GetByIdAsync(cancellationToken, policyDetailViewModel.PolicyId) != null;
            if (!policyIdIsValid)
                throw new CustomException("بیمه نامه");

            PolicyDetail policyDetail = new PolicyDetail
            {
                Type = policyDetailViewModel.Type,
                Field = policyDetailViewModel.Field,
                Criteria = policyDetailViewModel.Criteria,
                Value = policyDetailViewModel.Value,
                Discount = policyDetailViewModel.Discount,
                CalculationType = policyDetailViewModel.CalculationType,
                CreatedBy = policyDetailViewModel.CreatedBy,
                PolicyId = policyDetailViewModel.PolicyId
            };

            await _policyDetailRepository.AddAsync(policyDetail, cancellationToken);

            return _mapper.Map<PolicyDetailViewModel>( policyDetail);
        }
        #endregion

        #region Update
        public async Task<PolicyViewModel> UpdatePolicyAsync(long id, PolicyViewModel policyViewModel, CancellationToken cancellationToken)
        {
            var policy = await _policyRepository.GetByIdAsync(cancellationToken, id);
            if (policy == null)
                throw new CustomException("بیمه نامه");

            var insurerIdIsValid = await _insurerRepository.GetByIdAsync(cancellationToken, policyViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر ");

            policy.Code = policyViewModel.Code;
            policy.Title = policyViewModel.Title;
            policy.PolicyNumber = policyViewModel.PolicyNumber;
            policy.Description = policyViewModel.Description;
            policy.InsurerId = policyViewModel.InsurerId;
            policy.UpdatedAt = DateTime.Now;
            policy.UpdatedBy = policyViewModel.CreatedBy;

            await _policyRepository.UpdateAsync(policy, cancellationToken);

            return _mapper.Map<PolicyViewModel>(policy); 
        }
        public async Task<PolicyDetailViewModel> UpdatePolicyDetailAsync(long id, PolicyDetailViewModel policyDetailViewModel, CancellationToken cancellationToken)
        {
            var policyDetail = await _policyDetailRepository.GetByIdAsync(cancellationToken, id);
            if (policyDetail == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyIdIsValid = await _policyRepository.GetByIdAsync(cancellationToken, policyDetailViewModel.PolicyId) != null;
            if (!policyIdIsValid)
                throw new CustomException("بیمه نامه ");


            policyDetail.Type = policyDetailViewModel.Type;
            policyDetail.Field = policyDetailViewModel.Field;
            policyDetail.Criteria = policyDetailViewModel.Criteria;
            policyDetail.Value = policyDetailViewModel.Value;
            policyDetail.Discount = policyDetailViewModel.Discount;
            policyDetail.CalculationType = policyDetailViewModel.CalculationType;
            policyDetail.PolicyId = policyDetailViewModel.PolicyId;
            policyDetail.UpdatedBy = policyDetailViewModel.UpdatedBy;
            policyDetail.UpdatedAt = DateTime.Now;

            await _policyDetailRepository.UpdateAsync(policyDetail, cancellationToken);

            return _mapper.Map<PolicyDetailViewModel>(policyDetail); 
        }
        public async Task<PolicyHolderViewModel> UpdatePolicyHolderAsync(long id, PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyIdIsValid = await _policyHolderRepository.GetByIdAsync(cancellationToken, ViewModel.PolicyId) != null;
            if (!policyIdIsValid)
                throw new CustomException("بیمه نامه ");


            model.PolicyId = ViewModel.PolicyId;
            model.PolicyId = ViewModel.PolicyId;
            model.UpdatedBy = ViewModel.UpdatedBy;
            model.UpdatedAt = DateTime.Now;

            await _policyHolderRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyHolderViewModel>(model); 
        }
        public async Task<PolicyHolderCompanyViewModel> UpdatePolicyHolderCompanyAsync(long id, PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyIdIsValid = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, ViewModel.Id) != null;
            if (!policyIdIsValid)
                throw new CustomException("بیمه نامه ");

            model.PolicyHolderId = ViewModel.PolicyHolderId;
            model.CompanyId = ViewModel.CompanyId;

            model.UpdatedAt = DateTime.Now;

            await _policyHolderCompanyRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyHolderCompanyViewModel>(model); 
        }
   

        #endregion

        #region Get
        public async Task<PolicyViewModel> GetPolicyAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("بیمه نامه ");

            return _mapper.Map<PolicyViewModel>(model);
        }
        public async Task<PagedResult<PolicyViewModel>> GetAllPoliciesAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<Policyy> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map< PagedResult<PolicyViewModel>>(model); 
        }


        public async Task<PolicyDetailViewModel> GetPolicyDetailAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyDetailRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyDetailViewModel>(model); 
        }
        public async Task<PagedResult<PolicyDetailViewModel>> GetAllPolicyDetailsAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyDetail> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyDetailRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyDetailRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map< PagedResult<PolicyDetailViewModel>>(model);
        }

        public async Task<PagedResult<PolicyHolderViewModel>> GetAllPolicyHolderAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyHolder> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyHolderRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyHolderRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyHolderViewModel>>(model); 
        }
        public async Task<PagedResult<PolicyHolderCompanyViewModel>> GetAllPolicyHolderCompanyAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyHolderCompany> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyHolderCompanyRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyHolderCompanyRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyHolderCompanyViewModel>>(model);
        }

        public async Task<PolicyHolderViewModel> GetPolicyHolderAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyHolderViewModel>(model);
        }
        public async Task<PolicyHolderCompanyViewModel> GetPolicyHolderCompanyAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyHolderCompanyViewModel>(model);
        }
        #endregion

        #region Delete 
        public async Task<bool> DeleteDeletePolicyHolderPersonAsync(long id, CancellationToken cancellationToken)
        {
            var policy = await _policyHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (policy == null)
                throw new CustomException("بیمه نامه ");

            await _policyHolderPersonRepository.DeleteAsync(policy, cancellationToken);
            return true;
        }
        public async Task<bool> DeletePolicyAsync(long id, CancellationToken cancellationToken)
        {
            var policy = await _policyRepository.GetByIdAsync(cancellationToken, id);
            if (policy == null)
                throw new CustomException("بیمه نامه ");

            await _policyRepository.DeleteAsync(policy, cancellationToken);
            return true;
        }
        public async Task<bool> DeletePolicyDetailAsync(long id, CancellationToken cancellationToken)
        {
            var policyDetail = await _policyDetailRepository.GetByIdAsync(cancellationToken, id);
            if (policyDetail == null)
                throw new CustomException("جزئیات بیمه نامه");

            await _policyDetailRepository.DeleteAsync(policyDetail, cancellationToken);

            return true;
        }

        public async Task<bool> DeletePolicyHolderAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات holder بیمه نامه");

            await _policyHolderRepository.DeleteAsync(model, cancellationToken);
            return true;
        }
        public async Task<bool> DeletePolicyHolderCompanyAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات  بیمه نامه");

            await _policyHolderCompanyRepository.DeleteAsync(model, cancellationToken);
            return true;
        }
        public async Task<PolicyHolderViewModel> CreatePolicyHolderAsync(PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyHolderRepository.GetByIdAsync(cancellationToken, ViewModel.Id) != null;
            if (!model)
                throw new CustomException("بیمه گر");

            PolicyHolder policy = new PolicyHolder
            {
                PolicyId = ViewModel.PolicyId,
            };
            await _policyHolderRepository.AddAsync(policy, cancellationToken);
            return _mapper.Map<PolicyHolderViewModel>(policy);

        }

        public async Task<PolicyHolderCompanyViewModel> CreatePolicyHolderCompanyAsync(PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var data = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, ViewModel.Id) != null;
            if (!data)
                throw new CustomException("بیمه گر");

            PolicyHolderCompany model = new PolicyHolderCompany()
            {
                CompanyId = ViewModel.CompanyId,
            };
            await _policyHolderCompanyRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<PolicyHolderCompanyViewModel>(model); 
        }
        public async Task<PolicyHolderPersonViewModel> CreatePolicyHolderPersonAsync(PolicyHolderPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            var data = await _policyHolderPersonRepository.GetByIdAsync(cancellationToken, viewModel.Id) != null;
            if (!data)
                throw new CustomException("بیمه گر");

            PolicyHolderPerson model = new PolicyHolderPerson()
            {
                PolicyHolderId = viewModel.PolicyHolderId,
                PersonId = viewModel.PersonId,
            };
            await _policyHolderPersonRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<PolicyHolderPersonViewModel>(model);
        }

        public async Task<bool> DeletePolicyHolderPersonAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات  بیمه نامه");

            await _policyHolderCompanyRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<PolicyHolderPersonViewModel> UpdatePolicyHolderPersonAsync(long id, PolicyHolderPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _policyHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyIdIsValid = await _policyHolderRepository.GetByIdAsync(cancellationToken, viewModel.Id) != null;
            if (!policyIdIsValid)
                throw new CustomException("بیمه نامه ");


            model.PersonId = viewModel.PersonId;
            model.PolicyHolderId = viewModel.PolicyHolderId;
  
            model.UpdatedAt = DateTime.Now;

            await _policyHolderPersonRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyHolderPersonViewModel>(model);
        }

        public async Task<PolicyHolderPersonViewModel> GetPolicyHolderPersonAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyHolderPersonViewModel>(model);
        }

        public async Task<PagedResult<PolicyHolderPersonViewModel>> GetAllPolicyHolderPersonAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyHolderPerson> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyHolderPersonRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyHolderPersonRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map< PagedResult<PolicyHolderPersonViewModel>>(model);
        }


        #endregion
    }
}
