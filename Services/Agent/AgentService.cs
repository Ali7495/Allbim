using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using Models.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Common.Utilities;
using Models.PageAble;
using Common.Exceptions;
using Common.Extensions;
using Models.Person;
using Models.CompanyAgent;

namespace Services.Agent
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPersonAddressRepository _personAddressRepository;
        private readonly IMapper _mapper;

        public AgentService(IAgentRepository agentRepository, ICompanyRepository companyRepository, IPersonRepository personRepository, IUserRepository userRepository, IPersonCompanyRepository personCompanyRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ICityRepository cityRepository, IAddressRepository addressRepository, IPersonAddressRepository personAddressRepository, IMapper mapper)
        {
            _agentRepository = agentRepository;
            _companyRepository = companyRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
            _personCompanyRepository = personCompanyRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _cityRepository = cityRepository;
            _addressRepository = addressRepository;
            _personAddressRepository = personAddressRepository;
            _mapper = mapper;
        }

        public async Task<AgentPersonViewModel> CreateAgent(Guid code, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                User user = await _userRepository.GetByUserName(viewModel.User.Username);
                if (user != null)
                {
                    throw new BadRequestException("این نام کاربری قبلا ثبت شده است");
                }

                Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
                if (company == null)
                {
                    throw new BadRequestException("شرکت وجود ندارد");
                }

                Person person = new Person()
                {
                    Code = Guid.NewGuid(),
                    FirstName = viewModel.Person.FirstName,
                    LastName = viewModel.Person.LastName,
                    NationalCode = viewModel.Person.NationalCode,
                    Identity = viewModel.Person.Identity,
                    FatherName = viewModel.Person.FatherName,
                    BirthDate = viewModel.Person.BirthDate,
                    GenderId = viewModel.Person.GenderId,
                    MarriageId = viewModel.Person.MarriageId,
                    MillitaryId = viewModel.Person.MillitaryId,
                    JobName = viewModel.Person.JobName
                };

                person.Users.Add(new User()
                {
                    Code = Guid.NewGuid(),
                    Username = viewModel.User.Username,
                    Email = viewModel.User.Email,
                    Password = SecurityHelper.GetSha256Hash(viewModel.User.Password)
                });

                person.CompanyAgents.Add(new CompanyAgent()
                {
                    CompanyId = company.Id,
                    CityId = viewModel.CityId,
                    Description = viewModel.Description
                });

                await _personRepository.AddAsync(person, cancellationToken);

                transaction.Complete();

                return _mapper.Map<AgentPersonViewModel>(person);
            }
        }


        public async Task<DAL.Models.City> GetValidCityCommon(AddressForPersonViewModel personAddress, CancellationToken cancellationToken)
        {
            if (personAddress != null && personAddress.CityId.HasValue)
            {
                DAL.Models.City city = await _cityRepository.GetByIdAsync(cancellationToken, personAddress.CityId.Value);
                if (city == null)
                {
                    throw new BadRequestException("شهر مورد نظر وجود ندارد");
                }

                return city;
            }
            throw new BadRequestException("شهر مورد نظر وجود ندارد");
        }



        public async Task<PersonResultViewModel> CreateAgentCommon(long userId, PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                PersonResultViewModel model = new PersonResultViewModel();

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


                User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

                if (personCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }


                Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
                if (company == null)
                {
                    throw new BadRequestException("شرکت وجود ندارد");
                }

                Role role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);



                PersonCompany newPersonCompany = new PersonCompany()
                {
                    PersonId = person.Id,
                    ParentId = personCompany.Id,
                    CompanyId = company.Id,
                    Position = role.Caption,
                    IsDeleted = false
                };

                await _personCompanyRepository.AddAsync(newPersonCompany, cancellationToken);

                model.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(newPersonCompany);


                DAL.Models.City city = await GetValidCityCommon(personViewModel.PersonAddress, cancellationToken);


                if (personViewModel.User != null && personViewModel.User.Username != null)
                {
                    User oldUser = await _userRepository.GetByUserName(personViewModel.User.Username);
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

                    if (personViewModel.RoleId != null && personViewModel.RoleId == 2)
                    {
                        UserRole userRole = new UserRole()
                        {
                            RoleId = personViewModel.RoleId.Value,
                            UserId = user.Id
                        };

                        await _userRoleRepository.AddAsync(userRole, cancellationToken);

                        

                        model.Role = _mapper.Map<PersonRoleResultViewModel>(role);




                        CompanyAgent companyAgent = new CompanyAgent()
                        {
                            CompanyId = company.Id,
                            PersonId = person.Id,
                            CityId = city.Id,
                        };

                        await _agentRepository.AddAsync(companyAgent, cancellationToken);

                    }
                }


                if (personViewModel.PersonAddress != null)
                {

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


        public async Task<PersonResultViewModel> CreateAgentMine(long userId, PersonViewModel viewModel, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await CreateAgentCommon(userId, viewModel, cancellationToken);

            return person;
        }



        public async Task<AgentViewModel> GetAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            var agent = await _agentRepository.GetByCompanyAndPersonCodeAsync(code, personCode, cancellationToken);
            return _mapper.Map<AgentViewModel>(agent);
        }

        public async Task<PersonResultViewModel> GetAgentMine(long userId, Guid personCode, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }

            PersonCompany subPerson = await _personCompanyRepository.GetByParentIdAndPersonCodeAsync(personCompany.Id, personCode, cancellationToken);
            if (subPerson == null)
            {
                throw new BadRequestException("این شخص وجود ندارد وجود ندارد");
            }
            

            Person person = await _personRepository.GetByCodeNoTrackingWithDetails(personCode, cancellationToken);
            if (person == null)
                throw new BadRequestException("شخصی با این کد وجود ندارد");
            return _mapper.Map<PersonResultViewModel>(person);
        }

        public async Task<PagedResult<AgentViewModel>> GetAgents(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }
            // var agents = await _agentRepository.GetAllByCompanyCodeAsync(code, cancellationToken);
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            var agents = await _agentRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.CompanyId == company.Id,
                i => i.City,
                i => i.Company,
                i => i.Person
            );
            return _mapper.Map<PagedResult<AgentViewModel>>(agents);
        }
        public async Task<PagedResult<PersonResultWithAgentCompanyViewModel>> GetAgentsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            // PagedResult<Person> persons = await _personRepository.GetAllPersonsWithAgentByCompanyId(company.Id, pageAbleModel, cancellationToken);
            PagedResult<Person> persons = await _personCompanyRepository.GetAllPersonByParentId(personCompany.Id, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<PersonResultWithAgentCompanyViewModel>>(persons);
        }

        public async Task<List<AgentViewModel>> GetAgentsList(Guid code, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }

            List<CompanyAgent> agents = await _agentRepository.GetAllByCompanyCodeAsync(code, cancellationToken);
            return _mapper.Map<List<AgentViewModel>>(agents);
        }


        public async Task<List<AgentViewModel>> GetAgentsListMine(long userId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }



            List<CompanyAgent> agents = await _agentRepository.GetAllByCompanyCodeAsync(company.Code, cancellationToken);
            return _mapper.Map<List<AgentViewModel>>(agents);
        }


        public async Task<AgentPersonViewModel> UpdateAgent(Guid code, Guid personCode, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetWithUserAndAgentByCode(personCode, cancellationToken);
            if (person == null)
            {
                throw new BadRequestException("کد شخص وجود ندارد");
            }

            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("کد شرکت وجود ندارد");
            }

            CompanyAgent agent = await _agentRepository.GetByCompanyAndPersonCodeAsync(code, personCode, cancellationToken);
            if (agent == null)
            {
                throw new BadRequestException("نماینده وجود ندارد");
            }

            Person updatedPerson = await UpdateCompanyPerson(personCode, viewModel, cancellationToken);

            return _mapper.Map<AgentPersonViewModel>(updatedPerson);

        }



        public async Task<PersonResultViewModel> UpdateAgentCommon(long userId, Guid Code, UpdatePersonInputViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                PersonResultViewModel resultViewModel = new PersonResultViewModel();

                User currentUser = await _userRepository.GetwithPersonById(cancellationToken, userId);

                PersonCompany currentPersonCompany = await _personCompanyRepository.GetByPersonId(currentUser.PersonId, cancellationToken);

                if (currentPersonCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }

                Company company = await _companyRepository.GetByIdAsync(cancellationToken, currentPersonCompany.CompanyId);
                if (company == null)
                {
                    throw new BadRequestException("کد شرکت وجود ندارد");
                }

                DAL.Models.City city = await GetValidCityCommon(personViewModel.PersonAddress, cancellationToken);


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

                CompanyAgent oldAgent = await _agentRepository.GetByCompanyAndPersonCodeAsyncNoTracking(company.Code, Code, cancellationToken);


                if (personViewModel.User != null && personViewModel.User.Username != null)
                {
                    User user = await _userRepository.GetUserByPersonIdNoTracking(person.Id, cancellationToken);

                    if (user != null)
                    {
                        if (!await IsUsernameUniqueCommon(user.Username, personViewModel.User.Username, cancellationToken))
                        {
                            throw new BadRequestException("این نام کاربری تکراری است");
                        }

                        user.Username = personViewModel.User.Username;
                        user.Email = personViewModel.User.Email;


                        await _userRepository.UpdateAsync(user, cancellationToken);

                        resultViewModel.User = _mapper.Map<PersonUserResultViewModel>(user);
                    }




                    if (personViewModel.RoleId != null && personViewModel.RoleId != 0)
                    {
                        List<UserRole> userRoles =
                            await _userRoleRepository.GetUserRolesByUserId(user.Id, cancellationToken);
                        

                        if (userRoles == null || userRoles.Count == 0 )
                        {
                            if (personViewModel.RoleId != 3)
                            {
                                UserRole userRole = new UserRole()
                                {
                                    RoleId = personViewModel.RoleId.Value,
                                    UserId = user.Id
                                };

                                await _userRoleRepository.AddAsync(userRole, cancellationToken);

                                Role role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);


                                resultViewModel.Role = _mapper.Map<PersonRoleResultViewModel>(role);


                                CompanyAgent companyAgent = new CompanyAgent()
                                {
                                    PersonId = person.Id,
                                    CompanyId = company.Id,
                                    CityId = city.Id
                                };

                                await _agentRepository.AddAsync(companyAgent, cancellationToken);
                            }
                            else
                            {
                                throw new BadRequestException("شما نمی توانید این نقش را وارد کنید");
                            }
                        }
                        else
                        {
                            
                            await _userRoleRepository.DeleteRangeAsync(userRoles, cancellationToken);
                            if (personViewModel.RoleId != 3)
                            {
                                UserRole userRole = new UserRole()
                                {
                                    RoleId = personViewModel.RoleId.Value,
                                    UserId = user.Id
                                };

                                await _userRoleRepository.AddAsync(userRole, cancellationToken);

                                Role role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);


                                resultViewModel.Role = _mapper.Map<PersonRoleResultViewModel>(role);


                                
                                if (oldAgent == null)
                                {
                                    CompanyAgent companyAgent = new CompanyAgent()
                                    {
                                        PersonId = person.Id,
                                        CompanyId = company.Id,
                                        CityId = city.Id
                                    };

                                    await _agentRepository.AddAsync(companyAgent, cancellationToken);
                                }
                                else
                                {
                                    oldAgent.CompanyId = company.Id;
                                    oldAgent.CityId = city.Id;

                                    await _agentRepository.UpdateAsync(oldAgent, cancellationToken);
                                }
                            }
                            else
                            {
                                throw new BadRequestException("شما نمی توانید این نقش را وارد کنید");
                            }
                        }
                    }
                    //else
                    //{
                    //    List<UserRole> userRoles =
                    //        await _userRoleRepository.GetUserRolesByUserId(user.Id, cancellationToken);

                    //    if (userRoles != null || userRoles.Count != 0)
                    //    {
                    //        await _userRoleRepository.DeleteRangeAsync(userRoles, cancellationToken);
                    //        await _agentRepository.DeleteAsync(oldAgent, cancellationToken);
                    //    }
                    //}
                }


                if (personViewModel.PersonCompany != null)
                {
                    PersonCompany personCompany =
                        await _personCompanyRepository.GetByPersonIdAndParentIdNoTracking(person.Id, currentPersonCompany.Id, cancellationToken);

                    Role role = await _roleRepository.GetByIdAsync(cancellationToken, personViewModel.RoleId.Value);

                    if (personCompany == null)
                    {
                        personCompany = new PersonCompany()
                        {
                            CompanyId = company.Id,
                            ParentId = currentPersonCompany.Id,
                            PersonId = person.Id,
                            Position = role.Caption,
                            IsDeleted = false
                        };

                        await _personCompanyRepository.AddAsync(personCompany, cancellationToken);

                        resultViewModel.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(personCompany);
                    }
                    else
                    {
                        personCompany.CompanyId = company.Id;
                        personCompany.Position = role.Caption;

                        await _personCompanyRepository.UpdateAsync(personCompany, cancellationToken);

                        resultViewModel.PersonCompany = _mapper.Map<CompanyForPersonResultViewModel>(personCompany);
                    }
                }
                //else
                //{
                //    PersonCompany personCompany =
                //        await _personCompanyRepository.GetByPersonIdNoTracking(person.Id, cancellationToken);

                //    if (personCompany != null)
                //    {
                //        await _personCompanyRepository.DeleteAsync(personCompany, cancellationToken);
                //    }
                //}

                if (personViewModel.PersonAddress != null)
                {
                    PersonAddress personPrimaryAddress = await _personAddressRepository.GetPersonAddressByPersonId(person.Id, cancellationToken);
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
                //else
                //{
                //    PersonAddress personAddress = await _personAddressRepository.GetPersonAddressByPersonId(person.Id, cancellationToken);
                //    if (personAddress != null)
                //    {
                //        await _personAddressRepository.DeleteAsync(personAddress, cancellationToken);
                //    }
                //}

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



        public async Task<PersonResultViewModel> UpdateAgentMine(long userId, Guid personCode, UpdatePersonInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await UpdateAgentCommon(userId, personCode, viewModel, cancellationToken);


            return person;

        }


        public async Task<Person> UpdateCompanyPerson(Guid personCode, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetWithUserAndAgentByCode(personCode, cancellationToken);

            person.FirstName = viewModel.Person.FirstName;
            person.LastName = viewModel.Person.LastName;
            person.NationalCode = viewModel.Person.NationalCode;
            person.Identity = viewModel.Person.Identity;
            person.FatherName = viewModel.Person.FatherName;
            person.BirthDate = viewModel.Person.BirthDate;
            person.GenderId = viewModel.Person.GenderId;
            person.MarriageId = viewModel.Person.MarriageId;
            person.MillitaryId = viewModel.Person.MillitaryId;
            person.JobName = viewModel.Person.JobName;

            person.Users.FirstOrDefault().Username = viewModel.User.Username;
            person.Users.FirstOrDefault().Password = viewModel.User.Password;
            person.Users.FirstOrDefault().Email = viewModel.User.Email;

            person.CompanyAgents.FirstOrDefault().CityId = viewModel.CityId;
            person.CompanyAgents.FirstOrDefault().Description = viewModel.Description;

            await _personRepository.UpdateAsync(person, cancellationToken);

            return person;
        }

        public async Task<bool> DeleteAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Person person = await _personRepository.GetWithUserAndAgentByCode(personCode, cancellationToken);
                if (person == null)
                {
                    throw new BadRequestException("کد شخص وجود ندارد");
                }

                Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
                if (company == null)
                {
                    throw new BadRequestException("کد شرکت وجود ندارد");
                }

                CompanyAgent agent = await _agentRepository.GetByCompanyAndPersonCodeAsync(code, personCode, cancellationToken);
                if (agent == null)
                {
                    throw new BadRequestException("نماینده وجود ندارد");
                }

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonIdAndCompanyId(person.Id, company.Id, cancellationToken);
                if (personCompany == null)
                {
                    throw new BadRequestException("این شخص با شرکت مورد نظر ارتباطی ندارد");
                }

                UserRole userRole = await _userRoleRepository.GetSingleUserRoleByUserId(person.Users.FirstOrDefault().Id, cancellationToken);
                if (userRole == null)
                {
                    throw new BadRequestException("این شخص در سیستم نقشی ندارد");
                }

                User user = await _userRepository.GetByIdAsync(cancellationToken, userRole.UserId);

                personCompany.IsDeleted = true;
                user.IsDeleted = true;
                person.IsDeleted = true;

                await _agentRepository.DeleteAsync(agent, cancellationToken);
                await _personCompanyRepository.UpdateAsync(personCompany, cancellationToken);
                await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
                await _userRepository.UpdateAsync(person.Users.FirstOrDefault(), cancellationToken);
                await _personRepository.UpdateAsync(person, cancellationToken);

                transaction.Complete();

                return true;
            }
        }


        //public async Task<Guid> GetCompanyCodeWithValidation(long userId, Guid personCode, CancellationToken cancellationToken)
        //{
        //    User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

        //    PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

        //    if (personCompany == null)
        //    {
        //        throw new BadRequestException("شما دسترسی لازم ندارید");
        //    }

        //    Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
        //    if (company == null)
        //    {
        //        throw new BadRequestException("کد شرکت وجود ندارد");
        //    }

        //    return company.Code;
        //}

        public async Task<bool> DeleteAgentMine(long userId, Guid personCode, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

                PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

                if (personCompany == null)
                {
                    throw new BadRequestException("شما دسترسی لازم ندارید");
                }

                Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
                if (company == null)
                {
                    throw new BadRequestException("کد شرکت وجود ندارد");
                }

                Person person = await _personRepository.GetWithUserAndAddressByCode(personCode, cancellationToken);
                if (person == null)
                {
                    throw new BadRequestException("کد شخص وجود ندارد");
                }


                CompanyAgent agent = await _agentRepository.GetLonelyByPersonAndCompanyCodeAsync(company.Code, personCode, cancellationToken);
                if (agent == null)
                {
                    throw new BadRequestException("نماینده وجود ندارد");
                }

                UserRole userRole = await _userRoleRepository.GetSingleUserRoleByUserId(person.Users.FirstOrDefault().Id,cancellationToken);

                await _agentRepository.DeleteAsync(agent, cancellationToken);
                await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
                await _userRepository.DeleteAsync(person.Users.FirstOrDefault(), cancellationToken);

                List<Address> addresses = person.PersonAddresses.Select(s => s.Address).ToList();
                PersonCompany updatingpersonCompany = await _personCompanyRepository.GetByPersonCodeAndCompanyCodeAndParentId(personCode, company.Code,personCompany.Id , cancellationToken);
                if (updatingpersonCompany == null)
                {
                    throw new BadRequestException("شما فردی به هیچ فردی جهت تغییرات دسترسی ندارید");
                }

                await _personAddressRepository.DeleteRangeAsync(person.PersonAddresses, cancellationToken);
                await _addressRepository.DeleteRangeAsync(addresses, cancellationToken);
                await _personCompanyRepository.DeleteAsync(updatingpersonCompany, cancellationToken);
                await _personRepository.DeleteAsync(person, cancellationToken);

                transaction.Complete();

                return true;
            }
            
        }

        public async Task<PagedResult<CompanyAgentViewModel>> GetCompanyAgentsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);

            if (personCompany == null)
            {
                throw new BadRequestException("شما دسترسی لازم ندارید");
            }

            Company company = await _companyRepository.GetByIdAsync(cancellationToken, personCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<CompanyAgent> agents = await _agentRepository.GetCompanyAgents(company.Id, 2, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyAgentViewModel>>(agents);
        }
    }
}
