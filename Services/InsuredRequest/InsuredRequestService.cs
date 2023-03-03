using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Options;
using Models;
using Models.InsuranceRequest;
using Models.Settings;
using Services.ViewModels;

namespace Services
{
    public class InsuredRequestService : IInsuredRequestService
    {
        private readonly PagingSettings _pagingSettings;

        private readonly IRepository<InsuredRequest> _insuranceRepository;
        private readonly IRepository<InsuredRequestCompany> _insuranceCompanyRepository;
        private readonly IRepository<InsuredRequestPerson> _insurancePersonRepository;
        private readonly IRepository<InsuredRequestPlace> _insurancePlaceRepository;
        private readonly IRepository<InsuredRequestRelatedPerson> _insuranceRelatedPersonRepository;
        private readonly IRepository<InsuredRequestVehicle> _insuredRequestVehicleRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICompanyRepository _companyRepository;

        public InsuredRequestService(IRepository<InsuredRequest> insuranceRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IRepository<InsuredRequestCompany> insuranceCompanyRepository, IRepository<InsuredRequestPerson> insurancePersonRepository, IRepository<InsuredRequestPlace> insurancePlaceRepository,IRepository<InsuredRequestRelatedPerson> insuranceRelatedPersonRepository, IPolicyRequestRepository policyRequestRepository, IPersonRepository personRepository, ICompanyRepository companyRepository, IRepository<InsuredRequestVehicle> insuredRequestVehicleRepository)
        {
            _insuranceRelatedPersonRepository = insuranceRelatedPersonRepository;
            _insurancePlaceRepository = insurancePlaceRepository;
            _insurancePersonRepository = insurancePersonRepository;
            _insuranceRepository = insuranceRepository;
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _policyRequestRepository = policyRequestRepository;
            _personRepository = personRepository;
            _companyRepository = companyRepository;
            _insuredRequestVehicleRepository = insuredRequestVehicleRepository;
            _pagingSettings = pagingSettings.Value;
        }
        public async Task<InsuredRequest> CreateInsuranceRequest(InsuranceRequestViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = new InsuredRequest
            {
                PolicyRequestId = insuranceViewModel.PolicyRequestId,                
            };
            await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequest> UpdateInsuranceRequest(long id, InsuranceRequestViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            insurance.PolicyRequestId = insuranceViewModel.PolicyRequestId;
            await _insuranceRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> DeleteInsuranceRequest(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _insuranceRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public  Task<InsuredRequest> DetailInsuranceRequest(long id, CancellationToken cancellationToken)
        {
            var insurance =  _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequest>> GetInsuranceRequest(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }

        public async Task<InsuredRequestCompany> CreateInsuranceRequestCompany(InsuranceRequestCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(ViewModel.Code, cancellationToken);

            var insurance = new InsuredRequestCompany
            {
                InsuredRequestId = ViewModel.InsuredId,
                CompanyId = company.Id
            };
            await _insuranceCompanyRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<PagedResult<InsuredRequestCompany>> GetInsuredRequestCompany(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies =await _insuranceCompanyRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }

        public async Task<InsuredRequestCompany> UpdateInsuredRequestCompany(long id, InsuranceRequestCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");

            Company company = await _companyRepository.GetByCodeNoTracking(ViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = ViewModel.InsuredId;
            insurance.CompanyId = company.Id;
            await _insuranceCompanyRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> DeleteInsuredRequestCompany(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");

            await _insuranceCompanyRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public async Task<InsuredRequestCompany> DetailInsuredRequestCompany(long id, CancellationToken cancellationToken)
        {
            var insurance =await _insuranceCompanyRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");
            return insurance;
        }

        public async Task<InsuredRequestPerson> CreateInsuredRequestPerson( InsuranceRequestPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest policyRequest = await _policyRequestRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            var insurance = new InsuredRequestPerson
            {
                PersonId = policyRequest.RequestPersonId,
                InsuredRequestId = insuranceViewModel.InsuredId,

            };
            await _insurancePersonRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestPerson> UpdateInsuredRequestPerson(long id, InsuranceRequestPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = insuranceViewModel.InsuredId;
            insurance.PersonId = person.Id;
            await _insurancePersonRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> DeleteInsuredRequestPerson(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _insurancePersonRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public async Task<InsuredRequestPerson> DetailInsuredRequestPerson(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestPerson>> GetInsuredRequestPerson(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insurancePersonRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }
        public async Task<InsuredRequestPlace> CreateInsuredRequestPlace(InsuranceRequestPlaceViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = new InsuredRequestPlace
            {
                InsuredRequestId = insuranceViewModel.InsuredId,
                PlaceId = insuranceViewModel.PlaceId,

            };
            await _insurancePlaceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestPlace> UpdateInsuredRequestPlace(long id, InsuranceRequestPlaceViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePlaceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            insurance.InsuredRequestId = insuranceViewModel.InsuredId;
            insurance.PlaceId = insuranceViewModel.PlaceId;
            await _insurancePlaceRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> DeleteInsuredRequestPlace(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePlaceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _insurancePlaceRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public async Task<InsuredRequestPlace> DetailInsuredRequestPlace(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insurancePlaceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestPlace>> GetInsuredRequestPlace(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insurancePlaceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }
        public async Task<InsuredRequestRelatedPerson> CreateInsuredRequestRelatedPerson(InsuredRequestRelatedPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            var insurance = new InsuredRequestRelatedPerson
            {
                InsuredRequestId = insuranceViewModel.InsuredRequestId,
                PersonId = person.Id,

            };
            await _insuranceRelatedPersonRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestRelatedPerson> UpdateInsuredRequestRelatedPerson(long id, InsuredRequestRelatedPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRelatedPersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = insuranceViewModel.InsuredRequestId;
            insurance.PersonId = person.Id;
            await _insuranceRelatedPersonRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> DeleteInsuredRequestRelatedPerson(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRelatedPersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _insuranceRelatedPersonRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public async Task<InsuredRequestRelatedPerson> DetailInsuredRequestRelatedPerson(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRelatedPersonRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestRelatedPerson>> GetInsuredRequestRelatedPerson(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRelatedPersonRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }


        public async Task<InsuredRequestVehicle> CreateInsuredRequestVehicle(InsuredRequestVehicleViewModel insuredViewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(insuredViewModel.OwnerPersonCode.GetValueOrDefault(), cancellationToken);
            Company company = await _companyRepository.GetByCodeNoTracking(insuredViewModel.OwnerCompanyCode.GetValueOrDefault(), cancellationToken);

            var vehicle = new InsuredRequestVehicle
            {
                InsuredRequestId = insuredViewModel.InsuredRequestId,
                OwnerCompanyId = company.Id,
                OwnerPersonId = person.Id,
                VehicleId = insuredViewModel.VehicleId
            };
            await _insuredRequestVehicleRepository.AddAsync(vehicle, cancellationToken);
            return vehicle;
        }

        public async Task<InsuredRequestVehicle> UpdateInsuredRequestVehicle(long id, InsuredRequestVehicleViewModel vehicleViewModel, CancellationToken cancellationToken)
        {
            InsuredRequestVehicle vehicle = await _insuredRequestVehicleRepository.GetByIdAsync(cancellationToken, id);
            if (vehicle == null)
                throw new NotFoundException("کد  وجود ندارد");

            Person person = await _personRepository.GetByCodeNoTracking(vehicleViewModel.OwnerPersonCode.GetValueOrDefault(), cancellationToken);
            Company company = await _companyRepository.GetByCodeNoTracking(vehicleViewModel.OwnerCompanyCode.GetValueOrDefault(), cancellationToken);

            vehicle.InsuredRequestId = vehicleViewModel.InsuredRequestId;
            vehicle.OwnerPersonId = person.Id;
            vehicle.OwnerCompanyId = company.Id;
            vehicle.VehicleId = vehicleViewModel.VehicleId;

            await _insuredRequestVehicleRepository.UpdateAsync(vehicle, cancellationToken);
            return vehicle;
        }
    }
}
