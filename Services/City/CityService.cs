using Common.Exceptions;
using Common.Extensions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.City;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Models.Township;
using Models.Upload;
using Cityy = DAL.Models.City;

namespace Services.City
{
    public class CityService : ICityService
    {
        #region Fields
        private readonly ICityRepository _cityRepository;
        private readonly IRepository<TownShip> _townShipRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;
        
        #endregion

        #region CTOR
        public CityService(ICityRepository cityRepository, IRepository<TownShip> townShipRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IRepository<Province> provinceRepository,IMapper mapper)
        {
            _cityRepository = cityRepository;
            _townShipRepository = townShipRepository;
            _pagingSettings = pagingSettings.Value;
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }
        #endregion

        #region City
        public async Task<CityResultViewModel> CreateCityAsync(CityInputViewModel cityViewModel, CancellationToken cancellationToken)
        {
            var townShipIdIsValid = _townShipRepository.GetById(cityViewModel.TownShipId) != null;

            if (!townShipIdIsValid)
                throw new CustomException("NotFount");

            Cityy city = new()
            {
                // CreatedBy = cityViewModel.CreatedBy,
                Name = cityViewModel.Name,
                TownShipId = cityViewModel.TownShipId
            };

            await _cityRepository.AddAsync(city, cancellationToken);
            return _mapper.Map<CityResultViewModel>(city);
        }

      
        public async Task<CityResultViewModel> GetCityAsync(long id, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(cancellationToken, id);
            if (city == null)
                throw new CustomException("NotFount");

            return _mapper.Map<CityResultViewModel>(city);
        }
        public async Task<List<CityResultViewModel>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllWithProvince(cancellationToken);
            return _mapper.Map<List<CityResultViewModel>>(cities);
        }
        public async Task<PagedResult<CityResultViewModel>> GetCitiesAsync(int page, int? pageSize, string orderBy, CancellationToken cancellationToken)
        {
            // int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            PagedResult<Cityy> cities;

            if (string.IsNullOrEmpty(orderBy))
                cities = await _cityRepository.GetOrderedPagedAsync(page, pageSizeNotNull, orderBy, cancellationToken);
            else
                cities = await _cityRepository.GetPagedAsync(page, pageSizeNotNull, cancellationToken);

            return _mapper.Map<PagedResult<CityResultViewModel>>(cities);
        }


   
      
 
        public async Task<CityResultViewModel> UpdateCityAsync(long id, CityInputViewModel cityViewModel, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(cancellationToken, id);
            if (city == null)
                throw new CustomException("NotFount");

            var townShipIdIsValid = _townShipRepository.GetByIdAsync(cancellationToken, cityViewModel.TownShipId) != null;
            if (!townShipIdIsValid)
                throw new CustomException("NotFount");

            city.Name = cityViewModel.Name;
            city.UpdatedAt = DateTime.Now;
            // city.UpdatedBy = cityViewModel.UpdatedBy;
            city.TownShipId = cityViewModel.TownShipId;

            await _cityRepository.UpdateAsync(city, cancellationToken);
            return _mapper.Map<CityResultViewModel>(city);
        }

      
    
        public async Task<bool> DeleteCityAsync(long id, CancellationToken cancellationToken)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
                throw new CustomException("NotFount");

            await _cityRepository.DeleteAsync(city, cancellationToken);

            return true;
        }
  
       
        #endregion



        #region Province
        
        public async Task<ProvinceResultViewModel> CreateProvinceAsync(ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            Province province = new()
            {
                CreatedBy = provinceViewModel.CreatedBy,
                Name = provinceViewModel.Name
            };

            await _provinceRepository.AddAsync(province, cancellationToken);
            return _mapper.Map<ProvinceResultViewModel>(province);
        }
        
        public async Task<ProvinceResultViewModel> GetProvinceAsync(long id, CancellationToken cancellationToken)
        {
            var province = await _provinceRepository.GetByIdAsync(cancellationToken, id);
            if (province == null)
                throw new BadRequestException("استان یافت نشد");

            return _mapper.Map<ProvinceResultViewModel>(province);
        }
        public async Task<List<ProvinceResultViewModel>> GetAllProvincesAsync(CancellationToken cancellationToken)
        {
            var provinces = await _provinceRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ProvinceResultViewModel>>(provinces);
        }
        public async Task<PagedResult<ProvinceResultViewModel>> GetProvincesAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var provinces = await _provinceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<ProvinceResultViewModel>>(provinces);
        }
        public async Task<ProvinceResultViewModel> UpdateProvinceAsync(long id, ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            var province = await _provinceRepository.GetByIdAsync(cancellationToken, id);
            if (province == null)
                throw new BadRequestException("استان یافت نشد");

            province.Name = provinceViewModel.Name;
            province.UpdatedBy = provinceViewModel.UpdatedBy;
            province.UpdatedAt = DateTime.Now;

            return _mapper.Map<ProvinceResultViewModel>(province);
        }
        public async Task<bool> DeleteProvinceAsync(long id, CancellationToken cancellationToken)
        {
            var province = _provinceRepository.GetById(id);
            if (province == null)
                throw new BadRequestException("استان یافت نشد");

            await _provinceRepository.DeleteAsync(province, cancellationToken);

            return true;
        }
        

        #endregion


        #region Township
        public async Task<TownshipResultViewModel> GetTownShipDetailAsync(long id, CancellationToken cancellationToken)
        {
            var townShip = await _townShipRepository.GetByIdAsync(cancellationToken, id);
            if (townShip == null)
                throw new BadRequestException("استان یافت نشد");

            return _mapper.Map<TownshipResultViewModel>(townShip);
        }
        public async Task<List<TownshipResultViewModel>> GetTownShipListAsync(CancellationToken cancellationToken)
        {
            var townShips = await _townShipRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<TownshipResultViewModel>>(townShips);
        }
        public async Task<PagedResult<TownshipResultViewModel>> GetTownShipPagingAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var townShips = await _townShipRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<TownshipResultViewModel>>(townShips);
        }


        
        
        
        public async Task<TownshipResultViewModel> CreateTownShipAsync(TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var provinceIdIsValid = (await _provinceRepository.GetByIdAsync(cancellationToken, townShipViewModel.ProvinceId)) != null;
            if (!provinceIdIsValid)
                throw new CustomException("NotFount");

            TownShip townShip = new()
            {
                Name = townShipViewModel.Name,
                CreatedBy = townShipViewModel.CreatedBy,
                ProvinceId = townShipViewModel.ProvinceId
            };

            await _townShipRepository.AddAsync(townShip, cancellationToken);
            return _mapper.Map<TownshipResultViewModel>(townShip);
        }
        public async Task<TownshipResultViewModel> UpdateTownShipAsync(long id, TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var townShip = await _townShipRepository.GetByIdAsync(cancellationToken, id);
            if (townShip == null)
                throw new CustomException("NotFount");

            var provinceIdIsValid = await _provinceRepository.GetByIdAsync(cancellationToken, townShipViewModel.ProvinceId) != null;
            if (!provinceIdIsValid)
                throw new CustomException("NotFount");

            townShip.Name = townShipViewModel.Name;
            townShip.ProvinceId = townShipViewModel.ProvinceId;
            townShip.UpdatedAt = DateTime.Now;
            townShip.UpdatedBy = townShipViewModel.UpdatedBy;

            await _townShipRepository.UpdateAsync(townShip, cancellationToken);
            return _mapper.Map<TownshipResultViewModel>(townShip);

        }
        public async Task<bool> DeleteTownShipAsync(long id, CancellationToken cancellationToken)
        {
            var townShip = await _townShipRepository.GetByIdAsync(cancellationToken, id);
            if (townShip == null)
                throw new CustomException("NotFount");

            await _townShipRepository.DeleteAsync(townShip, cancellationToken);
            return true;
        }
        #endregion
    }
}
