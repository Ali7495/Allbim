using DAL.Contracts;
using DAL.Contracts.EnumIRepositories;
using DAL.Contracts.EnumRepositories;
using DAL.Repositories;
using DAL.Repositories.EnumRepositories;
using Logging.Contracts;
using Logging.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Agent;
using Services.ArticleManagement;
using Services.City;
using Services.CompanyCenter;
using Services.Contactus;
using Services.Damage;
using Services.FactorServices;
using Services.Info;
using Services.InsurerServices;
using Services.InsurerTermDetailServices;
using Services.LogSystem;
using Services.Policy;
using Services.PolicyRequest;
using Services.PolicyRequestComment;
using Services.PolicyRequestStatus;
using Services.Product;
using Services.PublicFaq;
using Services.Redis;
using Services.SmsService;

namespace albim.Configuration
{
    public static class IocConfig
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            #region Repositories

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ILoggingRepository<>), typeof(LoggingRepository<>));
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<ICenterRepository, CenterRepository>();
            services.AddScoped<IArticlesManagementRepository, ArticlesManagementRepository>();
            services.AddScoped<IArticleSectionRepository, ArticleSectionRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
            services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IPolicyRequestRepository, PolicyRequestRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IInsuranceRepository, InsuranceRepository>();
            services.AddScoped<IInsurerRepository, InsurerRepository>();
            services.AddScoped<IDamageRepository, DamageRepository>();
            services.AddScoped<IFinancialRepository, FinancialRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IDriverDiscountRepository, DriverDiscountRepository>();
            services.AddScoped<IThirdDiscountRepository, ThirdDiscountRepository>();
            services.AddScoped<IPolicyRequestFactorRepository, PolicyRequestFactorRepository>();
            services.AddScoped<IPolicyRequestHolderRepository, PolicyRequestHolderRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IInsuredRequestVehicleRepository, InsuredRequestVehicleRepository>();
            services.AddScoped<IPolicyRequestIssueRepository, PolicyRequestIssueRepository>();
            services.AddScoped<IPolicyRequestAttachmentRepository, PolicyRequestAttachmentRepository>();
            services.AddScoped<INoBodyDamageDiscountRepository, NoBodyDamageDiscountRepository>();
            services.AddScoped<IPolicyRequestInspectionRepository, PolicyRequestInspectionRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPolicyRequestStatusRepository, PolicyRequestStatusRepository>();
            services.AddScoped<IRegisterTempRepository, RegisterTempRepository>();
            services.AddScoped<IPolicyRequestDetailRepository, PolicyRequestDetailRepository>();
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IInsurerTermRepository, InsurerTermRepository>();
            services.AddScoped<ICompanyCenterRepository, CompanyCenterRepository>();
            services.AddScoped<ICompanyCenterScheduleRepository, CompanyCenterScheduleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IPersonCompanyRepository, PersonCompanyRepository>();
            services.AddScoped<IInsurerTermEnumRepository, InsurerTermEnumRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IArticleMetaKeyRepository, ArticleMetaKeyRepository>();
            services.AddScoped<IPolicyRequestCommentRepository, PolicyRequestCommentRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IInfoRepository, InfoRepository>();
            services.AddScoped<IInsuranceFAQServices, InsuranceFAQService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<ICentralRulesRepository, CentralRulesRepository>();
            services.AddScoped<IBodyNoDamageDiscountYearRepository, BodyNoDamageDiscountYearRepository>();
            services.AddScoped<IThirdInsuranceCreditMonthRepository, ThirdInsuranceCreditMonthRepository>();
            services.AddScoped<IThirdMaxFinancialCoverRepository, ThirdMaxFinancialCoverRepository>();
            services.AddScoped<IInspectionTypeRepository, InspectionTypeRepository>();
            services.AddScoped<IPolicyRequestCommentAttachmentRepository, PolicyRequestCommentAttachmentRepository>();
            services.AddScoped<IHandledErrorRepository, HandledErrorRepository>();
            services.AddScoped<IOprationLogRepository, OprationLogRepository>();
            services.AddScoped<ISystemErrorLogRepository, SystemErrorLogRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IAgentSelectionTypeRepository, AgentSelectionTypeRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPublicFaqRepository, PublicFaqRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInsurerTermDetailRepository, InsurerTermDetailRepository>();
            services.AddScoped<IPricingRepository, PricingRepository>();
            services.AddScoped<ICalculationRepository, CalculationRepository>();
            services.AddScoped<IConditionRepository, ConditionRepository>();
            services.AddScoped<IRelatedResourceRepository, RelatedResourceRepository>();
            services.AddScoped<IInsuranceTermTypeRepository, InsuranceTermTypeRepository>();
            services.AddScoped<IWithoutInsuranceRepository, WithoutInsuranceRepository>();
            services.AddScoped<IIsChagedOwnerRepository, IsChagedOwnerRepository>();
            services.AddScoped<IThirdLifeDamageRepository, ThirdLifeDamageRepository>();
            services.AddScoped<IThirdFinancialDamageRepository, ThirdFinancialDamageRepository>();
            services.AddScoped<IDriverLifeDamageRepository, DriverLifeDamageRepository>();
            services.AddScoped<IIsZeroKilometerRepository, IsZeroKilometerRepository>();
            services.AddScoped<IIsPrevDamagedRepository, IsPrevDamagedRepository>();
            services.AddScoped<IIsCashRepository, IsCashRepository>();
            services.AddScoped<IVehicleRuleCategoryRepository, VehicleRuleCategoryRepository>();
            services.AddScoped<ISchemaRepository, SchemaRepository>();
            services.AddScoped<ICentralRuleTypeRepository, CentralRuleTypeRepository>();
            services.AddScoped<IEnumerationRepository, EnumerationRepository>();
            services.AddScoped<IAgentSelectRepository, AgentSelectRepository>();
            services.AddScoped<IResourceOperationRepository, ResourceOperationRepository>();
            services.AddScoped<IFactorServices, FactorServices>();
            services.AddScoped<IPolicyRequestFactorDetailRepository, PolicyRequestFactorDetailRepository>();
            services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddScoped<ICompanyAgentPersonRepository, CompanyAgentPersonRepository>();
            services.AddScoped<IPaymentGatewayRepository, PaymentGatewayRepository>();

            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IPolicyRequestStatusService, PolicyRequestStatusService>();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<IPolicyRequestDetailService, PolicyRequestDetailService>();
            services.AddScoped<IPolicyRequestService, PolicyRequestService>();
            services.AddScoped<IPolicyRequestFactorService, PolicyRequestFactorService>();
            services.AddScoped<ISsoService, SsoService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IArticlesManagementService, ArticlesManagementService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IInsuredRequestService, InsuredRequestService>();
            services.AddScoped<IInsuredRequestCompanyService, InsuredRequestCompanyService>();
            services.AddScoped<IInsuredRequestPersonService, InsuredRequestPersonService>();
            services.AddScoped<IInsuredRequestPlaceService, InsuredRequestPlaceService>();
            services.AddScoped<IInsuredRequestRelatedPersonService, InsuredRequestRelatedPersonService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IPersonAddressService, PersonAddressService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IVehicleApplicationService, VehicleApplicationService>();
            services.AddScoped<IInsuranceCenteralRuleService, InsuranceCenteralRuleService>();
            // services.AddScoped<IStringLocalizer, StringLocalizer<Resource>>();
            services.AddScoped<IEnumService, EnumService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IInsurerTermService, InsurerTermService>();
            services.AddScoped<ICompanyCenterServices, CompanyCenterServices>();
            services.AddScoped<IInsurerServices, InsurerServices>();
            services.AddScoped<IDiscountServices, DiscountServices>();
            services.AddScoped<IPolicyRequestCommentService, PolicyRequestCommentService>();
            services.AddScoped<ICommentServices, CommentServices>();
            services.AddScoped<IInfoService, InfoService>();
            // services.AddScoped<IFAQServices, FAQService>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IInspectionServices, InspectionServices>();
            services.AddScoped<IIssueSessionService, IssueSessionService>();
            services.AddScoped<ILogsService, LogsService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IBodyRequestService, BodyRequestService>();
            services.AddScoped<IPublicFaqService, PublicFaqService>();
            services.AddScoped<IMarketFluctuationRepository, MarketFluctuationRepository>();
            services.AddScoped<IInsurerTermDetailServices, InsurerTermDetailService>();
            services.AddScoped<ISchemaServices, SchemaServices>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IResourceOperationService, ResourceOperationService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IInsuranceFAQRepository, InsuranceFAQRepository>();

            #endregion

            return services;
        }

        public static IServiceCollection AddIocConfig(
            this IServiceCollection services)
        {
            return Configure(services);
        }
    }
}