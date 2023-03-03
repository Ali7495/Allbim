using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuredRequestRelatedPersonService
    {
        #region Create
        public Task<InsuredRequestRelatedPerson> Create(InsuredRequestRelatedPersonViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Get
         Task<PagedResult<InsuredRequestRelatedPerson>> Get(int? page, int? pageSize, CancellationToken cancellationToken);
        #endregion

        #region Update
        public Task<InsuredRequestRelatedPerson> Update(long id, InsuredRequestRelatedPersonViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        public Task<bool> Delete(long id, CancellationToken cancellationToken);
        #endregion
        #region Detail
        Task<InsuredRequestRelatedPerson> Detail(long code, CancellationToken cancellationToken);
        #endregion
    }
}
