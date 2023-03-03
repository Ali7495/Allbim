<div align="right" dir="rtl">

فرانت با فراخوانی آدرس `third/price` که برای محاسبه بیمه شخص ثالث است و با متد Get و ارسال پارامتر ها وارد در کنترلر Product و اکشن زیر می شود:

</div>

```C#
        [AllowAnonymous]
        [HttpGet("third/price")] 
        public async Task<ApiResult<List<ThirdInsuranceResultViewModel>>> GetThirdProducts(string slug, [FromQuery] ThirdProductInputViewModel thirdProductInputViewModel, CancellationToken cancellationToken)
        {
            return await _policyRequestService.GetAvailableThirdInsuranceInsurers(thirdProductInputViewModel, cancellationToken);
        }

```

<div align="right" dir="rtl">

بدنه ویومدلی که پارامتر ها را دریافت میکند به شرح زیر است:

</div>

```C#

 public class ThirdProductInputViewModel
    {
        [FromQuery(Name = "vehicle_type_id")] public long VehicleTypeId { get; set; }

        [FromQuery(Name = "vehicle_brand_id")] public long VehicleBrandId { get; set; }

        [FromQuery(Name = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [FromQuery(Name = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }

        [FromQuery(Name = "vehicle_id")] public long? VehicleId { get; set; }

        [FromQuery(Name = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        [FromQuery(Name = "old_insurer_id")] public long? OldInsurerId { get; set; }

        [FromQuery(Name = "old_insurer_start_date")]
        public string OldInsurerStartDate { get; set; } = null;

        [FromQuery(Name = "old_insurer_expire_date")]
        public string OldInsurerExpireDate { get; set; } = null;

        [FromQuery(Name = "is_changed_owner")]
        public bool IsChangedOwner { get; set; }

        [FromQuery(Name = "third_discount_id")]
        public int? ThirdDiscountId { get; set; }

        [FromQuery(Name = "driver_discount_id")]
        public int? DriverDiscountId { get; set; }

        [FromQuery(Name = "third_life_damage_id")]
        public int? ThirdLifeDamageId { get; set; }

        [FromQuery(Name = "third_financial_damage_id")]
        public int? ThirdFinancialDamageId { get; set; }

        [FromQuery(Name = "driver_life_damage_id")]
        public int? DriverLifeDamageId { get; set; }

        [FromQuery(Name = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }

        [FromQuery(Name = "is_prev_damaged")] public bool? IsPrevDamaged { get; set; }

        [FromQuery(Name = "vehicle_clearance_date")]

        public string VehicleClearanceDate { get; set; } = null;

        [FromQuery(Name = "insurer_id")] 
        public long InsurerId { get; set; }

        [FromQuery(Name = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }
        [FromQuery(Name = "body_damage_discount")]
        public int? NoDamageDiscountId { get; set; }


        public decimal SuggestedPrice { get; set; }

    }

```

<div align="right" dir="rtl">

این اکشن سرویس `GetAvailableThirdInsuranceInsurers(thirdProductInputViewModel, cancellationToken)` را که وظیفه محاسبه دارد فراخوانی میکند



در زیر کد سرویس آورده شده:

</div>

```C#

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


```

<div align="right" dir="rtl">

همان طور که ملاحظه می کنید در بدنه این سرویس لیست استپ های بیمه شخص ثالث دریافت شده و به متد محاسبه کننده پاس داده می شود

>*برای مشاهده استپ های بیمه شخص ثالث [استپ های ثالث](./ThirdSteps.md) را مطالعه فرمایید*

برای محاسبه باید قوانین بیمه مرکزی را نیز لحاظ نمود. همچنین هر بیمه گر می تواند قوانین خودش را نیز داشته باشد.

>*برای مطالعه تشریح قوانین بیمه مرکزی [Central Rule](./CentralRule.md) را مشاهده فرمایید*


>*برای مطالعه تشریح قوانین بیمه گر [Insurer Term](./InsurerTerm.md) را مشاهده فرمایید*


و نهایتا خروجی این سرویس لیستی از ویومدل زیر برای نمایش قیمت ها می باشد: 

</idv>


```C#

public class ThirdInsuranceResultViewModel
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

        public List<ThirdFactorViewModel> ThirdMaxFinancialCovers { get; set; }
        public List<ThirdFactorViewModel> ThirdInsuranceCreditDurations { get; set; }
    }


```

