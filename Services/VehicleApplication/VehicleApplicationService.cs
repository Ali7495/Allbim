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
    public class VehicleApplicationService : IVehicleApplicationService
    {
        #region Fields 

        private readonly IRepository<VehicleApplication> _vehicleApplicationRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;
        #endregion

        #region CTOR

        public VehicleApplicationService(IRepository<VehicleApplication> vehicleApplicationRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IMapper mapper)
        {
            _vehicleApplicationRepository = vehicleApplicationRepository;
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
        }

        public async Task<VehicleApplicationResultViewModel> Create(VehicleApplicationInputViewModel ViewModel, CancellationToken cancellationToken)
        {
            VehicleApplication model = new()
            {
                Name = ViewModel.Name,
                VehicleTypeId = ViewModel.VehicleTypeId,
            };

            await _vehicleApplicationRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<VehicleApplicationResultViewModel>(model);
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken, long VehicleApplicationId)
        {
            var model = await _vehicleApplicationRepository.GetByIdAsync(cancellationToken, id);
            _vehicleApplicationRepository.Delete(model, true);
            return true;
        }

        public async Task<VehicleApplicationResultViewModel> Get(long id, CancellationToken cancellationToken, long VehicleApplicationId)
        {
            var model = await _vehicleApplicationRepository.GetByIdAsync(cancellationToken, VehicleApplicationId);
            if (model == null)
                throw new CustomException(" error");

            return _mapper.Map<VehicleApplicationResultViewModel>(model);

        }

        public async Task<PagedResult<VehicleApplicationResultViewModel>> GetAll(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;

            var c = await _vehicleApplicationRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return _mapper.Map<PagedResult<VehicleApplicationResultViewModel>>(c);
        }

        public async Task<List<VehicleApplicationResultViewModel>> GetVehicleApplicationsAsync(CancellationToken cancellationToken)
        {
            var model = await _vehicleApplicationRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<VehicleApplicationResultViewModel>>(model);
        }

        public async Task<VehicleApplicationResultViewModel> Update(long id, VehicleApplicationInputViewModel ViewModel, CancellationToken cancellationToken)
        {
            VehicleApplication updae = new VehicleApplication
            {
                Id = id,
                Name = ViewModel.Name,
                VehicleTypeId = ViewModel.VehicleTypeId
            };
            await _vehicleApplicationRepository.UpdateAsync(updae,cancellationToken,true);
            return _mapper.Map<VehicleApplicationResultViewModel>(updae);
        }


        #endregion

    }
}
