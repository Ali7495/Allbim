using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuredRequestCompanyService
    {
        #region Create
        public Task<InsuredRequestCompany> Create(InsuranceRequestCompanyViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<PagedResult<InsuredRequestCompany>> Get(int? page, int? pageSize, CancellationToken cancellationToken);
        #endregion

        #region Update
        public Task<InsuredRequestCompany> Update(long id, InsuranceRequestCompanyViewModel cityViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        public Task<bool> Delete(long id, CancellationToken cancellationToken);
        #endregion
        #region Detail
        Task<InsuredRequestCompany> Detail(long code, CancellationToken cancellationToken);
        #endregion
    }
}
