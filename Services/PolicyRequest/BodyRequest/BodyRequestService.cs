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
using Models.Insurance;
using Models.Product;
using Models.QueryParams;
using Services.PipeLine;
using System.Reflection;
using System.Transactions;
using DAL.Repositories;
using Models.PolicyRequestIssue;
using Models.PolicyRequestSupplement;
using Models.PolicyRequestInspection;
using Models.PolicyRequestPaymentInfo;
using Models.BodySupplementInfo;
using Models.Person;
using Models.MultipleDiscount;
using Common.Extensions;

namespace Services.PolicyRequest
{
    public class BodyRequestService : IBodyRequestService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly SiteSettings _siteSettings;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IRepository<DAL.Models.PolicyRequest> _policyRepository;
        private readonly IRepository<PolicyRequestDetail> _policyRequestDetailRepository;
        private readonly IRepository<PolicyRequestHolderCompany> _policyRequestHolderCompanyRepository;
        private readonly IRepository<PolicyRequestHolderPerson> _policyRequestHolderPersonRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IPolicyRequestAttachmentRepository _policyRequestAttachmentRepository;
        private readonly ICentralRulesRepository _centralRulesRepository;


        private readonly IPaymentService _paymentService;
        private readonly IInsurerRepository _insurerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPolicyRequestHolderRepository _policyRequestHolderRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IInsuredRequestVehicleRepository _insuredRequestVehicleRepository;
        private readonly IPolicyRequestIssueRepository _policyRequestIssueRepository;
        private readonly IPolicyRequestInspectionRepository _policyRequestInspectionRepository;
        private readonly IPolicyRequestFactorRepository _policyRequestFactorRepository;
        private readonly IPolicyRequestDetailRepository _policyRequestPaymentDetailRepository;
        private readonly IRepository<VehicleType> _vehicleType;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IRepository<PersonAddress> _personAddressRepository;
        private readonly IPolicyRequestStatusRepository _policyRequestStatusRepository;

        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;
        private readonly IPersonAddressService _personAddressService;
        private readonly IPolicyRequestCommentRepository _policyRequestCommentRepository;

        #endregion

        #region CTOR

        public BodyRequestService(IPolicyRequestRepository policyRequestRepository,
            IRepository<PolicyRequestDetail> policyRequestDetailRepository,
            IPolicyRequestCommentRepository policyRequestCommentRepository,
            IPolicyRequestHolderRepository policyRequestHolderRepository,
            IRepository<PolicyRequestHolderCompany> policyRequestHolderCompanyRepository,
            IRepository<PolicyRequestHolderPerson> policyRequestHolderPersonRepository, IMapper mapper,
            IOptionsSnapshot<SiteSettings> siteSettings, IAttachmentService attachmentService,
            IAttachmentRepository attachmentRepository,
            IPolicyRequestAttachmentRepository policyRequestAttachmentRepository,
            IInsuranceRepository insuranceRepository, IVehicleRepository vehicleRepository,
            IPaymentService paymentService, IRepository<DAL.Models.PolicyRequest> policyRepository,
            IInsurerRepository insurerRepository1, IUserRepository userRepository,
            IInsurerRepository insurerRepository, IPersonRepository personRepository,
            IAddressRepository addressRepository, IInsuredRequestVehicleRepository insuredRequestVehicleRepository,
            IPolicyRequestIssueRepository policyRequestIssueRepository,
            IPolicyRequestInspectionRepository policyRequestInspectionRepository,
            IPolicyRequestFactorRepository policyRequestFactorRepository,
            IPolicyRequestDetailRepository policyRequestPaymentDetailRepository, IRepository<VehicleType> vehicleType,
            ICompanyRepository companyRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository
            , IPersonCompanyRepository personCompanyRepository, ICentralRulesRepository centralRulesRepository,
            IPersonService personService, IAddressService addressService,
            IRepository<PersonAddress> personAddressRepository,
            IPersonAddressService personAddressService,
            IPolicyRequestStatusRepository policyRequestStatusRepository)
        {
            _policyRequestAttachmentRepository = policyRequestAttachmentRepository;
            _insuranceRepository = insuranceRepository;
            _vehicleRepository = vehicleRepository;
            _attachmentRepository = attachmentRepository;
            _policyRequestCommentRepository = policyRequestCommentRepository;
            _attachmentService = attachmentService;
            _siteSettings = siteSettings.Value;
            _mapper = mapper;
            _policyRequestHolderPersonRepository = policyRequestHolderPersonRepository;
            _policyRequestHolderCompanyRepository = policyRequestHolderCompanyRepository;
            _policyRequestHolderRepository = policyRequestHolderRepository;
            _policyRequestRepository = policyRequestRepository;
            _policyRequestDetailRepository = policyRequestDetailRepository;
            _insurerRepository = insurerRepository;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _insuredRequestVehicleRepository = insuredRequestVehicleRepository;
            _policyRequestIssueRepository = policyRequestIssueRepository;
            _paymentService = paymentService;
            _policyRepository = policyRepository;
            _userRepository = userRepository;

            _policyRequestInspectionRepository = policyRequestInspectionRepository;
            _policyRequestFactorRepository = policyRequestFactorRepository;
            _policyRequestPaymentDetailRepository = policyRequestPaymentDetailRepository;
            _vehicleType = vehicleType;
            _companyRepository = companyRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _personCompanyRepository = personCompanyRepository;
            _centralRulesRepository = centralRulesRepository;
            _personService = personService;
            _addressService = addressService;
            _personAddressRepository = personAddressRepository;
            _personAddressService = personAddressService;
            _policyRequestStatusRepository = policyRequestStatusRepository;
        }

        #endregion


        #region PaymentInfo

        public async Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetails(Guid code,
            CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetPaymentDetailsByCode(code, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست بیمه وجود ندارد");
            }

            PolicyRequestPaymentViewModel result = await GetPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }


        public async Task<bool> IsUserValidCommon(long userId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                return false;
            }

            return true;
        }


        public async Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetailsMine(long userId, Guid code,
           CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }


            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetPaymentDetailsByCode(code, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست بیمه وجود ندارد");
            }

            PolicyRequestPaymentViewModel result = await GetPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }





        public async Task<PolicyRequestPaymentViewModel> GetPaymentInfoCommon(DAL.Models.PolicyRequest policyRequest, CancellationToken cancellationToken)
        {
            List<PolicyRequestDetail> details =
                await _policyRequestPaymentDetailRepository.GetDetailsByPolicyRequestId(policyRequest.Id,
                    cancellationToken);


            string VehicleId = details.FirstOrDefault(d => d.Field == "VehicleId")?.UserInput;
            string VehicleConstructionYear =
                details.FirstOrDefault(d => d.Field == "VehicleConstructionYear")?.UserInput;
            Vehicle vehicle = null;
            if (VehicleId.HasValue())
            {
                vehicle = await _vehicleRepository.GetWithRuleCategoryAndBrandAndType(
                    long.Parse(VehicleId), cancellationToken);
            }


            List<PolicyRequestPaymentDetailViewModel> paymentDetails = new List<PolicyRequestPaymentDetailViewModel>();
            paymentDetails.Add(new PolicyRequestPaymentDetailViewModel()
            {
                Field = "VehicleType",
                Value = vehicle?.VehicleBrand?.VehicleType?.Name
            });
            paymentDetails.Add(new PolicyRequestPaymentDetailViewModel()
            {
                Field = "VehicleModel",
                Value = vehicle?.VehicleBrand?.Name + " " + vehicle?.Name
            });
            paymentDetails.Add(new PolicyRequestPaymentDetailViewModel()
            {
                Field = "ConstructionYear",
                Value = VehicleConstructionYear
            });
            PolicyRequestPaymentViewModel model = new PolicyRequestPaymentViewModel()
            {
                Insurer = policyRequest.Insurer.Insurance.Name,
                PaymentDetailViewModels = paymentDetails,
                //PaymentPrice = policyRequest.PolicyRequestFactors.FirstOrDefault().Payment.Price.ToString()
            };

            return model;
        }

        #endregion

    }
}