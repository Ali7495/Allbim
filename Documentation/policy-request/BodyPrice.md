<div align="right" dir="rtl">

فرانت با فراخوانی آدرس `body/price` که برای محاسبه بیمه شخص ثالث است و با متد Get و  ارسال پارامتر ها وارد اکشن زیر در کنترلر Product می شود:

</div>

```C#
        [AllowAnonymous]
        [HttpGet("body/price")] 
        public async Task<ApiResult<List<ThirdInsuranceResultViewModel>>> GetThirdProducts(string slug, [FromQuery] ThirdProductInputViewModel thirdProductInputViewModel, CancellationToken cancellationToken)
        {
            return await _policyRequestService.GetAvailableThirdInsuranceInsurers(thirdProductInputViewModel, cancellationToken);
        }

```

<div align="right" dir="rtl">

بدنه ویومدلی که پارامتر ها را دریافت میکند به شرح زیر است:

</div>

```C#

public class BodyProductInputViewModel
    {


        [FromQuery(Name = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [FromQuery(Name = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [FromQuery(Name = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }
        
        [FromQuery(Name = "vehicle_id")]
        public long? VehicleId { get; set; }

        [FromQuery(Name = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        
        public long VehicleRuleCategoryId { get; set; }

        [FromQuery(Name = "car_value")]
        public string CarValue { get; set; }  
        [FromQuery(Name = "non_fabrique_assets_value")]
        public decimal NonFabriqueAssetsValue { get; set; }

        [FromQuery(Name = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }

        [FromQuery(Name = "prev_isnurance_company_id")]
        public Guid? PrevInsuranceCompanyId { get; set; }
        [FromQuery(Name = "prev_insurance_end_date")]
        public DateTime PrevInsuranceEndDate { get; set; }
        [FromQuery(Name = "body_no_damage_discount_id")]
        public long BodyNoDamageDisountId { get; set; }
        
        [FromQuery(Name = "has_third_insurance")]
        public bool? HasThirdInsurance { get; set; }
        [FromQuery(Name = "third_insurance_company_id")]
        public Guid? ThirdInsuranceCompanyId { get; set; }
        [FromQuery(Name = "third_no_damage_discount_id")]
        public long ThirdNoDamageDisountId { get; set; }
        
        [FromQuery(Name = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }

        

        [FromQuery(Name = "transportation")]
        public int? Transportation { get; set; }


        [FromQuery(Name = "flood_and_earth_quake_id")]
        public int? FloodAndEarthquakeId { get; set; }

        [FromQuery(Name = "glass_breaking_id")]
        public int? GlassBreakingId { get; set; }

        [FromQuery(Name = "acid_and_chemical_id")]
        public int? AcidAndChemicalId { get; set; }

        [FromQuery(Name = "stealing_requested_parts_id")]
        public int? StealingRequestedPartsId { get; set; }

        [FromQuery(Name = "stealing_all_parts_id")]
        public int? StealingAllPartsId { get; set; }

        [FromQuery(Name = "franchise_removal_id")]
        public int? FranchiseRemovalId { get; set; }

        [FromQuery(Name = "market_fluctuate_cover_id")]
        public int? MarketFluctuateCoverId { get; set; }

        [FromQuery(Name = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }


        [FromQuery(Name = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [FromQuery(Name = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [FromQuery(Name = "is_cash")]
        public bool? IsCash { get; set; }

        [FromQuery(Name = "tax_id")]
        public int? TaxId { get; set; }


        // Bank
        [FromQuery(Name = "has_bank_long_account")]
        public bool? HasBankLongAccount { get; set; }
        [FromQuery(Name = "bank_account_id")]
        public long? BankAccountId { get; set; }

        public decimal SuggestedPrice { get; set; }
    }

```

<div align="right" dir="rtl">

این اکشن سرویس `GetAvailableBodyInsurers(slug, bodyProductInputViewModel, cancellationToken)` را که وظیفه محاسبه دارد فراخوانی میکند



در زیر کد سرویس آورده شده:

</div>

```C#

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

```

<div align="right" dir="rtl">

همان طور که ملاحظه می کنید در بدنه این سرویس لیست استپ های بیمه بدنه دریافت شده و به متد محاسبه کننده پاس داده می شود

>*برای مشاهده استپ های بیمه شخص ثالث [استپ های بدنه](./BodySteps.md) را مطالعه فرمایید*

برای محاسبه باید قوانین بیمه مرکزی را نیز لحاظ نمود. همچنین هر بیمه گر می تواند قوانین خودش را نیز داشته باشد.

>*برای مطالعه تشریح قوانین بیمه مرکزی [Central Rule](./CentralRule.md) را مشاهده فرمایید*


>*برای مطالعه تشریح قوانین بیمه گر [Insurer Term](./InsurerTerm.md) را مشاهده فرمایید*


و نهایتا خروجی این سرویس لیستی از ویومدل زیر برای نمایش قیمت ها می باشد: 

</idv>


```C#

 public class BodyInsuranceResultViewModel
    {
        public long Id { get; set; }
        public string title { get; set; }
        public string Price { get; set; }
        public string number { get; set; }
        public string level { get; set; }
        public string num { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        public List<InsurerDetailTestViewModel> products { get; set; }

    }

```

