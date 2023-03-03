using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Settings;
using Models.Vehicle;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class VehicleService : IVehicleService
    {
        #region Fields 

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleBrandRepository _vehicleBrandRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;
        private readonly IRepository<VehicleDetail> _vehicleDetailRepository;
        private readonly IVehicleRuleCategoryRepository _vehicleRuleCategoryRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;
        #endregion

        #region CTOR

        public VehicleService(IVehicleRepository vehicleRepository, IRepository<VehicleType> vehicleTypeRepository, IVehicleBrandRepository vehicleBrandRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IMapper mapper, IRepository<VehicleDetail> vehicleDetailRepository, IVehicleRuleCategoryRepository vehicleRuleCategoryRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleBrandRepository = vehicleBrandRepository;
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
            _vehicleDetailRepository = vehicleDetailRepository;
            _vehicleRuleCategoryRepository = vehicleRuleCategoryRepository;
        }
        #endregion

        #region Get
        public async Task<List<VehicleResultViewModel>> GetVehiclesByBrandAsync(long vehicleBrandId, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetByVehicleBrandId(vehicleBrandId, cancellationToken);
            return _mapper.Map<List<VehicleResultViewModel>>(vehicles);
        }
    
        public async Task<List<VehicleTypeViewModel>> GetVehicleTypesAsync(CancellationToken cancellationToken)
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAllAsync(cancellationToken);
            var vehicletypeViewModel = _mapper.Map<List<VehicleTypeViewModel>>(vehicleTypes);
            return vehicletypeViewModel;
        }

        public async Task<List<VehicleDetail>> GetAllVehicleDetailsAsync(CancellationToken cancellationToken)
        {
            var vehicleDetails = await _vehicleDetailRepository.GetAllAsync(cancellationToken);
            return vehicleDetails;
        }
        public async Task<PagedResult<VehicleDetail>> GetVehicleDetainsAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var vehicleDetails = await _vehicleDetailRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return vehicleDetails;
        }
        public async Task<VehicleDetail> GetVehicleDetailAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleDetail = await _vehicleDetailRepository.GetByIdAsync(cancellationToken, id);
            if (vehicleDetail == null)
                throw new BadRequestException("جزئیات ماشین یافت نشد");

            return vehicleDetail;
        }
        
        public async Task<VehicleBrandResultViewModel> GetVehicleBrandAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleBrand = await _vehicleBrandRepository.GetByIdAsync(cancellationToken, id);
            if (vehicleBrand == null)
                throw new BadRequestException("برند ماشین یافت نشد");

           
            return _mapper.Map<VehicleBrandResultViewModel>(vehicleBrand);
        }
     
        public async Task<VehicleType> GetVehicleTypeAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(cancellationToken, id);
            if (vehicleType == null)
                
                throw new BadRequestException("نوع ماشین یافت نشد");

            return vehicleType;
        }
        public async Task<PagedResult<VehicleType>> GetVehicleTypesAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var vehicleTypes = await _vehicleTypeRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return vehicleTypes;
        }
        public async Task<List<VehicleTypeViewModel>> GetAllVehicleTypesAsync(CancellationToken cancellationToken)
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<VehicleTypeViewModel>>(vehicleTypes);
        }

        public async Task<List<VehicleRuleCategoryResultViewModel>> GetAllVehicleRuleCategoryAsync(CancellationToken cancellationToken)
        {
            List<VehicleRuleCategory> vehicleRuleCategories = await _vehicleRuleCategoryRepository.GetAllVehicleRuleCategories(cancellationToken);
            return _mapper.Map<List<VehicleRuleCategoryResultViewModel>>(vehicleRuleCategories);
        }
        #endregion

        #region Create
        public async Task<VehicleType> CreateVehicleTypeAsync(VehicleTypeViewModel vehicleTypeVM, CancellationToken cancellationToken)
        {
            VehicleType vehicleType = new()
            {
                Name = vehicleTypeVM.Name,
                Description = vehicleTypeVM.Description,
            };

            await _vehicleTypeRepository.AddAsync(vehicleType, cancellationToken);
            return vehicleType;
        }
        
      
        public async Task<VehicleDetail> CreateVehicleDetailAsync(VehicleDetailViewModel vehicleDetailViewModel, CancellationToken cancellationToken)
        {
            var vehicleIdIsValid = await _vehicleRepository.GetByIdAsync(cancellationToken, vehicleDetailViewModel.VehicleId) != null;
            if (!vehicleIdIsValid)
                throw new BadRequestException("ماشین یافت نشد");

            VehicleDetail vehicleDetail = new()
            {
                Description = vehicleDetailViewModel.Description,
                CreatedYear = vehicleDetailViewModel.CreatedYear,
                VehicleId = vehicleDetailViewModel.VehicleId
            };

            await _vehicleDetailRepository.AddAsync(vehicleDetail, cancellationToken);

            return vehicleDetail;
        }
        #endregion

        
        
        #region Update
        public async Task<VehicleType> UpdateVehicleTypeAsync(long id, VehicleTypeViewModel vehicleTypeViewModel, CancellationToken cancellationToken)
        {
            var vehicleType = _vehicleTypeRepository.GetById(id);
            if (vehicleType == null)
                throw new BadRequestException("نوع ماشین یافت نشد");

            vehicleType.Description = vehicleTypeViewModel.Description;
            vehicleType.Name = vehicleType.Name;

            await _vehicleTypeRepository.UpdateAsync(vehicleType, cancellationToken);

            return vehicleType;
        }
      
        public async Task<VehicleDetail> UpdateVehicleDetailAsync(long id, VehicleDetailViewModel vehicleDetailViewModel, CancellationToken cancellationToken)
        {
            var vehicleDetail = _vehicleDetailRepository.GetById(id);
            if (vehicleDetail == null)
                throw new BadRequestException("جزئیات ماشین یافت نشد");

            var vehicleIdIsValid = await _vehicleRepository.GetByIdAsync(cancellationToken, vehicleDetailViewModel.VehicleId) != null;
            if (!vehicleIdIsValid)
                throw new BadRequestException("ماشین یافت نشد");

            vehicleDetail.Description = vehicleDetailViewModel.Description;
            vehicleDetail.CreatedYear = vehicleDetail.CreatedYear;

            await _vehicleDetailRepository.UpdateAsync(vehicleDetail, cancellationToken);
            return vehicleDetail;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteVehicleTypeAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleType = _vehicleTypeRepository.GetById(id);
            if (vehicleType == null)
                throw new BadRequestException(" نوع ماشین یافت نشد");

            await _vehicleTypeRepository.DeleteAsync(vehicleType, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteVehicleAsync(long id, CancellationToken cancellationToken)
        {
            var vehicle = _vehicleRepository.GetById(id);
            if (vehicle == null)
                throw new BadRequestException("ماشین یافت نشد");

            await _vehicleRepository.DeleteAsync(vehicle, cancellationToken);

            return true;
        }
        public async Task<bool> DeleteVehicleDetailAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleDetail = _vehicleDetailRepository.GetById(id);
            if (vehicleDetail == null)
                throw new BadRequestException("جزئیات ماشین یافت نشد");

            await _vehicleDetailRepository.DeleteAsync(vehicleDetail, cancellationToken);
            return true;
        }

        #endregion
        
        
        
        
        
        
        #region vehicle
        public async Task<VehicleResultViewModel> GetVehicleAsync(long id, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(cancellationToken, id);
            if (vehicle == null)
                throw new BadRequestException("ماشین یافت نشد");

            return  _mapper.Map<VehicleResultViewModel>(vehicle);
        }
        public async Task<List<VehicleResultViewModel>> GetAllVehiclesAsync(CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken);
            return  _mapper.Map<List<VehicleResultViewModel>>(vehicles);
        }
        public async Task<PagedResult<VehicleResultViewModel>> GetVehiclesAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var vehicles = await _vehicleRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return  _mapper.Map<PagedResult<VehicleResultViewModel>>(vehicles);
        }
        public async Task<VehicleResultViewModel> UpdateVehicleAsync(long id, VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken)
        {
            var vehicle = _vehicleRepository.GetById(id);
            if (vehicle == null)
                throw new BadRequestException("ماشین یافت نشد");

            var vehicleBrandIdIsValid = await _vehicleBrandRepository.GetByIdAsync(cancellationToken, vehicleViewModel.VehicleBrandId) != null;
            if (!vehicleBrandIdIsValid)
                throw new BadRequestException("برند ماشین یافت نشد");

            vehicle.Name = vehicleViewModel.Name;
            vehicle.Description = vehicleViewModel.Description;
            vehicle.VehicleBrandId = vehicleViewModel.VehicleBrandId;

            await _vehicleRepository.UpdateAsync(vehicle, cancellationToken);

            return  _mapper.Map<VehicleResultViewModel>(vehicle);
        }
        public async Task<VehicleResultViewModel> CreateVehicleAsync(VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken)
        {
            var vehicleBrandIdIsValid = await _vehicleBrandRepository.GetByIdAsync(cancellationToken, vehicleViewModel.VehicleBrandId) != null;
            if (!vehicleBrandIdIsValid)
                throw new BadRequestException("برند ماشین یافت نشد");

            Vehicle vehicle = new()
            {
                Name = vehicleViewModel.Name,
                Description = vehicleViewModel.Description,
                VehicleBrandId = vehicleViewModel.VehicleBrandId
            };

            await _vehicleRepository.AddAsync(vehicle, cancellationToken);

            return  _mapper.Map<VehicleResultViewModel>(vehicle);
        }
        
        #endregion
        
        #region vehilce brand
        public async Task<PagedResult<VehicleBrandResultViewModel>> GetVehicleBrandsAsync(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var vehicleBrands = await _vehicleBrandRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<VehicleBrandResultViewModel>>(vehicleBrands);
        }
        public async Task<List<VehicleBrandResultViewModel>> GetVehicleBrandsByTypeAsync(long vehicleTypeId, CancellationToken cancellationToken)
        {
            var vehicleBrands = await _vehicleBrandRepository.GetByVehicleTypeId(vehicleTypeId, cancellationToken);
            return _mapper.Map<List<VehicleBrandResultViewModel>>(vehicleBrands);
        }
        public async Task<List<VehicleBrandResultViewModel>> GetAllVehicleBrandsAsync(CancellationToken cancellationToken)
        {
            var vehicleBrands = await _vehicleBrandRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<VehicleBrandResultViewModel>>(vehicleBrands);
        }

        
        public async Task<VehicleBrandResultViewModel> CreateVehicleBrandAsync(VehicleBrandInputViewModel vehicleBrandVM, CancellationToken cancellationToken)
        {
            var vehicleTypeIdIsValid = await _vehicleTypeRepository.GetByIdAsync(cancellationToken, vehicleBrandVM.VehicleTypeId) != null;
            if (!vehicleTypeIdIsValid)
                throw new BadRequestException("نوع ماشین یافت نشد");

            VehicleBrand vehicleBrand = new()
            {
                Description = vehicleBrandVM.Description,
             
                Name = vehicleBrandVM.Name,
                VehicleTypeId = vehicleBrandVM.VehicleTypeId
            };

            await _vehicleBrandRepository.AddAsync(vehicleBrand, cancellationToken);

            return _mapper.Map<VehicleBrandResultViewModel>(vehicleBrand);
        }
        public async Task<VehicleBrandResultViewModel> UpdateVehicleBrandAsync(long id, VehicleBrandInputViewModel vehicleBrandViewModel, CancellationToken cancellationToken)
        {
            var vehicleBrand = _vehicleBrandRepository.GetById(id);
            if (vehicleBrand == null)
                throw new BadRequestException("برند ماشین یافت نشد");

            var vehicleTypeIdIsValid = await _vehicleTypeRepository.GetByIdAsync(cancellationToken, vehicleBrandViewModel.VehicleTypeId) != null;
            if (!vehicleTypeIdIsValid)
                throw new BadRequestException("نوع ماشین یافت نشد");

            vehicleBrand.Name = vehicleBrandViewModel.Name;
            vehicleBrand.Description = vehicleBrandViewModel.Description;
            vehicleBrand.VehicleTypeId = vehicleBrandViewModel.VehicleTypeId;

            await _vehicleBrandRepository.UpdateAsync(vehicleBrand, cancellationToken);

            return _mapper.Map<VehicleBrandResultViewModel>(vehicleBrand);
        }
        public async Task<bool> DeleteVehicleBrandAsync(long id, CancellationToken cancellationToken)
        {
            var vehicleBrand = await _vehicleBrandRepository.GetByIdAsync(cancellationToken, id);
            if (vehicleBrand == null)
                throw new BadRequestException("برند ماشین یافت نشد");

            await _vehicleBrandRepository.DeleteAsync(vehicleBrand, cancellationToken);

            return true;
        }
        
        
        
        
        #endregion
        
    }
}
