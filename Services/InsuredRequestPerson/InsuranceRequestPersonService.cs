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
    public class InsuredRequestPersonService : IInsuredRequestPersonService
    {
        private readonly IRepository<InsuredRequestPerson> _insuranceRepository;
        private readonly IPersonRepository _personRepository;
        private readonly PagingSettings _pagingSettings;

        public InsuredRequestPersonService(IRepository<InsuredRequestPerson> insuranceRepository, IPersonRepository personRepository, IOptionsSnapshot<PagingSettings> pagingSettings)
        {
            _insuranceRepository = insuranceRepository;
            _personRepository = personRepository;
            _pagingSettings = pagingSettings.Value;
        }



        public async Task<InsuredRequestPerson> Create(InsuranceRequestPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            var insurance = new InsuredRequestPerson
            {
                PersonId = person.Id,
                InsuredRequestId = insuranceViewModel.InsuredId,               

            };
            await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestPerson> Update(long id, InsuranceRequestPersonViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            Person person = await _personRepository.GetByCodeNoTracking(insuranceViewModel.Code, cancellationToken);

            insurance.InsuredRequestId = insuranceViewModel.InsuredId;
            insurance.PersonId = person.Id;
            await _insuranceRepository.UpdateAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _insuranceRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }

        public async Task<InsuredRequestPerson> Detail(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestPerson>> Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }
      
    }
}
