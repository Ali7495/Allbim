using Common.Utilities;
using DAL.Models;
using Models;
using Models.InsuranceRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuredRequestService
    {
        #region Create
        public Task<InsuredRequest> CreateInsuranceRequest(InsuranceRequestViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestCompany> CreateInsuranceRequestCompany(InsuranceRequestCompanyViewModel ViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestPerson> CreateInsuredRequestPerson(InsuranceRequestPersonViewModel ViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestPlace> CreateInsuredRequestPlace(InsuranceRequestPlaceViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestRelatedPerson> CreateInsuredRequestRelatedPerson(InsuredRequestRelatedPersonViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestVehicle> CreateInsuredRequestVehicle(InsuredRequestVehicleViewModel vehicleViewModel, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<PagedResult<InsuredRequest>> GetInsuranceRequest(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<PagedResult<InsuredRequestCompany>> GetInsuredRequestCompany(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<PagedResult<InsuredRequestPerson>> GetInsuredRequestPerson(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<PagedResult<InsuredRequestPlace>> GetInsuredRequestPlace(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<PagedResult<InsuredRequestRelatedPerson>> GetInsuredRequestRelatedPerson(int? page, int? pageSize, CancellationToken cancellationToken);
        #endregion

        #region Update
        public Task<InsuredRequest> UpdateInsuranceRequest(long id, InsuranceRequestViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestCompany> UpdateInsuredRequestCompany(long id, InsuranceRequestCompanyViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestPerson> UpdateInsuredRequestPerson(long id, InsuranceRequestPersonViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestPlace> UpdateInsuredRequestPlace(long id, InsuranceRequestPlaceViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<InsuredRequestRelatedPerson> UpdateInsuredRequestRelatedPerson(long id, InsuredRequestRelatedPersonViewModel cityViewModel, CancellationToken cancellationToken);

        public Task<InsuredRequestVehicle> UpdateInsuredRequestVehicle(long id, InsuredRequestVehicleViewModel vehicleViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        public Task<bool> DeleteInsuranceRequest(long id, CancellationToken cancellationToken);
        public Task<bool> DeleteInsuredRequestCompany(long id, CancellationToken cancellationToken);
        public Task<bool> DeleteInsuredRequestPerson(long id, CancellationToken cancellationToken);
        public Task<bool> DeleteInsuredRequestPlace(long id, CancellationToken cancellationToken);
        public Task<bool> DeleteInsuredRequestRelatedPerson(long id, CancellationToken cancellationToken);

        #endregion
        #region Detail
        Task<InsuredRequest> DetailInsuranceRequest(long code, CancellationToken cancellationToken);
        Task<InsuredRequestCompany> DetailInsuredRequestCompany(long code, CancellationToken cancellationToken);
        Task<InsuredRequestPerson> DetailInsuredRequestPerson(long code, CancellationToken cancellationToken);
        Task<InsuredRequestPlace> DetailInsuredRequestPlace(long code, CancellationToken cancellationToken);
        Task<InsuredRequestRelatedPerson> DetailInsuredRequestRelatedPerson(long code, CancellationToken cancellationToken);

        #endregion





    }
}
