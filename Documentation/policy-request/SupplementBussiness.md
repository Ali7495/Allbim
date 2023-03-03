<div align="right" dir="rtl">

عملیات درج یا ویرایش جداول PolicyRequestHolder و InsuredRequestVehicle بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر PolicyRequest و Company قرار دارند چراکه هر شرکت بیمه باید قوانین خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید company/ یا policy-request/ بیاید.

در این قسمت به شرح درج و ویرایش مشخصات بیمه بدنه می پردازیم به دلیل اینکه تمام این عملیات ها در تمام کنترلر ها و برای بیمه ها یکسان و مشابه است.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

<br>

<br>


سرویس های کنترلر PolicyRequest : 

</div>

```C#

        [HttpPut("{code}/BodySupplement")] 
        public async Task<ApiResult<BodySupplementInfoViewModel>> AddOrUpdateBodySupplementIssue(string code, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var result = await _policyRequestService.AddOrUpdateBodySupplement(code,userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodySupplementIssue(string code, CancellationToken cancellationToken)
        {
            BodySupplementInfoViewModel result = await _policyRequestService.GetBodySupplement(code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }




        [HttpPut("mine/{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> AddOrUpdateBodySupplementIssueMine(string code, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            var result = await _policyRequestService.AddOrUpdateBodySupplementMine(code, userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodySupplementIssueMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            BodySupplementInfoViewModel result = await _policyRequestService.GetBodySupplementMine(userId, code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }







        [HttpPost("{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateHolderSupplementInfo(string code, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicySupplementViewModel result = await _policyRequestService.CreatePolicyRequestHolderSupplementInfoAsync(code, viewModel, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }

        [HttpGet("{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfo(string code, CancellationToken cancellationToken)
        {
            PolicySupplementViewModel result = await _policyRequestService.GetPolicyRequestHolderSupplementInfo(code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }


        [HttpPost("mine/{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateHolderSupplementInfoMine(Guid code, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _policyRequestService.CreatePolicyRequestHolderSupplementInfoAsyncMine(userId, code, viewModel, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }

        [HttpGet("mine/{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfoMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _policyRequestService.GetPolicyRequestHolderSupplementInfoMine(userId, code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }

```


<div align="right" dir="rtl">
<br>
<br>


سرویس های کنترلر Company : 

</div>


```C#

 [HttpPost("mine/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateCompanyHolderSupplementInfoMine(Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _companyService.CreateCompanyPolicyRequestHolderSupplementInfoAsyncMine(userId, policyCode, viewModel, cancellationToken);
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfoMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _companyService.GetCompanyPolicyRequestHolderSupplementInfoMine(userId, policyCode, cancellationToken);

            return result;

        }

        [HttpPut("mine/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<CompanyBodySupplementInfoViewModel>> AddOrUpdateCompanyBodySupplementIssueMine(Guid policyCode, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            CompanyBodySupplementInfoViewModel result = await _companyService.AddOrUpdateCompanyBodySupplementMine(policyCode, userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodyCompanySupplementIssueMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            BodySupplementInfoViewModel result = await _companyService.GetCompanyBodySupplementMine(userId, policyCode, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }







        [HttpPost("{Code}/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateCompanyHolderSupplementInfo(Guid Code, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {

            PolicySupplementViewModel result = await _companyService.CreateCompanyPolicyRequestHolderSupplementInfoAsync(Code, policyCode, viewModel, cancellationToken);
            return result;
        }
        [HttpGet("{Code}/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfo(Guid Code, Guid policyCode, CancellationToken cancellationToken)
        {

            PolicySupplementViewModel result = await _companyService.GetCompanyPolicyRequestHolderSupplementInfo(Code, policyCode, cancellationToken);

            return result;

        }


        [HttpPut("{Code}/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<CompanyBodySupplementInfoViewModel>> AddOrUpdateCompanyBodySupplementIssue(Guid Code, Guid policyCode, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {

            CompanyBodySupplementInfoViewModel result = await _companyService.AddOrUpdateCompanyBodySupplement(policyCode, Code, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodyCompanySupplementIssue(Guid Code, Guid policyCode, CancellationToken cancellationToken)
        {


            BodySupplementInfoViewModel result = await _companyService.GetCompanyBodySupplement(Code, policyCode, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }


```


<div align="right" dir="rtl">

**درج یا ویرایش (سرویس Put)** : این سرویس متد AddOrUpdateBodySupplement(code,userId, viewModel, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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






        public async Task<PolicyRequestHolder> AddOrUpdatePolicyRequestHolderCommon(long userId, DAL.Models.PolicyRequest policyRequest, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            User AuthenticatedUser =
                await _userRepository.GetwithPersonByIdTracked(cancellationToken, userId);
            if (AuthenticatedUser == null)
            {
                throw new BadRequestException("اطلاعات کاربری شخص وارد شده، وجود ندارد");
            }

            PolicyRequestHolder policyRequestHolder = await
                _policyRequestHolderRepository.GetByPolicyRequestCode(policyRequest.Code, cancellationToken);
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


```

<div align="right" dir="rtl">

ویومدل ورودی :

</div>

```C#

public class BodySupplementInfoViewModel
    {

        [JsonProperty("issued_person_type")]
        public byte? IssuedPersonType { get; set; }

        [JsonProperty("owner_person")]
        public virtual BodyOwnerSupplementViewModel OwnerPerson { get; set; }

        [JsonProperty("issued_person")]
        public virtual BodyIssueSupplementInfoViewModel IssuedPerson { get; set; }

        // [JsonProperty("address")]
        // public virtual BodySupplementAddressViewModel Address { get; set; }
    }


```


<div align="right" dir="rtl">

در متد `AddOrUpdatePolicyRequestHolderCommon` بررسی می شود اگر IssuedPersonType برابر 1 بود یعنی برای خود صادر شده و متد AddOrUpdateBodyOwner صدا زده می شود در غیر این صورت یعنی برای دیگری صادر شده و متد  AddOrUpdateBodyIssued کال می شود.

</div>


```C#

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
                _insuredRequestVehicleRepository.GetInsuredRequestVehicleByPolicyRequestId(policyRequestHolder.PolicyRequestId,
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


```





<div align="right" dir="rtl">
همان طور که مشاهده می شود، در متد AddOrUpdateBodyOwner چون کاربر خود قبلا حساب داشته و Person موجود است صرفا ویرایش می شود. همچنین به تبع آن جداول PersonAddress ویرایش و جدول Address درصورتی که آدرس جدیدی باشد درج می شوند.

<br>

در متد AddOrUpdateBodyIssued اگر شخصی که باید برایش بیمه صادر شود اطلاعات داشته باشد و کاربر استفاده کننده شخص دیگری باشد امکان ویرایش اطلاعات ندارد. اما اگر شخص بیمه شونده اطلاعات در سیستم نداشته باشد آنگاه درج می شود. سرویس درج کننده 
 `personService.Create(personViewModel, cancellationToken)` است که ویودل ورودی آن به شرح زیر است : 
<br>

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


<br>
<br>
<div align="right" dir="rtl">

**دریافت (سرویس Get)** : این سرویس متد `GetBodySupplement(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```



<div align="right" dir="rtl">

سرویس های mine فرایند های بالا در تمامی کنترلر ها نیز به همین منوال است که تفاوت های آن در مفاهیم مشترک و پایه ای آمده است. 
<br>