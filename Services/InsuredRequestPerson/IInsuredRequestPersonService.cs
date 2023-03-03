using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuredRequestPersonService
    {
        #region Create
        public Task<InsuredRequestPerson> Create(InsuranceRequestPersonViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<PagedResult<InsuredRequestPerson>> Get(int? page, int? pageSize, CancellationToken cancellationToken);
        #endregion

        #region Update
        public Task<InsuredRequestPerson> Update(long id, InsuranceRequestPersonViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        public Task<bool> Delete(long id, CancellationToken cancellationToken);
        #endregion
        #region Detail
        Task<InsuredRequestPerson> Detail(long code, CancellationToken cancellationToken);
        #endregion
    }
}
