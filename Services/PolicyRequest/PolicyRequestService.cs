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
using Models.PolicyRequestFactor;
using Models.Payment;

namespace Services.PolicyRequest
{
    public class PolicyRequestService : IPolicyRequestService
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
        private readonly IAgentRepository _agentRepository;

        #endregion

        #region CTOR

        public PolicyRequestService(IPolicyRequestRepository policyRequestRepository,
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
            IPolicyRequestStatusRepository policyRequestStatusRepository, IAgentRepository agentRepository)
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
            _agentRepository = agentRepository;
        }

        #endregion


        #region Product

        public async Task<Pipe<ThirdProductInputViewModel, OutputViewModel>> RunThirdPipeLine(ProductInsuranceViewModel insurer,
            OutputViewModel outPut, List<string> StepNames,
            ThirdProductInputViewModel thirdProductInputViewModel,
            CancellationToken cancellationToken)
        {
            Pipe<ThirdProductInputViewModel, OutputViewModel> pipe =
                new Pipe<ThirdProductInputViewModel, OutputViewModel>();
            pipe.insurer = insurer;
            pipe.productRequestViewModel = thirdProductInputViewModel;
            pipe.OutPut = outPut;
            pipe.StepNames = StepNames;

            await pipe.Run();
            return pipe;
        }

        public async Task<Pipe<BodyProductInputViewModel, OutputViewModel>> RunBodyPipeLine(ProductInsuranceViewModel insurer,
            OutputViewModel outPut, List<string> StepNames,
            BodyProductInputViewModel bodyProductInputViewModel,
            CancellationToken cancellationToken)
        {
            Pipe<BodyProductInputViewModel, OutputViewModel>
                pipe = new Pipe<BodyProductInputViewModel, OutputViewModel>();
            pipe.insurer = insurer;
            pipe.productRequestViewModel = bodyProductInputViewModel;
            pipe.OutPut = outPut;
            pipe.StepNames = StepNames;

            await pipe.Run();
            return pipe;
        }


        public async Task<List<ThirdInsuranceResultViewModel>> GetAvailableThirdInsuranceInsurers(
            ThirdProductInputViewModel thirdProductInputViewModel,
            CancellationToken cancellationToken)
        {
            string slug = "third";
            List<ThirdInsuranceResultViewModel> thirdInsuranceResults = new List<ThirdInsuranceResultViewModel>();

            ProductInsuranceViewModel productInsuranceViewModel = new ProductInsuranceViewModel();

            Insurance insurance = await _insuranceRepository.GetInsurancesWithStepsBySlug(slug, cancellationToken);

            List<InsuranceCentralRule> centralRules =
                await _centralRulesRepository.GetByInsuranceSlug(slug, cancellationToken);


            productInsuranceViewModel.InsuranceCentralRules = _mapper.Map<List<ProductCentralRuleViewModel>>(centralRules);



            List<Insurer> insurers = await _insurerRepository.GetAllInsurersByInsuranceId(insurance.Id, cancellationToken);




            //insurance.Insurers = insurers;

            //insurance.InsuranceCentralRules = centralRules;

            if (thirdProductInputViewModel.VehicleId != null)
            {
                Vehicle vehicle =
                    await _vehicleRepository.GetWithVehicleRuleCategory(thirdProductInputViewModel.VehicleId.Value,
                        cancellationToken);
                if (vehicle == null)
                {
                    throw new NotFoundException("vehicle not found");
                }

                thirdProductInputViewModel.VehicleRuleCategoryId = vehicle.VehicleRuleCategoryId;
            }

            foreach (Insurer item in insurers)
            {
                ProductViewModel Product = new ProductViewModel()
                {
                    Price = 0,
                    Title = item.Company.Name,
                    BranchNumber = 5,
                    WealthLevel = 2,
                    LogoUrl = item.Company.Name,
                    DamagePaymentSatisfactionRating = 5,
                };

                OutputViewModel OutPut = new OutputViewModel();
                OutPut.Product = Product;

                List<string> StepNames = insurance.InsuranceSteps.OrderBy(o => o.StepOrder).Select(i => i.StepName)
                    .ToList();

                productInsuranceViewModel.Insurer = _mapper.Map<ProductInsurerViewModel>(item);
                productInsuranceViewModel.InsurerTerms = _mapper.Map<List<ProductInsurerTermViewModel>>(item.InsurerTerms);

                Pipe<ThirdProductInputViewModel, OutputViewModel> pipe = await RunThirdPipeLine(productInsuranceViewModel, OutPut, StepNames,
                thirdProductInputViewModel, cancellationToken);

                ThirdInsuranceResultViewModel Data = new ThirdInsuranceResultViewModel();

                // List<InsurerDetailTestViewModel> Details = new List<InsurerDetailTestViewModel>();
                Data = new ThirdInsuranceResultViewModel()
                {
                    Id = productInsuranceViewModel.Insurer.Id,
                    title = productInsuranceViewModel.Insurer.CompanyName,
                    AvatarUrl = productInsuranceViewModel.Insurer.AvatarUrl,
                    level = pipe.OutPut.Product.WealthLevel.ToString(),
                    num = pipe.OutPut.Product.BranchNumber.ToString(),
                    number = pipe.OutPut.Product.DamagePaymentSatisfactionRating.ToString(),
                    Price = pipe.OutPut.Product.Price.ToString("#,##"),
                    ThirdInsuranceCreditDurations = pipe.OutPut.Product.ThirdInsuranceCreditDurations,
                    ThirdMaxFinancialCovers = pipe.OutPut.Product.ThirdMaxFinancialCovers
                };
                thirdInsuranceResults.Add(Data);
            }

            return thirdInsuranceResults;
        }

        public async Task<List<BodyInsuranceResultViewModel>> GetAvailableBodyInsurers(string slug,
            BodyProductInputViewModel bodyProductInputViewModel,
            CancellationToken cancellationToken)
        {
            List<BodyInsuranceResultViewModel> bodyInsurances = new List<BodyInsuranceResultViewModel>();

            ProductInsuranceViewModel productInsuranceViewModel = new ProductInsuranceViewModel();

            Insurance insurance = await _insuranceRepository.GetInsurancesWithStepsBySlug(slug, cancellationToken);

            List<InsuranceCentralRule> centralRules =
                await _centralRulesRepository.GetByInsuranceSlug(slug, cancellationToken);


            productInsuranceViewModel.InsuranceCentralRules = _mapper.Map<List<ProductCentralRuleViewModel>>(centralRules);



            List<Insurer> insurers = await _insurerRepository.GetAllInsurersByInsuranceId(insurance.Id, cancellationToken);


            if (bodyProductInputViewModel.VehicleId != null)
            {
                Vehicle vehicle =
                    await _vehicleRepository.GetWithVehicleRuleCategory(bodyProductInputViewModel.VehicleId.Value,
                        cancellationToken);

                if (vehicle == null)
                {
                    throw new NotFoundException("vehicle not found");
                }

                bodyProductInputViewModel.VehicleRuleCategoryId = vehicle.VehicleRuleCategoryId;
            }


            foreach (Insurer item in insurers)
            {
                ProductViewModel Product = new ProductViewModel()
                {
                    Price = 0,
                    Title = item.Company.Name,
                    BranchNumber = 5,
                    WealthLevel = 2,
                    LogoUrl = item.Company.Name,
                    DamagePaymentSatisfactionRating = 5,
                };

                OutputViewModel OutPut = new OutputViewModel();
                OutPut.Product = Product;

                List<string> StepNames = insurance.InsuranceSteps.OrderBy(o => o.StepOrder).Select(i => i.StepName)
                    .ToList();

                productInsuranceViewModel.Insurer = _mapper.Map<ProductInsurerViewModel>(item);
                productInsuranceViewModel.InsurerTerms = _mapper.Map<List<ProductInsurerTermViewModel>>(item.InsurerTerms);

                Pipe<BodyProductInputViewModel, OutputViewModel> pipe = await RunBodyPipeLine(productInsuranceViewModel, OutPut, StepNames,
                    bodyProductInputViewModel, cancellationToken);


                BodyInsuranceResultViewModel Data = new BodyInsuranceResultViewModel();

                // List<InsurerDetailTestViewModel> Details = new List<InsurerDetailTestViewModel>();
                Data = new BodyInsuranceResultViewModel()
                {
                    Id = productInsuranceViewModel.Insurer.Id,
                    title = productInsuranceViewModel.Insurer.CompanyName,
                    AvatarUrl = productInsuranceViewModel.Insurer.AvatarUrl,
                    level = pipe.OutPut.Product.WealthLevel.ToString(),
                    num = pipe.OutPut.Product.BranchNumber.ToString(),
                    number = pipe.OutPut.Product.DamagePaymentSatisfactionRating.ToString(),
                    Price = pipe.OutPut.Product.Price.ToString("#,##"),
                    //ThirdInsuranceCreditDurations = pipe.OutPut.ThirdInsuranceCreditDurations,
                    //ThirdMaxFinancialCovers = pipe.OutPut.ThirdMaxFinancialCovers
                };

                if (!string.IsNullOrEmpty(Data.Price))
                {
                    bodyInsurances.Add(Data);
                }


            }

            return bodyInsurances;
        }

        #endregion


        #region Create

        public async Task<PolicyRequestSummaryViewModel> Create(PolicyRequestInputViewModel viewModel, long userId,
            CancellationToken cancellationToken)
        {


            User user = await _userRepository.GetWithPerson(userId);
            if (user == null)
            {
                throw new NotFoundException("کد کاربر وجود ندارد");
            }

            Insurer insurer =
                await _insurerRepository.GetInsurerWithInsuranceById(viewModel.InsurerId,
                    cancellationToken);
            if (insurer == null)
            {
                throw new NotFoundException("بیمه گر وجود ندارد");
            }


            DAL.Models.PolicyRequest model = await CreatePolicyRequestCommon(insurer, user, viewModel, cancellationToken);



            return _mapper.Map<PolicyRequestSummaryViewModel>(model);
        }


        public async Task<PolicyRequestViewModel> CreatePolicyRequestAsync(
            PolicyRequestViewModel policyRequestViewModel, CancellationToken cancellationToken)
        {
            var insurerIdIsValid =
                await _insurerRepository.GetByIdAsync(cancellationToken, policyRequestViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر");

            DAL.Models.PolicyRequest policyRequest = new DAL.Models.PolicyRequest
            {
                Code = policyRequestViewModel.Code,
                Title = policyRequestViewModel.Title,
                // PolicyRequestNumber = policyRequestViewModel.PolicyRequestNumber,
                Description = policyRequestViewModel.Description,
                // CreatedBy = policyRequestViewModel.CreatedBy,
                InsurerId = policyRequestViewModel.InsurerId
            };

            await _policyRequestRepository.AddAsync(policyRequest, cancellationToken);

            return _mapper.Map<PolicyRequestViewModel>(policyRequest);
            ;
        }

        public async Task<PolicyRequestDetailViewModel> CreatePolicyRequestDetailAsync(
            PolicyRequestDetailViewModel policyRequestDetailViewModel, CancellationToken cancellationToken)
        {
            // var policyRequestIdIsValid = await _policyRequestRepository.GetByIdAsync(cancellationToken, policyRequestDetailViewModel.PolicyRequestId) != null;
            // if (!policyRequestIdIsValid)
            //     throw new CustomException("بیمه نامه");

            PolicyRequestDetail policyRequestDetail = new PolicyRequestDetail
            {
                Type = policyRequestDetailViewModel.Type,
                Field = policyRequestDetailViewModel.Field,
                Criteria = policyRequestDetailViewModel.Criteria,
                Value = policyRequestDetailViewModel.Value,
                Discount = policyRequestDetailViewModel.Discount,
                CalculationType = policyRequestDetailViewModel.CalculationType,
                // CreatedBy = policyRequestDetailViewModel.CreatedBy,
                // PolicyRequestId = policyRequestDetailViewModel.PolicyRequestId
            };

            await _policyRequestDetailRepository.AddAsync(policyRequestDetail, cancellationToken);

            return _mapper.Map<PolicyRequestDetailViewModel>(policyRequestDetail);
        }


        public async Task<DAL.Models.PolicyRequest> CreatePolicyRequestCommon(Insurer insurer, User user, PolicyRequestInputViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest model = new DAL.Models.PolicyRequest();

                string title = $"{insurer.Insurance.Name} {insurer.Company.Name}";

                model.Code = Guid.NewGuid();
                model.RequestPersonId = user.PersonId;
                model.Title = title;
                model.InsurerId = viewModel.InsurerId;
                model.PolicyNumber = 3.ToString();
                // model.Description = insurer.Insurance.Description;

                // عدد 1 بیانگر وضعیت ورود اطلاعات است
                model.PolicyRequestStatusId = 1;
                model.IsDeleted = 0;


                InsuredRequest insuredRequest = new InsuredRequest();
                if (viewModel.VehicleId != null)
                {
                    insuredRequest.InsuredRequestVehicles.Add(new InsuredRequestVehicle()
                    {
                        VehicleId = viewModel.VehicleId.Value
                    });
                }

                model.InsuredRequests.Add(insuredRequest);

                ProductRequestViewModel productRequestViewModel = _mapper.Map<ProductRequestViewModel>(viewModel);

                if (productRequestViewModel.VehicleId != null)
                {
                    Vehicle vehicle =
                        await _vehicleRepository.GetWithVehicleRuleCategory(productRequestViewModel.VehicleId.Value,
                            cancellationToken);
                    if (vehicle == null)
                    {
                        throw new NotFoundException("vehicle not found");
                    }

                    productRequestViewModel.VehicleRuleCategoryId = vehicle.VehicleRuleCategoryId;
                }


                // await pipe.Run();
                List<String> StepNames = insurer.Insurance.InsuranceSteps.OrderBy(o => o.StepOrder)
                    .Select(i => i.StepName)
                    .ToList();

                ProductViewModel Product = new ProductViewModel()
                {
                    Price = 0,
                    BacePrice = 0,
                    Title = insurer.Company.Name,
                    BranchNumber = 5,
                    WealthLevel = 2,
                    LogoUrl = insurer.Company.Name,
                    DamagePaymentSatisfactionRating = 5,
                };

                OutputViewModel OutPut = new OutputViewModel();
                OutPut.Product = Product;


                ProductInsuranceViewModel productInsuranceViewModel = new ProductInsuranceViewModel();

                List<InsuranceCentralRule> centralRules =
                    await _centralRulesRepository.GetByInsuranceSlug(insurer.Insurance.Slug, cancellationToken);


                productInsuranceViewModel.InsuranceCentralRules = _mapper.Map<List<ProductCentralRuleViewModel>>(centralRules);
                productInsuranceViewModel.Insurer = _mapper.Map<ProductInsurerViewModel>(insurer);
                productInsuranceViewModel.InsurerTerms = _mapper.Map<List<ProductInsurerTermViewModel>>(insurer.InsurerTerms);


                decimal Price = 0;
                if (insurer.Insurance.Slug == "third")
                {
                    ThirdProductInputViewModel thirdProductInput =
                        _mapper.Map<ThirdProductInputViewModel>(productRequestViewModel);

                    var pipe = await RunThirdPipeLine(productInsuranceViewModel, OutPut, StepNames, thirdProductInput,
                        cancellationToken);
                    Price = pipe.OutPut.Product.Price;
                    thirdProductInput.SuggestedPrice = Price;

                    foreach (PropertyInfo property in thirdProductInput.GetType().GetProperties())
                    {
                        model.PolicyRequestDetails.Add(new PolicyRequestDetail
                        {
                            Type = 2,
                            Field = property.Name,
                            Criteria = "Criteria",
                            Value = "test",
                            Discount = "test",
                            CalculationType = "+",
                            UserInput = property.GetValue(thirdProductInput)?.ToString(),
                            InsurerTermId = null
                        });
                    }


                }
                else if (insurer.Insurance.Slug == "body")
                {
                    BodyProductInputViewModel bodyProductInputViewModel = _mapper.Map<BodyProductInputViewModel>(productRequestViewModel);

                    var pipe = await RunBodyPipeLine(productInsuranceViewModel, OutPut, StepNames, bodyProductInputViewModel,
                        cancellationToken);

                    Price = pipe.OutPut.Product.Price;
                    bodyProductInputViewModel.SuggestedPrice = Price;

                    foreach (PropertyInfo property in bodyProductInputViewModel.GetType().GetProperties())
                    {
                        model.PolicyRequestDetails.Add(new PolicyRequestDetail
                        {
                            Type = 2,
                            Field = property.Name,
                            Criteria = "Criteria",
                            Value = "test",
                            Discount = "test",
                            CalculationType = "+",
                            UserInput = property.GetValue(bodyProductInputViewModel)?.ToString(),
                            InsurerTermId = null
                        });
                    }


                }

                ThirdProductInputViewModel thirdProductInputViewModel =
                    _mapper.Map<ThirdProductInputViewModel>(productRequestViewModel);


                // Insert payment and factor based on price in pipe's output


                model.PolicyRequestFactors.Add(new PolicyRequestFactor()
                {
                    PolicyRequestId = model.Id,
                    Payment = new Payment()
                    {
                        Price = Price,
                        PaymentStatusId = 1,
                        CreatedDateTime = DateTime.Now,
                        PaymentCode = Guid.NewGuid().ToString(),
                        Description = title,
                        IsDeleted = false
                    }
                });
                await _policyRepository.AddAsync(model, cancellationToken);

                transaction.Complete();

                return model;
            }
        }



        public async Task<PolicyRequestSummaryViewModel> CreatePolicyRequestMine(PolicyRequestInputViewModel viewModel, long userId,
           CancellationToken cancellationToken)
        {
            //using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            User user = await _userRepository.GetWithPerson(userId);
            if (user == null)
            {
                throw new NotFoundException("کد کاربر وجود ندارد");
            }

            Insurer insurer =
                await _insurerRepository.GetInsurerWithInsuranceById(viewModel.InsurerId,
                    cancellationToken);
            if (insurer == null)
            {
                throw new NotFoundException("بیمه گر وجود ندارد");
            }

            List<InsuranceCentralRule> centralRules =
                await _centralRulesRepository.GetByInsuranceSlug(insurer.Insurance.Slug, cancellationToken);

            //insurer.Insurance.InsuranceCentralRules = centralRules;

            DAL.Models.PolicyRequest model = await CreatePolicyRequestCommon(insurer, user, viewModel, cancellationToken);


            // transaction.Complete();

            return _mapper.Map<PolicyRequestSummaryViewModel>(model);
            //}
        }

        #endregion

        #region Update

        public async Task<PolicyRequestViewModel> UpdatePolicyRequestAsync(long id,
            PolicyRequestViewModel policyRequestViewModel, CancellationToken cancellationToken)
        {
            var policyRequest = await _policyRequestRepository.GetByIdAsync(cancellationToken, id);
            if (policyRequest == null)
                throw new CustomException("بیمه نامه");

            var insurerIdIsValid =
                await _insurerRepository.GetByIdAsync(cancellationToken, policyRequestViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر ");

            policyRequest.Code = policyRequestViewModel.Code;
            policyRequest.Title = policyRequestViewModel.Title;
            // policyRequest.PolicyRequestNumber = policyRequestViewModel.PolicyRequestNumber;
            policyRequest.Description = policyRequestViewModel.Description;
            policyRequest.InsurerId = policyRequestViewModel.InsurerId;
            // policyRequest.UpdatedAt = DateTime.Now;
            // policyRequest.UpdatedBy = policyRequestViewModel.CreatedBy;

            await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);

            return _mapper.Map<PolicyRequestViewModel>(policyRequest);
        }

        public async Task<PolicyRequestDetailViewModel> UpdatePolicyRequestDetailAsync(long id,
            PolicyRequestDetailViewModel policyRequestDetailViewModel, CancellationToken cancellationToken)
        {
            var policyRequestDetail = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (policyRequestDetail == null)
                throw new CustomException("جزئیات بیمه نامه ");

            // var policyRequestIdIsValid = await _policyRequestRepository.GetByIdAsync(cancellationToken, policyRequestDetailViewModel.PolicyRequestId) != null;
            // if (!policyRequestIdIsValid)
            // throw new CustomException("بیمه نامه ");


            policyRequestDetail.Type = policyRequestDetailViewModel.Type;
            policyRequestDetail.Field = policyRequestDetailViewModel.Field;
            policyRequestDetail.Criteria = policyRequestDetailViewModel.Criteria;
            policyRequestDetail.Value = policyRequestDetailViewModel.Value;
            policyRequestDetail.Discount = policyRequestDetailViewModel.Discount;
            policyRequestDetail.CalculationType = policyRequestDetailViewModel.CalculationType;
            // policyRequestDetail.PolicyRequestId = policyRequestDetailViewModel.PolicyRequestId;


            await _policyRequestDetailRepository.UpdateAsync(policyRequestDetail, cancellationToken);

            return _mapper.Map<PolicyRequestDetailViewModel>(policyRequestDetail);
        }

        public async Task<PolicyRequestHolderViewModel> UpdatePolicyRequestHolderAsync(long id,
            PolicyRequestHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            // var policyRequestIdIsValid = await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, ViewModel.PolicyRequestId) != null;
            // if (!policyRequestIdIsValid)
            //     throw new CustomException("بیمه نامه ");

            //
            // model.PolicyRequestId = ViewModel.PolicyRequestId;
            // model.PolicyRequestId = ViewModel.PolicyRequestId;
            // model.UpdatedBy = ViewModel.UpdatedBy;
            // model.UpdatedAt = DateTime.Now;

            await _policyRequestHolderRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestHolderViewModel>(model);
        }

        public async Task<PolicyRequestHolderCompanyViewModel> UpdatePolicyRequestHolderCompanyAsync(long id,
            PolicyRequestHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyRequestIdIsValid =
                await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, ViewModel.Id) != null;
            if (!policyRequestIdIsValid)
                throw new CustomException("بیمه نامه ");

            // model.PolicyRequestHolderId = ViewModel.PolicyRequestHolderId;
            model.CompanyId = ViewModel.CompanyId;

            model.UpdatedAt = DateTime.Now;

            await _policyRequestHolderCompanyRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestHolderCompanyViewModel>(model);
        }


        public async Task<PolicyRequestAttachmentViewModel> UpdatePolicyRequestAttachmentAsync(long id,
            PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestAttachment model =
                await _policyRequestAttachmentRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(viewModel.Code, cancellationToken);
            Attachment attachment = await _attachmentRepository.GetByCode(viewModel.AttachmentCode, cancellationToken);

            model.PolicyRequestId = policyRequest.Id;
            model.AttachmentId = attachment.Id;
            model.TypeId = viewModel.TypeId;
            model.Name = viewModel.Name;

            await _policyRequestAttachmentRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestAttachmentViewModel>(model);
        }

        #endregion

        #region Get

        public async Task<PolicyRequestViewModel> GetPolicyRequestAsync(long id,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("بیمه نامه ");

            return _mapper.Map<PolicyRequestViewModel>(model);
        }

        /*
         دو ورودی اضافه شود به نام
          roleId
         userId
         
         حالا اگه
         role = CompanyAdmin -> همه درخواست های مربوط به خود شرکتش -> .insurer.company,
         user -> person -> company related
         
         role=CompanyAgent -> فقط درخواست های مربوط به خودش
         */
        public async Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsync(long userId, long roleId,
            List<Guid> companyCodes, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {


            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new BadRequestException("کاربر وجود ندارد");
            }

            var role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);
            if (role == null)
            {
                throw new BadRequestException("نقش وجود ندارد");
            }

            var userRole = await _userRoleRepository.GetUserRole(userId, roleId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("این کاربر با این نقش وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<DAL.Models.PolicyRequest> model = new PagedResult<DAL.Models.PolicyRequest>();

            PersonCompany personCompany =
                await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            //if (personCompany == null)
            //{
            //    throw new BadRequestException("شما با هیچ شرکتی رابطه ای ندارید");
            //}

            switch (userRole.Role.Name)
            {
                case "CompanyAdmin":
                    model = await _policyRequestRepository.GetByCompanyId(personCompany.CompanyId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "CompanyAgent":
                    model = await _policyRequestRepository.GetByReviewerId(personCompany.PersonId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "Admin":
                    if (companyCodes.Count != 0)
                    {
                        model = await _policyRequestRepository.GetAllByPagingAndCompanyCodes(companyCodes ,pageAbleModel, cancellationToken);
                    }
                    else
                    {
                        model = await _policyRequestRepository.GetAllByPaging(pageAbleModel, cancellationToken);
                    }
                    break;
                default:
                    throw new BadRequestException("شما دسترسی لازم را ندارید");
            }

            return _mapper.Map<PagedResult<PolicyRequestViewModel>>(model);
        }


        public async Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsyncMine(long userId, long roleId,
           PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new BadRequestException("کاربر وجود ندارد");
            }

            var role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);
            if (role == null)
            {
                throw new BadRequestException("نقش وجود ندارد");
            }

            var userRole = await _userRoleRepository.GetUserRole(userId, roleId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("این کاربر با این نقش وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<DAL.Models.PolicyRequest> model = new PagedResult<DAL.Models.PolicyRequest>();

            PersonCompany personCompany =
                await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);



            switch (userRole.Role.Name)
            {
                case "CompanyAdmin":
                    model = await _policyRequestRepository.GetByCompanyId(personCompany.CompanyId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "CompanyAgent":
                    model = await _policyRequestRepository.GetByReviewerId(personCompany.PersonId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "User":
                    model = await _policyRequestRepository.GetByMyRequests(user.PersonId, pageAbleModel, cancellationToken);
                    break;
                case "Admin":
                    model = await _policyRequestRepository.GetAllByPaging(pageAbleModel, cancellationToken);
                    break;
                default:
                    throw new BadRequestException("شما دسترسی لازم را ندارید");
            }

            return _mapper.Map<PagedResult<PolicyRequestViewModel>>(model);
        }

        public async Task<PolicyRequestViewModel> GetPolicyRequestByCodeMine(long userId, Guid Code, CancellationToken cancellationToken)
        {
            if (!await IsMinePolicyRequestCommon(userId, Code, cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            DAL.Models.PolicyRequest model = await _policyRequestRepository.GetPolicyRequestDetailByCode(Code, cancellationToken);
            if (model == null)
                throw new BadRequestException(" خطا در دریافت اطلاعات  ");

            return _mapper.Map<PolicyRequestViewModel>(model);
        }


        public async Task<bool> IsMinePolicyRequestCommon(long userId, Guid Code, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdNoTracking(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetByCodeWithoutRelationNoTracking(Code, cancellationToken);

            if (policyRequest.RequestPersonId != user.PersonId)
            {
                return false;
            }

            return true;
        }



        public async Task<PolicyRequestDetailViewModel> GetPolicyRequestDetailAsync(long id,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyRequestDetailViewModel>(model);
        }

        public async Task<PagedResult<PolicyRequestDetailViewModel>> GetAllPolicyRequestDetailsAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyRequestDetail> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestDetailRepository.GetPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize,
                    cancellationToken);
            else
                model = await _policyRequestDetailRepository.GetOrderedPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map<PagedResult<PolicyRequestDetailViewModel>>(model);
        }

        public async Task<PagedResult<PolicyRequestHolderViewModel>> GetAllPolicyRequestHolderAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyRequestHolder> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestHolderRepository.GetPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize,
                    cancellationToken);
            else
                model = await _policyRequestHolderRepository.GetOrderedPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestHolderViewModel>>(model);
        }

        public async Task<PagedResult<PolicyRequestHolderCompanyViewModel>> GetAllPolicyRequestHolderCompanyAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyRequestHolderCompany> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestHolderCompanyRepository.GetPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyRequestHolderCompanyRepository.GetOrderedPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestHolderCompanyViewModel>>(model);
        }

        public async Task<PolicyRequestHolderViewModel> GetPolicyRequestHolderAsync(long id,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyRequestHolderViewModel>(model);
        }

        public async Task<PolicyRequestHolderCompanyViewModel> GetPolicyRequestHolderCompanyAsync(long id,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyRequestHolderCompanyViewModel>(model);
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteDeletePolicyRequestHolderPersonAsync(long id,
            CancellationToken cancellationToken)
        {
            var policyRequest = await _policyRequestHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (policyRequest == null)
                throw new CustomException("بیمه نامه ");

            await _policyRequestHolderPersonRepository.DeleteAsync(policyRequest, cancellationToken);
            return true;
        }

        public async Task<bool> DeletePolicyRequestAsync(long id, CancellationToken cancellationToken)
        {
            var policyRequest = await _policyRequestRepository.GetByIdAsync(cancellationToken, id);
            if (policyRequest == null)
                throw new CustomException("بیمه نامه ");

            await _policyRequestRepository.DeleteAsync(policyRequest, cancellationToken);
            return true;
        }

        public async Task<bool> DeletePolicyRequestDetailAsync(long id, CancellationToken cancellationToken)
        {
            var policyRequestDetail = await _policyRequestDetailRepository.GetByIdAsync(cancellationToken, id);
            if (policyRequestDetail == null)
                throw new CustomException("جزئیات بیمه نامه");

            await _policyRequestDetailRepository.DeleteAsync(policyRequestDetail, cancellationToken);

            return true;
        }

        public async Task<bool> DeletePolicyRequestHolderAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات holder بیمه نامه");

            await _policyRequestHolderRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<bool> DeletePolicyRequestHolderCompanyAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات  بیمه نامه");

            await _policyRequestHolderCompanyRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<PolicyRequestHolderViewModel> CreatePolicyRequestHolderAsync(
            PolicyRequestHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, ViewModel.Id) != null;
            if (!model)
                throw new CustomException("بیمه گر");

            PolicyRequestHolder policyRequest = new PolicyRequestHolder
            {
                // PolicyRequestId = ViewModel.PolicyRequestId,
            };
            await _policyRequestHolderRepository.AddAsync(policyRequest, cancellationToken);
            return _mapper.Map<PolicyRequestHolderViewModel>(policyRequest);
        }

        public async Task<PolicyRequestHolderCompanyViewModel> CreatePolicyRequestHolderCompanyAsync(
            PolicyRequestHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var data = await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, ViewModel.Id) !=
                       null;
            if (!data)
                throw new CustomException("بیمه گر");

            PolicyRequestHolderCompany model = new PolicyRequestHolderCompany()
            {
                CompanyId = ViewModel.CompanyId,
            };
            await _policyRequestHolderCompanyRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<PolicyRequestHolderCompanyViewModel>(model);
        }

        public async Task<PolicyRequestHolderPersonViewModel> CreatePolicyRequestHolderPersonAsync(
            PolicyRequestHolderPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            var data = await _policyRequestHolderPersonRepository.GetByIdAsync(cancellationToken, viewModel.Id) !=
                       null;
            if (!data)
                throw new CustomException("بیمه گر");

            PolicyRequestHolderPerson model = new PolicyRequestHolderPerson()
            {
                PolicyRequestHolderId = viewModel.PersonId,
                PersonId = viewModel.PersonId,
            };
            await _policyRequestHolderPersonRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<PolicyRequestHolderPersonViewModel>(model);
        }

        public async Task<bool> DeletePolicyRequestHolderPersonAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات  بیمه نامه");

            await _policyRequestHolderCompanyRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<PolicyRequestHolderPersonViewModel> UpdatePolicyRequestHolderPersonAsync(long id,
            PolicyRequestHolderPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            var policyRequestIdIsValid =
                await _policyRequestHolderRepository.GetByIdAsync(cancellationToken, viewModel.Id) != null;
            if (!policyRequestIdIsValid)
                throw new CustomException("بیمه نامه ");


            model.PersonId = viewModel.PersonId;
            model.PolicyRequestHolderId = viewModel.PersonId;

            model.UpdatedAt = DateTime.Now;

            await _policyRequestHolderPersonRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestHolderPersonViewModel>(model);
        }

        public async Task<PolicyRequestHolderPersonViewModel> GetPolicyRequestHolderPersonAsync(long id,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestHolderPersonRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("جزئیات بیمه نامه ");

            return _mapper.Map<PolicyRequestHolderPersonViewModel>(model);
        }

        public async Task<PagedResult<PolicyRequestHolderPersonViewModel>> GetAllPolicyRequestHolderPersonAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PolicyRequestHolderPerson> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestHolderPersonRepository.GetPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, cancellationToken);
            else
                model = await _policyRequestHolderPersonRepository.GetOrderedPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestHolderPersonViewModel>>(model);
        }


        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var model = await _policyRequestRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("خطا در دریافت اطلاعات");

            await _policyRequestRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        //public async Task<bool> DeleteMyRequest(Guid code, CancellationToken cancellationToken)
        //{
        //    var model = await _policyRequestRepository.GetByCode(code, cancellationToken);
        //    if (model == null)
        //        throw new CustomException("خطا در دریافت اطلاعات");

        //    await _policyRequestRepository.DeleteAsync(model, cancellationToken);
        //    return true;
        //}


        public async Task<PolicyRequestViewModel> Update(long id, PolicyRequestViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var model = await _policyRequestRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("خطا در دریافت اطلاعات ");

            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.InsurerId = viewModel.InsurerId;
            model.PolicyNumber = viewModel.PolicyNumber;
            model.Title = viewModel.Title;
            model.RequestPersonId = viewModel.RequestPersonId;
            model.Id = viewModel.Id;

            await _policyRequestRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestViewModel>(model);
        }


        public async Task<PolicyRequestViewModel> UpdateRequestMine(long userId, Guid code, PolicyRequestViewModel viewModel,
            CancellationToken cancellationToken)
        {

            if (!await IsMinePolicyRequestCommon(userId, code, cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            var model = await _policyRequestRepository.GetByCode(code, cancellationToken);
            if (model == null)
                throw new CustomException("خطا در دریافت اطلاعات ");

            //model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.InsurerId = viewModel.InsurerId;
            model.PolicyNumber = viewModel.PolicyNumber;
            model.Title = viewModel.Title;
            model.RequestPersonId = viewModel.RequestPersonId;
            //model.Id = viewModel.Id;

            await _policyRequestRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PolicyRequestViewModel>(model);
        }


        public async Task<PagedResult<PolicyRequestViewModel>> GetAll(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken)
        {
            PagedResult<DAL.Models.PolicyRequest> model;
            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _policyRequestRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize,
                    cancellationToken);
            else
                model = await _policyRequestRepository.GetOrderedPagedAsync(pageAbleResult.Page,
                    pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);
            return _mapper.Map<PagedResult<PolicyRequestViewModel>>(model);
        }

        public async Task<PolicyRequestViewModel> GetEveryThing(Guid code, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetAllByCode(code, cancellationToken);
            if (policyRequest == null)
                throw new CustomException("بیمه نامه");

            return _mapper.Map<PolicyRequestViewModel>(policyRequest);
        }

        #endregion


        #region Attachment

        public async Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachment(CancellationToken cancellationToken,
            IFormFile files, string policyCode, int typeId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var code = Guid.Parse(policyCode);
                var SameTypeExists =
                    await _policyRequestAttachmentRepository.GetByPolicyRequestCodeTypeId(code, typeId, cancellationToken);
                if (SameTypeExists.Count > 0)
                {
                    await _policyRequestAttachmentRepository.DeleteRangeAsync(SameTypeExists, cancellationToken);
                    var attachments = SameTypeExists.Select(x => x.Attachment).ToList();
                    await _attachmentRepository.DeleteRangeAsync(attachments, cancellationToken);
                }

                var extension = Path.GetExtension(files.FileName);
                var newName = Guid.NewGuid() + extension;

                Attachment model = await _attachmentService.CreateAttachment(cancellationToken, files);


                var modelPolicy = await _policyRequestRepository.GetByCode(code, cancellationToken);
                var modelAttachment = new DAL.Models.PolicyRequestAttachment()
                {
                    AttachmentId = model.Id,
                    //Attachment = model,
                    TypeId = typeId,
                    PolicyRequestId = modelPolicy.Id,
                    Name = Path.GetFileName(files.FileName)
                };

                await _policyRequestAttachmentRepository.AddAsync(modelAttachment, cancellationToken);
                // modelAttachment = await _policyRequestAttachmentRepository.Table.Include(c => c.PolicyRequest)
                //     .Include(c => c.Attachment)
                //     .FirstOrDefaultAsync(x => x.Id == modelAttachment.Id, cancellationToken);

                transaction.Complete();
                return _mapper.Map<PolicyRequestAttachmentViewModel>(modelAttachment);
            }
        }

        public async Task<List<PolicyRequestAttachmentDownloadViewModel>> GetPolicyRequestAttachments(string code,
            CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);
            if (policyRequest == null)
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            List<PolicyRequestAttachment> policyRequestAttachments =
                await _policyRequestAttachmentRepository.GetByPolicyRequestCode(policyRequest.Code,
                    cancellationToken);
            return _mapper.Map<List<PolicyRequestAttachmentDownloadViewModel>>(policyRequestAttachments);
        }


        // اضافی می باشد
        public async Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachmentAsync(
            PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(viewModel.Code, cancellationToken);
            Attachment attachment = await _attachmentRepository.GetByCode(viewModel.AttachmentCode, cancellationToken);

            PolicyRequestAttachment policyRequestAttachment = new PolicyRequestAttachment
            {
                PolicyRequestId = policyRequest.Id,
                AttachmentId = attachment.Id,
                Name = viewModel.Name,
                TypeId = viewModel.TypeId
            };
            await _policyRequestAttachmentRepository.AddAsync(policyRequestAttachment, cancellationToken);
            return _mapper.Map<PolicyRequestAttachmentViewModel>(policyRequestAttachment);
        }

        #endregion


        #region Supplement

        public async Task<Person> addOrUpdatePolicyRequestHolderPerson(PolicySupplementPersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            Person person;
            if (personViewModel.Code != null)
            {
                person = await _personRepository.GetByCode(personViewModel.Code.Value, cancellationToken);
                if (person != null)
                {
                    person.NationalCode = personViewModel.NationalCode;
                    person.BirthDate = personViewModel.BirthDate;
                    person.GenderId = personViewModel.GenderId;
                    await _personRepository.UpdateAsync(person, cancellationToken);
                    return person;
                }
            }

            person = new Person
            {
                NationalCode = personViewModel.NationalCode,
                BirthDate = personViewModel.BirthDate,
                GenderId = personViewModel.GenderId,
            };
            await _personRepository.AddAsync(person, cancellationToken);
            return person;
        }


        public async Task<Address> addOrUpdatePolicyRequestHolderAddress(
            PolicyRequestHolderPersonAddressViewModel personAddressViewModel, CancellationToken cancellationToken)
        {
            Address address;
            if (personAddressViewModel.Code != null)
            {
                address = await _addressRepository.GetAddressByCode(personAddressViewModel.Code.Value, cancellationToken);
                if (address != null)
                {
                    address.CityId = personAddressViewModel.CityId;
                    address.Description = personAddressViewModel.Description;
                    address.Phone = personAddressViewModel.Phone;
                    await _addressRepository.UpdateAsync(address, cancellationToken);
                    return address;
                }
            }

            address = new Address
            {
                CityId = personAddressViewModel.CityId,
                Description = personAddressViewModel.Description,
                Phone = personAddressViewModel.Phone,
            };
            await _addressRepository.AddAsync(address, cancellationToken);
            return address;
        }


        public async Task<PolicySupplementViewModel> CreatePolicyRequestHolderSupplementInfoAsync(string code,
            PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            var inputHolder = viewModel;


            # region OwnerPerson

            Person ownerPerson =
                await addOrUpdatePolicyRequestHolderPerson(viewModel.OwnerPerson, cancellationToken);
            if (ownerPerson == null)
            {
                throw new BadRequestException("خطای درج داده");
            }

            InsuredRequestVehicle insuredRequestVehicle = await
                _insuredRequestVehicleRepository.GetInsuredRequestVehicleByPolicyRequestId(policyRequest.Id, cancellationToken);
            if (insuredRequestVehicle != null)
            {
                insuredRequestVehicle.OwnerPersonId = ownerPerson.Id;
                await _insuredRequestVehicleRepository.UpdateAsync(insuredRequestVehicle, cancellationToken);
            }

            #endregion

            # region Issued

            long? issuedPersonId = null;
            // اگر به نام مالک صادر شود
            if (viewModel.IssuedPersonType == 1)
            {
                issuedPersonId = ownerPerson.Id;
            }
            // اگر به نام دیگری صادر شود
            else if (viewModel.IssuedPersonType == 2)
            {
                if (viewModel.IssuedPerson == null)
                {
                    throw new BadRequestException("اطلاعات شخصی صادر کننده بیمه ناقص است");
                }

                Person issuedPerson =
                    await addOrUpdatePolicyRequestHolderPerson(viewModel.IssuedPerson, cancellationToken);
                if (issuedPerson == null)
                {
                    throw new BadRequestException("خطای درج داده");
                }

                issuedPersonId = issuedPerson.Id;
            }

            Address address = await addOrUpdatePolicyRequestHolderAddress(viewModel.Address, cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("خطای درج داده");
            }

            long addressId = address.Id;

            #endregion

            #region Policy Holder

            PolicyRequestHolder holder;
            holder = await _policyRequestHolderRepository.GetByPolicyRequestCode(policyCode,
                cancellationToken);
            if (holder == null)
            {
                holder = new PolicyRequestHolder();
                holder.PersonId = issuedPersonId;
                holder.AddressId = addressId;
                holder.PolicyRequestId = policyRequest.Id;
                holder.IssuedPersonRelation = inputHolder.IssuedPersonRelation;
                holder.IssuedPersonType = inputHolder.IssuedPersonType;
                await _policyRequestHolderRepository.AddAsync(holder, cancellationToken);
            }
            else
            {
                holder.PersonId = issuedPersonId;
                holder.AddressId = addressId;
                holder.PolicyRequestId = policyRequest.Id;
                holder.IssuedPersonRelation = inputHolder.IssuedPersonRelation;
                holder.IssuedPersonType = inputHolder.IssuedPersonType;
                await _policyRequestHolderRepository.UpdateAsync(holder, cancellationToken);
            }

            #endregion


            return _mapper.Map<PolicySupplementViewModel>(holder);
        }



        public async Task<PolicySupplementViewModel> CreatePolicyRequestHolderSupplementInfoAsyncMine(long userId, Guid code, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, code, cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            //Guid policyRequestCode = code;
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicySupplementViewModel inputHolder = viewModel;

            # region OwnerPerson

            Person ownerPerson =
                await addOrUpdatePolicyRequestHolderPerson(viewModel.OwnerPerson, cancellationToken);
            if (ownerPerson == null)
            {
                throw new BadRequestException("خطای درج داده");
            }

            InsuredRequestVehicle insuredRequestVehicle = await
                _insuredRequestVehicleRepository.GetInsuredRequestVehicleByPolicyRequestId(policyRequest.Id, cancellationToken);
            if (insuredRequestVehicle != null)
            {
                insuredRequestVehicle.OwnerPersonId = ownerPerson.Id;
                await _insuredRequestVehicleRepository.UpdateAsync(insuredRequestVehicle, cancellationToken);
            }

            #endregion

            # region Issued

            long? issuedPersonId = null;
            // اگر به نام مالک صادر شود
            if (viewModel.IssuedPersonType == 1)
            {
                issuedPersonId = ownerPerson.Id;
            }
            // اگر به نام دیگری صادر شود
            else if (viewModel.IssuedPersonType == 2)
            {
                if (viewModel.IssuedPerson == null)
                {
                    throw new BadRequestException("اطلاعات شخصی صادر کننده بیمه ناقص است");
                }

                Person issuedPerson =
                    await addOrUpdatePolicyRequestHolderPerson(viewModel.IssuedPerson, cancellationToken);
                if (issuedPerson == null)
                {
                    throw new BadRequestException("خطای درج داده");
                }

                issuedPersonId = issuedPerson.Id;
            }

            Address address = await addOrUpdatePolicyRequestHolderAddress(viewModel.Address, cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("خطای درج داده");
            }

            long addressId = address.Id;

            #endregion

            #region Policy Holder

            PolicyRequestHolder holder;
            holder = await _policyRequestHolderRepository.GetByPolicyRequestCode(code,
                cancellationToken);
            if (holder == null)
            {
                holder = new PolicyRequestHolder();
                holder.PersonId = issuedPersonId;
                holder.AddressId = addressId;
                holder.PolicyRequestId = policyRequest.Id;
                holder.IssuedPersonRelation = inputHolder.IssuedPersonRelation;
                holder.IssuedPersonType = inputHolder.IssuedPersonType;
                await _policyRequestHolderRepository.AddAsync(holder, cancellationToken);
            }
            else
            {
                holder.PersonId = issuedPersonId;
                holder.AddressId = addressId;
                holder.PolicyRequestId = policyRequest.Id;
                holder.IssuedPersonRelation = inputHolder.IssuedPersonRelation;
                holder.IssuedPersonType = inputHolder.IssuedPersonType;
                await _policyRequestHolderRepository.UpdateAsync(holder, cancellationToken);
            }

            #endregion

            return _mapper.Map<PolicySupplementViewModel>(holder);
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

        public async Task<PolicySupplementViewModel> GetPolicyRequestHolderSupplementInfo(string code,
            CancellationToken cancellationToken)
        {
            Guid policyRequestCode = Guid.Parse(code);

            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.checkPolicyRequestExistsByCode(policyRequestCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequestCode, cancellationToken);

            return _mapper.Map<PolicySupplementViewModel>(policyRequestHolder);
        }

        public async Task<PolicySupplementViewModel> GetPolicyRequestHolderSupplementInfoMine(long userId, string code,
           CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyRequestCode = Guid.Parse(code);

            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.checkPolicyRequestExistsByCode(policyRequestCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequestCode, cancellationToken);

            return _mapper.Map<PolicySupplementViewModel>(policyRequestHolder);
        }

        #endregion

        #region Issue

        public async Task<PolicyRequestIssueViewModel> CreateOrUpdatePolicyRequestHolderIssueAsync(string code,
            PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {

            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            Address address = new Address();
            if (viewModel.NeedPrint)
            {
                address = await _addressRepository.GetAddressByCode(viewModel.ReceiverAddressCode.Value,
                    cancellationToken);
                if (address == null)
                {
                    throw new BadRequestException("کد آدرس وجود ندارد");
                }
            }

            PolicyRequestIssue issue = await AddOrUpdateIssueCommon(address.Id, policyRequest, viewModel, cancellationToken);


            PolicyRequestIssueViewModel issueViewModel = _mapper.Map<PolicyRequestIssueViewModel>(issue);


            return issueViewModel;

        }

        public async Task<PolicyRequestIssueViewModel> CreateOrUpdatePolicyRequestHolderIssueAsyncMine(long userId, string code, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            Address address = new Address();
            if (viewModel.NeedPrint)
            {
                address = await _addressRepository.GetAddressByCode(viewModel.ReceiverAddressCode.Value,
                    cancellationToken);
                if (address == null)
                {
                    throw new BadRequestException("کد آدرس وجود ندارد");
                }
            }


            PolicyRequestIssue issue = await AddOrUpdateIssueCommon(address.Id, policyRequest, viewModel, cancellationToken);


            PolicyRequestIssueViewModel issueViewModel = _mapper.Map<PolicyRequestIssueViewModel>(issue);


            return issueViewModel;
        }


        public async Task<PolicyRequestIssue> AddOrUpdateIssueCommon(long addressId, DAL.Models.PolicyRequest policyRequest, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestIssue issue = await _policyRequestIssueRepository.GetByPolicyRequestCode(policyRequest.Code,
                cancellationToken);
            bool exist = true;
            if (issue == null)
            {
                exist = false;
                issue = new PolicyRequestIssue();
            }

            issue.PolicyRequestId = policyRequest.Id;
            issue.EmailAddress = viewModel.EmailAddress;
            issue.NeedPrint = viewModel.NeedPrint;
            if (viewModel.NeedPrint)
            {
                issue.ReceiverAddressId = addressId;
                issue.ReceiveDate = DateTime.Parse(viewModel.ReceiveDate);
                issue.IssueSessionId = viewModel.IssueSessionId;
                issue.Description = viewModel.Description;
                issue.WalletId = viewModel.WalletId;
            }
            else
            {
                issue.ReceiverAddressId = null;
                issue.ReceiveDate = null;
                issue.IssueSessionId = null;
                issue.Description = null;
                issue.WalletId = null;
            }

            if (exist)
            {
                await _policyRequestIssueRepository.UpdateAsync(issue, cancellationToken);
            }
            else
            {
                await _policyRequestIssueRepository.AddAsync(issue, cancellationToken);
            }

            return issue;

        }


        public async Task<PolicyRequestIssueViewModel> GetPolicyRequestHolderIssueAsync(string code,
            CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }



            PolicyRequestIssue issue;
            issue = await _policyRequestIssueRepository.GetByPolicyRequestCode(policyCode,
                cancellationToken);

            PolicyRequestIssueViewModel viewModel = _mapper.Map<PolicyRequestIssueViewModel>(issue);



            return viewModel;
        }

        public async Task<PolicyRequestIssueViewModel> GetPolicyRequestHolderIssueAsyncMine(long userId, string code, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }


            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestIssue issue;
            issue = await _policyRequestIssueRepository.GetByPolicyRequestCode(policyCode,
                cancellationToken);

            PolicyRequestIssueViewModel viewModel = _mapper.Map<PolicyRequestIssueViewModel>(issue);




            return viewModel;

        }



        public async Task<PolicyRequestInspectionResultViewModel> CreateOrUpdatePolicyRequestHolderInspectionAsync(
            string code, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestInspection inspection = await AddOrUpdatePolicyRequestInspectionCommon(policyRequest, viewModel, cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }


        public async Task<PolicyRequestInspectionResultViewModel> CreateOrUpdatePolicyRequestHolderInspectionAsyncMine(long userId,
            string code, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestInspection inspection = await AddOrUpdatePolicyRequestInspectionCommon(policyRequest, viewModel, cancellationToken);

            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }


        public async Task<PolicyRequestInspection> AddOrUpdatePolicyRequestInspectionCommon(DAL.Models.PolicyRequest policyRequest, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCode(policyRequest.Code,
                cancellationToken);
            if (inspection == null)
            {
                inspection = new PolicyRequestInspection();
                inspection.PolicyRequestId = policyRequest.Id;
                inspection.InspectionTypeId = viewModel.InspectionTypeId;


                if (viewModel.InspectionTypeId == 1)
                {
                    Address address = new Address();

                    address = await _addressRepository.GetAddressByCode(viewModel.LocationInspection.AddressCode.Value,
                        cancellationToken);
                    if (address == null)
                    {
                        throw new BadRequestException("کد آدرس وجود ندارد");
                    }

                    inspection.InspectionSessionDate =
                        DateTime.Parse(viewModel.LocationInspection.InspectionSessionDate);
                    inspection.InspectionAddressId = address.Id;
                    inspection.InspectionSessionId = viewModel.LocationInspection.InspectionSessionId;
                }
                else if (viewModel.InspectionTypeId == 2)
                {
                    inspection.InspectionSessionDate = DateTime.Parse(viewModel.CenterInspection.InspectionSessionDate);
                    inspection.CompanyCenterScheduleId = viewModel.CenterInspection.CompanyCenterScheduleId;
                }

                await _policyRequestInspectionRepository.AddAsync(inspection, cancellationToken);
            }
            else
            {
                inspection.PolicyRequestId = policyRequest.Id;
                inspection.InspectionTypeId = viewModel.InspectionTypeId;

                if (viewModel.InspectionTypeId == 1)
                {
                    Address address = new Address();

                    address = await _addressRepository.GetAddressByCode(viewModel.LocationInspection.AddressCode.Value,
                        cancellationToken);
                    if (address == null)
                    {
                        throw new BadRequestException("کد آدرس وجود ندارد");
                    }

                    inspection.InspectionAddressId = address.Id;
                    inspection.InspectionSessionId = viewModel.LocationInspection.InspectionSessionId;
                    inspection.InspectionSessionDate =
                        DateTime.Parse(viewModel.LocationInspection.InspectionSessionDate);
                    inspection.CompanyCenterScheduleId = null;
                }
                else if (viewModel.InspectionTypeId == 2)
                {
                    inspection.CompanyCenterScheduleId = viewModel.CenterInspection.CompanyCenterScheduleId;
                    inspection.InspectionSessionDate = DateTime.Parse(viewModel.CenterInspection.InspectionSessionDate);
                    inspection.InspectionAddressId = null;
                    inspection.InspectionSessionId = null;
                }

                await _policyRequestInspectionRepository.UpdateAsync(inspection, cancellationToken);
            }

            return inspection;

        }




        public async Task<PolicyRequestInspectionResultViewModel> GetPolicyRequestHolderInspectionAsync(string code,
            CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCodeNoTracking(policyCode,
                cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }

        public async Task<PolicyRequestInspectionResultViewModel> GetPolicyRequestHolderInspectionAsyncMine(long userId, string code,
            CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCodeNoTracking(policyCode,
                cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }

        #endregion


        #region PaymentInfo

        public async Task<PolicyRequestPaymentInfoViewModel> GetPolicyRequestPaymentInfoAsync(string code,
            CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            #region Policy Request Issue

            DAL.Models.PolicyRequestFactor paymentInfo;
            paymentInfo = await _policyRequestFactorRepository.GetPaymentInfoByPolicyId(policyRequest.Id,
                cancellationToken);

            #endregion


            return _mapper.Map<PolicyRequestPaymentInfoViewModel>(paymentInfo);
        }

        #endregion

        public async Task<List<MyPolicyRequestViewModel>> GetMinePolicyInsurance(long userId,
            CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetWithPerson(userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            List<DAL.Models.PolicyRequest> policyRequest =
                await _policyRequestRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (policyRequest == null)
            {
                throw new NotFoundException("اطلاعات وجود ندارد");
            }

            List<MyPolicyRequestViewModel> results = _mapper.Map<List<MyPolicyRequestViewModel>>(policyRequest);
            return results;
        }


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


        public async Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetailsMine(long userId, Guid code,
            CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, code, cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
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


            List<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetFactorsWithDetailsByPolicyId(policyRequest.Id, cancellationToken);

            PolicyRequestPaymentViewModel model = new PolicyRequestPaymentViewModel()
            {
                Insurer = policyRequest.Insurer.Insurance.Name,
                PaymentDetailViewModels = paymentDetails,
                Factors = _mapper.Map<List<PolicyFactorResultViewModel>>(factors),
                PaymentInfo = _mapper.Map<PaymentResultViewModel>(factors.FirstOrDefault(f=> f.Payment.PaymentStatusId != 3 && f.Payment.PaymentStatusId != 4).Payment)
            };

            return model;
        }







        public async Task<List<PolicyRequestByProvinceViewModel>> GetPolicyRequestBasedOnProvince(long provinceId,
            CancellationToken cancellationToken)
        {
            List<DAL.Models.PolicyRequest> policyRequests =
                await _policyRequestRepository.GetByProvinceId(provinceId, cancellationToken);

            if (policyRequests == null)
            {
                throw new NotFoundException("اطلاعات وجود ندارد");
            }

            List<PolicyRequestByProvinceViewModel> viewModels =
                _mapper.Map<List<PolicyRequestByProvinceViewModel>>(policyRequests);


            return viewModels;
        }


        #region PolicyRequest

        public async Task<PolicyRequestViewModel> getPolicyRequestByCode(Guid Code, CancellationToken cancellationToken)
        {
            var model = await _policyRequestRepository.GetPolicyRequestDetailByCode(Code, cancellationToken);
            if (model == null)
                throw new BadRequestException(" خطا در دریافت اطلاعات  ");

            return _mapper.Map<PolicyRequestViewModel>(model);
        }

        #endregion


        #region PolicyRequest By Company

        public async Task<PagedResult<MyPolicyRequestViewModel>> GetAllByCompanyId(Guid code,
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }

            // var agents = await _agentRepository.GetAllByCompanyCodeAsync(code, cancellationToken);
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            var agents = await _policyRequestRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.Insurer.CompanyId == company.Id,
                i => i.Insurer,
                i => i.InsuredRequests,
                i => i.RequestPerson,
                i => i.PolicyRequestStatus
            );
            return _mapper.Map<PagedResult<MyPolicyRequestViewModel>>(agents);
        }

        #endregion

        #region BodySupplement

        public async Task<PolicyRequestHolder> AddOrUpdateBodyOwner(User AuthenticatedUser,
            BodySupplementInfoViewModel viewModel,
            PolicyRequestHolder policyRequestHolder, CancellationToken cancellationToken)
        {
            Person ownerPerson = AuthenticatedUser.Person;
            ownerPerson.NationalCode = viewModel.OwnerPerson.NationalCode;
            ownerPerson.BirthDate = viewModel.OwnerPerson.BirthDate;
            ownerPerson.GenderId = viewModel.OwnerPerson.GenderId;
            await _personRepository.UpdateAsync(ownerPerson, cancellationToken);

            InsuredRequestVehicle insuredRequestVehicle = await
                _insuredRequestVehicleRepository.GetInsuredRequestVehicleByPolicyRequestIdWithoutRelation(policyRequestHolder.PolicyRequestId,
                    cancellationToken);
            if (insuredRequestVehicle != null)
            {
                insuredRequestVehicle.OwnerPersonId = ownerPerson.Id;
                await _insuredRequestVehicleRepository.UpdateAsync(insuredRequestVehicle, cancellationToken);
            }

            long? addressId = policyRequestHolder?.AddressId;
            long personId = AuthenticatedUser.PersonId;
            AddressViewModel addressViewModel = new AddressViewModel()
            {
                Description = viewModel.OwnerPerson.Description,
                CityId = viewModel.OwnerPerson.CityId,
                Mobile = AuthenticatedUser.Username, // نام کاربری همان شماره تلفن کاربر است
            };
            PersonAddress personAddress = await _personAddressService.AddOrUpdateByAddressId(addressId, personId,
                addressViewModel, cancellationToken);
            policyRequestHolder.AddressId = personAddress.AddressId;
            policyRequestHolder.PersonId = personId;
            await _policyRequestHolderRepository.UpdateAsync(policyRequestHolder, cancellationToken);
            return policyRequestHolder;
        }

        public async Task<PolicyRequestHolder> AddOrUpdateBodyIssued(User AuthenticatedUser,
            BodySupplementInfoViewModel viewModel,
            PolicyRequestHolder policyRequestHolder, CancellationToken cancellationToken)
        {
            Person issuedPerson = null;
            long? issuedPersonId = policyRequestHolder?.PersonId;
            // وقتی به نام دیگری صادر می شود یعنی کاربر لاگین شده، با شخص صادر شده متفاوت است و نباید اطلاعات کاربر ویرایش شود
            if (issuedPersonId.HasValue && issuedPersonId.Value != AuthenticatedUser.PersonId)
            {
                issuedPerson = await _personRepository.GetByIdAsync(cancellationToken, issuedPersonId);
                if (issuedPerson == null)
                {
                    throw new BadRequestException("اطلاعات شخص صادر شده وجود ندارد");
                }

                issuedPerson.FirstName = viewModel.IssuedPerson.FirstName;
                issuedPerson.LastName = viewModel.IssuedPerson.LastName;
                issuedPerson.NationalCode = viewModel.IssuedPerson.NationalCode;
                issuedPerson.GenderId = viewModel.IssuedPerson.GenderId;
                issuedPerson.BirthDate = viewModel.IssuedPerson.BirthDate;

                await _personRepository.UpdateAsync(issuedPerson, cancellationToken);
            }
            else
            {
                PersonViewModel personViewModel = _mapper.Map<PersonViewModel>(viewModel.IssuedPerson);
                issuedPerson = await _personService.Create(personViewModel, cancellationToken);
            }

            long? addressId = policyRequestHolder?.AddressId;
            long personId = issuedPerson.Id;
            AddressViewModel addressViewModel = new AddressViewModel()
            {
                Description = viewModel.IssuedPerson.Description,
                CityId = viewModel.IssuedPerson.CityId,
                Mobile = viewModel.IssuedPerson.Mobile,
            };
            PersonAddress personAddress = await _personAddressService.AddOrUpdateByAddressId(addressId, personId,
                addressViewModel, cancellationToken);
            policyRequestHolder.AddressId = personAddress.AddressId;
            policyRequestHolder.PersonId = personId;
            await _policyRequestHolderRepository.UpdateAsync(policyRequestHolder, cancellationToken);
            return policyRequestHolder;
        }

        public async Task<BodySupplementInfoViewModel> AddOrUpdateBodySupplement(string code, long AuthenticatedUserId,
            BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder = await AddOrUpdatePolicyRequestHolderCommon(AuthenticatedUserId, policyRequest, viewModel, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<BodySupplementInfoViewModel> AddOrUpdateBodySupplementMine(string code, long userId,
            BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder = await AddOrUpdatePolicyRequestHolderCommon(userId, policyRequest, viewModel, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<PolicyRequestHolder> AddOrUpdatePolicyRequestHolderCommon(long userId, DAL.Models.PolicyRequest policyRequest, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            User AuthenticatedUser =
                await _userRepository.GetwithPersonByIdTracked(cancellationToken, userId);
            if (AuthenticatedUser == null)
            {
                throw new BadRequestException("اطلاعات کاربری شخص وارد شده، وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder = await
                _policyRequestHolderRepository.GetByPolicyRequestCodeWithoutRelation(policyRequest.Code, cancellationToken);
            if (policyRequestHolder == null)
            {
                policyRequestHolder = new PolicyRequestHolder();

                policyRequestHolder.PolicyRequestId = policyRequest.Id;

                await _policyRequestHolderRepository.AddAsync(policyRequestHolder, cancellationToken);
            }

            policyRequestHolder.IssuedPersonType = viewModel.IssuedPersonType;
            if (viewModel.IssuedPersonType == 1)
            {
                await AddOrUpdateBodyOwner(AuthenticatedUser, viewModel, policyRequestHolder, cancellationToken);
            }
            else if (viewModel.IssuedPersonType == 2) // به نام دیگری صادر شود
            {
                await AddOrUpdateBodyIssued(AuthenticatedUser, viewModel, policyRequestHolder, cancellationToken);
            }

            return policyRequestHolder;
        }




        public async Task<BodySupplementInfoViewModel> GetBodySupplement(string code,
            CancellationToken cancellationToken)
        {
            Guid policyRequestCode = Guid.Parse(code);

            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.checkPolicyRequestExistsByCode(policyRequestCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequestCode, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<BodySupplementInfoViewModel> GetBodySupplementMine(long userId, string code,
            CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            if (!await IsMinePolicyRequestCommon(userId, Guid.Parse(code), cancellationToken))
            {
                throw new BadRequestException("شما به این اطلاعات دسترسی ندارید");
            }

            Guid policyRequestCode = Guid.Parse(code);

            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.checkPolicyRequestExistsByCode(policyRequestCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequestCode, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
        }





        public async Task<PolicyRequestSummaryOutputViewModel> PlicyRequestStatusChange(Guid code, long RoleID,
            long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel,
            CancellationToken cancellationToken)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
                    if (User == null)
                        throw new BadRequestException("کاربری یافت نشد");
                    UserRole userRole = await _userRoleRepository.GetUserRole(UserID, RoleID, cancellationToken);
                    if (userRole == null)
                        throw new BadRequestException("این کاربر با این نقش وجود ندارد");
                    DAL.Models.PolicyRequest Result_PolicyRequest = await _policyRequestRepository.GetByCode(code, cancellationToken);
                    Result_PolicyRequest.PolicyRequestStatusId =
                        _policyReqiestStatusInputViewModel.PolicyRequestStatusId;
                    await _policyRequestRepository.UpdateAsync(Result_PolicyRequest, cancellationToken);


                    Person Person = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
                    string StatusStr = _policyRequestStatusRepository.GetByIdAsync(cancellationToken,
                        _policyReqiestStatusInputViewModel.PolicyRequestStatusId).Result.Name;
                    byte? AuthorTypeID = User.PersonId == Result_PolicyRequest.RequestPersonId ? (byte?)1 : (byte?)2;
                    DAL.Models.PolicyRequestComment Model = new DAL.Models.PolicyRequestComment
                    {
                        PolicyRequestId = Result_PolicyRequest.Id,
                        AuthorId = Person.Id,
                        AuthorTypeId = AuthorTypeID,
                        CreatedDateTime = DateTime.Now,
                        Description =
                            $"این درخواست توسط کاربر {Person.FirstName + " " + Person.LastName} به وضعیت {StatusStr} تغییر یافت",
                        IsDeleted = false
                    };

                    await _policyRequestCommentRepository.AddAsync(Model, cancellationToken);

                    transaction.Complete();

                    return _mapper.Map<PolicyRequestSummaryOutputViewModel>(Result_PolicyRequest);
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsyncBySlug(string slug, long userId,
            long roleId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new BadRequestException("کاربر وجود ندارد");
            }

            var role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);
            if (role == null)
            {
                throw new BadRequestException("نقش وجود ندارد");
            }

            var userRole = await _userRoleRepository.GetUserRole(userId, roleId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("این کاربر با این نقش وجود ندارد");
            }

            PolicyRequestSlug policyRequestSlug = new PolicyRequestSlug();
            if (slug == "new")
                policyRequestSlug = PolicyRequestSlug.New_Slug;
            else if (slug == "inProgress")
                policyRequestSlug = PolicyRequestSlug.inProgress_Slug;
            else if (slug == "accepted")
                policyRequestSlug = PolicyRequestSlug.accepted_Slug;
            else
                throw new CustomException("وضعیت درخواست وارد شده نامعتبر است");

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<DAL.Models.PolicyRequest> model = new PagedResult<DAL.Models.PolicyRequest>();

            PersonCompany personCompany =
                await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            switch (userRole.Role.Name)
            {
                case "CompanyAdmin":
                    model = await _policyRequestRepository.GetByCompanyIdBySlug(policyRequestSlug,
                        personCompany.CompanyId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "CompanyAgent":
                    model = await _policyRequestRepository.GetByReviewerIdBySlug(policyRequestSlug,
                        personCompany.PersonId.Value, pageAbleModel,
                        cancellationToken);
                    break;
                case "Admin":
                    model = await _policyRequestRepository.GetAllByPagingBySlug(policyRequestSlug, pageAbleModel,
                        cancellationToken);
                    break;
                default:
                    throw new BadRequestException("شما دسترسی لازم را ندارید");
            }

            return _mapper.Map<PagedResult<PolicyRequestViewModel>>(model);
        }

        #endregion

        #region PolicyRequestAgentSelect

        public async Task<PolicyRequestAgetSelectGetViewModel> GetPolicyRequestAgentSelect(Guid code,
            CancellationToken cancellationToken)
        {
            var ResultData = await _policyRequestRepository.GetPolicyRequestAndCompanyByCode(code, cancellationToken);
            if (ResultData == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");
            return _mapper.Map<PolicyRequestAgetSelectGetViewModel>(ResultData);
        }

        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> PolicyRequestAgentSelectUpdate(Guid code,
            PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate,
            CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest PolicyRequest = await _policyRequestRepository.GetByCode(code, cancellationToken);
            if (PolicyRequest == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");

            CompanyAgent companyAgent = await _agentRepository.GetByIdAsync(cancellationToken, PolicyRequestAgetSelectUpdate.AgentSelectedId);

            PolicyRequest.AgentSelectionTypeId = PolicyRequestAgetSelectUpdate.AgentSelectionTypeId;
            if (PolicyRequestAgetSelectUpdate.AgentSelectedId.HasValue)
            {
                PolicyRequest.AgentSelectedId = PolicyRequestAgetSelectUpdate.AgentSelectedId;
                PolicyRequest.ReviewerId = companyAgent.PersonId;
            }
            else
            {
                PolicyRequest.AgentSelectedId = null;
            }

            await _policyRequestRepository.UpdateAsync(PolicyRequest, cancellationToken);
            return _mapper.Map<PolicyRequestAgetSelectUpdateOutputViewModel>(PolicyRequest);
        }







        #endregion
    }
}