using System.Collections.Generic;
using Common.Utilities;
using DAL.Models;
using Models.InsuranceRequest;
using Models.Place;
using System.Threading;
using System.Threading.Tasks;
using Models.Resource;

namespace Services
{
    public interface IResourceOperationService
    {
        #region Create
        public Task<ResourceOperationViewModel> Create(ResourceOperationInputViewModel resourceOperationInputViewModel, CancellationToken cancellationToken);

        #endregion

        #region Get

        Task<List<ResourceOperationViewModel>> GetByResourceName(string ResourceName,long userId,long RoleId, CancellationToken cancellationToken);
        
        #endregion

        #region Update
        public Task<ResourceOperationViewModel> Update(long id, ResourceOperationInputViewModel resourceOperationInputViewModel, CancellationToken cancellationToken);
        
        #endregion

        #region Delete
        public Task<ResourceOperationViewModel> Delete(long id, CancellationToken cancellationToken);
       

        #endregion





    }
}
