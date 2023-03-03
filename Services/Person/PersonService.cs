using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Person;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Models.PolicyRequest;
using System.Transactions;
using Common.Extensions;
using Models.PageAble;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private PagingSettings _pagingSettings;
        private readonly SiteSettings _siteSettings;
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IPersonAddressService _personAddressService;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IRepository<PersonAttachment> _personAttachmentRepository;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;
        private readonly IUserRepository _userRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly ICompanyAgentPersonRepository _companyAgentPersonRepository;


        public PersonService(IPersonRepository personRepository, IOptionsSnapshot<PagingSettings> pagingSettings,
            IUserRepository userRepository, IPolicyRequestRepository policyRequestRepository,
            IOptionsSnapshot<SiteSettings> siteSettings,
            IAddressRepository addressRepository, IPersonAddressRepository personAddressRepository,
            IPersonAddressService personAddressService, IAttachmentRepository attachmentRepository,
            IRepository<PersonAttachment> personAttachmentRepository,
            IMapper mapper
            , IAttachmentService attachmentService, IUserRoleRepository userRoleRepository,
            ICompanyRepository companyRepository, IPersonCompanyRepository personCompanyRepository,
            IRoleRepository roleRepository, ICityRepository cityRepository, IAgentRepository agentRepository,
            ICompanyAgentPersonRepository companyAgentPersonRepository)
        {
            _policyRequestRepository = policyRequestRepository;
            _userRepository = userRepository;
            _personAddressService = personAddressService;
            _personRepository = personRepository;
            _pagingSettings = pagingSettings.Value;
            _siteSettings = siteSettings.Value;
            _addressRepository = addressRepository;
            _personAddressRepository = personAddressRepository;
            _attachmentRepository = attachmentRepository;
            _personAttachmentRepository = personAttachmentRepository;
            _mapper = mapper;
            _attachmentService = attachmentService;
            _userRoleRepository = userRoleRepository;
            _personCompanyRepository = personCompanyRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _cityRepository = cityRepository;
            _agentRepository = agentRepository;
            _companyAgentPersonRepository = companyAgentPersonRepository;
        }


        public async Task<CompanyAgentPerson> HandleAddCompanyAgentPerson(PersonViewModel personViewModel,
            Person person, CancellationToken cancellationToken)
        {
            // roleId=6 کارشناس نماینده بیمه 
            CompanyAgentPerson companyAgentPerson = new CompanyAgentPerson();
            if (personViewModel.RoleId.HasValue && personViewModel.RoleId == 6)
            {
                if (!personViewModel.AgentId.HasValue)
                {
                    throw new BadRequestException("برای نقش کارشناس نمایندگی، انتخاب شناسه نماینده اجباری است");
                }

                companyAgentPerson = new CompanyAgentPerson()
                {
                    CompanyAgentId = personViewModel.AgentId.Value,
                    PersonId = person.Id,
                    Position = "کارشناس نمایندگی"
                };
                await _companyAgentPersonRepository.AddAsync(companyAgentPerson, cancellationToken);
                return companyAgentPerson;
            }

            return companyAgentPerson;
        }

        public async Task<CompanyAgent> HandleAddCompanyAgent(PersonViewModel personViewModel, Person person,
            Company company, CancellationToken cancellationToken)
        {
            // roleId=2 نماینده بیمه
            CompanyAgent companyAgent = new CompanyAgent();
            if (personViewModel.RoleId.HasValue && personViewModel.RoleId == 2)
            {
                if (!personViewModel.AgentCityId.HasValue)
                {
                    throw new BadRequestException("برای نقش نماینده، شهر نمایندگی اجباری است");
                }

                companyAgent = new CompanyAgent()
                {
                    CompanyId = company.Id,
                    PersonId = person.Id,
                    CityId = personViewModel.AgentCityId.Value
                };
                await _agentRepository.AddAsync(companyAgent, cancellationToken);
            }

            return companyAgent;
        }

        public async Task ValidateAddInput(PersonViewModel personViewModel, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (personViewModel.RoleId.HasValue &&
                (personViewModel.User == null || personViewModel.User.Username == null))
                {
                    throw new BadRequestException("نقش نیازمند تعریف کاربری می باشد");
                }


                List<long> CompanyRoles = new List<long>() { 1, 2, 5, 6 };
                if ((personViewModel.PersonCompany == null || !personViewModel.PersonCompany.CompanyCode.HasValue &&
                        personViewModel.User == null || personViewModel.User.Username == null) &&
                    personViewModel.RoleId != null && CompanyRoles.Contains(personViewModel.RoleId.Value)
                )
                {
                    throw new BadRequestException("نقش های مربوط به شرکت، نیازمند اطلاعات کاربری و اطلاعات شرکت می باشد");
                }

                if (!personViewModel.AgentCityId.HasValue && personViewModel.RoleId != null && personViewModel.RoleId == 2)
                {
                    throw new BadRequestException("نقش کارشناس نمایندگی، نیازمند انتخاب شهر نماینده می باشد");
                }

                if ((!personViewModel.AgentId.HasValue) && personViewModel.RoleId != null && personViewModel.RoleId == 6)
                {
                    throw new BadRequestException("نقش کارشناس نمایندگی، نیازمند انتخاب نماینده می باشد");
                }
            });
        }

        public async Task<PersonResultViewModel> CreatePersonCommon(PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await ValidateAddInput(personViewModel, cancellationToken);

                PersonResultViewModel model = new PersonResultViewModel();
                Role role = new Role();
                Company company = new Company();
                Person person = new Person
                {
                    Code = Guid.NewGuid(),
                    FirstName = personViewModel.FirstName,
                    LastName = personViewModel.LastName,
                    NationalCode = personViewModel.NationalCode,
                    Identity = personViewModel.Identity,
                    FatherName = personViewModel.FatherName,
                    BirthDate = personViewModel.BirthDate,
                    GenderId = personViewModel.GenderId,
                    MarriageId = personViewModel.MarriageId,
                    MillitaryId = personViewModel.MillitaryId,
                    JobName = personViewModel.JobName,
                    IsDeleted = false
                };

                await _personRepository.AddAsync(person, cancellationToken);

                model = _mapper.Map<PersonResultViewModel>(person);

                if (personViewModel.User != null && personViewModel.User.Username != null)
                {
                    User oldUser = await _userRepository.GetByUserNameNoTracking(personViewModel.User.Username);
                    if (oldUser != null)
                    {
                        throw new BadRequestException("این نام کاربری تکراری است");
                    }

                    if (String.IsNullOrEmpty(personViewModel.User.Password))
                    {
                        throw new BadRequestException("پسورد نمیتواند خالی باشد");
                    }

                    User user = new User()
                    {
                        PersonId = person.Id,
                        Username = personViewModel.User.Username,
                        Code = Guid.NewGuid(),
                        Password = SecurityHelper.GetSha256Hash(personViewModel.User.Password),
                        Email = personViewModel.User.Email,
                        IsDeleted = false
                    };


                    await _userRepository.AddAsync(user, cancellationToken);

                    model.User = _mapper.Map<PersonUserResultViewModel>(user);

                    if (personViewModel.RoleId != null && personViewModel.RoleId != 0)
                    {
                        UserRole userRole = new UserRole()
                        {
                            RoleId = personViewModel.RoleId.Value,
                            UserId = user.Id
                        };

                        await _userRoleRepository.AddAsync(userRole, cancellationToken);

                        role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);

                        model.Role = _mapper.Map<PersonRoleResultViewModel>(role);
                    }
                }

                if (personViewModel.PersonCompany != null && personViewModel.PersonCompany.CompanyCode.HasValue)
                {
                    // اگر نقش دارد همان نقش بعنوان سمت در نظر گرفته شود.
                    if (role != null)
                    {
                        personViewModel.PersonCompany.Position = role.Caption;
                    }

                    company = await _companyRepository.GetByCode(personViewModel.PersonCompany.CompanyCode.Value,
                        cancellationToken);
                    if (company == null)
                    {
                        throw new BadRequestException("این کد شرکت وجود ندارد");
                    }

                    PersonCompany personCompany = new PersonCompany()
                    {
                        CompanyId = company.Id,
                        PersonId = person.Id,
                        Position = personViewModel.PersonCompany.Position,
                        IsDeleted = false
                    };

                    await _personCompanyRepository.AddAsync(personCompany, cancellationToken);

                    model.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(personCompany);
                }


                var companyAgent = await HandleAddCompanyAgent(personViewModel, person, company, cancellationToken);
                if (companyAgent != null)
                {
                    model.AgentCityId = companyAgent.CityId;
                }

                var companyAgentPerson = await HandleAddCompanyAgentPerson(personViewModel, person, cancellationToken);
                if (companyAgentPerson != null)
                {
                    model.AgentId = companyAgentPerson.CompanyAgentId;
                }

                if (personViewModel.PersonAddress != null && personViewModel.PersonAddress.CityId.HasValue)
                {
                    DAL.Models.City city = await _cityRepository.GetByIdAsync(cancellationToken,
                        personViewModel.PersonAddress.CityId.Value);
                    if (city == null)
                    {
                        throw new BadRequestException("شهر مورد نظر وجود ندارد");
                    }

                    Address address = new Address()
                    {
                        Code = Guid.NewGuid(),
                        CityId = city.Id,
                        Description = personViewModel.PersonAddress.Description,
                        Mobile = personViewModel.User.Username,
                        IsDeleted = false
                    };

                    await _addressRepository.AddAsync(address, cancellationToken);

                    PersonAddress personAddress = new PersonAddress()
                    {
                        AddressId = address.Id,
                        PersonId = person.Id,
                        IsDeleted = false,
                        AddressTypeId = 1
                    };

                    await _personAddressRepository.AddAsync(personAddress, cancellationToken);

                    model.PersonAddress = _mapper.Map<AddressForPersonViewModel>(personAddress);
                }

                transaction.Complete();


                return model;
            }
        }


        public async Task<PersonResultViewModel> UpdatePersonCommon(Guid Code,
            UpdatePersonInputViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                PersonResultViewModel resultViewModel = new PersonResultViewModel();
                Role role = new Role();
                Person person = await _personRepository.GetByCodeNoTracking(Code, cancellationToken);
                if (person == null)
                    throw new BadRequestException(" این شخص وجود ندارد");

                person.BirthDate = personViewModel.BirthDate;
                person.FirstName = personViewModel.FirstName;
                person.LastName = personViewModel.LastName;
                person.FatherName = personViewModel.FatherName;
                person.NationalCode = personViewModel.NationalCode;
                person.Identity = personViewModel.Identity;
                person.GenderId = personViewModel.GenderId;
                person.MarriageId = personViewModel.MarriageId;
                person.MillitaryId = personViewModel.MillitaryId;
                person.JobName = personViewModel.JobName;

                await _personRepository.UpdateAsync(person, cancellationToken);

                resultViewModel = _mapper.Map<PersonResultViewModel>(person);


                if (personViewModel.User != null && personViewModel.User.Username != null)
                {
                    User user = await _userRepository.GetUserByPersonIdNoTracking(person.Id, cancellationToken);


                    if (user != null)
                    {
                        if (!await IsUsernameUniqueCommon(user.Username, personViewModel.User.Username,
                            cancellationToken))
                        {
                            throw new BadRequestException("این نام کاربری تکراری است");
                        }

                        user.Username = personViewModel.User.Username;
                        user.Email = personViewModel.User.Email;


                        await _userRepository.UpdateAsync(user, cancellationToken);

                        resultViewModel.User = _mapper.Map<PersonUserResultViewModel>(user);
                    }


                    if (personViewModel.RoleId != null)
                    {
                        List<UserRole> userRoles =
                            await _userRoleRepository.GetUserRolesByUserId(user.Id, cancellationToken);
                        role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);
                        if (userRoles == null || userRoles.Count == 0)
                        {
                            UserRole userRole = new UserRole()
                            {
                                RoleId = personViewModel.RoleId.Value,
                                UserId = user.Id
                            };

                            await _userRoleRepository.AddAsync(userRole, cancellationToken);
                            resultViewModel.Role = _mapper.Map<PersonRoleResultViewModel>(role);
                            // اگر کاربر دارای کارشناس نمایندگی بود
                            if (personViewModel.PersonCompany != null &&
                                personViewModel.PersonCompany.CompanyCode.HasValue &&
                                personViewModel.User != null && personViewModel.User.Username != null &&
                                personViewModel.RoleId != null && personViewModel.RoleId == 6 &&
                                personViewModel.AgentId.HasValue
                            )
                            {
                                CompanyAgentPerson companyAgentPerson = new CompanyAgentPerson()
                                {
                                    CompanyAgentId = personViewModel.AgentId.Value,
                                    PersonId = person.Id,
                                    Position = "کارشناس نمایندگی"
                                };
                                await _companyAgentPersonRepository.AddAsync(companyAgentPerson, cancellationToken);
                            }
                        }
                        else
                        {
                            UserRole userRole = new UserRole()
                            {
                                RoleId = personViewModel.RoleId.Value,
                                UserId = user.Id
                            };
                            var AgentExperRole = userRoles.Find(x => x.Id == 6);
                            // اگر نقش کارشناس نمایندگی موجود بود اما نقش جدید کارشناسی نمایندگی نبود، باید از جدول CompanyAgentPerson حذف شود.
                            if (AgentExperRole != null && userRole.RoleId != 6)
                            {
                                var agentPerson =
                                    await _companyAgentPersonRepository.GetAgentPersonByCompanyCodeAndPerson(
                                        personViewModel.PersonCompany.CompanyCode.Value, person.Id, cancellationToken);
                                if (agentPerson != null)
                                {
                                    await _companyAgentPersonRepository.DeleteAsync(agentPerson, cancellationToken);
                                }
                            }

                            await _userRoleRepository.DeleteRangeAsync(userRoles, cancellationToken);

                            await _userRoleRepository.AddAsync(userRole, cancellationToken);
                            resultViewModel.Role = _mapper.Map<PersonRoleResultViewModel>(role);
                        }


                        resultViewModel.Role = _mapper.Map<PersonRoleResultViewModel>(role);
                    }
                    else
                    {
                        List<UserRole> userRoles =
                            await _userRoleRepository.GetUserRolesByUserId(user.Id, cancellationToken);

                        if (userRoles != null || userRoles.Count != 0)
                        {
                            await _userRoleRepository.DeleteRangeAsync(userRoles, cancellationToken);
                        }
                    }
                }

                Company company = new Company();
                if (personViewModel.PersonCompany != null && personViewModel.PersonCompany.CompanyCode.HasValue)
                {
                    PersonCompany personCompany =
                        await _personCompanyRepository.GetByPersonIdNoTracking(person.Id, cancellationToken);


                    company =
                        await _companyRepository.GetByCodeNoTracking(personViewModel.PersonCompany.CompanyCode.Value,
                            cancellationToken);
                    if (company == null)
                    {
                        throw new BadRequestException("این شرکت وجود ندارد");
                    }

                    // اگر نقش دارد همان نقش بعنوان سمت در نظر گرفته شود.
                    if (role != null)
                    {
                        personViewModel.PersonCompany.Position = role.Caption;
                    }

                    if (personCompany == null)
                    {
                        personCompany = new PersonCompany()
                        {
                            CompanyId = company.Id,
                            PersonId = person.Id,
                            Position = personViewModel.PersonCompany.Position,
                            IsDeleted = false
                        };

                        await _personCompanyRepository.AddAsync(personCompany, cancellationToken);

                        resultViewModel.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(personCompany);
                    }
                    else
                    {
                        personCompany.CompanyId = company.Id;
                        personCompany.Position = personViewModel.PersonCompany.Position;

                        await _personCompanyRepository.UpdateAsync(personCompany, cancellationToken);

                        resultViewModel.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(personCompany);
                    }
                }
                else
                {
                    PersonCompany personCompany =
                        await _personCompanyRepository.GetByPersonIdNoTracking(person.Id, cancellationToken);

                    if (personCompany != null)
                    {
                        await _personCompanyRepository.DeleteAsync(personCompany, cancellationToken);
                    }
                }


                if (personViewModel.PersonAddress != null && personViewModel.PersonAddress.CityId.HasValue)
                {
                    PersonAddress personPrimaryAddress =
                        await _personAddressRepository.GetPersonAddressByPersonId(person.Id, cancellationToken);
                    if (personPrimaryAddress != null)
                    {
                        personPrimaryAddress.Address.CityId = personViewModel.PersonAddress.CityId.Value;
                        personPrimaryAddress.Address.Description = personViewModel.PersonAddress.Description;
                        await _addressRepository.UpdateAsync(personPrimaryAddress.Address, cancellationToken);

                        resultViewModel.PersonAddress = _mapper.Map<AddressForPersonViewModel>(personPrimaryAddress);
                    }
                    else
                    {
                        Address newAddress = new Address()
                        {
                            CityId = personViewModel.PersonAddress.CityId.Value,
                            Description = personViewModel.PersonAddress.Description,
                        };

                        await _addressRepository.AddAsync(newAddress, cancellationToken);
                        PersonAddress personAddress = new PersonAddress()
                        {
                            PersonId = person.Id,
                            AddressId = newAddress.Id,
                            CreatedAt = DateTime.Now,
                            AddressTypeId = 1,
                        };


                        await _personAddressRepository.AddAsync(personAddress, cancellationToken);

                        resultViewModel.PersonAddress = _mapper.Map<AddressForPersonViewModel>(personAddress);
                    }
                }
                else
                {
                    PersonAddress personAddress =
                        await _personAddressRepository.GetPersonAddressByPersonId(person.Id, cancellationToken);
                    if (personAddress != null)
                    {
                        await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
                    }
                }

                transaction.Complete();


                return resultViewModel;
            }
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


        public async Task<Person> Create(PersonViewModel personViewModel, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName,
                NationalCode = personViewModel.NationalCode,
                Identity = personViewModel.Identity,
                FatherName = personViewModel.FatherName,
                BirthDate = personViewModel.BirthDate,
                GenderId = personViewModel.GenderId,
                MarriageId = personViewModel.MarriageId,
                MillitaryId = personViewModel.MillitaryId,
            };
            await _personRepository.AddAsync(person, cancellationToken);


            return person;
        }

        public async Task<PersonResultViewModel> PersonPostMapperService(PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await CreatePersonCommon(personViewModel, cancellationToken);

            return person;
        }

        public async Task<PersonResultViewModel> Update(Guid code, UpdatePersonInputViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel viewModel = await UpdatePersonCommon(code, personViewModel, cancellationToken);

            return viewModel;
        }


        //اینجا بررسی میکنیم اگر طرف پرونده نداشت حذف بشه،،، و اینکه اگر طرف خودش مسئول بود، آیدیش توی پرونده های مربوطش حذف بشه
        public async Task<bool> Delete(Guid personCode, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Person person = await _personRepository.GetWithUser(personCode, cancellationToken);
                if (person == null)
                {
                    throw new BadRequestException("کد شخص وجود ندارد");
                }

                PersonCompany personCompany =
                    await _personCompanyRepository.GetByPersonId(person.Id, cancellationToken);
                if (personCompany == null)
                {
                    throw new BadRequestException("این فرد با شرکتی ارتباطی ندارد");
                }

                UserRole userRole =
                    await _userRoleRepository.GetSingleUserRoleByUserId(person.Users.FirstOrDefault().Id,
                        cancellationToken);
                if (userRole == null)
                {
                    throw new BadRequestException("این شخص در سیستم نقشی ندارد");
                }

                List<DAL.Models.PolicyRequest> policyRequests =
                    await _policyRequestRepository.GetByPersonIdWithoutDetail(person.Id, cancellationToken);

                if (policyRequests.Count == 0 || policyRequests == null)
                {
                    await DeleteReviewerOrAgentSelectCommon(person.Id, personCompany.CompanyId.Value,
                        cancellationToken);
                }
                else
                {
                    throw new BadRequestException("این فرد دارای پرونده است و نمی توان آن را حذف کرد");
                }

                personCompany.IsDeleted = true;

                bool result = await DeletePersonAndUserCommon(person, cancellationToken);

                await _personCompanyRepository.UpdateAsync(personCompany, cancellationToken);
                await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
                //await _userRepository.UpdateAsync(person.Users.FirstOrDefault(), cancellationToken);


                transaction.Complete();

                return result;
            }
        }

        public async Task<bool> DeleteReviewerOrAgentSelectCommon(long personId, long companyId,
            CancellationToken cancellationToken)
        {
            List<DAL.Models.PolicyRequest> reviewerPolicies =
                await _policyRequestRepository.GetListByReviewerId(personId, cancellationToken);

            if (reviewerPolicies.Count == 0 || reviewerPolicies == null)
            {
                await DeleteAgentSelect(personId, companyId, cancellationToken);
            }
            else
            {
                await DeleteReviewer(reviewerPolicies, cancellationToken);
            }

            return true;
        }

        public async Task<bool> DeleteReviewer(List<DAL.Models.PolicyRequest> reviewerPolicies,
            CancellationToken cancellationToken)
        {
            for (int i = 0; i < reviewerPolicies.Count; i++)
            {
                if (reviewerPolicies[i].AgentSelected != null && reviewerPolicies[i].Reviewer.Code ==
                    reviewerPolicies[i].AgentSelected.Person.Code)
                {
                    reviewerPolicies[i].ReviewerId = null;
                    reviewerPolicies[i].Reviewer = null;
                    reviewerPolicies[i].AgentSelectedId = null;
                    reviewerPolicies[i].AgentSelected.IsDeleted = true;

                    await _agentRepository.UpdateAsync(reviewerPolicies[i].AgentSelected, cancellationToken);

                    reviewerPolicies[i].AgentSelected = null;
                    reviewerPolicies[i].AgentSelectionTypeId = 1;

                    await _policyRequestRepository.UpdateAsync(reviewerPolicies[i], cancellationToken);
                }
                else
                {
                    reviewerPolicies[i].ReviewerId = null;
                    reviewerPolicies[i].Reviewer = null;

                    await _policyRequestRepository.UpdateAsync(reviewerPolicies[i], cancellationToken);
                }
            }

            return true;
        }

        public async Task<bool> DeleteAgentSelect(long personId, long companyId, CancellationToken cancellationToken)
        {
            List<DAL.Models.PolicyRequest> agentSelectPolicies =
                await _policyRequestRepository.GetListByAgentSelectPersonId(personId, cancellationToken);
            if (agentSelectPolicies.Count != 0)
            {
                CompanyAgent companyAgent =
                    await _agentRepository.GetByPersonIdAndCompanyId(personId, companyId, cancellationToken);
                if (companyAgent == null)
                {
                    throw new BadRequestException("این فرد نماینده شرکتی نیست");
                }

                for (int i = 0; i < agentSelectPolicies.Count; i++)
                {
                    agentSelectPolicies[i].AgentSelectedId = null;
                    agentSelectPolicies[i].AgentSelectionTypeId = 1;
                }

                await _policyRequestRepository.UpdateRangeAsync(agentSelectPolicies, cancellationToken);
                await _agentRepository.DeleteAsync(companyAgent, cancellationToken);
            }

            return true;
        }

        public async Task<bool> DeletePersonAndUserCommon(Person person, CancellationToken cancellationToken)
        {
            person.IsDeleted = true;
            await _personRepository.UpdateAsync(person, cancellationToken);


            User user = await _userRepository.GetUserByIdNoTracking(person.Users.FirstOrDefault().Id,
                cancellationToken);
            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user, cancellationToken);

            return true;
        }

        public async Task<PersonResultViewModel> GetPersonDetail(Guid code, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTrackingWithDetails(code, cancellationToken);
            if (person == null)
                throw new BadRequestException(" شخصی با این کد وجود ندارد");
            var output = _mapper.Map<PersonResultViewModel>(person);
            // قرارداد کردیم که شخص فقط به یک شرکت متصل است
            var company = person.PersonCompanies.Select(x=>x.Company).FirstOrDefault();
            if (company != null)
            {
                
                var companyAgent = person.CompanyAgents.Where(x=>x.CompanyId==company.Id).FirstOrDefault();
                if (companyAgent != null)
                {
                    // برای نماینده، شهر نماینده لازم است
                    output.AgentCityId = companyAgent.CityId;
                    output.AgentProvinceId = companyAgent.City.TownShip.ProvinceId;
                }
                
                var companyAgentPerson = person.CompanyAgentPeople.Where(x=>x.CompanyAgent.CompanyId==company.Id).FirstOrDefault();
                if (companyAgentPerson != null)
                {
                    // برای کارشناس نماینده، شناسه نماینده لازم است
                    output.AgentId = companyAgentPerson.CompanyAgentId;
                }
            }
            


            return output;
        }

        public async Task<Person> GetDetailMine(long UserID, CancellationToken cancellationToken)
        {
            var User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            var person = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
            if (person == null)
                throw new CustomException(" شخص");
            return person;
        }

        public async Task<PagedResult<PersonResultViewModel>> all(PageAbleResult PageAbleResult,
            CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(PageAbleResult);


            var people = await _personRepository.GetAllAsync(pageAbleModel, cancellationToken);


            return _mapper.Map<PagedResult<PersonResultViewModel>>(people);
        }


        public async Task<bool> DeleteAddress(CancellationToken cancellationToken, string code, int addressId)
        {
            var person_code = Guid.Parse(code);
            var person = await _personRepository.GetByCodeNoTracking(person_code, cancellationToken);
            if (person == null)
                throw new CustomException(" شخص وجود ندارد");

            var personAddress =
                await _personAddressService.GetByPersonIdAddressId(person.Id, addressId, cancellationToken);
            if (personAddress == null)
                throw new CustomException("اطلاعات صحیح نمی باشد");

            await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAddressByCode(CancellationToken cancellationToken, Guid person_code,
            Guid address_code)
        {
            var person = await _personRepository.GetByCodeNoTracking(person_code, cancellationToken);
            if (person == null)
                throw new CustomException(" شخص وجود ندارد");

            Address address = await _addressRepository.GetAddressByCode(address_code, cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("کد آدرس وجود ندارد");
            }

            PersonAddress personAddress =
                await _personAddressRepository.GetByPersonIdAddrerssId(person.Id, address.Id, cancellationToken);
            if (personAddress == null)
            {
                throw new BadRequestException("آدرسی برای این فرد وجود ندارد");
            }

            await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
            address.IsDeleted = true;
            await _addressRepository.UpdateAsync(address, cancellationToken);
            return true;
        }

        public async Task<PersonAttachmentViewModel> CreatePersonAttachment(CancellationToken cancellationToken,
            IFormFile file, string personCode, int typeId)
        {
            var result = new PersonAttachmentViewModel()
            {
                PersonCode = Guid.Parse(personCode),
                TypeId = typeId,
            };


            // var extension = Path.GetExtension(file.FileName);
            // var newName = Guid.NewGuid() + extension;

            Attachment model = await _attachmentService.CreateAttachment(cancellationToken, file);

            var code = Guid.Parse(personCode);
            var person = await _personRepository.GetByCodeNoTracking(code, cancellationToken);
            var personAttachment = new PersonAttachment()
            {
                PersonId = person.Id,
                AttachmentId = model.Id,
                TypeId = typeId
            };

            await _personAttachmentRepository.AddAsync(personAttachment, cancellationToken);
            personAttachment = await _personAttachmentRepository.Table.Include(c => c.Person).Include(c => c.Attachment)
                .FirstOrDefaultAsync(x => x.Id == personAttachment.Id, cancellationToken);

            return _mapper.Map<PersonAttachmentViewModel>(personAttachment);
        }


        public async Task<List<PersonAddressViewModel>> GetPersonAddresses(Guid code,
            CancellationToken cancellationToken)
        {
            DAL.Models.Person person = await _personRepository.GetByCodeNoTracking(code, cancellationToken);
            if (person == null)
            {
                throw new BadRequestException("کد فرد وجود ندارد");
            }

            List<PersonAddress> personAddresses =
                await _personAddressRepository.GetPersonAddresses(person.Id, cancellationToken);

            return _mapper.Map<List<PersonAddressViewModel>>(personAddresses);
        }

        public async Task<List<PersonAddressViewModel>> GetPersonAddressesMine(long UserID,
            CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            DAL.Models.Person person = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
            if (person == null)
            {
                throw new BadRequestException("کد فرد وجود ندارد");
            }

            List<PersonAddress> personAddresses =
                await _personAddressRepository.GetPersonAddresses(person.Id, cancellationToken);

            return _mapper.Map<List<PersonAddressViewModel>>(personAddresses);
        }

        public async Task<AddressViewModel> CreatePersonAddress(AddressViewModel viewModel,
            CancellationToken cancellationToken,
            string code)
        {
            var model = new Address
            {
                CityId = viewModel.CityId,
                Code = Guid.NewGuid(),
                Name = viewModel.Name,
                Description = viewModel.Description,
                ZoneNumber = viewModel.ZoneNumber,
                Phone = viewModel.Phone,
                Mobile = viewModel.Mobile,
                IsDeleted = false,
            };
            await _addressRepository.AddAsync(model, cancellationToken);
            var person_code = Guid.Parse(code);
            var person = await _personRepository.GetByCodeNoTracking(person_code, cancellationToken);
            var personAddress = new PersonAddress
            {
                AddressId = model.Id,
                PersonId = person.Id,
            };
            await _personAddressRepository.AddAsync(personAddress, cancellationToken);
            return _mapper.Map<AddressViewModel>(model);
        }

        public async Task<AddressViewModel> UpdatePersonAddress(string code, string addressCode,
            AddressInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(Guid.Parse(code), cancellationToken);
            if (person == null)
            {
                throw new BadRequestException("کد فرد وجود ندارد");
            }

            Address address = await _addressRepository.GetAddressByCode(Guid.Parse(addressCode), cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("کد آدرس وجود ندارد");
            }

            PersonAddress personAddress =
                await _personAddressRepository.GetByPersonIdAddrerssId(person.Id, address.Id, cancellationToken);
            if (personAddress == null)
            {
                throw new BadRequestException("آدرسی برای این فرد وجود ندارد");
            }

            var SavedAddress = await this.UpdatePersonAddressCommon(personAddress, viewModel, cancellationToken);
            return _mapper.Map<AddressViewModel>(SavedAddress);
        }

        public async Task<AddressViewModel> UpdatePersonAddressMine(long UserID, string addressCode,
            AddressInputViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            Person person = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
            if (person == null)
            {
                throw new BadRequestException("کد فرد وجود ندارد");
            }

            Address address = await _addressRepository.GetAddressByCode(Guid.Parse(addressCode), cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("کد آدرس وجود ندارد");
            }

            PersonAddress personAddress =
                await _personAddressRepository.GetByPersonIdAddrerssId(person.Id, address.Id, cancellationToken);
            if (personAddress == null)
            {
                throw new BadRequestException("آدرسی برای این فرد وجود ندارد");
            }

            var SavedAddress = await this.UpdatePersonAddressCommon(personAddress, viewModel, cancellationToken);
            return _mapper.Map<AddressViewModel>(SavedAddress);
        }

        public async Task<Address> UpdatePersonAddressCommon(PersonAddress personAddress,
            AddressInputViewModel viewModel,
            CancellationToken cancellationToken)
        {
            personAddress.AddressTypeId = viewModel.AddressTypeId;
            await _personAddressRepository.UpdateAsync(personAddress, cancellationToken);
            var address = personAddress.Address;
            address.Name = viewModel.Name;
            address.CityId = viewModel.CityId;
            address.Description = viewModel.Description;
            address.ZoneNumber = viewModel.ZoneNumber;
            address.Phone = viewModel.Phone;
            address.Mobile = viewModel.Mobile;

            await _addressRepository.UpdateAsync(address, cancellationToken);
            return address;
        }


        public async Task<MyPersonViewModel> GetPersondata(long userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            //var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            var person = await GetPersondata_Core(user.PersonId, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(person);
        }

        public async Task<MyPersonViewModel> UpdatePersondata(long userId, MyPersonUpdateViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            if (person == null)
            {
                throw new NotFoundException(" اطلاعات وجود ندارد");
            }

            DAL.Models.Person Result = await UpdatePersondataCommon(person, viewModel, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(Result);
        }

        public async Task<MyPersonViewModel> GetPersondataMine(long userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            //var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            var person = await GetPersondata_Core(user.PersonId, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(person);
        }

        public async Task<Person> GetPersondata_Core(long PersonId, CancellationToken cancellationToken)
        {
            return await _personRepository.GetByPersonIdwithPrimaryAddressAsync(PersonId, cancellationToken);
        }

        public async Task<MyPersonViewModel> UpdatePersondataMine(long userId, MyPersonUpdateViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            var person = await _personRepository.GetByPersonIdwithPrimaryAddressAsync(user.PersonId, cancellationToken);
            if (person == null)
            {
                throw new NotFoundException(" اطلاعات وجود ندارد");
            }

            DAL.Models.Person Result = await UpdatePersondataCommon(person, viewModel, cancellationToken);
            return _mapper.Map<MyPersonViewModel>(Result);
        }

        public async Task<Person> UpdatePersondataCommon(Person person, MyPersonUpdateViewModel viewModel,
            CancellationToken cancellationToken)
        {
            PersonAddress personPrimaryAddress = person.PersonAddresses.Where(x => x.AddressTypeId == 1).FirstOrDefault();
            if (personPrimaryAddress != null)
            {
                personPrimaryAddress.Address.CityId = viewModel.CityId;
                personPrimaryAddress.Address.Description = viewModel.Address;
                await _addressRepository.UpdateAsync(personPrimaryAddress.Address, cancellationToken);
            }
            else
            {
                Address newAddress = new Address()
                {
                    CityId = viewModel.CityId,
                    Description = viewModel.Address,
                };

                await _addressRepository.AddAsync(newAddress, cancellationToken);
                PersonAddress personAddress = new PersonAddress()
                {
                    AddressId = newAddress.Id,
                    CreatedAt = DateTime.Now,
                    AddressTypeId = 1,
                };
                person.PersonAddresses.Add(personAddress);
            }

            person.FirstName = viewModel.FirstName;
            person.LastName = viewModel.LastName;
            person.BirthDate = viewModel.BirthDate;
            person.JobName = viewModel.JobName;
            person.NationalCode = viewModel.NationalCode;
            await _personRepository.UpdateAsync(person, cancellationToken);
            return person;
        }

        public async Task<AddressViewModel> CreatePersonAddressMine(long UserID, AddressViewModel viewModel,
            CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            Person personData = await _personRepository.GetByIdAsync(cancellationToken, User.PersonId);
            if (personData == null)
            {
                throw new BadRequestException("کد فرد وجود ندارد");
            }

            //var model = new Address
            //{
            //    CityId = viewModel.CityId,
            //    Code = Guid.NewGuid(),
            //    Name = viewModel.Name,
            //    Description = viewModel.Description,
            //    ZoneNumber = viewModel.ZoneNumber,
            //    Phone = viewModel.Phone,
            //    Mobile = viewModel.Mobile,
            //    IsDeleted = false,
            //};
            //await _addressRepository.AddAsync(model, cancellationToken);
            //var person = await _personRepository.GetByCode(personData.Code, cancellationToken);
            //var personAddress = new PersonAddress
            //{
            //    AddressId = model.Id,
            //    PersonId = person.Id,
            //};
            //await _personAddressRepository.AddAsync(personAddress, cancellationToken);

            var model = await CreatePersonAddressCommon(personData, viewModel, cancellationToken);
            return _mapper.Map<AddressViewModel>(model);
        }

        public async Task<Address> CreatePersonAddressCommon(Person personData, AddressViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var model = new Address
            {
                CityId = viewModel.CityId,
                Code = Guid.NewGuid(),
                Name = viewModel.Name,
                Description = viewModel.Description,
                ZoneNumber = viewModel.ZoneNumber,
                Phone = viewModel.Phone,
                Mobile = viewModel.Mobile,
                IsDeleted = false,
            };
            await _addressRepository.AddAsync(model, cancellationToken);
            var person = await _personRepository.GetByCode(personData.Code, cancellationToken);
            var personAddressModel = new PersonAddress
            {
                AddressId = model.Id,
                PersonId = person.Id,
            };
            await _personAddressRepository.AddAsync(personAddressModel, cancellationToken);

            return model;
        }

        public async Task<bool> DeleteAddressByCodeMine(CancellationToken cancellationToken, long UserID,
            Guid address_code)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (user == null)
            {
                throw new NotFoundException(" کاربر وجود ندارد");
            }

            var person = await _personRepository.GetByIdAsync(cancellationToken, user.PersonId);
            if (person == null)
                throw new CustomException(" شخص وجود ندارد");

            Address address = await _addressRepository.GetAddressByCode(address_code, cancellationToken);
            if (address == null)
            {
                throw new BadRequestException("کد آدرس وجود ندارد");
            }

            //PersonAddress personAddress = await _personAddressRepository.GetByPersonIdAddrerssId(person.Id, address.Id, cancellationToken);
            //if (personAddress == null)
            //{
            //    throw new BadRequestException("آدرسی برای این فرد وجود ندارد");
            //}

            //await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
            //address.IsDeleted = true;
            //await _addressRepository.UpdateAsync(address, cancellationToken);
            return await DeleteAddressByCodeCommon(address, person, cancellationToken);
        }

        public async Task<bool> DeleteAddressByCodeCommon(Address address, Person person,
            CancellationToken cancellationToken)
        {
            PersonAddress personAddress =
                await _personAddressRepository.GetByPersonIdAddrerssId(person.Id, address.Id, cancellationToken);
            if (personAddress == null)
            {
                throw new BadRequestException("آدرسی برای این فرد وجود ندارد");
            }

            await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
            address.IsDeleted = true;
            await _addressRepository.UpdateAsync(address, cancellationToken);

            return true;
        }

        public async Task<PagedResult<PersonResultViewModel>> GetAllPersonsWithoutUser(string search_text,
            PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Person> persons = new PagedResult<Person>();

            if (String.IsNullOrEmpty(search_text))
            {
                persons = await _personRepository.GetAllWithoutUserAsync(pageAbleModel, cancellationToken);
            }
            else
            {
                persons = await _personRepository.GetAllWithoutUserBySearchTextAsync(search_text, pageAbleModel,
                    cancellationToken);
            }

            return _mapper.Map<PagedResult<PersonResultViewModel>>(persons);
        }

        public async Task<PagedResult<PersonResultViewModel>> GetSearchedPersons(string search_text,
            PageAbleResult PageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(PageAbleResult);

            PagedResult<Person> persons = new PagedResult<Person>();

            if (String.IsNullOrEmpty(search_text))
            {
                persons = await _personRepository.GetAllAsync(pageAbleModel, cancellationToken);
            }
            else
            {
                persons = await _personRepository.GetAllBySearchAsync(search_text, pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<PersonResultViewModel>>(persons);
        }
    }
}