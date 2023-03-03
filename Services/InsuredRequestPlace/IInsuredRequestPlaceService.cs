using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuredRequestPlaceService
    {
        #region Create
        public Task<InsuredRequestPlace> Create(InsuranceRequestPlaceViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<PagedResult<InsuredRequestPlace>> Get(int? page, int? pageSize, CancellationToken cancellationToken);
        #endregion

        #region Update
        public Task<InsuredRequestPlace> Update(long id, InsuranceRequestPlaceViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        public Task<bool> Delete(long id, CancellationToken cancellationToken);
        #endregion
        #region Detail
        Task<InsuredRequestPlace> Detail(long code, CancellationToken cancellationToken);
        #endregion
    }
}
