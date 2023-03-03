<div align="right" dir="rtl">

عملیات CRUD جدول CompanyAgent بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Company قرار دارند چراکه هر شرکت بیمه باید نماینده خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /company بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

        [AllowAnonymous]
        [HttpGet("{code}/agent")]
        public async Task<ApiResult<PagedResult<AgentViewModel>>> GetCompanyAgents(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgents(code, pageAbleResult, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/agent/list")]
        public async Task<ApiResult<List<AgentViewModel>>> GetAgentsList(Guid code, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgentsList(code, cancellationToken);
        }

        [HttpPost("{code}/agent")]
        public async Task<ApiResult<AgentPersonViewModel>> CreateCompanyAgent(Guid code, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _agentService.CreateAgent(code, viewModel, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/agent/{personCode}")]
        public async Task<ApiResult<AgentViewModel>> GetCompanyAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgent(code, personCode, cancellationToken);
        }

        [HttpPut("{code}/agent/{personCode}")]
        public async Task<ApiResult<AgentPersonViewModel>> UpdateCompanyAgent(Guid code, Guid personCode, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _agentService.UpdateAgent(code, personCode, viewModel, cancellationToken);
        }

        [HttpDelete("{code}/agent/{personCode}")]
        public async Task<ApiResult<string>> DeleteCompanyAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            string result = (await _agentService.DeleteAgent(code, personCode, cancellationToken)).ToString();
            return result;
        }


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `CreateAgent(code, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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


```


<div align="right" dir="rtl">

در این سرویس عملیات درج کاربری انجام میگیرد و بعد از آن درج جدول CompanyAgent

>*برای مطالعه پیاده سازی کاربری [پیاده سازی کاربری](./PersonUserBussiness.md) را مطالعه فرمایید*

<br>

ویومدل ورودی این سرویس : 

</div>

```C#

 public class CopmanyAgentAndPersonViewModel
    {
        
        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("person")]
        public AgentPersonInputViewModel Person { get; set; }
        [JsonProperty("user")]
        public AgentUserForUpdateViewModel User { get; set; }
    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `UpdateAgent(code, personCode, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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


```

<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و موجودیت شخص و کاربری ویرایش می شوند و فیلد شهر و توضیحات جدول CompanyAgent ویرایش می شوند.

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetAgent(code, personCode, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<AgentViewModel> GetAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            var agent = await _agentRepository.GetByCompanyAndPersonCodeAsync(code, personCode, cancellationToken);
            return _mapper.Map<AgentViewModel>(agent);
        }

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `GetAgents(code, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```


<div align="right" dir="rtl">

**دریافت لیست (سرویس Get)** : این سرویس متد `GetAgentsList(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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
```




<div align="right" dir="rtl">

**حذف (سرویس Delete)** : این سرویس متد `DeleteAgent(code, personCode, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

<div align="right" dir="rtl">

در این سرویس تمام جداول درگیر باید بصورت آبشاری عملیات حذف را انجام دهند.

<br>

سرویس های mine فرایند های CompanyAgent نیز به همین منوال است که تفاوت های آن در مفاهیم مشترک و پایه ای آمده است
<br>