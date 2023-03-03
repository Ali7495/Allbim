using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IResourceOperationRepository : IRepository<ResourceOperation>
    {
        Task<List<ResourceOperation>> GetAllByResource(string ResourceName, CancellationToken cancellationToken);
        Task<List<ResourceOperation>> GetAllByResourceAndPermissionIds(string ResourceName,List<long>permissionIds, CancellationToken cancellationToken);

    }
}
