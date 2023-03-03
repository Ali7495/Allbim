using DAL.Models;
using Models.Center;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ICenterService
    {
        #region Create

        Task<CenterResultViewModel> Create(CenterInputViewModel roleViewModel, CancellationToken cancellationToken);

        #endregion

        #region Get

        Task<List<CenterResultViewModel>> GetAll( CancellationToken cancellation);

        #endregion

        #region Update

        Task<CenterResultViewModel> Update(long id , CenterInputViewModel roleViewModel, CancellationToken cancellationToken);


        #endregion

        #region Delete

        Task<bool> Delete(long id, CancellationToken cancellationToken);
        Task<CenterResultViewModel> Get(long id, CancellationToken cancellationToken);

        #endregion




    }
}
