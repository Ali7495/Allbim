using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Company;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Models.Articles;
using Services.ArticleManagement;
using Models.Customer;
using Models.PageAble;
using DAL.Repositories;
using Models.Person;
using Models.PersonCompany;
using Models.CompanyPolicyRequest;
using Models.PolicyRequestSupplement;
using Models.BodySupplementInfo;
using Models.PolicyRequestIssue;
using Models.PolicyRequest;
using System.Linq;
using Common.Extensions;
using Models.PolicyRequestInspection;
using Models.CompanyUser;
using Models.User;
using Models.CompanyComment;
using Models.CompanyComission;
using Models.PolicyRequestCommet;
using Models.CompanyPolicySuplement;
using Models.CompanyFactor;
using Models.PolicyRequestFactor;
using Models.Payment;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IArticlesManagementService _articleService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly IInsuredRequestVehicleRepository _insuredRequestVehicleRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPolicyRequestHolderRepository _policyRequestHolderRepository;
        private readonly IPersonAddressService _personAddressService;
        private readonly IPersonService _personService;
        private readonly IPolicyRequestIssueRepository _policyRequestIssueRepository;
        private readonly IPolicyRequestDetailRepository _policyRequestDetailRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPolicyRequestInspectionRepository _policyRequestInspectionRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IPolicyRequestAttachmentRepository _policyRequestAttachmentRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserService _userService;
        private readonly IPolicyRequestCommentRepository _policyRequestCommentRepository;
        private readonly IPolicyRequestCommentAttachmentRepository _policyRequestCommentAttachmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IPolicyRequestStatusRepository _policyRequestStatusRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPolicyRequestFactorRepository _policyRequestFactorRepository;
        private readonly IPolicyRequestFactorDetailRepository _policyRequestFactorDetailRepository;

        public CompanyService(IPersonRepository personRepository, IPersonCompanyRepository personCompanyRepository, IUserRepository userRepository, ICompanyRepository companyRepository, IOptionsSnapshot<PagingSettings> pagingSettings, ICustomerRepository customerRepository,
            IMapper mapper, IArticlesManagementService articleService, IPolicyRequestRepository policyRequestRepository, IInsuredRequestVehicleRepository insuredRequestVehicleRepository, IAddressRepository addressRepository, IPolicyRequestHolderRepository policyRequestHolderRepository, IPersonAddressService personAddressService, IPersonService personService, IPolicyRequestIssueRepository policyRequestIssueRepository, IPolicyRequestDetailRepository policyRequestDetailRepository, IVehicleRepository vehicleRepository, IPolicyRequestInspectionRepository policyRequestInspectionRepository, IAgentRepository agentRepository, IPolicyRequestAttachmentRepository policyRequestAttachmentRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IUserService userService, IPolicyRequestCommentRepository policyRequestCommentRepository, IPolicyRequestCommentAttachmentRepository policyRequestCommentAttachmentRepository, IAttachmentRepository attachmentRepository, IPolicyRequestStatusRepository policyRequestStatusRepository, IPaymentRepository paymentRepository, IPolicyRequestFactorRepository policyRequestFactorRepository, IPolicyRequestFactorDetailRepository policyRequestFactorDetailRepository)
        {
            _personRepository = personRepository;
            _companyRepository = companyRepository;
            _pagingSettings = pagingSettings.Value;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _articleService = articleService;
            _personCompanyRepository = personCompanyRepository;
            _mapper = mapper;
            _policyRequestRepository = policyRequestRepository;
            _insuredRequestVehicleRepository = insuredRequestVehicleRepository;
            _addressRepository = addressRepository;
            _policyRequestHolderRepository = policyRequestHolderRepository;
            _personAddressService = personAddressService;
            _personService = personService;
            _policyRequestIssueRepository = policyRequestIssueRepository;
            _policyRequestDetailRepository = policyRequestDetailRepository;
            _vehicleRepository = vehicleRepository;
            _policyRequestInspectionRepository = policyRequestInspectionRepository;
            _agentRepository = agentRepository;
            _policyRequestAttachmentRepository = policyRequestAttachmentRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userService = userService;
            _policyRequestCommentRepository = policyRequestCommentRepository;
            _policyRequestCommentAttachmentRepository = policyRequestCommentAttachmentRepository;
            _attachmentRepository = attachmentRepository;
            _policyRequestStatusRepository = policyRequestStatusRepository;
            _paymentRepository = paymentRepository;
            _policyRequestFactorRepository = policyRequestFactorRepository;
            _policyRequestFactorDetailRepository = policyRequestFactorDetailRepository;
        }


        public async Task<CompanyDetailViewModel> create(CompanyInputViewModel companyInputViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                ArticleResultViewModel article = null;
                if (companyInputViewModel.Article != null)
                {
                    article = await _articleService.create(companyInputViewModel.Article, cancellationToken);
                }

                var company = new Company
                {
                    Name = companyInputViewModel.Name,
                    Description = companyInputViewModel.Description,
                    ArticleId = article?.Id,
                    AvatarUrl = companyInputViewModel.AvatarUrl,
                    EstablishedYear = companyInputViewModel.EstablishedYear
                };

                await _companyRepository.AddAsync(company, cancellationToken);

                transaction.Complete();
                return _mapper.Map<CompanyDetailViewModel>(company);
            }
        }

        public async Task<CompanyDetailViewModel> update(string code, CompanyInputViewModel companyViewModel, CancellationToken cancellationToken)
        {
            var company_code = Guid.Parse(code);
            var company = await _companyRepository.GetByCode(company_code, cancellationToken);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            company.Name = companyViewModel.Name;
            company.Description = companyViewModel.Description;

            await updateCommon(company, cancellationToken);
            return _mapper.Map<CompanyDetailViewModel>(company);
        }

        public async Task<bool> delete(string code, CancellationToken cancellationToken)
        {
            var company_code = Guid.Parse(code);
            var company = await _companyRepository.GetByCode(company_code, cancellationToken);
            if (company == null)
                throw new BadRequestException(" شرکت");

            company.IsDeleted = true;
            await _companyRepository.UpdateAsync(company, cancellationToken);
            return true;
        }

        public async Task<CompanyDetailViewModel> detail(string code, CancellationToken cancellationToken)
        {
            var company_code = Guid.Parse(code);
            var company = await _companyRepository.GetByCodeNoTracking(company_code, cancellationToken);
            if (company == null)
                throw new BadRequestException(" شرکت");
            return _mapper.Map<CompanyDetailViewModel>(company);
        }

        public async Task<PagedResult<CompanyViewModel>> all(int? page, int? pageSize,
            CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = await _companyRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<CompanyViewModel>>(companies);
        }
        public async Task<List<CompanyViewModel>> allWithoutPaging(
            CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<CompanyViewModel>>(companies);
        }

        public async Task<PagedResult<CustomerViewModel>> GetAllCustomersMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با شرکتی ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<DAL.Models.PolicyRequest> Customers = await _customerRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.Insurer.CompanyId == company.Id,
                i => i.RequestPerson
            );

            return _mapper.Map<PagedResult<CustomerViewModel>>(Customers);
        }

        public async Task<PagedResult<CustomerViewModel>> GetAllCustomersOfCompany(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<DAL.Models.PolicyRequest> Customers = await _customerRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.Insurer.CompanyId == company.Id,
                i => i.RequestPerson
            );

            return _mapper.Map<PagedResult<CustomerViewModel>>(Customers);
        }

        public async Task<CompanyDetailViewModel> detailMine(long UserID, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }

            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            return _mapper.Map<CompanyDetailViewModel>(company);
        }

        public async Task<CompanyDetailViewModel> updateMine(long UserID, CompanyInputViewModel companyViewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }

            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            company.Name = companyViewModel.Name;
            company.Description = companyViewModel.Description;

            await updateCommon(company, cancellationToken);
            return _mapper.Map<CompanyDetailViewModel>(company);
        }

        public async Task updateCommon(Company company, CancellationToken cancellationToken)
        {
            await _companyRepository.UpdateAsync(company, cancellationToken);
        }

        public async Task<PersonCompanyDTOViewModel> UpdatePersonCompany(Guid companyCode, Guid personCode, PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByCode(personCode, cancellationToken);
            if (person == null)
                throw new BadRequestException("کد فرد وجود ندارد");
            var company = await _companyRepository.GetByCode(companyCode, cancellationToken);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");
            PersonCompany _personCompany = await UpdatePersonCompanyCommon(person.Id, company.Id, personCompanyInputViewModel, cancellationToken);
            return new PersonCompanyDTOViewModel { Position = personCompanyInputViewModel.Position, CompanyCode = companyCode, PersonCode = personCode };
        }
        public async Task<PersonCompany> UpdatePersonCompanyCommon(long PersonID, long CompanyID, PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken)
        {
            var PersonCompanyResult = await _personCompanyRepository.GetByPersonIdAndCompanyId(PersonID, CompanyID, cancellationToken);
            if (PersonCompanyResult == null)
            {
                PersonCompany Model = new PersonCompany
                {
                    CompanyId = CompanyID,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    PersonId = PersonID,
                    Position = personCompanyInputViewModel.Position
                };
                await _personCompanyRepository.AddAsync(Model, cancellationToken);
                return Model;
            }
            else
            {
                PersonCompanyResult.PersonId = PersonID;
                PersonCompanyResult.CompanyId = CompanyID;
                PersonCompanyResult.Position = personCompanyInputViewModel.Position;
                await _personCompanyRepository.UpdateAsync(PersonCompanyResult, cancellationToken);
                return PersonCompanyResult;
            }

        }

        public async Task<bool> DeletePersonCompany(Guid companyCode, Guid personCode, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByCode(personCode, cancellationToken);
            if (person == null)
                throw new BadRequestException("کد فرد وجود ندارد");
            var company = await _companyRepository.GetByCode(companyCode, cancellationToken);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");
            bool Result = await DeletePersonCompanyCommon(person.Id, company.Id, cancellationToken);
            return Result;
        }

        public async Task<bool> DeletePersonCompanyCommon(long PersonID, long CompanyID, CancellationToken cancellationToken)
        {
            var PersonCompanyResult = await _personCompanyRepository.GetByPersonIdAndCompanyId(PersonID, CompanyID, cancellationToken);
            if (PersonCompanyResult == null)
                throw new BadRequestException("موردی بافت نشد");
            await _personCompanyRepository.DeleteAsync(PersonCompanyResult, cancellationToken);
            return true;
        }

        public async Task<PersonCompanyDTOViewModel> UpdatePersonCompanyMine(Guid companyCode, long UserID, PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var person = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
            //var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            var company = await _companyRepository.GetByCode(companyCode, cancellationToken);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");
            PersonCompany _personCompany = await UpdatePersonCompanyCommon(User.PersonId, company.Id, personCompanyInputViewModel, cancellationToken);
            //return _mapper.Map<PersonCompanyDTOViewModel>(_personCompany);
            return new PersonCompanyDTOViewModel { CompanyCode = companyCode, PersonCode = person.Code, Position = personCompanyInputViewModel.Position };
        }

        public async Task<bool> DeletePersonCompanyMine(Guid companyCode, long UserID, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
                throw new BadRequestException(" کاربر وجود ندارد");

            //var person = await _personService.GetByIdAsync(cancellationToken, User.PersonId);
            //if (person == null)
            //    throw new BadRequestException("کد فرد وجود ندارد");
            var company = await _companyRepository.GetByCode(companyCode, cancellationToken);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");
            bool Result = await DeletePersonCompanyCommon(User.PersonId, company.Id, cancellationToken);
            return Result;
        }

        public async Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeusts(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            if (!await IsValidCompanyCommon(code, cancellationToken))
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<DAL.Models.PolicyRequest> policyRequests = await _policyRequestRepository.GetRequestsByCompanyCode(code, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestViewModel>>(policyRequests);
        }

        public async Task<bool> IsValidCompanyCommon(Guid code, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCode(code, cancellationToken);

            if (company == null)
            {
                return false;
            }

            return true;
        }

        public async Task<DAL.Models.PolicyRequest> GetPolicyRequestCommon(Guid code, Guid policyCode, CancellationToken cancellation)
        {
            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetRequestByCompanyCode(code, policyCode, cancellation);

            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست وجود ندارد");
            }

            return policyRequest;
        }

        public async Task<CompanyPolicyRequestViewModel> GetCompanyPolicyReqeust(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsValidCompanyCommon(code, cancellationToken))
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetPolicyRequestCommon(code, policyCode, cancellationToken);

            return _mapper.Map<CompanyPolicyRequestViewModel>(policyRequest);
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

        public async Task<PersonCompany> GetPersonCompanyCommon(long userId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیچ شرکتی ندارید");
            }

            return personCompany;
        }

        public async Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<DAL.Models.PolicyRequest> policyRequests = await _policyRequestRepository.GetRequestsByCompanyId(personCompany.CompanyId.Value, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestViewModel>>(policyRequests);
        }

        public async Task<CompanyPolicyRequestViewModel> GetCompanyPolicyReqeustMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);


            DAL.Models.PolicyRequest policyRequest = await GetPolicyRequestCommon(personCompany.Company.Code, policyCode, cancellationToken);

            return _mapper.Map<CompanyPolicyRequestViewModel>(policyRequest);
        }

        public async Task<DAL.Models.PolicyRequest> GetMyCompanyPolicyRequestCommon(long companyId, Guid policyCode, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != companyId)
            {
                throw new BadRequestException("درخواست متعلق به این شرکت نمی باشد");
            }

            return policyRequest;
        }

        public async Task<Person> addOrUpdatePolicyRequestHolderPersonCommon(PolicySupplementPersonViewModel personViewModel,
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

        public async Task<Address> addOrUpdatePolicyRequestHolderAddressCommon(
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


        public async Task<PolicySupplementViewModel> CreateCompanyPolicyRequestHolderSupplementInfoCommon(DAL.Models.PolicyRequest policyRequest, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {


            PolicySupplementViewModel inputHolder = viewModel;

            # region OwnerPerson

            Person ownerPerson =
                await addOrUpdatePolicyRequestHolderPersonCommon(viewModel.OwnerPerson, cancellationToken);
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
                    await addOrUpdatePolicyRequestHolderPersonCommon(viewModel.IssuedPerson, cancellationToken);
                if (issuedPerson == null)
                {
                    throw new BadRequestException("خطای درج داده");
                }

                issuedPersonId = issuedPerson.Id;
            }

            Address address = await addOrUpdatePolicyRequestHolderAddressCommon(viewModel.Address, cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("خطای درج داده");
            }

            long addressId = address.Id;

            #endregion

            #region Policy Holder

            PolicyRequestHolder holder;
            holder = await _policyRequestHolderRepository.GetByPolicyRequestCode(policyRequest.Code,
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


        public async Task<PolicySupplementViewModel> CreateCompanyPolicyRequestHolderSupplementInfoAsyncMine(long userId, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);


            return await CreateCompanyPolicyRequestHolderSupplementInfoCommon(policyRequest, policyCode, viewModel, cancellationToken);
        }

        public async Task<PolicyRequestHolder> AddOrUpdateCompanyBodyOwner(Person ownerPerson,
            CompanyBodySupplementInfoViewModel viewModel,
            PolicyRequestHolder policyRequestHolder, CancellationToken cancellationToken)
        {

            ownerPerson.NationalCode = viewModel.OwnerPerson.NationalCode;
            ownerPerson.BirthDate = viewModel.OwnerPerson.BirthDate;
            ownerPerson.GenderId = viewModel.OwnerPerson.GenderId;
            await _personRepository.UpdateAsync(ownerPerson, cancellationToken);

            InsuredRequestVehicle insuredRequestVehicle = await
                _insuredRequestVehicleRepository.GetInsuredRequestVehicleByPolicyRequestIdNoTracking(policyRequestHolder.PolicyRequestId,
                    cancellationToken);
            if (insuredRequestVehicle != null)
            {
                insuredRequestVehicle.OwnerPersonId = ownerPerson.Id;
                await _insuredRequestVehicleRepository.UpdateAsync(insuredRequestVehicle, cancellationToken);
            }

            long? addressId = policyRequestHolder?.AddressId;
            long personId = ownerPerson.Id;
            AddressViewModel addressViewModel = new AddressViewModel()
            {
                Description = viewModel.OwnerPerson.Description,
                CityId = viewModel.OwnerPerson.CityId,
                Mobile = ownerPerson.Users.FirstOrDefault().Username, // نام کاربری همان شماره تلفن کاربر است
            };
            PersonAddress personAddress = await _personAddressService.AddOrUpdateByAddressId(addressId, personId,
                addressViewModel, cancellationToken);
            policyRequestHolder.AddressId = personAddress.AddressId;
            policyRequestHolder.PersonId = personId;
            await _policyRequestHolderRepository.UpdateAsync(policyRequestHolder, cancellationToken);
            return policyRequestHolder;
        }

        public async Task<PolicyRequestHolder> AddOrUpdateCompanyBodyIssued(Person ownerPerson,
            CompanyBodySupplementInfoViewModel viewModel,
            PolicyRequestHolder policyRequestHolder, CancellationToken cancellationToken)
        {
            Person issuedPerson = null;
            long? issuedPersonId = policyRequestHolder?.PersonId;
            // وقتی به نام دیگری صادر می شود یعنی کاربر لاگین شده، با شخص صادر شده متفاوت است و نباید اطلاعات کاربر ویرایش شود

            if (issuedPersonId.HasValue && issuedPersonId.Value != ownerPerson.Id)
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

        public async Task<PolicyRequestHolder> AddOrUpdateCompanyPolicyRequestHolderCommon(DAL.Models.PolicyRequest policyRequest, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {


            PolicyRequestHolder policyRequestHolder = await
                _policyRequestHolderRepository.GetByPolicyRequestCodeNoTrackingWithoutDetails(policyRequest.Code, cancellationToken);
            if (policyRequestHolder == null)
            {
                policyRequestHolder = new PolicyRequestHolder();

                policyRequestHolder.PolicyRequestId = policyRequest.Id;

                await _policyRequestHolderRepository.AddAsync(policyRequestHolder, cancellationToken);
            }

            Person ownerPerson = await _personRepository.GetByCodeNoTrackingWithUser(viewModel.OwnerPerson.Code, cancellationToken);
            ownerPerson.Users.FirstOrDefault().Person = null;

            policyRequestHolder.IssuedPersonType = viewModel.IssuedPersonType;
            if (viewModel.IssuedPersonType == 1)
            {
                await AddOrUpdateCompanyBodyOwner(ownerPerson, viewModel, policyRequestHolder, cancellationToken);
            }
            else if (viewModel.IssuedPersonType == 2) // به نام دیگری صادر شود
            {
                await AddOrUpdateCompanyBodyIssued(ownerPerson, viewModel, policyRequestHolder, cancellationToken);
            }

            return policyRequestHolder;
        }

        public async Task<PolicySupplementViewModel> GetCompanyPolicyRequestHolderSupplementInfoMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);


            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code, cancellationToken);

            return _mapper.Map<PolicySupplementViewModel>(policyRequestHolder);
        }

        public async Task<CompanyBodySupplementInfoViewModel> AddOrUpdateCompanyBodySupplementMine(Guid policyCode, long userId, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            PolicyRequestHolder policyRequestHolder = await AddOrUpdateCompanyPolicyRequestHolderCommon(policyRequest, viewModel, cancellationToken);

            return _mapper.Map<CompanyBodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<BodySupplementInfoViewModel> GetCompanyBodySupplementMine(long userId, Guid policyCode,
            CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
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

        public async Task<PolicyRequestIssueViewModel> CreateOrUpdateCompanyPolicyRequestHolderIssueAsyncMine(long userId, Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

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


            return _mapper.Map<PolicyRequestIssueViewModel>(issue);
        }

        public async Task<PolicyRequestIssueViewModel> GetCompanyPolicyRequestHolderIssueAsyncMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            PolicyRequestIssue issue;
            issue = await _policyRequestIssueRepository.GetByPolicyRequestCode(policyRequest.Code,
                cancellationToken);

            return _mapper.Map<PolicyRequestIssueViewModel>(issue);

        }


        public async Task<PolicyRequestPaymentViewModel> GetCompanyPaymentInfoCommon(DAL.Models.PolicyRequest policyRequest, CancellationToken cancellationToken)
        {
            List<PolicyRequestDetail> details =
                await _policyRequestDetailRepository.GetDetailsByPolicyRequestId(policyRequest.Id,
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
                PaymentInfo = _mapper.Map<PaymentResultViewModel>(factors.FirstOrDefault(f => f.Payment.PaymentStatusId != 3 && f.Payment.PaymentStatusId != 4).Payment)
            };

            return model;
        }

        public async Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestPaymentDetailsMine(long userId, Guid policyCode,
            CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);


            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetPaymentDetailsByCode(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست مورد نظر وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != personCompany.CompanyId.Value)
            {
                throw new BadRequestException("شما به این درخواست دسترسی ندارید");
            }

            PolicyRequestPaymentViewModel result = await GetCompanyPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }



        public async Task<PolicyRequestInspection> AddOrUpdateCompanyPolicyRequestInspectionCommon(DAL.Models.PolicyRequest policyRequest, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
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

        public async Task<PolicyRequestInspectionResultViewModel> CreateOrUpdateCompanyPolicyRequestHolderInspectionAsyncMine(long userId,
            Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            PolicyRequestInspection inspection = await AddOrUpdateCompanyPolicyRequestInspectionCommon(policyRequest, viewModel, cancellationToken);

            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }


        public async Task<PolicyRequestInspectionResultViewModel> GetCompanyPolicyRequestHolderInspectionAsyncMine(long userId, Guid policyCode,
           CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);


            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code,
                cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }


        public async Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestBodyPaymentDetailsMine(long userId, Guid policyCode,
           CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }


            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);


            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetPaymentDetailsByCode(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست مورد نظر وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != personCompany.CompanyId.Value)
            {
                throw new BadRequestException("شما به این درخواست دسترسی ندارید");
            }

            PolicyRequestPaymentViewModel result = await GetPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }


        public async Task<PolicyRequestPaymentViewModel> GetPaymentInfoCommon(DAL.Models.PolicyRequest policyRequest, CancellationToken cancellationToken)
        {
            List<PolicyRequestDetail> details =
                await _policyRequestDetailRepository.GetDetailsByPolicyRequestId(policyRequest.Id,
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
                PaymentInfo = _mapper.Map<PaymentResultViewModel>(factors.FirstOrDefault(f => f.Payment.PaymentStatusId != 3 && f.Payment.PaymentStatusId != 4).Payment)
            };

            return model;
        }

        public async Task<PolicyRequestMineViewModel> GetCompanyPolicyDetailsMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            // PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            // PagedResult<PolicyRequestDetail> policyDetails = await _policyRequestDetailRepository.GetCompanyPolicyRequestDetailsByPolicyCode(policyRequest.Code, pageAbleModel, cancellationToken);

            return _mapper.Map<PolicyRequestMineViewModel>(policyRequest);
        }

        public async Task<PolicyRequestAgetSelectGetViewModel> GetCompanyPolicyRequestAgentSelectMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            if (!await IsUserValidCommon(userId, cancellationToken))
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            var ResultData = await _policyRequestRepository.GetPolicyRequestAndCompanyByCode(policyRequest.Code, cancellationToken);
            if (ResultData == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");
            return _mapper.Map<PolicyRequestAgetSelectGetViewModel>(ResultData);
        }

        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdateMine(long userId, Guid policyCode, PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken)
        {
            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            CompanyAgent companyAgent = await _agentRepository.GetByIdNoTracking(PolicyRequestAgetSelectUpdate.AgentSelectedId.Value, cancellationToken);
            if (companyAgent == null)
            {
                throw new BadRequestException("چنین نماینده ای وجود ندارد");
            }

            policyRequest.AgentSelectionTypeId = PolicyRequestAgetSelectUpdate.AgentSelectionTypeId;
            if (PolicyRequestAgetSelectUpdate.AgentSelectedId.HasValue)
            {
                policyRequest.AgentSelectedId = PolicyRequestAgetSelectUpdate.AgentSelectedId;
                policyRequest.ReviewerId = companyAgent.PersonId;
            }
            else
            {
                policyRequest.AgentSelectedId = null;
            }

            await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);
            return _mapper.Map<PolicyRequestAgetSelectUpdateOutputViewModel>(policyRequest);
        }


        public async Task<List<PolicyRequestAttachmentDownloadViewModel>> GetCompanyPolicyRequestAttachmentsMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(personCompany.CompanyId.Value, policyCode, cancellationToken);

            List<PolicyRequestAttachment> policyRequestAttachments =
                await _policyRequestAttachmentRepository.GetByPolicyRequestCode(policyRequest.Code,
                    cancellationToken);
            return _mapper.Map<List<PolicyRequestAttachmentDownloadViewModel>>(policyRequestAttachments);
        }

        public async Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsByStatusMine(long userId, long roleId, string status, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PersonCompany personCompany = await GetPersonCompanyCommon(userId, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            //PagedResult<DAL.Models.PolicyRequest> policyRequests = await _policyRequestRepository.GetAllByPaging(pageAbleModel, cancellationToken);

            PagedResult<DAL.Models.PolicyRequest> policyRequests = new PagedResult<DAL.Models.PolicyRequest>();

            Role role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);



            switch (status)
            {
                case "all":

                    if (role.Name == "CompanyAgent")
                    {
                        policyRequests = await _policyRequestRepository.GetStatusAllRequestsByAgentCompanyId(personCompany.CompanyId.Value, personCompany.PersonId.Value, pageAbleModel, cancellationToken);
                    }
                    else
                    {
                        policyRequests = await _policyRequestRepository.GetStatusAllRequestsByCompanyId(personCompany.CompanyId.Value, pageAbleModel, cancellationToken);
                    }


                    break;
                case "new":

                    if (role.Name == "CompanyAgent")
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByNewStatusAndCompanyAgentId(personCompany.CompanyId.Value, personCompany.PersonId.Value, pageAbleModel, cancellationToken);
                    }
                    else
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByNewStatusAndCompanyId(personCompany.CompanyId.Value, pageAbleModel, cancellationToken);
                    }

                    break;
                case "finished":

                    if (role.Name == "CompanyAgent")
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByFinishedStatusAndCompanyAgentId(personCompany.CompanyId.Value, personCompany.PersonId.Value, pageAbleModel, cancellationToken);
                    }
                    else
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByFinishedStatusAndCompanyId(personCompany.CompanyId.Value, pageAbleModel, cancellationToken);
                    }


                    break;
                case "unfinished":

                    if (role.Name == "CompanyAgent")
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByUnFinishedStatusAndCompanyAgentId(personCompany.CompanyId.Value, personCompany.PersonId.Value, pageAbleModel, cancellationToken);
                    }
                    else
                    {
                        policyRequests = await _policyRequestRepository.GetRequestsByUnFinishedStatusAndCompanyId(personCompany.CompanyId.Value, pageAbleModel, cancellationToken);
                    }


                    break;
            }

            if (policyRequests == null)
            {
                throw new BadRequestException("درخواستی وجود ندارد");
            }

            return _mapper.Map<PagedResult<CompanyPolicyRequestViewModel>>(policyRequests);
        }

        public async Task<PagedResult<CompanyUserResultViewModel>> GetCompanyUsers(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Person> persons = await _personCompanyRepository.GetUsersByParentId(personCompany.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyUserResultViewModel>>(persons);
        }

        public async Task<bool> IsUsernameUniqueCommon(string oldUsername, string newUsername,
            CancellationToken cancellationToken)
        {
            if (oldUsername != newUsername)
            {
                User otherUser = await _userRepository.GetByUserName(newUsername);
                if (otherUser != null)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> IsUserAssignedToYou(User user, long parentId, CancellationToken cancellationToken)
        {
            PersonCompany personCompany = await _personCompanyRepository.GetUserByParentAndUserId(parentId, user.Id, cancellationToken);

            if (personCompany == null)
            {
                return false;
            }

            return true;
        }

        public async Task<UpdatedUserResultViewModel> UpdateCompanyUser(long userId, Guid userCode, CompanyUserUpdateInputViewModel viewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }


            User user = await _userRepository.GetUserByCode(userCode, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("یوزری با این کد وجود ندارد");
            }

            if (!await IsUserAssignedToYou(user, personCompany.Id, cancellationToken))
            {
                throw new BadRequestException("کاربری با این کد زیر نظر شما وجود ندارد");
            }

            if (!await IsUsernameUniqueCommon(user.Username, viewModel.Username, cancellationToken))
            {
                throw new BadRequestException("این نام کاربری تکراری می باشد");
            }



            user.Username = viewModel.Username;
            user.Email = viewModel.Email;

            await _userRepository.UpdateAsync(user, cancellationToken);

            UpdatedUserResultViewModel model = _mapper.Map<UpdatedUserResultViewModel>(user);

            if (viewModel.RoleId == 2)
            {
                UserRole userRole = await _userRoleRepository.GetSingleUserRoleByUserId(user.Id, cancellationToken);
                await _userRoleRepository.DeleteAsync(userRole, cancellationToken);

                userRole = new UserRole()
                {
                    RoleId = viewModel.RoleId,
                    UserId = user.Id
                };

                await _userRoleRepository.AddAsync(userRole, cancellationToken);

                Role role = await _roleRepository.GetByIdAsync(cancellationToken, viewModel.RoleId);

                model.Role = _mapper.Map<PersonRoleResultViewModel>(role);
            }



            return model;

        }

        public async Task<bool> DeleteCompanyUser(long userId, Guid userCode, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

                if (personCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }

                Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
                if (company == null)
                {
                    throw new BadRequestException("کد شرکت وجود ندارد");
                }


                User user = await _userRepository.GetUserByCode(userCode, cancellationToken);
                if (user == null)
                {
                    throw new BadRequestException("یوزری با این کد وجود ندارد");
                }

                if (!await IsUserAssignedToYou(user, personCompany.Id, cancellationToken))
                {
                    throw new BadRequestException("کاربری با این کد زیر نظر شما وجود ندارد");
                }

                UserRole userRole = await _userRoleRepository.GetSingleUserRoleByUserId(user.Id, cancellationToken);

                if (userRole != null)
                {
                    await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
                }

                await _userRepository.DeleteAsync(user, cancellationToken);

                transaction.Complete();

                return true;
            }
        }

        public async Task<CompanySingleUserResultViewModel> GetCompanyUser(long userId, Guid UserCode, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }


            User user = await _userRepository.GetUserByCodeWithDetail(UserCode, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("یوزری با این کد وجود ندارد");
            }

            if (!await IsUserAssignedToYou(user, personCompany.Id, cancellationToken))
            {
                throw new BadRequestException("کاربری با این کد زیر نظر شما وجود ندارد");
            }

            return _mapper.Map<CompanySingleUserResultViewModel>(user);
        }

        public async Task<string> ChangeUserPassword(long userId, Guid UserCode, UserChangePasswordViewModel viewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            User user = await _userRepository.GetUserByCodeWithDetail(UserCode, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("یوزری با این کد وجود ندارد");
            }

            if (!await IsUserAssignedToYou(user, personCompany.Id, cancellationToken))
            {
                throw new BadRequestException("کاربری با این کد زیر نظر شما وجود ندارد");
            }

            string result = await _userService.ChangeUserPasswordCommon(UserCode, viewModel, cancellationToken);

            return result;
        }

        public async Task<PagedResult<PersonResultViewModel>> GetAllPersonsWithoutUser(long userId, string search_text, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }


            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Person> persons = new PagedResult<Person>();

            if (String.IsNullOrEmpty(search_text))
            {
                persons = await _personCompanyRepository.GetAllPersonsWithoutUserByParentIdAsync(personCompany.Id, pageAbleModel, cancellationToken);
            }
            else
            {
                persons = await _personCompanyRepository.GetAllPersonsWithoutUserByParentIdBySearchTextAsync(personCompany.Id, search_text, pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<PersonResultViewModel>>(persons);
        }


        public async Task<UserResultViewModel> CreateCompanyUser(long userId, UserInputViewModel userInputViewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            Person person = await _personRepository.GetByCodeNoTracking(userInputViewModel.PersonCode, cancellationToken);
            if (person == null)
                throw new BadRequestException(" این شخص وجود ندارد");


            User user = await _userRepository.GetUserByPersonCode(userInputViewModel.PersonCode, cancellationToken);
            if (user != null)
            {
                throw new BadRequestException(" این شخص دارای حساب کاربری می باشد و نمی توان حساب جدیدی درج کرد");
            }

            User oldUser = await _userRepository.GetByUserName(userInputViewModel.Username);
            if (oldUser != null)
            {
                throw new BadRequestException("این نام کاربری قبلا ثبت شده است");
            }


            user = new User
            {
                Code = Guid.NewGuid(),
                Username = userInputViewModel.Username,
                Password = SecurityHelper.GetSha256Hash(userInputViewModel.Password),
                Email = userInputViewModel.Email,
                PersonId = person.Id,
                IsDeleted = false
            };
            await _userRepository.AddAsync(user, cancellationToken);

            UserResultViewModel result = _mapper.Map<UserResultViewModel>(user);

            result.PersonCode = person.Code;
            result.Person = _mapper.Map<UserPersonViewModel>(person);

            return result;
        }

        public async Task<PolicyRequestCommentGetAllOutputViewModel> CreateCompanyCommentMine(long userId, Guid policyCode, CompanyCommentInputMineViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonIdWithPerson(currentUser.PersonId, cancellationToken);

                if (personCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }

                DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetByCompanyIdAndPolicyCode(personCompany.CompanyId.Value, policyCode, cancellationToken);
                if (policyRequest == null)
                {
                    throw new BadRequestException("درخواستی با این کد برای شما وجود ندارد");
                }

                //Person author = await _personRepository.GetByCodeNoTracking(viewModel.AuthorCode, cancellationToken);
                //if (author == null)
                //{
                //    throw new BadRequestException("شخصی با این کد وجود ندارد");
                //}
                byte? AuthorTypeId = currentUser.PersonId == policyRequest.RequestPersonId ? (byte?)1 : (byte?)2;
                policyRequest.PolicyRequestStatusId = viewModel.Status;


                DAL.Models.PolicyRequestComment comment = new DAL.Models.PolicyRequestComment()
                {
                    PolicyRequestId = policyRequest.Id,
                    Description = viewModel.Description,
                    AuthorId = currentUser.PersonId,
                    AuthorTypeId = AuthorTypeId
                };

                await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);
                await _policyRequestCommentRepository.AddAsync(comment, cancellationToken);


                if (viewModel.AttachmentCodes != null)
                {
                    for (int i = 0; i < viewModel.AttachmentCodes.Count; i++)
                    {


                        PolicyRequestCommentAttachment commentAttachment = new PolicyRequestCommentAttachment();

                        commentAttachment.PolicyRequestCommentId = comment.Id;
                        commentAttachment.AttachmentTypeId = 1;

                        //if (viewModel.PolicyRequestCommentAttachments[i].AttachmentCode != null)
                        //{
                        //    Attachment attachment = await _attachmentRepository.GetByCode(viewModel.PolicyRequestCommentAttachments[i].AttachmentCode.Value, cancellationToken);
                        //    commentAttachment.AttachmentId = attachment.Id;
                        //}
                        //else
                        //{
                        //    commentAttachment.AttachmentId = null;
                        //}

                        Attachment attachment = await _attachmentRepository.GetByCode(viewModel.AttachmentCodes[i], cancellationToken);
                        commentAttachment.AttachmentId = attachment.Id;


                        await _policyRequestCommentAttachmentRepository.AddAsync(commentAttachment, cancellationToken);
                    }
                }

                List<PolicyRequestCommentAttachment> attachments = await _policyRequestCommentAttachmentRepository.GetCommentAttachmentByCommentId(comment.Id, cancellationToken);

                transaction.Complete();

                PolicyRequestCommentGetAllOutputViewModel returnedViewModel = _mapper.Map<PolicyRequestCommentGetAllOutputViewModel>(comment);



                returnedViewModel.PolicyRequestCode = policyCode;
                returnedViewModel.Attachments = _mapper.Map<List<PolicyRequestCommentAttachmentViewModel>>(attachments);

                return returnedViewModel;
            }
        }

        public async Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllPolicyCommentsMine(long userId, Guid policyCode, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            List<DAL.Models.PolicyRequestComment> comments = await _policyRequestCommentRepository.GetPolicyRequestCommentsByCompanyIdAndPolicyCode(personCompany.CompanyId.Value, policyCode, cancellationToken);
            // if (comments == null || comments.Count == 0)
            // {
            //     throw new BadRequestException("توضیحاتی با این کد برای شما وجود ندارد");
            // }

            return _mapper.Map<List<PolicyRequestCommentGetAllOutputViewModel>>(comments);
        }

        public async Task<PolicyRequestCommentGetAllOutputViewModel> CreateCompanyComment(long userId, Guid companyCode, Guid policyCode, CompanyCommentInputViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

                DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetByCodeNoTracking(policyCode, cancellationToken);
                if (policyRequest == null)
                {
                    throw new BadRequestException("درخواستی با این کد وجود ندارد");
                }

                byte? AuthorTypeId;
                long authorId = 0;

                if (viewModel.AuthorCode != null)
                {
                    Person author = await _personRepository.GetByCodeNoTracking(viewModel.AuthorCode.Value, cancellationToken);
                    if (author == null)
                    {
                        throw new BadRequestException("شخصی با این کد وجود ندارد");
                    }
                    AuthorTypeId = author.Id == policyRequest.RequestPersonId ? (byte?)1 : (byte?)2;
                    policyRequest.PolicyRequestStatusId = viewModel.Status;
                    authorId = author.Id;
                }
                else
                {
                    AuthorTypeId = currentUser.PersonId == policyRequest.RequestPersonId ? (byte?)1 : (byte?)2;
                    policyRequest.PolicyRequestStatusId = viewModel.Status;
                    authorId = currentUser.PersonId;
                }




                DAL.Models.PolicyRequestComment comment = new DAL.Models.PolicyRequestComment()
                {
                    PolicyRequestId = policyRequest.Id,
                    Description = viewModel.Description,
                    AuthorId = authorId,
                    AuthorTypeId = AuthorTypeId
                };

                await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);
                await _policyRequestCommentRepository.AddAsync(comment, cancellationToken);


                if (viewModel.AttachmentCodes != null)
                {
                    for (int i = 0; i < viewModel.AttachmentCodes.Count; i++)
                    {


                        PolicyRequestCommentAttachment commentAttachment = new PolicyRequestCommentAttachment();

                        commentAttachment.PolicyRequestCommentId = comment.Id;
                        commentAttachment.AttachmentTypeId = 1;

                        //if (viewModel.PolicyRequestCommentAttachments[i].AttachmentCode != null)
                        //{
                        //    Attachment attachment = await _attachmentRepository.GetByCode(viewModel.PolicyRequestCommentAttachments[i].AttachmentCode.Value, cancellationToken);
                        //    commentAttachment.AttachmentId = attachment.Id;
                        //}
                        //else
                        //{
                        //    commentAttachment.AttachmentId = null;
                        //}

                        Attachment attachment = await _attachmentRepository.GetByCode(viewModel.AttachmentCodes[i], cancellationToken);
                        commentAttachment.AttachmentId = attachment.Id;

                        await _policyRequestCommentAttachmentRepository.AddAsync(commentAttachment, cancellationToken);
                    }
                }

                List<PolicyRequestCommentAttachment> attachments = await _policyRequestCommentAttachmentRepository.GetCommentAttachmentByCommentId(comment.Id, cancellationToken);

                transaction.Complete();

                PolicyRequestCommentGetAllOutputViewModel returnedViewModel = _mapper.Map<PolicyRequestCommentGetAllOutputViewModel>(comment);



                returnedViewModel.PolicyRequestCode = policyCode;
                returnedViewModel.Attachments = _mapper.Map<List<PolicyRequestCommentAttachmentViewModel>>(attachments);

                return returnedViewModel;
            }
        }

        public async Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllPolicyComments(Guid companyCode, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(companyCode, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکتی  با این کد وجود ندارد");
            }

            List<DAL.Models.PolicyRequestComment> comments = await _policyRequestCommentRepository.GetPolicyRequestCommentsByCompanyIdAndPolicyCode(company.Id, policyCode, cancellationToken);
            if (comments == null || comments.Count == 0)
            {
                throw new BadRequestException("توضیحاتی با این کد برای شما وجود ندارد");
            }

            return _mapper.Map<List<PolicyRequestCommentGetAllOutputViewModel>>(comments);
        }

        public async Task<CompanyComissionResultViewModel> UpdateCompanyPolicyComission(long userId, Guid policyCode, CompanyComissionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetByCompanyIdAndPolicyCode(personCompany.CompanyId.Value, policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("این درخواست به شرکت شما تعلق ندارد");
            }

            CompanyAgent companyAgent = await _agentRepository.GetByIdAsync(cancellationToken, viewModel.AgentSelectedId);
            if (companyAgent == null)
            {
                throw new BadRequestException("این نماینده وجود ندارد");
            }

            if (companyAgent.CompanyId != personCompany.CompanyId)
            {
                throw new BadRequestException("این نماینده به شرکت شما تعلق ندارد");
            }

            policyRequest.AgentSelectedId = viewModel.AgentSelectedId;
            policyRequest.AgentSelectionTypeId = 1;

            await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);

            CompanyComissionResultViewModel result = new CompanyComissionResultViewModel() { PolicyCode = policyCode };

            return result;
        }


        public async Task<PolicyRequestSummaryOutputViewModel> CompanyPlicyRequestStatusChange(Guid code, Guid policyCode, long RoleID,
            long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel,
            CancellationToken cancellationToken)
        {

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
                if (User == null)
                    throw new BadRequestException("کاربری یافت نشد");
                UserRole userRole = await _userRoleRepository.GetUserRole(UserID, RoleID, cancellationToken);
                if (userRole == null)
                    throw new BadRequestException("این کاربر با این نقش وجود ندارد");


                Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
                if (company == null)
                {
                    throw new BadRequestException("این شرکت وجود ندارد");
                }

                DAL.Models.PolicyRequest Result_PolicyRequest = await _policyRequestRepository.GetByCompanyIdAndPolicyCode(company.Id, policyCode, cancellationToken);
                if (Result_PolicyRequest == null)
                {
                    throw new BadRequestException("درخواستی برای این شرکت وجود ندارد");
                }

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

        public async Task<PolicyRequestSummaryOutputViewModel> CompanyPlicyRequestStatusChangeMine(Guid policyCode, long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
                if (User == null)
                    throw new BadRequestException("کاربری یافت نشد");

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);

                if (personCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }

                Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.Value, cancellationToken);
                if (company == null)
                {
                    throw new BadRequestException("شما ارتباطی به این شرکت ندارید");
                }

                DAL.Models.PolicyRequest Result_PolicyRequest = await _policyRequestRepository.GetByCompanyIdAndPolicyCode(company.Id, policyCode, cancellationToken);
                if (Result_PolicyRequest == null)
                {
                    throw new BadRequestException("درخواستی برای این شرکت وجود ندارد");
                }

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

        public async Task<PolicySupplementViewModel> CreateCompanyPolicyRequestHolderSupplementInfoAsync(Guid code, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            return await CreateCompanyPolicyRequestHolderSupplementInfoCommon(policyRequest, policyCode, viewModel, cancellationToken);
        }

        public async Task<PolicySupplementViewModel> GetCompanyPolicyRequestHolderSupplementInfo(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);


            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code, cancellationToken);

            return _mapper.Map<PolicySupplementViewModel>(policyRequestHolder);
        }

        public async Task<CompanyBodySupplementInfoViewModel> AddOrUpdateCompanyBodySupplement(Guid policyCode, Guid code, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestHolder policyRequestHolder = await AddOrUpdateCompanyPolicyRequestHolderCommon(policyRequest, viewModel, cancellationToken);

            return _mapper.Map<CompanyBodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<BodySupplementInfoViewModel> GetCompanyBodySupplement(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestHolder policyRequestHolder =
                await _policyRequestHolderRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code, cancellationToken);

            return _mapper.Map<BodySupplementInfoViewModel>(policyRequestHolder);
        }

        public async Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsByStatus(Guid code, string status, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);


            PagedResult<DAL.Models.PolicyRequest> policyRequests = new PagedResult<DAL.Models.PolicyRequest>();




            switch (status)
            {
                case "all":

                    policyRequests = await _policyRequestRepository.GetStatusAllRequestsByCompanyId(company.Id, pageAbleModel, cancellationToken);


                    break;
                case "new":

                    policyRequests = await _policyRequestRepository.GetRequestsByNewStatusAndCompanyId(company.Id, pageAbleModel, cancellationToken);


                    break;
                case "finished":

                    policyRequests = await _policyRequestRepository.GetRequestsByFinishedStatusAndCompanyId(company.Id, pageAbleModel, cancellationToken);



                    break;
                case "unfinished":

                    policyRequests = await _policyRequestRepository.GetRequestsByUnFinishedStatusAndCompanyId(company.Id, pageAbleModel, cancellationToken);



                    break;
            }

            if (policyRequests == null)
            {
                throw new BadRequestException("درخواستی وجود ندارد");
            }

            return _mapper.Map<PagedResult<CompanyPolicyRequestViewModel>>(policyRequests);
        }

        public async Task<PolicyRequestIssueViewModel> GetCompanyPolicyRequestHolderIssueAsync(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }
            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestIssue issue;
            issue = await _policyRequestIssueRepository.GetByPolicyRequestCode(policyRequest.Code,
                cancellationToken);

            return _mapper.Map<PolicyRequestIssueViewModel>(issue);
        }

        public async Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestPaymentDetails(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }


            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetPaymentDetailsByCode(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست مورد نظر وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != company.Id)
            {
                throw new BadRequestException("شما به این درخواست دسترسی ندارید");
            }

            PolicyRequestPaymentViewModel result = await GetCompanyPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }

        public async Task<PolicyRequestInspectionResultViewModel> GetCompanyPolicyRequestHolderInspectionAsync(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }



            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);


            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCodeNoTracking(policyRequest.Code,
                cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }

        public async Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestBodyPaymentDetails(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }


            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetPaymentDetailsByCode(policyCode, cancellationToken);
            if (policyRequest == null)
            {
                throw new BadRequestException("درخواست مورد نظر وجود ندارد");
            }

            if (policyRequest.Insurer.CompanyId != company.Id)
            {
                throw new BadRequestException("این درخواست به شرکت مورد نظر ارتباطی ندارد");
            }

            PolicyRequestPaymentViewModel result = await GetPaymentInfoCommon(policyRequest, cancellationToken);

            return result;
        }

        public async Task<PolicyRequestMineViewModel> GetCompanyPolicyDetails(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);



            return _mapper.Map<PolicyRequestMineViewModel>(policyRequest);
        }

        public async Task<PolicyRequestAgetSelectGetViewModel> GetCompanyPolicyRequestAgentSelect(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            var ResultData = await _policyRequestRepository.GetPolicyRequestAndCompanyByCode(policyRequest.Code, cancellationToken);
            if (ResultData == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");
            return _mapper.Map<PolicyRequestAgetSelectGetViewModel>(ResultData);
        }

        public async Task<List<PolicyRequestAttachmentDownloadViewModel>> GetCompanyPolicyRequestAttachments(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            List<PolicyRequestAttachment> policyRequestAttachments =
                await _policyRequestAttachmentRepository.GetByPolicyRequestCode(policyRequest.Code,
                    cancellationToken);
            return _mapper.Map<List<PolicyRequestAttachmentDownloadViewModel>>(policyRequestAttachments);
        }

        public async Task<PolicyRequestIssueViewModel> CreateOrUpdateCompanyPolicyRequestHolderIssueAsync(Guid code, Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

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


            return _mapper.Map<PolicyRequestIssueViewModel>(issue);
        }

        public async Task<PolicyRequestInspectionResultViewModel> CreateOrUpdateCompanyPolicyRequestHolderInspectionAsync(Guid code, Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }


            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestInspection inspection = await AddOrUpdateCompanyPolicyRequestInspectionCommon(policyRequest, viewModel, cancellationToken);

            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }

        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdate(Guid code, Guid policyCode, PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            CompanyAgent companyAgent = await _agentRepository.GetByIdNoTracking(PolicyRequestAgetSelectUpdate.AgentSelectedId.Value, cancellationToken);
            if (companyAgent == null)
            {
                throw new BadRequestException("چنین نماینده ای وجود ندارد");
            }

            policyRequest.AgentSelectionTypeId = PolicyRequestAgetSelectUpdate.AgentSelectionTypeId;
            if (PolicyRequestAgetSelectUpdate.AgentSelectedId.HasValue)
            {
                policyRequest.AgentSelectedId = PolicyRequestAgetSelectUpdate.AgentSelectedId;
                policyRequest.ReviewerId = companyAgent.PersonId;
            }
            else
            {
                policyRequest.AgentSelectedId = null;
            }

            await _policyRequestRepository.UpdateAsync(policyRequest, cancellationToken);
            return _mapper.Map<PolicyRequestAgetSelectUpdateOutputViewModel>(policyRequest);
        }

        public async Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllFactors(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetAllFactorsOfCompany(company.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestFactorResultViewModel>>(factors);
        }

        public async Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllPolicyFactors(Guid code, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetAllPolicyFactorsOfCompany(company.Id, policyRequest.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestFactorResultViewModel>>(factors);
        }

        public async Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactor(Guid code, Guid policyCode, long factorId, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetFactorByIdNoTrakingWithDetails(factorId, cancellationToken);

            return _mapper.Map<CompanyPolicyRequestFactorResultViewModel>(factor);
        }

        public async Task<PolicyRequestFactor> CheckIfFactorPaid(long factorId, CancellationToken cancellationToken)
        {
            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByIdWithPaymentAndDetails(factorId, cancellationToken);
            if (factor == null)
            {
                throw new BadRequestException("فاکتور مورد نظر وجود ندارد");
            }

            if (factor.Payment != null && factor.Payment.PaymentStatusId == 3)
            {
                throw new BadRequestException("این فاکتور پرداخت شده و امکان حذف ندارد");
            }

            return factor;
        }

        public async Task<bool> DeleteFactor(Guid code, Guid policyCode, long factorId, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

            factor.IsDeleted = true;
            factor.PolicyRequestFactorDetails.ToList().ForEach(d => d.IsDeleted = true);

            await _policyRequestFactorRepository.UpdateAsync(factor, cancellationToken);

            return true;
        }

        public async Task<decimal> CalculateSumOfDetailsPriceCommon(List<CompanyFactorDetailInputViewModel> details)
        {
            return await Task.Run<decimal>(() =>
            {
                decimal price = 0;

                for (int i = 0; i < details.Count; i++)
                {
                    if (details[i].CalculationTypeId == 1)
                    {
                        price -= details[i].Amount;
                    }
                    else
                    {
                        price += details[i].Amount;
                    }
                }

                return price;
            });
        }

        public async Task<decimal> CalculateUpdatedPriceCommon(CompanyFactorDetailInputViewModel detail, decimal price, decimal oldAmount)
        {
            return await Task.Run<decimal>(() =>
            {
                price -= oldAmount;

                if (detail.CalculationTypeId == 1)
                {
                    price -= detail.Amount;
                }
                else
                {
                    price += detail.Amount;
                }


                return price;
            });
        }

        public async Task<DAL.Models.PolicyRequest> CheckCompanyAndPolicyValidationCommon(Guid companyCode, Guid policyCode, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(companyCode, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("چنین شرکتی وجود ندارد");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            return policyRequest;
        }

        public async Task<CompanyPolicyRequestFactorResultViewModel> CreatePaymentFactorCommon(Guid code, Guid policyCode, CompanyPolicyFactorInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(code, policyCode, cancellationToken);

                long? paymentId = null;

                CompanyPolicyRequestFactorResultViewModel resultViewModel = new CompanyPolicyRequestFactorResultViewModel();
                CompanyPaymentResultViewModel companyPayment = new CompanyPaymentResultViewModel();

                if (inputViewModel.Payment != null)
                {
                    Payment payment = new Payment()
                    {
                        PaymentCode = inputViewModel.Payment.PaymentCode,
                        PaymentStatusId = 1,
                        PaymentGatewayId = inputViewModel.Payment.PaymentGatewayId,
                        CreatedDateTime = DateTime.Now,
                        Price = await CalculateSumOfDetailsPriceCommon(inputViewModel.PolicyRequestFactorDetails),
                        IsDeleted = false,
                        Description = inputViewModel.Payment.Description
                    };

                    await _paymentRepository.AddAsync(payment, cancellationToken);

                    paymentId = payment.Id;

                    companyPayment = _mapper.Map<CompanyPaymentResultViewModel>(payment);
                }

                PolicyRequestFactor policyRequestFactor = new PolicyRequestFactor()
                {
                    PaymentId = paymentId,
                    PolicyRequestId = policyRequest.Id,
                    IsDeleted = false
                };

                await _policyRequestFactorRepository.AddAsync(policyRequestFactor, cancellationToken);

                List<PolicyRequestFactorDetail> details = new List<PolicyRequestFactorDetail>();

                for (int i = 0; i < inputViewModel.PolicyRequestFactorDetails.Count; i++)
                {
                    PolicyRequestFactorDetail detail = new PolicyRequestFactorDetail()
                    {
                        PolicyRequestFactorId = policyRequestFactor.Id,
                        Amount = inputViewModel.PolicyRequestFactorDetails[i].Amount,
                        CalculationTypeId = inputViewModel.PolicyRequestFactorDetails[i].CalculationTypeId,
                        Description = "خرید " + policyRequest.Description,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    details.Add(detail);
                }

                await _policyRequestFactorDetailRepository.AddRangeAsync(details, cancellationToken);

                transaction.Complete();

                resultViewModel.Payment = companyPayment;
                resultViewModel.PolicyRequestFactorDetails = _mapper.Map<List<CompanyFactorDetailResultViewModel>>(details);
                resultViewModel.PolicyRequest = _mapper.Map<CompanyPolicyRequestResultViewModel>(policyRequest);
                resultViewModel.Id = policyRequestFactor.Id;


                return resultViewModel;
            }
        }

        public async Task<CompanyPolicyRequestFactorResultViewModel> CreatePaymentFactor(Guid code, Guid policyCode, CompanyPolicyFactorInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await CreatePaymentFactorCommon(code, policyCode, inputViewModel, cancellationToken);
        }

        public async Task<CompanyPolicyRequestFactorResultViewModel> CreatePaymentFactorMine(long userId, Guid policyCode, CompanyPolicyFactorInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            return await CreatePaymentFactorCommon(company.Code, policyCode, inputViewModel, cancellationToken);
        }

        public async Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllFactorsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetAllFactorsOfCompany(company.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestFactorResultViewModel>>(factors);
        }

        public async Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllPolicyFactorsMine(long userId, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = await _policyRequestFactorRepository.GetAllPolicyFactorsOfCompany(company.Id, policyRequest.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyPolicyRequestFactorResultViewModel>>(factors);
        }

        public async Task<CompanyFactorDetailResultViewModel> AddFactorDetailCommon(Guid companyCode, Guid policyCode, long factorId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(companyCode, policyCode, cancellationToken);

                PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);


                PolicyRequestFactorDetail detail = new PolicyRequestFactorDetail()
                {
                    PolicyRequestFactorId = factor.Id,
                    Amount = inputViewModel.Amount,
                    CalculationTypeId = inputViewModel.CalculationTypeId,
                    CreatedDate = DateTime.Now,
                    Description = "خرید " + policyRequest.Description,
                    IsDeleted = false
                };

                decimal price = await CalculateUpdatedPriceCommon(inputViewModel, factor.Payment.Price,0);

                Payment payment = factor.Payment;
                payment.Price = price;
                payment.UpdatedDateTime = DateTime.Now;

                factor.Payment = null;

                await _policyRequestFactorDetailRepository.AddAsync(detail, cancellationToken);
                await _paymentRepository.UpdateAsync(payment, cancellationToken);

                transaction.Complete();

                return _mapper.Map<CompanyFactorDetailResultViewModel>(detail);
            }
        }

        public async Task<CompanyFactorDetailResultViewModel> CreatePaymentFactorDetail(Guid code, Guid policyCode, long factorId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await AddFactorDetailCommon(code, policyCode, factorId, inputViewModel, cancellationToken);
        }

        public async Task<CompanyFactorDetailResultViewModel> CreatePaymentFactorDetailMine(long userId, Guid policyCode, long factorId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            return await AddFactorDetailCommon(company.Code, policyCode, factorId, inputViewModel, cancellationToken);

        }

        public async Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactorMine(long userId, Guid policyCode, long factorId, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetFactorByIdNoTrakingWithDetails(factorId, cancellationToken);

            return _mapper.Map<CompanyPolicyRequestFactorResultViewModel>(factor);
        }

        public async Task<bool> DeleteFactorMine(long userId, Guid policyCode, long factorId, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

            factor.IsDeleted = true;
            factor.PolicyRequestFactorDetails.ToList().ForEach(d => d.IsDeleted = true);

            await _policyRequestFactorRepository.UpdateAsync(factor, cancellationToken);

            return true;
        }

        public async Task<CompanyFactorDetailResultViewModel> UpdateFactorDetailCommon(Guid companyCode, Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(companyCode, policyCode, cancellationToken);

                PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

                PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

                decimal price = await CalculateUpdatedPriceCommon(inputViewModel, factor.Payment.Price, detail.Amount);

                detail.Description = inputViewModel.Description;
                detail.Amount = inputViewModel.Amount;
                detail.CalculationTypeId = inputViewModel.CalculationTypeId;

                factor.PolicyRequestFactorDetails = null;



                Payment payment = factor.Payment;
                payment.Price = price;
                payment.UpdatedDateTime = DateTime.Now;

                await _policyRequestFactorDetailRepository.UpdateAsync(detail, cancellationToken);
                await _paymentRepository.UpdateAsync(payment, cancellationToken);

                transaction.Complete();

                return _mapper.Map<CompanyFactorDetailResultViewModel>(detail);

            }
        }

        public async Task<CompanyFactorDetailResultViewModel> UpdatePaymentFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await UpdateFactorDetailCommon(code, policyCode, factorId, detailId, inputViewModel, cancellationToken);
        }

        public async Task<CompanyFactorDetailResultViewModel> UpdatePaymentFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            return await UpdateFactorDetailCommon(policyCode, policyRequest.Code, factorId, detailId, inputViewModel, cancellationToken);
        }

        public async Task<PagedResult<CompanyFactorDetailResultViewModel>> GetAllPolicyFactorDetials(Guid code, Guid policyCode, long factorId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(code, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByIdNoTracking(factorId, cancellationToken);
            if (factor == null)
            {
                throw new BadRequestException("فاکتور مورد نظر وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactorDetail> details = await _policyRequestFactorDetailRepository.GetFactorDetialsPaging(factorId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyFactorDetailResultViewModel>>(details);
        }

        public async Task<CompanyFactorDetailResultViewModel> GetCompanyFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(code, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByIdNoTracking(factorId, cancellationToken);
            if (factor == null)
            {
                throw new BadRequestException("فاکتور مورد نظر وجود ندارد");
            }

            PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

            return _mapper.Map<CompanyFactorDetailResultViewModel>(detail);
        }

        public async Task<bool> DeleteFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(code, policyCode, cancellationToken);

            PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

            factor.PolicyRequestFactorDetails = null;

            PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

            detail.IsDeleted = true;

            await _policyRequestFactorDetailRepository.UpdateAsync(detail, cancellationToken);

            return true;
        }

        public async Task<PagedResult<CompanyFactorDetailResultViewModel>> GetAllPolicyFactorDetialsMine(long userId, Guid policyCode, long factorId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByIdNoTracking(factorId, cancellationToken);
            if (factor == null)
            {
                throw new BadRequestException("فاکتور مورد نظر وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactorDetail> details = await _policyRequestFactorDetailRepository.GetFactorDetialsPaging(factorId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyFactorDetailResultViewModel>>(details);
        }

        public async Task<CompanyFactorDetailResultViewModel> GetCompanyFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetByIdNoTracking(factorId, cancellationToken);
            if (factor == null)
            {
                throw new BadRequestException("فاکتور مورد نظر وجود ندارد");
            }

            PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

            return _mapper.Map<CompanyFactorDetailResultViewModel>(detail);
        }

        public async Task<bool> DeleteFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            DAL.Models.PolicyRequest policyRequest = await GetMyCompanyPolicyRequestCommon(company.Id, policyCode, cancellationToken);

            PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

            factor.PolicyRequestFactorDetails = null;

            PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

            detail.IsDeleted = true;

            await _policyRequestFactorDetailRepository.UpdateAsync(detail, cancellationToken);

            return true;
        }

        public async Task<PagedResult<CompanyFactorViewModel>> GetAllPaymentsMine(long userId, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شما با این شرکت ارتباطی ندارید");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<PolicyRequestFactor> factors = new PagedResult<PolicyRequestFactor>();

            if (statusId != null)
            {
                factors = await _policyRequestFactorRepository.GetAllCompanyFactorsByStatusId(company.Id, statusId.Value, pageAbleModel, cancellationToken);
            }
            else
            {
                factors = await _policyRequestFactorRepository.GetAllCompanyFactors(company.Id, pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<CompanyFactorViewModel>>(factors);
        }
    }
}