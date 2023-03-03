<div align="right" dir="rtl">

عملیات CRUD جدول PersonCompany بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Company قرار دارند چراکه هر شرکت بیمه باید نماینده خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /company بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

       [HttpGet("mine/person")]
        public async Task<ApiResult<PagedResult<PersonResultWithAgentCompanyViewModel>>> GetCompanyAgentPersonsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _agentService.GetAgentsMine(userId, pageAbleResult, cancellationToken);
        }


        [HttpGet("mine/agent/list")]
        public async Task<ApiResult<List<AgentViewModel>>> GetAgentsListMine(CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.GetAgentsListMine(userId, cancellationToken);
        }

        [HttpGet("mine/agent")]
        public async Task<ApiResult<PagedResult<CompanyAgentViewModel>>> GetCompanyAgentsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _agentService.GetCompanyAgentsMine(userId, pageAbleResult, cancellationToken);
        }

        [HttpPost("mine/person")]
        public async Task<ApiResult<PersonResultViewModel>> CreateCompanyAgentMine(PersonViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.CreateAgentMine(userId, viewModel, cancellationToken);
        }

        [HttpGet("mine/person/{personCode}")]
        public async Task<ApiResult<PersonResultViewModel>> GetCompanyAgentMine(Guid personCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.GetAgentMine(userId, personCode, cancellationToken);
        }

        [HttpPut("mine/person/{personCode}")]
        public async Task<ApiResult<PersonResultViewModel>> UpdateCompanyAgentMine(Guid personCode, UpdatePersonInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.UpdateAgentMine(userId, personCode, viewModel, cancellationToken);
        }

        [HttpDelete("mine/person/{personCode}")]
        public async Task<ApiResult<string>> DeleteCompanyAgentMine(Guid personCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            string result = (await _agentService.DeleteAgentMine(userId, personCode, cancellationToken)).ToString();
            return result;
        }


        [HttpGet("mine/person/without_user")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetPersonsOfCompanyWithoutUser([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PagedResult<PersonResultViewModel> persons = await _companyService.GetAllPersonsWithoutUser(userId, search_text, pageAbleResult, cancellationToken);
            return persons;
        }


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `CreateAgentMine(userId, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<PersonResultViewModel> CreateAgentMine(long userId, PersonViewModel viewModel, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await CreateAgentCommon(userId, viewModel, cancellationToken);

            return person;
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


```


<div align="right" dir="rtl">

در این سرویس عملیات درج کاربری انجام میگیرد و بعد از آن درج جدول CompanyPerson و CompanyAgent

>*برای مطالعه پیاده سازی کاربری [پیاده سازی کاربری](./PersonUserBussiness.md) را مطالعه فرمایید*

<br>

ویومدل ورودی این سرویس : 

</div>

```C#

 public class PersonViewModel
    {
        [JsonProperty("code")]
        public Guid? Code { get; set; }
        [JsonProperty("first_name")]
        [Required]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        [Required]
        public string LastName { get; set; }
        [JsonProperty("national_code")]
        [Required]
        public string NationalCode { get; set; }
        [JsonProperty("identity")]
        [Required]
        public string Identity { get; set; }
        [JsonProperty("father_name")]
        [Required]
        public string FatherName { get; set; }
        [JsonProperty("birth_date")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("gender_id")]
        [Required]
        public byte GenderId { get; set; }
        [JsonProperty("marriage_id")]
        [Required]
        public byte MarriageId { get; set; }
        [JsonProperty("millitary_id")]
        public byte? MillitaryId { get; set; }

        [JsonProperty("job_name")]
        public string JobName { get; set; }

        [JsonProperty("role_id")]
        public long? RoleId { get; set; }

        [JsonProperty("user")]
        public virtual PersonUserViewModel User { get; set; }

        [JsonProperty("person_company")]
        public virtual CompanyForPersonViewModel PersonCompany { get; set; } 

        [JsonProperty("person_address")]
        public virtual AddressForPersonViewModel PersonAddress { get; set; }
        [JsonProperty("agent_id")]
        public long? AgentId { get; set; }
        [JsonProperty("agent_city_id")]
        public long? AgentCityId { get; set; }

    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `UpdateAgentMine(userId, personCode, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

public async Task<PersonResultViewModel> UpdateAgentMine(long userId, Guid personCode, UpdatePersonInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await UpdateAgentCommon(userId, personCode, viewModel, cancellationToken);


            return person;

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

```

<div align="right" dir="rtl">
پس از اعتبار سنجی موجودیت ها، تمتم جداول درگیر در درج ویرایش شدند.

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetAgentMine(userId, personCode, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `GetCompanyAgentsMine(userId, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```


<div align="right" dir="rtl">

**دریافت لیست (سرویس Get)** : این سرویس متد `GetAgentsListMine(userId, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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
```




<div align="right" dir="rtl">

**حذف (سرویس Delete)** : این سرویس متد `DeleteAgentMine(userId, personCode, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

<div align="right" dir="rtl">

در این سرویس تمام جداول درگیر باید بصورت آبشاری عملیات حذف را انجام دهند.

<br>

</div>


<div align="right" dir="rtl">

**دریافت افراد بر اساس پارامتر ورودی (سرویس Get)** : این سرویس متد `_companyService.GetAllPersonsWithoutUser(userId, search_text, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

این متد برای دریافت اشخاصی است که کاربر ندارند یعنی User ندارند.

پارامتر search_text از سمت فرانت می آید و بر اساس مقدار آن در نام و نام خانوادگی اشخاص جستجو می شود.

</div>


```C#

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
```


<br>
