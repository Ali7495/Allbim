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
    public class InsuredRequestRelatedPersonService : IInsuredRequestRelatedPersonService
    {
        private readonly IRepository<InsuredRequestRelatedPerson> _insuranceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly PagingSettings _pagingSettings;

        public InsuredRequestRelatedPersonService(
            IRepository<InsuredRequestRelatedPerson> insuranceRepository,
            IPersonRepository personRepository,
            IOptionsSnapshot<PagingSettings> pagingSettings
            
            )
        {
            _insuranceRepository = insuranceRepository;
            _pagingSettings = pagingSettings.Value;
            _personRepository = personRepository;
        }
        

        public async Task<InsuredRequestRelatedPerson> Create(InsuredRequestRelatedPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            var insurance = new InsuredRequestRelatedPerson
            {
                InsuredRequestId = insuranceViewModel.InsuredRequestId,
                PersonId = person.Id,

            };
            await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestRelatedPerson> Update(long id, InsuredRequestRelatedPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");

            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = insuranceViewModel.InsuredRequestId;
            insurance.PersonId = person.Id;
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

        public async Task<InsuredRequestRelatedPerson> Detail(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد شرکت وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestRelatedPerson>> Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }     
    }
}
