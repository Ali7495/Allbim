<div align="right" dir="rtl">

عملیات CRUD درخواست بیمه هم در کنترلر PolicyRequest انجام می شود هم در Company چراکه ادمین و شرکت هم باید این دسترسی را داشته باشند.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*



سرویس های کنترلر Policy

سرویس ویرایش و حذف موجود نیست
</div>


```C#

        [HttpPost("")]
        public async Task<PolicyRequestSummaryViewModel> CreatePolicyRequest(
            PolicyRequestInputViewModel policyRequestInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            
            var result = await _policyRequestService.Create(policyRequestInputViewModel, userId, cancellationToken);
            return result;
        }

[HttpGet("")]
        public async Task<ApiResult<PagedResult<PolicyRequestViewModel>>> GetAllPlolicyRequests([FromQuery] List<Guid> companyCodes, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            var result = await _policyRequestService.GetAllPolicyRequestsAsync(userId, roleId,companyCodes, pageAbleResult, cancellationToken);
            return result;
        }


        [HttpGet("Status/{slug}")]
        public async Task<ApiResult<PagedResult<PolicyRequestViewModel>>> GetAllPlolicyRequestsStatus([FromRoute] string slug, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            var result = await _policyRequestService.GetAllPolicyRequestsAsyncBySlug(slug, userId, roleId, pageAbleResult, cancellationToken);
            return result;
        }

```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `Create(policyRequestInputViewModel, userId, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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


```


<div align="right" dir="rtl">

در سرویس درج برای درج قیمت مجددا محاسبه قیمت انجام می شود.


ویومدل ورودی سرویس : 
</div>


```C#

public class PolicyRequestInputViewModel
    {
        [JsonProperty(PropertyName = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [JsonProperty(PropertyName = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [JsonProperty(PropertyName = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [JsonProperty(PropertyName = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }
        
        [JsonProperty(PropertyName = "vehicle_id")]
        public long? VehicleId { get; set; }

        [JsonProperty(PropertyName = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        [JsonProperty(PropertyName = "old_insurer_id")]
        public long? OldInsurerId { get; set; }

        [JsonProperty(PropertyName = "old_insurer_start_date")]
        public string OldInsurerStartDate { get; set; } = null;

        [JsonProperty(PropertyName = "old_insurer_expire_date")]
        public string OldInsurerExpireDate { get; set; }    = null;
        
        [JsonProperty(PropertyName = "is_changed_owner")]
        public bool IsChangedOwner { get; set; }

        [JsonProperty(PropertyName = "third_discount_id")]
        public int? ThirdDiscountId { get; set; }

        [JsonProperty(PropertyName = "driver_discount_id")]
        public int? DriverDiscountId { get; set; }

        [JsonProperty(PropertyName = "third_life_damage_id")]
        public int? ThirdLifeDamageId { get; set; }

        [JsonProperty(PropertyName = "third_financial_damage_id")]
        public int? ThirdFinancialDamageId { get; set; }

        [JsonProperty(PropertyName = "driver_life_damage_id")]
        public int? DriverLifeDamageId { get; set; } 
        [JsonProperty( PropertyName = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }  
        [JsonProperty( PropertyName = "is_prev_damaged")]
       
        public bool? IsPrevDamaged { get; set; }

        [JsonProperty(PropertyName = "vehicle_clearance_date")]

        public string VehicleClearanceDate { get; set; } = null;
        
        
        [JsonProperty(PropertyName = "insurer_id")]
        public long InsurerId { get; set; }


        [JsonProperty(PropertyName = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }
        
        
        
        
        
        // Body
        
        
        [JsonProperty(PropertyName ="car_value")]
        public string CarValue { get; set; }

        [JsonProperty(PropertyName = "transportation")]
        public int? Transportation { get; set; }

        [JsonProperty(PropertyName = "flood_and_earth_quake_id")]
        public int? FloodAndEarthquakeId { get; set; }

        [JsonProperty(PropertyName = "glass_breaking_id")]
        public int? GlassBreakingId { get; set; }

        [JsonProperty(PropertyName = "acid_and_chemical_id")]
        public int? AcidAndChemicalId { get; set; }

        [JsonProperty(PropertyName = "stealing_requested_parts_id")]
        public int? StealingRequestedPartsId { get; set; }

        [JsonProperty(PropertyName = "stealing_all_parts_id")]
        public int? StealingAllPartsId { get; set; }

        [JsonProperty(PropertyName = "franchise_removal_id")]
        public int? FranchiseRemovalId { get; set; }

        //[JsonProperty(PropertyName = "price_fluctuatiuon_id")]
        //public int? PriceFluctuatiuonId { get; set; }

        [JsonProperty(PropertyName = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }

        [JsonProperty(PropertyName = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [JsonProperty(PropertyName = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [JsonProperty(PropertyName = "tax_id")]
        public int? TaxId { get; set; }

      
        [JsonProperty(PropertyName = "non_fabrique_assets_value")]
        public decimal NonFabriqueAssetsValue { get; set; }

       
        [JsonProperty(PropertyName =  "prev_isnurance_company_id")]
        public Guid? PrevInsuranceCompanyId { get; set; }
        [JsonProperty(PropertyName = "prev_insurance_end_date")]
        public DateTime PrevInsuranceEndDate { get; set; }
        [JsonProperty(PropertyName = "body_no_damage_discount_id")]
        public DateTime BodyNoDamageDisountId { get; set; }
        
        [JsonProperty(PropertyName = "has_third_insurance")]
        public bool? HasThirdInsurance { get; set; }
        [JsonProperty(PropertyName = "third_insurance_company_id")]
        public Guid? ThirdInsuranceCompanyId { get; set; }
        [JsonProperty(PropertyName = "third_no_damage_discount_id")]
        public DateTime ThirdNoDamageDisountId { get; set; }

        [JsonProperty(PropertyName = "has_natural_disaster")]
        public bool? NaturalDisaster { get; set; }
        

        [JsonProperty(PropertyName = "has_acidic_spray")]
        public bool? AcidicSpray { get; set; }
        

        // [JsonProperty(PropertyName = "franchise_removal_id")]
        // public int? FranchiseRemovalId { get; set; }

        [JsonProperty(PropertyName = "market_fluctuate_cover_id")]
        public int? MarketFluctuateCoverId { get; set; }
        
        // [JsonProperty(PropertyName = "group_discount_id")]
        // public int? GroupDiscountId { get; set; }

        [JsonProperty(PropertyName = "is_cash")]
        public bool? IsCash { get; set; }

        // [JsonProperty(PropertyName = "tax_id")]
        // public int? TaxId { get; set; }

        
        // Life

        [JsonProperty(PropertyName = "is_life_insurance")]
        public bool? IsLifeInsurance { get; set; }
        [JsonProperty(PropertyName = "life_company_id")]
        public Guid? LifeCompanyId { get; set; }



        // Bank
        [JsonProperty(PropertyName = "has_bank_long_account")]
        public bool? HasBankLongAccount { get; set; }
        [JsonProperty(PropertyName = "bank_account_id")]
        public long? BankAccountId { get; set; }

    }


```




<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `GetAllPolicyRequestsAsync(userId, roleId,companyCodes, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

<div align="right" dir="rtl">

پس از بررسی موجودیت ها اطلاعات دریافتی بر اساس صفحه بندی (Paging) مپ شده و برگردانده می شوند. این سرویس اطلاعات را بر اساس نقش کاربر درخواست دهنده بر میگرداند.

<br>

این سرویس ها دقیقا مشابه سمت شرکت و ادمین هستند و تنها تفاوت اعتبار سنجی آن هاست.

</div>


