<div align="right" dir="rtl">

عملیات CRUD جدول Person برای دسترسی ادمین بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Person قرار دارند چراکه ادمین  باید هر فردی را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /person بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

        [HttpPost()]
        public async Task<ApiResult<PersonResultViewModel>> Create(PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await _personService.PersonPostMapperService(personViewModel, cancellationToken);
            return person;
        }


        [HttpGet("{code}")]
        public async Task<ApiResult<PersonResultViewModel>> GetDetail(Guid code, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await _personService.GetPersonDetail(code, cancellationToken);
            return person;
        }

        [HttpGet()]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetAll([FromQuery] PageAbleResult pageAbleResult,
            [FromQuery] DateableViewModel dateableViewModel, CancellationToken cancellationToken)
        {
            var people =
                await _personService.all(pageAbleResult, cancellationToken);
            return people;
        }

        [HttpPut("{code}")]
        public async Task<ApiResult<PersonResultViewModel>> Update(Guid code, UpdatePersonInputViewModel personViewModel, CancellationToken cancellationToken)
        {
            return await _personService.Update(code, personViewModel, cancellationToken);
        }

        [HttpDelete("{code}")]
        public async Task<ApiResult<string>> Delete(Guid code, CancellationToken cancellationToken)
        {
            var res = await _personService.Delete(code, cancellationToken);
            return res.ToString();
        }


        [HttpGet("without_user")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetWithoutUser([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PersonResultViewModel> persons = await _personService.GetAllPersonsWithoutUser(search_text, pageAbleResult, cancellationToken);
            return persons;
        }

        [HttpGet("search")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetSearchedPersons([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PersonResultViewModel> persons = await _personService.GetSearchedPersons(search_text, pageAbleResult, cancellationToken);
            return persons;
        }


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `PersonPostMapperService(personViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<PersonResultViewModel> PersonPostMapperService(PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await CreatePersonCommon(personViewModel, cancellationToken);

            return person;
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


```


<div align="right" dir="rtl">

در این سرویس عملیات درج کاربری انجام میگیرد و بعد از آن درج جدول CompanyPerson 

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

        public async Task<PersonResultViewModel> Update(Guid code, UpdatePersonInputViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel viewModel = await UpdatePersonCommon(code, personViewModel, cancellationToken);

            return viewModel;
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

```

<div align="right" dir="rtl">
پس از اعتبار سنجی موجودیت ها، تمتم جداول درگیر در درج ویرایش شدند.

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetPersonDetail(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `all(pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<PagedResult<PersonResultViewModel>> all(PageAbleResult PageAbleResult,
            CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(PageAbleResult);


            var people = await _personRepository.GetAllAsync(pageAbleModel, cancellationToken);


            return _mapper.Map<PagedResult<PersonResultViewModel>>(people);
        }

```







<div align="right" dir="rtl">

**حذف (سرویس Delete)** : این سرویس متد `Delete(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

<div align="right" dir="rtl">

در این سرویس تمام جداول درگیر باید بصورت آبشاری عملیات حذف را انجام دهند.

<br>

</div>


<div align="right" dir="rtl">

**دریافت افراد بر اساس پارامتر ورودی (سرویس Get)** : این سرویس متد `GetAllPersonsWithoutUser(search_text, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

این متد برای دریافت اشخاصی است که کاربر ندارند یعنی User ندارند.

پارامتر search_text از سمت فرانت می آید و بر اساس مقدار آن در نام و نام خانوادگی اشخاص جستجو می شود.

</div>


```C#

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


```


<br>

<div align="right" dir="rtl">

**دریافت افراد بر اساس پارامتر ورودی (سرویس Get)** : این سرویس متد `GetSearchedPersons(search_text, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

همه کاربران چه با user چه بدون user

پارامتر search_text از سمت فرانت می آید و بر اساس مقدار آن در نام و نام خانوادگی اشخاص جستجو می شود.

</div>


```C#

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

        
```


<br>
