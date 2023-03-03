using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.PageAble;
using Models.Payment;
using Models.PaymentGateway;
using Models.PaymentStatus;
using Models.Policy;
using Models.PolicyRequest;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.PolicyRequest
{
    public class PaymentService : IPaymentService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly SiteSettings _siteSettings;
        private readonly IPaymentRepository _peymentRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IPolicyRequestFactorRepository _policyRequestFactorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IPaymentGatewayRepository _paymentGatewayRepository;

        #endregion

        #region CTOR

        public PaymentService(IPaymentRepository peymentRepository, IMapper mapper, IOptionsSnapshot<SiteSettings> siteSettings, IPolicyRequestRepository policyRequestRepository, IPolicyRequestFactorRepository policyRequestFactorRepository, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IPaymentStatusRepository paymentStatusRepository, IPaymentGatewayRepository paymentGatewayRepository)
        {
            _policyRequestFactorRepository = policyRequestFactorRepository;
            _siteSettings = siteSettings.Value;
            _mapper = mapper;
            _peymentRepository = peymentRepository;
            _policyRequestRepository = policyRequestRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentGatewayRepository = paymentGatewayRepository;
        }

        public async Task<PaymentViewModel> Create(PaymentViewModel viewModel, Guid code, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest data = await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);
                if (data == null)
                    throw new NotFoundException("کد  وجود ندارد");

                Payment model = new Payment
                {
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now,
                    PaymentCode = viewModel.PaymentCode,
                    Price = viewModel.Price,
                    PaymentStatusId = viewModel.PaymentStatusId,
                    Description = viewModel.Description
                };

                await _peymentRepository.AddAsync(model, cancellationToken);
                PolicyRequestFactor factor = new PolicyRequestFactor
                {
                    PaymentId = model.Id,
                    PolicyRequestId = data.Id
                };
                await _policyRequestFactorRepository.AddAsync(factor, cancellationToken);

                transaction.Complete();

                return _mapper.Map<PaymentViewModel>(model);
            }

        }

        public async Task<GetewayResultViewModel> CreatePaymentGateway(GatewayInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            PaymentGateway paymentGateway = _mapper.Map<PaymentGateway>(inputViewModel);

            await _paymentGatewayRepository.AddAsync(paymentGateway, cancellationToken);

            return _mapper.Map<GetewayResultViewModel>(paymentGateway);
        }

        public async Task<PaymentStatusResultViewModel> CreatePaymentStatus(PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            PaymentStatus paymentStatus = _mapper.Map<PaymentStatus>(inputViewModel);

            await _paymentStatusRepository.AddAsync(paymentStatus,cancellationToken);

            return _mapper.Map<PaymentStatusResultViewModel>(paymentStatus);
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            Payment model = await _peymentRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new BadRequestException(" خطا در دریافت اطلاعات");

            await _peymentRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteGateway(long id, CancellationToken cancellationToken)
        {
            PaymentGateway paymentGateway = await _paymentGatewayRepository.GetGatewayByIdWithDetailsNoTraking(id, cancellationToken);
            if (paymentGateway == null)
            {
                throw new BadRequestException("این درگاه وجود ندارد");
            }

            paymentGateway.IsDeleted = true;
            paymentGateway.PaymentGatewayDetails.ToList().ForEach(d => d.IsDeleted = true);

            await _paymentGatewayRepository.UpdateAsync(paymentGateway,cancellationToken);

            return true;
        }

        public async Task<bool> DeletePaymentStatus(long id, CancellationToken cancellationToken)
        {
            PaymentStatus paymentStatus = await _paymentStatusRepository.GetStatusByIdNoTracking(id, cancellationToken);
            if (paymentStatus == null)
            {
                throw new BadRequestException("این وضعیت وجود ندارد");
            }

            paymentStatus.IsDeleted = true;

            await _paymentStatusRepository.UpdateAsync(paymentStatus, cancellationToken);

            return true;
        }

        public async Task<List<GetewayResultViewModel>> GetAllPaymentGatewaysList(CancellationToken cancellationToken)
        {
            List<PaymentGateway> paymentGateways = await _paymentGatewayRepository.GetAllGatewaysWithDetailsList(cancellationToken);

            return _mapper.Map<List<GetewayResultViewModel>>(paymentGateways);
        }

        public async Task<PagedResult<GetewayResultViewModel>> GetAllPaymentGatewaysPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<PaymentGateway> gateways = await _paymentGatewayRepository.GetAllGatewaysWithDetailsPaging(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<GetewayResultViewModel>>(gateways);
        }

        public async Task<PagedResult<PaymentFactorViewModel>> GetAllPayments(Guid? companyCode, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<PolicyRequestFactor> factors = new PagedResult<PolicyRequestFactor>();

            if (statusId != null && companyCode != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByAllParameters(companyCode.Value, statusId.Value, pageAbleModel, cancellationToken);
            }
            else if (companyCode != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByCompany(companyCode.Value, pageAbleModel, cancellationToken);
            }
            else if (statusId != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByStatus(statusId.Value, pageAbleModel, cancellationToken);
            }
            else
            {
                factors = await _policyRequestFactorRepository.GetAllFactors(pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<PaymentFactorViewModel>>(factors);
        }

        public async Task<PagedResult<PaymentFactorViewModel>> GetAllPaymentsMine(long userId, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdNoTracking(userId, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException(" کاربری با این مشخصات وجود ندارد");
            }

            UserRole userRole = await _userRoleRepository.GetSingleUserRoleByUserId(userId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("شما در این سیستم نقشی ندارید");
            }

            if (userRole.Role.Name != "User")
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<PolicyRequestFactor> factors = new PagedResult<PolicyRequestFactor>();

            if (statusId != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByPersonAndStatusId(user.PersonId, statusId.Value, pageAbleModel, cancellationToken);
            }
            else
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByPersonId(user.PersonId, pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<PaymentFactorViewModel>>(factors);
        }

        public async Task<List<PaymentStatusResultViewModel>> GetAllPaymentStatus(CancellationToken cancellationToken)
        {
            List<PaymentStatus> statuses = await _paymentStatusRepository.GetAllStatusesNoTraking(cancellationToken);

            return _mapper.Map<List<PaymentStatusResultViewModel>>(statuses);
        }

        public async Task<PolicyRequestFactorViewModel> GetByFactorId(Guid code, long id, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest model = await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);
            if (model == null)
                throw new BadRequestException("خطا در دریافت اطلاعات ");
            PolicyRequestFactor data = await _policyRequestFactorRepository.GetByIdNoTracking(id, cancellationToken);
            return _mapper.Map<PolicyRequestFactorViewModel>(data);
        }

        public Task<PaymentViewModel> GetById(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<GetewayResultViewModel> GetPaymentGateWayWithDetails(long id, CancellationToken cancellationToken)
        {
            PaymentGateway paymentGateway = await _paymentGatewayRepository.GetGatewayByIdWithDetailsNoTraking(id, cancellationToken);

            return _mapper.Map<GetewayResultViewModel>(paymentGateway);
        }

        public async Task<PaymentStatusResultViewModel> GetPaymentStatus(long id, CancellationToken cancellationToken)
        {
            PaymentStatus paymentStatus = await _paymentStatusRepository.GetStatusByIdNoTracking(id, cancellationToken);

            return _mapper.Map<PaymentStatusResultViewModel>(paymentStatus);
        }

        public async Task<PaymentViewModel> GetPolicyFactor(long id, CancellationToken cancellationToken)
        {
            var model = await _peymentRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new BadRequestException(" خطا دردریافت اطلاعات  ");

            return _mapper.Map<PaymentViewModel>(model);
        }

        public async Task<PaymentViewModel> Update(Guid code, long id, PaymentViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest model = await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);
            if (model == null)
                throw new BadRequestException("خطا در دریافت اطلاعات ");
            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByPolicyIdFactorId(id, model.Id, cancellationToken);
            if (factor == null)
                throw new BadRequestException("خطا در دریافت اطلاعات ");
            Payment peyment = await _peymentRepository.GetByIdAsync(cancellationToken, factor.PaymentId);
            if (peyment == null)
                throw new BadRequestException("خطا در دریافت اطلاعات ");


            peyment.CreatedDateTime = viewModel.CreatedDateTime;
            peyment.UpdatedDateTime = viewModel.UpdatedDateTime;
            peyment.PaymentCode = viewModel.PaymentCode;
            peyment.Price = viewModel.Price;
            peyment.PaymentStatusId = viewModel.PaymentStatusId;
            peyment.Description = viewModel.Description;

            await _peymentRepository.UpdateAsync(peyment, cancellationToken);

            return _mapper.Map<PaymentViewModel>(peyment);
        }

        public async Task<GetewayResultViewModel> UpdatePaymentGateway(long id, GatewayUpdateInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            PaymentGateway paymentGateway = await _paymentGatewayRepository.GetGatewayByIdWithDetailsNoTraking(id, cancellationToken);
            if (paymentGateway == null)
            {
                throw new BadRequestException("این درگاه وجود ندارد");
            }

            paymentGateway = _mapper.Map<PaymentGateway>(inputViewModel);
            paymentGateway.Id = id;

            await _paymentGatewayRepository.UpdateAsync(paymentGateway, cancellationToken);

            return _mapper.Map<GetewayResultViewModel>(paymentGateway);
        }

        public async Task<PaymentStatusResultViewModel> UpdatePaymentStatus(long id, PaymentStatusInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            PaymentStatus paymentStatus = await _paymentStatusRepository.GetStatusByIdNoTracking(id, cancellationToken);
            if (paymentStatus == null)
            {
                throw new BadRequestException("این وضعیت وجود ندارد");
            }

            paymentStatus = _mapper.Map<PaymentStatus>(inputViewModel);
            paymentStatus.Id = id;

            await _paymentStatusRepository.UpdateAsync(paymentStatus, cancellationToken);

            return _mapper.Map<PaymentStatusResultViewModel>(paymentStatus);
        }
        #endregion

    }
}
