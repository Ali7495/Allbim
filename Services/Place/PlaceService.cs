using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Options;
using Models.InsuranceRequest;
using Models.Place;
using Models.Settings;
using Services.ViewModels;

namespace Services
{
    public class PlaceService : IPlaceService
    {
        private readonly PagingSettings _pagingSettings;

        private readonly IRepository<Place> _placeRepository;
        private readonly IRepository<PlaceAddress> _placeAddressRepository;
        private readonly IMapper _mapper;

        public PlaceService(IRepository<Place> placeRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IRepository<PlaceAddress> placeAddressRepository, IMapper mapper)
        {
            _mapper = mapper;
            _placeRepository = placeRepository;
            _pagingSettings = pagingSettings.Value;
            _placeAddressRepository= placeAddressRepository;
        }
      




        #region Place

          public async Task<PlaceResultViewModel> Create(PlaceInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var place = new Place
            {
                Description = viewModel.Description,
                Name=viewModel.Name
            };
            await _placeRepository.AddAsync(place, cancellationToken);
            return _mapper.Map<PlaceResultViewModel>(place) ;
        }

        public async Task<PlaceResultViewModel> Update(long id, PlaceInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _placeRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new NotFoundException("کد  وجود ندارد");

            model.Description = viewModel.Description;
            model.Name = viewModel.Name;
            await _placeRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<PlaceResultViewModel>(model) ;
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var place = await _placeRepository.GetByIdAsync(cancellationToken, id);
            if (place == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _placeRepository.DeleteAsync(place, cancellationToken);
            return true;
        }

        public async Task<PlaceResultViewModel> Detail(long id, CancellationToken cancellationToken)
        {
            var place = await _placeRepository.GetByIdAsync(cancellationToken, id);
            if (place == null)
                throw new NotFoundException("کد  وجود ندارد");
            return _mapper.Map<PlaceResultViewModel>(place) ;
        }

        public async Task<PagedResult<PlaceResultViewModel>> Get(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = await _placeRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<PlaceResultViewModel>>(companies) ;
        }

        #endregion






        #region Place Address

        public async Task<PlaceAddressResultViewModel> CreatePlaceAddress(PlaceAddressInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var place = new PlaceAddress
            {
                AddressId = viewModel.AddressId,
                PlaceId=viewModel.PlaceId
            };
            await _placeAddressRepository.AddAsync(place, cancellationToken);
            return _mapper.Map<PlaceAddressResultViewModel>(place);
        }

        public async Task<PlaceAddressResultViewModel> DetailPlaceAddress(long id, CancellationToken cancellationToken)
        {
            var place = await _placeAddressRepository.GetByIdAsync(cancellationToken, id);
            if (place == null)
                throw new NotFoundException("کد شرکت وجود ندارد");
            return _mapper.Map<PlaceAddressResultViewModel>(place);
        }

        public async  Task<PagedResult<PlaceAddressResultViewModel>> GetPlaceAddress(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var places = await _placeAddressRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<PlaceAddressResultViewModel>>(places);
        }

        public async Task<bool> DeletePlaceAddress(int id, CancellationToken cancellationToken)
        {
            var model = await _placeAddressRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new NotFoundException("کد  وجود ندارد");

            await _placeAddressRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<PlaceAddressResultViewModel> UpdatePlaceAddress(long id, PlaceAddressInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _placeAddressRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new NotFoundException("کد  وجود ندارد");

            model.AddressId = viewModel.AddressId;
            model.PlaceId = viewModel.PlaceId;
            await _placeAddressRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<PlaceAddressResultViewModel>(model);
        }


        

        #endregion
    }
}
