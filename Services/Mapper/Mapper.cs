using System;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using Common.Utilities;
using DAL.Models;
using Models.City;
using Models.Company;
using Models.Enums;
using Models.Person;
using Models.Policy;
using Models.Vehicle;
using Models.Insurance;
using Models.PolicyRequest;
using Models.PolicyRequestIssue;
using Models.User;
using Models.PolicyRequestSupplement;
using Models.PolicyRequestInspection;
using Models.PolicyRequestPaymentInfo;
using Cityy = DAL.Models.City;
using Models.Center;
using Models.Reminder;
using Models.Agent;
using Models.PageAble;
using Models.InsurerTerm;
using Models.CompanyCenter;
using Models.Insurer;
using Models.Articles;
using Models.Discount;
using Models.PolicyRequestCommet;
using Models.Comment;
using Models.Article;
using Models.Info;
using Models.FAQ;
using Models.Category;
using Models.Product;
using Models.QueryParams;
using Models.BodySupplementInfo;
using Models.Inspection;
using Models.CompanyCenterSchedule;
using Models.Issue;
using Logging.LogModels;
using Models.LogsModels;
using Models.ContactUs;
using Services.ViewModels.Menu;
using Models.PublicFaq;
using Models.Attachment;
using Models.Customer;
using Models.PersonCompany;
using Models.CompanyPolicyRequest;
using Models.InsurerTernDetail;
using Models.InsuranceTermType;
using Models.Role;
using Models.CompanyUser;
using Models.Schema;
using Models.InsuranceCentralRule;
using Models.CentralRuleType;
using Models.CompanyComment;
using Models.CompanyAgent;
using Models.Permission;
using Models.Resource;
using Models.CompanyPolicySuplement;
using Models.CompanyFactor;
using Models.Factor;
using Models.PolicyRequestFactor;
using Models.Payment;
using Models.PaymentGateway;
using Models.PaymentStatus;
using Models.Place;
using Models.RolePermission;
using Models.Township;
using Models.Upload;

namespace Services.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            AllowNullDestinationValues = true;
            CreateMap<Payment, PaymentViewModel>();
            //Source -> Destination
            CreateMap<Reminder, ReminderResultViewModel>();
            CreateMap<Center, CenterResultViewModel>();
            CreateMap<DAL.Models.PolicyRequestStatus, PolicyRequestStatusViewModel>();


            CreateMap<VehicleType, VehicleTypeViewModel>();
            CreateMap<VehicleBrand, VehicleBrandResultViewModel>();
            CreateMap<VehicleBrand, VehicleBrandInputViewModel>();
            CreateMap<Vehicle, VehicleResultViewModel>();
            CreateMap<Vehicle, VehicleInputViewModel>();
            CreateMap<DAL.Models.Policy, PolicyViewModel>();
            CreateMap<Article, ArticleSummaryViewModel>();
            CreateMap<PolicyDetail, PolicyDetailViewModel>();
            CreateMap<PolicyHolder, PolicyHolderViewModel>();
            CreateMap<PolicyHolderCompany, PolicyHolderCompanyViewModel>();
            CreateMap<PolicyHolderPerson, PolicyHolderPersonViewModel>();
            CreateMap<Insurance, InsuranceViewModel>();
            CreateMap<InsuranceCentralRule, InsuranceCenteralRuleViewModel>();
            CreateMap<Insured, InsuredViewModel>();
           
            CreateMap<InsuredCompany, InsuredCompanyViewModel>();
            CreateMap<Article, ArticleResultViewModel>();
            CreateMap<InsuredPerson, InsuredPersonViewModel>();
            CreateMap<InsuredPlace, InsuredPlaceViewModel>();
            CreateMap<InsuredRelatedPerson, InsuredRelatedPersonViewModel>();
            CreateMap<InsuredRequest, InsuredRequestViewModel>();
            CreateMap<InsuredRequestCompany, InsuredRequestCompanyViewModel>();
            CreateMap<InsuredRequestPerson, InsuredRequestPersonViewModel>();
            CreateMap<InsuredRequestPlace, InsuredRequestPlaceViewModel>();
            CreateMap<InsuredRequestRelatedPerson, InsuredRequestRelatedPersonViewModel>();
            CreateMap<InsuredRequestVehicle, InsuredRequestVehicleDetailViewModel>();
            CreateMap<InsuredRequestVehicleDetail, InsuredViewModel>();
            CreateMap<InsuredVehicle, InsuredVehicleViewModel>();
            CreateMap<InsurerTerm, InsurerTermViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestSummaryViewModel>();
            CreateMap<PolicyRequestDetail, PolicyRequestDetailViewModel>();
            CreateMap<PolicyRequestFactor, PolicyRequestFactorViewModel>();
            CreateMap<User, UserInfoViewModel>();
            // .ForMember(des=>des.FirstName,opt=>opt.MapFrom(src=>src.Person.FirstName));
            CreateMap<Person, PersonInfoViewModel>();
            CreateMap<Payment, PaymentViewModel>();


            CreateMap<VehicleApplication, VehicleApplicationInputViewModel>();
            CreateMap<VehicleApplication, VehicleApplicationResultViewModel>();
            CreateMap<PersonAttachment, PersonAttachmentViewModel>()
                .ForMember(dest =>
                        dest.PersonCode,
                    opt => opt.MapFrom(src => src.Person.Code))
                .ForMember(dest =>
                        dest.attachmentCode,
                    opt => opt.MapFrom(src => src.Attachment.Code));

            CreateMap<PolicyRequestHolder, PolicyRequestHolderViewModel>().ForMember(dest => dest.Code,
                opt => opt.MapFrom(src => src.PolicyRequest.Code));

            CreateMap<PolicyRequestAttachment, PolicyRequestAttachmentViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest => dest.AttachmentCode, opt => opt.MapFrom(src => src.Attachment.Code));
            CreateMap<PolicyRequestAttachment, PolicyRequestAttachmentDownloadViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest => dest.AttachmentCode,
                    opt => opt.MapFrom(src => src.Attachment.Code.ToString() + src.Attachment.Extension.ToString()));
            CreateMap<PolicyRequestHolder, PolicySupplementViewModel>()
                .ForMember(dest =>
                    dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest =>
                        dest.IssuedPerson,
                    opt => opt.MapFrom(src =>
                        src.IssuedPersonType == 1
                            ? src.PolicyRequest.InsuredRequests.First().InsuredRequestVehicles.First().OwnerPerson
                            : src.Person))
                .ForMember(dest =>
                        dest.OwnerPerson,
                    opt => opt.MapFrom(src =>
                        src.PolicyRequest.InsuredRequests.First().InsuredRequestVehicles.First().OwnerPerson))
                // dest.OwnerPerson, opt => opt.MapFrom(src => src.IssuedPersonType == 1 ? src.Person : null))
                ;


            CreateMap<Insurer, InsurerViewModel>()
                .ForMember(dest =>
                        dest.Name,
                    opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<Company, CompanyViewModel>();
            CreateMap<DAL.Models.Enumeration, EnumViewModel>()
                .ForMember(dest =>
                    dest.Value, opt => opt.MapFrom(src => src.EnumId))
                .ForMember(dest =>
                    dest.Name, opt => opt.MapFrom(src => src.EnumCaption));
            //.ForMember(dest =>
            //        dest.PolicyRequestCode,
            //    opt => opt.MapFrom(src => src.PolicyRequest.Code))
            //.ForMember(dest =>
            //        dest.AttachmentCode,
            //    opt => opt.MapFrom(src => src.Attachment.Code))
            ;


            // CreateMap<User, UserViewModel>();

            CreateMap<Company, PolicySupplementCompanyViewModel>();
            CreateMap<Address, PolicyRequestHolderPersonAddressViewModel>();
            CreateMap<PersonAddress, PersonAddressViewModel>();
            CreateMap<PersonAddress, AddressViewModel>();
            CreateMap<Address, AddressViewModel>();


            CreateMap<Person, PolicySupplementPersonViewModel>();
            CreateMap<PolicyRequestIssue, PolicyRequestIssueViewModel>()
                .ForMember(dest =>
                    dest.ReceiverAddressCode, opt => opt.MapFrom(src => src.ReceiverAddress.Code))
                .ForMember(dest =>
                    dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest => dest.ReceiveDate,
                    opt => opt.MapFrom(src =>
                        src.ReceiveDate != null ? src.ReceiveDate.GetValueOrDefault().ToString("yyyy/MM/dd") : null));


            CreateMap<Province, ProvinceResultViewModel>();
            CreateMap<Province, ProvinceInputViewModel>();
            CreateMap<Cityy, CityResultViewModel>()
                .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.TownShip.ProvinceId));

            CreateMap<PolicyRequestFactor, PolicyRequestPaymentInfoViewModel>()
                .ForMember(dest =>
                    dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest =>
                    dest.InsurancePrice, opt =>
                    opt.MapFrom(src => src.Payment.Price))
                .ForMember(dest =>
                    dest.TotalPice, opt =>
                    opt.MapFrom(src => src.Payment.Price))
                .ForMember(dest =>
                    dest.DriverDiscountId, opt =>
                    opt.MapFrom(src =>
                        src.PolicyRequest.PolicyRequestDetails.Where(x => x.Field == "DriverDiscountId").First().Value))
                .ForMember(dest =>
                    dest.ThirdDiscountId, opt =>
                    opt.MapFrom(src =>
                        src.PolicyRequest.PolicyRequestDetails.Where(x => x.Field == "ThirdDiscountId").First().Value))
                // .ForMember(dest =>
                //     dest.VehicleConstructionYear, opt => 
                //         opt.MapFrom(src => src.PolicyRequest.PolicyRequestDetails.Where(x=>x.Field=="VehicleConstructionYear").First().Value))
                .ForMember(dest =>
                    dest.OldInsurerStartDate, opt =>
                    opt.MapFrom(src =>
                        src.PolicyRequest.PolicyRequestDetails.Where(x => x.Field == "OldInsurerStartDate").First()
                            .Value))
                .ForMember(dest =>
                    dest.OldInsurerExpireDate, opt =>
                    opt.MapFrom(src =>
                        src.PolicyRequest.PolicyRequestDetails.Where(x => x.Field == "OldInsurerExpireDate").First()
                            .Value))
                .ForMember(dest =>
                    dest.VehicleFullName, opt =>
                    opt.MapFrom(src =>
                        src.PolicyRequest.InsuredRequests.First().InsuredRequestVehicles.First().Vehicle.Name))
                .ForMember(dest =>
                    dest.Insurance, opt => opt.MapFrom(src => src.PolicyRequest.Insurer.Insurance))
                ;

            CreateMap<DAL.Models.PolicyRequest, MyPolicyRequestViewModel>()
                .ForMember(dest => dest.insurance,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Insurer != null));
                        opt.MapFrom(src => src.Insurer.Insurance);
                    }
                )
                .ForMember(dest => dest.Company,
                    opt =>
                    {
                        opt.PreCondition(src => (src.Insurer != null));
                        opt.MapFrom(src => src.Insurer.Company);
                    }
                )
                .ForMember(dest => dest.Vehicle,
                    opt =>
                    {
                        opt.PreCondition(src =>
                            (src.InsuredRequests.Count > 0 &&
                             src.InsuredRequests.First().InsuredRequestVehicles.Count > 0));
                        opt.MapFrom(src => src.InsuredRequests.First().InsuredRequestVehicles.First().Vehicle);
                    });


            ;
            CreateMap<Person, MyPersonViewModel>()
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.JobName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Email))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault().Address.Description))
                .ForMember(dest => dest.City,
                    opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault().Address.City))
                .ForMember(dest => dest.Province,
                    opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault().Address.City.TownShip.Province))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Users.First().Username))
                ;

            CreateMap<Person, PersonUpdateViewModel>()
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.JobName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
                .ForMember(dest => dest.PersonAddress, opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault()))
                ;
            CreateMap<Person, MyPersonUpdateViewModel>()
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.JobName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Email))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault().Address.Name))
                .ForMember(dest => dest.CityId,
                    opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault().Address.City.Id))
                ;


            CreateMap<Reminder, ReminderInputViewModel>();
            CreateMap<Reminder, ReminderResultViewModel>()
                .ForMember(dest => dest.ReminderPeriod, opt => opt.MapFrom(src => src.ReminderPeriod))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Insurance, opt => opt.MapFrom(src => src.Insurance))
                ;
            CreateMap<ReminderPeriod, ReminderPeriodResultViewModel>();
            CreateMap<ReminderPeriod, ReminderPeriodInputViewModel>();


            CreateMap<DAL.Models.PolicyRequest, PolicyRequestByProvinceViewModel>()
                .ForMember(dest => dest.Insurer,
                    opt => opt.MapFrom(src => src.Insurer))
                .ForMember(dest => dest.Vehicle,
                    opt => opt.MapFrom(
                        src => src.InsuredRequests.First().InsuredRequestVehicles.First().Vehicle))
                .ForMember(dest => dest.RequestPerson, opt => opt.MapFrom(src => src.RequestPerson));


            CreateMap<Person, PersonViewModel>();
            CreateMap<Insurer, InsurerByProvinceViewModel>();
            CreateMap<Insurance, InsuranceByProvinceViewModel>();


            CreateMap<Person, AgentPersonViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));
            CreateMap<User, AgentUserViewModel>()
                .ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.Person.Code));
            CreateMap<Company, AgentCompanyViewModel>();
            CreateMap<DAL.Models.City, AgentCityViewModel>();
            CreateMap<TownShip, AgentTownShipViewModel>();
            CreateMap<Province, AgentProvinceViewModel>();
            CreateMap<CompanyAgent, AgentViewModel>();


            CreateMap<InsurerTerm, InsurerTermResultViewModel>();
            CreateMap<Company, InsurerResultCompanyViewModel>();
            CreateMap<Insurance, InsurerResultInsuranceViewModel>();
            CreateMap<Insurer, InsurerInsurerTermResultViewModel>();
            CreateMap<InsurerTerm, InsurerTermDetailedResultViewModel>();

            CreateMap<Province, CenterProvinceResultViewModel>();
            CreateMap<TownShip, CenterTownShipResultViewModel>();
            CreateMap<DAL.Models.City, CenterCityViewModel>();
            CreateMap<Company, CenterCompanyResultViewModel>();
            CreateMap<CompanyCenterSchedule, CenterScheduleResultViewModel>();
            CreateMap<DAL.Models.CompanyCenter, CompanyCenterResultViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Company.Code));
            CreateMap<CenterScheduleInputViewModel, CompanyCenterSchedule>();
            CreateMap<CompanyCenterInputViewModel, DAL.Models.CompanyCenter>();


            CreateMap<Company, InsurerCompanyResultViewModel>();
            CreateMap<Insurance, InsurerInsuranceResultViewModel>();
            CreateMap<Insurer, InsurerResultViewModel>()
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Company.Code));


            //CreateMap<CompanyAgent, AgentViewModel>()
            //    .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
            //    .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

            CreateMap<Insurance, DiscountInsuranceResultViewModel>();
            CreateMap<Person, DiscountPersonResultViewModel>();
            CreateMap<Discount, DiscountResultViewModel>();
            CreateMap<DiscountInputViewModel, Discount>();


            CreateMap<Company, InsuranceCompanyViewModel>();
            CreateMap<Insurer, InsuranceCompanyViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Company.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Company.Description))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Company.AvatarUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Company.Name))
                ;

            CreateMap<Insurance, InsuranceDetailsViewModel>()
                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Insurers))
                .ForMember(dest => dest.FAQ, opt => opt.MapFrom(src => src.InsuranceFaqs))
                ;


            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<PageAbleResult, PageAbleModel>();
            CreateMap<ArticleMetaKey, ArticleMetaKeyOutputViewModel>();
            CreateMap<DAL.Models.PolicyRequestComment, PolicyRequestCommentOutputViewModel>();

            CreateMap<ArticleSection, ArticleResultViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Article.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Article.Title))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Article.Summary))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Article.Description))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Article.Priority))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Article.Slug))
                .ForMember(dest => dest.IsActivated, opt => opt.MapFrom(src => src.Article.IsActivated))
                .ForMember(dest => dest.IsArchived, opt => opt.MapFrom(src => src.Article.IsArchived))
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.Article.CreatedDateTime))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Article.IsDeleted))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Article.AuthorId))
                .ForMember(dest => dest.ArticleMetaKeys, opt => opt.MapFrom(src => src.Article.ArticleMetaKeys))
                ;


            CreateMap<Article, CommentArticleResultViewModel>();
            CreateMap<Person, CommentAuthorResutlVeiwModel>();
            CreateMap<Comment, CommentResultViewModel>();
           
            CreateMap<ArticleSection, ArticleSectionViewModel>();
            CreateMap<InfoInputViewModel, DAL.Models.Info>();
            CreateMap<DAL.Models.Info, InfoResultViewModel>();
     
            CreateMap<Person, ArticleAuthorViewModel>();
            CreateMap<Article, ArticlesViewModel>();
            CreateMap<Company, CompanyDetailViewModel>();

            CreateMap<Insurance, FAQInsuranceResultViewModel>();
            CreateMap<InsuranceFaq, FAQResultViewModel>();
            CreateMap<FAQInputViewModel, InsuranceFaq>();

            CreateMap<Article, CategoryArticleResutlViewModel>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName));
            CreateMap<Category, CategoryResultViewModel>()
                .ForMember(dest => dest.ArticleCategories, opt => opt.MapFrom(src => src.ArticleCategories.Select(x => x.Article).ToList()));
            CreateMap<CategoryInputViewModel, Category>();

            CreateMap<ArticleMetaKey, ArticleLatestMetaKeyViewModel>();
            CreateMap<ArticleCategory, ArticleLatestCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Category.Slug))
                ;
            CreateMap<Article, ArticleLatestResultViewModel>();
            CreateMap<DAL.Models.Enumeration, BodyNoDamageDiscountYearOutPutViewModel>();
            CreateMap<DAL.Models.Enumeration, ThirdInsuranceCreditMonthViewModel>();
            CreateMap<DAL.Models.Enumeration, ThirdMaxFinancialCoverViewModel>();
            CreateMap<ProductRequestViewModel, ThirdProductInputViewModel>();
            CreateMap<ProductRequestViewModel, BodyProductInputViewModel>();
            CreateMap<PolicyRequestInputViewModel, ThirdProductInputViewModel>();
            CreateMap<PolicyRequestInputViewModel, ProductRequestViewModel>();
            // CreateMap<ThirdProductInputViewModel, ProductRequestViewModel>();
            CreateMap<InsuranceFaq, InsuranceFAQViewModel>();
            CreateMap<DAL.Models.CompanyCenter, CenterResultViewModel>();

            CreateMap<BodyIssueSupplementInfoViewModel, PersonViewModel>();
            CreateMap<BodySupplementAddressViewModel, AddressViewModel>();
            CreateMap<Person, BodyOwnerSupplementViewModel>();
            CreateMap<Person, BodyIssueSupplementInfoViewModel>();
            CreateMap<Address, BodySupplementAddressViewModel>();
            CreateMap<PolicyRequestHolder, BodySupplementInfoViewModel>()
                .ForPath(dest =>
                    dest.IssuedPerson.CityId, opt => opt.MapFrom(src => src.Address.CityId))
                .ForPath(dest =>
                    dest.IssuedPerson.Description, opt => opt.MapFrom(src => src.Address.Description))
                .ForPath(dest =>
                    dest.IssuedPerson.Mobile, opt => opt.MapFrom(src => src.Address.Mobile))
                .ForMember(dest =>
                    dest.IssuedPerson, opt => opt.MapFrom(src => src.Person))
                .ForMember(dest =>
                        dest.OwnerPerson,
                    opt => opt.MapFrom(src =>
                        src.PolicyRequest.InsuredRequests.First().InsuredRequestVehicles.First().OwnerPerson))
                .ForPath(dest =>
                    dest.OwnerPerson.CityId, opt => opt.MapFrom(src => src.Address.CityId))
                .ForPath(dest =>
                    dest.OwnerPerson.Description, opt => opt.MapFrom(src => src.Address.Description))
                ;


            CreateMap<InspectionSession, InspectionSessionViewModel>();
            CreateMap<CompanyCenterSchedule, CenterScheduleViewModel>();
            CreateMap<DAL.Models.CompanyCenter, CompanyCenterViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Company.Code));

            CreateMap<PolicyRequestInspection, PolicyRequestInspectionResultViewModel>();


            CreateMap<DAL.Models.CompanyCenter, PolicyRequestCenterViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Company.Code));

            CreateMap<CompanyCenterSchedule, PolicyRequestCenterScheduleViewModel>();
            CreateMap<Address, PolicyRequestAddressViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestForInspectionViewModel>()
                .ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.RequestPerson.Code));

            CreateMap<PolicyRequestInspection, PolicyRequestInspectionResultViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code))
                .ForMember(dest => dest.AddressCode, opt => opt.MapFrom(src => src.InspectionAddress.Code))
                .ForMember(dest => dest.InspectionSessionDate,
                    opt => opt.MapFrom(src =>
                        DateTime.Parse(src.InspectionSessionDate.ToString()).ToString("yyyy/MM/dd")));
            CreateMap<DAL.Models.PolicyRequestComment, PolicyRequestCommentGetAllOutputViewModel>()
                .ForMember(dest => dest.createdDateTime,
                    opt => opt.MapFrom(src =>
                        src.CreatedDateTime != null
                            ? src.CreatedDateTime.GetValueOrDefault().ToString("yyyy/MM/dd")
                            : null))
                .ForMember(dest => dest.JalaliDate,
                    opt => opt.MapFrom(src =>
                        src.CreatedDateTime != null
                            ? new PersianDateTime((DateTime)src.CreatedDateTime).ToString("yyyy/MM/dd")
                            : null))
                .ForMember(dest => dest.Attachments,
                    opt => opt.MapFrom(src =>
                        src.PolicyRequestCommentAttachments))
                ;


            CreateMap<IssueSession, IssueSessionsViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestSummaryOutputViewModel>();
            CreateMap<DAL.Models.Attachment, PolicyRequestCommentAttachmentViewModel>()
                .ForMember(dest => dest.AttachmentCode,
                    opt => opt.MapFrom(src => src.Code.ToString() + src.Extension.ToString()))
                .ForMember(dest => dest.Code,
                    opt => opt.MapFrom(src => src.Code.ToString()));

            CreateMap<PolicyRequestCommentAttachment, PolicyRequestCommentAttachmentViewModel>()
                .ForMember(dest => dest.AttachmentCode,
                    opt => opt.MapFrom(src => src.Attachment.Code.ToString() + src.Attachment.Extension.ToString()))
                .ForMember(dest => dest.Code,
                    opt => opt.MapFrom(src => src.Attachment.Code));


            CreateMap<HandledErrorLog, HandledErrorLogOutputViewModel>();
            CreateMap<OperationLog, OprationLogOutputViewModel>();
            CreateMap<SystemErrorLog, SystemErrorLogOutputViewModel>();
            CreateMap<DAL.Models.Person, PersonInfoViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestAgetSelectUpdateOutputViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestAgetSelectGetViewModel>()
                .ForMember(dest => dest.CompanyCode, option => option.MapFrom(src => src.Insurer.Company.Code));
       
            CreateMap<DAL.Models.ContactUs, ContactUsFrontResultViewModel>();
            CreateMap<DAL.Models.Menu, AccessMenuViewModel>();
            CreateMap<DAL.Models.Faq, PublicFaqResultViewModel>();
            CreateMap<PublicFaqInputViewModel, PublicFaqResultViewModel>();
            CreateMap<Attachment, AttachmentInputViewModel>();
            CreateMap<Person, CustomerPersonViewModel>();
            CreateMap<DAL.Models.PolicyRequest, CustomerViewModel>();
            CreateMap<DAL.Models.PersonCompany, PersonCompanyDTOViewModel>();

            CreateMap<DAL.Models.PolicyRequestStatus, CompanyPolicyStatusViewModel>();
            CreateMap<Insurance, CompanyRequestInsuranceViewModel>();
            CreateMap<Person, RequestPersonVeiwModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));
            CreateMap<Insurer, CompanyInsurerViewModel>()
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Company.Code));
            CreateMap<DAL.Models.PolicyRequest, CompanyPolicyRequestViewModel>()
                .ForMember(dest => dest.RequestPersonCode, opt => opt.MapFrom(src => src.RequestPerson.Code))
                .ForMember(dest => dest.ReviewerCode, opt => opt.MapFrom(src => src.Reviewer.Code))
                .ForMember(dest => dest.AgentSelectedCode, opt => opt.MapFrom(src => src.AgentSelected.Person.Code));


            CreateMap<InsurerTermDetail, TermDetailResultViewModel>();
            CreateMap<TermDetailInputViewModel, InsurerTermDetail>();

            CreateMap<InsuranceField, InsuranceFieldViewModel>();
            CreateMap<InsuranceTermType, InsuranceTermTypeViewModel>();

            CreateMap<InsurerTermInputViewModel, InsurerTerm>();

            CreateMap<PolicyRequestDetail, CompanyPolicyDetailViewModel>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PolicyRequest.Code));

            CreateMap<InsuranceFrontTab, InsuranceFrontTabViewModel>();
            CreateMap<Insurance, InsuranceMineViewModel>();
            CreateMap<Insurer, InsurerMineViewModel>();
            CreateMap<DAL.Models.PolicyRequest, PolicyRequestMineViewModel>();

            CreateMap<User, PersonUserResultViewModel>()
                .ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.Person.Code));

            CreateMap<PersonCompany, CompanyForPersonResultViewModel>()
                .ForMember(dest => dest.PesonCode, opt => opt.MapFrom(src => src.Person.Code))
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Company.Code));

            CreateMap<PersonAddress, AddressForPersonViewModel>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Address.CityId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Address.Description));

            CreateMap<Role, PersonRoleResultViewModel>();
            CreateMap<Role, RoleResultViewModel>();
            CreateMap<Role, RoleInputViewModel>();
            CreateMap<RolePermission, RolePermissionResultViewModel>();
            CreateMap<RolePermission, RolePermissionInputViewModel>();

            CreateMap<Person, PersonResultViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Users.FirstOrDefault().UserRoles.FirstOrDefault().Role))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Users.FirstOrDefault()))
                .ForMember(dest => dest.PersonCompany, opt => opt.MapFrom(src => src.PersonCompanies.FirstOrDefault()))
                // .ForMember(dest => dest.AgentId, opt 
                //     => opt.MapFrom(src => src.PersonCompanies.FirstOrDefault()?.Company.CompanyAgents.FirstOrDefault()?.Id))
                .ForMember(dest => dest.PersonAddress, opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault()));

            CreateMap<Person, UserPersonViewModel>();
            CreateMap<User, UserResultViewModel>()
                .ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.Person.Code))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role));

            CreateMap<DAL.Models.City, PersonCityViewModel>();

            CreateMap<Person, PersonResultWithAgentCompanyViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Users.FirstOrDefault().UserRoles.FirstOrDefault().Role))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Users.FirstOrDefault()))
                .ForMember(dest => dest.PersonCompany, opt => opt.MapFrom(src => src.PersonCompanies.FirstOrDefault()))
                .ForMember(dest => dest.PersonAddress, opt => opt.MapFrom(src => src.PersonAddresses.FirstOrDefault()))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CompanyAgents.FirstOrDefault().City));


            CreateMap<Person, CompanyUserResultViewModel>()
                .ForMember(dest => dest.UserCode, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Code))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Email));

            CreateMap<User, UpdatedUserResultViewModel>();

            CreateMap<User, CompanySingleUserResultViewModel>()
                .ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.Person.Code))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName));
            CreateMap<Menu, MenuTreeItemViewModel>();

            CreateMap<VehicleRuleCategory, VehicleRuleCategoryResultViewModel>();

            CreateMap<SchemaVersion, ShemaVersionViewModel>()
                .ForMember(dest => dest.Applied, opt => opt.MapFrom(src => src.Applied.ToString()));

            CreateMap<Insurance, InsuranceForInsuranceCentraulRuleResultViewModel>();

            CreateMap<InsuranceCentralRule, InsuranceCentralRuleResultViewModel>()
                .ForMember(dest => dest.Insurance, opt => opt.MapFrom(src => src.CentralRuleType.InsuranceField.Insurance));

            CreateMap<EnumerationInputViewModel, Enumeration>();
            CreateMap<Enumeration, EnumerationResultViewModel>();

            CreateMap<Insurer, ProductInsurerViewModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Company.AvatarUrl));

            CreateMap<InsuranceTermType, ProductInsuranceTermTypeViewModel>();

            CreateMap<InsurerTermDetail, ProductInsurerDetailViewModel>();

            CreateMap<InsurerTerm, ProductInsurerTermViewModel>();

            CreateMap<CentralRuleType, ProductCentralRuleTypeViewModel>();

            CreateMap<InsuranceCentralRule, ProductCentralRuleViewModel>();

            CreateMap<CentralRuleType, CentralRuleTypeViewModel>();


            CreateMap<Attachment, AttachmentResultViewModel>()
                .ForMember(dest => dest.AttachmentCode,
                    opt => opt.MapFrom(src => src.Code.ToString() + src.Extension.ToString()));

            CreateMap<Person, CompanyCommentAuthorViewModel>();

            CreateMap<DAL.Models.PolicyRequest, CompanyCommentPolicyRequestViewModel>()
                .ForMember(dest => dest.RequestPersonCode, opt => opt.MapFrom(src => src.RequestPerson.Code));

            CreateMap<Attachment, CompanyAttachmentViewModel>();

            CreateMap<PolicyRequestCommentAttachment, CompanyCommentAttachmentResultViewModel>()
                .ForMember(dest => dest.AttachmentCode, opt => opt.MapFrom(src => src.Attachment.Code));

            CreateMap<DAL.Models.PolicyRequestComment, CompanyCommentResultViewModel>()
                .ForMember(dest => dest.PolicyCode, opt => opt.MapFrom(src => src.PolicyRequest.Code));

            CreateMap<Company, CompanyOfAgentViewModel>();

            CreateMap<Province, CompanyAgentProvinceViewModel>();

            CreateMap<TownShip, CompanyAgentTownShipViewModel>();

            CreateMap<DAL.Models.City, CompanyAgentCityViewModel>();

            CreateMap<User, CompanyAgentUserViewModel>().ForMember(dest => dest.PersonCode, opt => opt.MapFrom(src => src.Person.Code));

            CreateMap<Person, CompanyAgentPersonViewModel>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Users.FirstOrDefault()));

            CreateMap<CompanyAgent, CompanyAgentViewModel>();
            CreateMap<ResourceOperation, ResourceOperationViewModel>();
            CreateMap<Permission, PermissionResultViewModel>();
            CreateMap<Resource, ResourceViewModel>();
            CreateMap<ContactUs, ContactUsDashboardResultViewModel>();


            CreateMap<Person, CompanyBodyOwnerSupplementViewModel>();

            CreateMap<PolicyRequestHolder, CompanyBodySupplementInfoViewModel>()
                .ForPath(dest =>
                    dest.IssuedPerson.CityId, opt => opt.MapFrom(src => src.Address.CityId))
                .ForPath(dest =>
                    dest.IssuedPerson.Description, opt => opt.MapFrom(src => src.Address.Description))
                .ForPath(dest =>
                    dest.IssuedPerson.Mobile, opt => opt.MapFrom(src => src.Address.Mobile))
                .ForMember(dest =>
                    dest.IssuedPerson, opt => opt.MapFrom(src => src.Person))
                .ForMember(dest =>
                        dest.OwnerPerson,
                    opt => opt.MapFrom(src =>
                        src.PolicyRequest.InsuredRequests.First().InsuredRequestVehicles.First().OwnerPerson))
                .ForPath(dest =>
                    dest.OwnerPerson.CityId, opt => opt.MapFrom(src => src.Address.CityId))
                .ForPath(dest =>
                    dest.OwnerPerson.Description, opt => opt.MapFrom(src => src.Address.Description))
                ;

            CreateMap<PolicyRequestFactor, CompanyPolicyRequestFactorResultViewModel>();
            CreateMap<PolicyRequestFactor, FactorViewModel>();

            CreateMap<DAL.Models.PolicyRequest, CompanyPolicyRequestResultViewModel>()
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Insurer.Company.Code))
                .ForMember(dest => dest.RequestPersonCode, opt => opt.MapFrom(src => src.RequestPerson.Code));

            CreateMap<Payment, CompanyPaymentResultViewModel>()
                .ForMember(dest => dest.PaymentSatus, opt => opt.MapFrom(src => src.PaymentStatus.Name));

            CreateMap<PolicyRequestFactorDetail, CompanyFactorDetailResultViewModel>();

            CreateMap<PolicyRequestFactor, CompanyPolicyRequestFactorResultViewModel>();


            CreateMap<PolicyRequestFactor, PolicyFactorResultViewModel>();


            CreateMap<Payment, PaymentResultViewModel>()
                .ForMember(dest => dest.PaymentSatus, opt => opt.MapFrom(src => src.PaymentStatus.Name));
            CreateMap<Payment, PaymentResultWithAllRelationViewModel>()
                            .ForMember(dest => dest.PaymentSatus, opt => opt.MapFrom(src => src.PaymentStatus.Name));

            CreateMap<Company, PaymentCompanyViewModel>();
            CreateMap<PaymentGateway, PaymentGatewayViewModel>();

            CreateMap<DAL.Models.PolicyRequest, PaymentPolicyRequestViewModel>()
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Insurer.Company.Code))
                .ForMember(dest => dest.RequestPersonCode, opt => opt.MapFrom(src => src.RequestPerson.Code))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Insurer.Company));

            CreateMap<PolicyRequestFactor, PaymentFactorViewModel>();

            CreateMap<Payment, CompanyPaymentViewModel>()
                            .ForMember(dest => dest.PaymentSatus, opt => opt.MapFrom(src => src.PaymentStatus.Name));

            CreateMap<PolicyRequestFactor, CompanyFactorViewModel>();
            CreateMap<PaymentStatus, PaymentStatusResultViewModel>();
            CreateMap<PaymentGateway, GetewayResultViewModel>();
            CreateMap<GatewayInputViewModel, PaymentGateway>();
            CreateMap<GetewayDetailInputViewModel, PaymentGatewayDetail>();
            CreateMap<PaymentGatewayDetail, PaymentGatewayDetailResultViewModel>();
            CreateMap<GatewayUpdateInputViewModel, PaymentGateway>();
            CreateMap<GatewayDetailUpdateInputViewModel, PaymentGatewayDetail>();
            CreateMap<PaymentStatusInputViewModel, PaymentStatus>();
            
            
            CreateMap<TownShip, TownshipInputViewModel>();
            CreateMap<TownShip, TownshipResultViewModel>();
            CreateMap<Place, PlaceResultViewModel>();
            CreateMap<Place, PlaceInputViewModel>();
            CreateMap<PlaceAddress, PlaceAddressResultViewModel>();
            CreateMap<PlaceAddress, PlaceAddressInputViewModel>();
        }
    }
}