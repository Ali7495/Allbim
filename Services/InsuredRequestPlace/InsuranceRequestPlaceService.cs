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
    public class InsuredRequestPlaceService : IInsuredRequestPlaceService
    {
        private readonly IRepository<InsuredRequestPlace> _insuranceRepository;
        private readonly PagingSettings _pagingSettings;

        public InsuredRequestPlaceService(IRepository<InsuredRequestPlace> insuranceRepository, IOptionsSnapshot<PagingSettings> pagingSettings)
        {
            _insuranceRepository = insuranceRepository;
            _pagingSettings = pagingSettings.Value;
        }



        public async Task<InsuredRequestPlace> Create(InsuranceRequestPlaceViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = new InsuredRequestPlace
            {
                InsuredRequestId = insuranceViewModel.InsuredId,
                PlaceId = insuranceViewModel.PlaceId,

            };
            await _insuranceRepository.AddAsync(insurance, cancellationToken);
            return insurance;
        }

        public async Task<InsuredRequestPlace> Update(long id, InsuranceRequestPlaceViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");

            insurance.InsuredRequestId = insuranceViewModel.InsuredId;
            insurance.PlaceId = insuranceViewModel.PlaceId;
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

        public async Task<InsuredRequestPlace> Detail(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new NotFoundException("کد  وجود ندارد");
            return insurance;
        }

        public Task<PagedResult<InsuredRequestPlace>> Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }

       
    }
}
