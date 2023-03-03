using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using Models.Place;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IPlaceService
    {
        
       



        #region Place
        
        Task<PagedResult<PlaceResultViewModel>> Get(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<PlaceResultViewModel> Detail(long code, CancellationToken cancellationToken);
        public Task<PlaceResultViewModel> Create(PlaceInputViewModel ViewModel, CancellationToken cancellationToken);
        public Task<PlaceResultViewModel> Update(long id, PlaceInputViewModel cityViewModel, CancellationToken cancellationToken);
        public Task<bool> Delete(long id, CancellationToken cancellationToken);
        #endregion

        
        
        
        #region PlaceAddress
     
        Task<PlaceAddressResultViewModel> CreatePlaceAddress(PlaceAddressInputViewModel placeViewModel, CancellationToken cancellationToken);
        Task<PlaceAddressResultViewModel> DetailPlaceAddress(long id, CancellationToken cancellationToken);
        Task<PagedResult<PlaceAddressResultViewModel>> GetPlaceAddress(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<bool> DeletePlaceAddress(int id, CancellationToken cancellationToken);
        Task<PlaceAddressResultViewModel> UpdatePlaceAddress(long id, PlaceAddressInputViewModel viewModel, CancellationToken cancellationToken);


        #endregion

    }
}
