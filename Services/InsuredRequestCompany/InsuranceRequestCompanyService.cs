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
using Models.InsuranceRequest;
using Models.Settings;
using Services.ViewModels;

namespace Services
{
    public class InsuredRequestCompanyService : IInsuredRequestCompanyService
    {
        private readonly IRepository<InsuredRequestCompany> _insuranceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly PagingSettings _pagingSettings;

        public InsuredRequestCompanyService(IRepository<InsuredRequestCompany> insuranceRepository, ICompanyRepository companyRepository, IOptionsSnapshot<PagingSettings> pagingSettings)
        {
            _insuranceRepository = insuranceRepository;
            _companyRepository = companyRepository;
            _pagingSettings = pagingSettings.Value;
        }



        public async Task<InsuredRequestCompany> Create(InsuranceRequestCompanyViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = new InsuredRequestCompany
            {
                //Name = insuranceViewModel.Name,
                //Description = insuranceViewModel.Description,

            };
            await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestCompany> Update(long id, InsuranceRequestCompanyViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");

            Company company = await _companyRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = insuranceViewModel.InsuredId;
            insurance.CompanyId = company.Id;
            await _insuranceRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");

            await _insuranceRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public  Task<InsuredRequestCompany> Detail(long id, CancellationToken cancellationToken)
        {
            var insurance =  _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");
            return insurance;
        }

        

        Task<PagedResult<InsuredRequestCompany>> IInsuredRequestCompanyService.Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }

        
    }
}
