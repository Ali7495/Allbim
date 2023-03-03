using Common.Utilities;
using DAL.Models;
using Models.City;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Township;
using Models.Upload;
using Cityy = DAL.Models.City;

namespace Services.City
{
    public interface ICityService
    {
        #region City

        public Task<CityResultViewModel> CreateCityAsync(CityInputViewModel cityViewModel, CancellationToken cancellationToken);
      

        public Task<CityResultViewModel> GetCityAsync(long id, CancellationToken cancellationToken);
        public Task<List<CityResultViewModel>> GetAllCitiesAsync(CancellationToken cancellationToken);
        public Task<PagedResult<CityResultViewModel>> GetCitiesAsync(int page, int? pageSize, string orderBy, CancellationToken cancellationToken);
        public Task<CityResultViewModel> UpdateCityAsync(long id, CityInputViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<bool> DeleteCityAsync(long id, CancellationToken cancellationToken);

      
        #endregion








        #region Province

        
        public Task<ProvinceResultViewModel> CreateProvinceAsync(ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken);
        public Task<ProvinceResultViewModel> GetProvinceAsync(long id, CancellationToken cancellationToken);
        public Task<List<ProvinceResultViewModel>> GetAllProvincesAsync(CancellationToken cancellationToken);
        public Task<PagedResult<ProvinceResultViewModel>> GetProvincesAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        public Task<ProvinceResultViewModel> UpdateProvinceAsync(long id, ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken);

        public Task<bool> DeleteProvinceAsync(long id, CancellationToken cancellationToken);
        #endregion
        
        
        
        
        #region Township
        public Task<List<TownshipResultViewModel>> GetTownShipListAsync(CancellationToken cancellationToken);
        public Task<PagedResult<TownshipResultViewModel>> GetTownShipPagingAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        public Task<TownshipResultViewModel> GetTownShipDetailAsync(long id, CancellationToken cancellationToken);
        public Task<TownshipResultViewModel> CreateTownShipAsync(TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken);
        public Task<TownshipResultViewModel> UpdateTownShipAsync(long id, TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken);
        public Task<bool> DeleteTownShipAsync(long id, CancellationToken cancellationToken);
        #endregion
    }
}
