using System.Collections.Generic;
using Common.Utilities;
using DAL.Models;
using Models.Insurance;
using Models.PageAble;
using System.Threading;
using System.Threading.Tasks;
using Insurancee = DAL.Models.Insurance;
using Models.InsuranceTermType;
using Models.CentralRuleType;

namespace Services
{
    public interface IInsuranceService
    {
        #region Create
        Task<InsuranceViewModel> CreateInsurance(InsuranceInputViewModel insuranceViewModel, CancellationToken cancellationToken);
        // Task<InsurerViewModel> CreateInsurerAsync(InsurerViewModel insurerViewModel, CancellationToken cancellationToken);
        #endregion

        #region Update
        Task<InsuranceViewModel> UpdateInsurance(long id, InsuranceInputViewModel insuranceViewModel, CancellationToken cancellationToken);
        // Task<InsurerViewModel> UpdateInsurerAsync(long id, InsurerViewModel insurerViewModel, CancellationToken cancellationToken);

        #endregion

        #region Delete
        Task<bool> DeleteInsurance(long id, CancellationToken cancellationToken);
        //Task<bool> DeleteInsurerAsync(long id, CancellationToken cancellationToken);

        #endregion

        #region Get
        Task<List<InsurerViewModel>> GetInsuranceInsurer(string slug, CancellationToken cancellationToken);
        Task<InsuranceViewModel> GetInsurance(long id, CancellationToken cancellationToken);
        // Task<PagedResult<InsuranceViewModel>> GetAllInsurances(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<InsuranceViewModel>> GetAllInsurances(CancellationToken cancellationToken);
        Task<List<InsuranceViewModel>> GetAllWithoutPaginate(CancellationToken cancellationToken);


        Task<InsurerViewModel> GetInsurerAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<InsurerViewModel>> GetAllInsurersAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<InsurerTermViewModel> CreateInsurerTermAsync(InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken);
        //Task<bool> DeleteInsurerTermAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<InsurerTermViewModel>> GetAllInsurerTermsAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<InsurerTerm> GetInsurerTermAsync(long id, CancellationToken cancellationToken);
        Task<InsurerTermViewModel> UpdateInsurerTermAsync(long id, InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken);

        Task<List<InsuranceDetailsViewModel>> GetInsuranceDetails(string slug, CancellationToken cancellationToken);


        Task<List<InsuranceTermTypeViewModel>> GetAllInsuranceTermTypes(long insuranceId, CancellationToken cancellationToken);
        Task<List<CentralRuleTypeViewModel>> GetAllCentralRuleTypes(long insuranceId, CancellationToken cancellationToken);
        #endregion
    }
}
