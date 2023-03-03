using DAL.Models;
using Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IPermissionService
    {
        Task<PermissionResultViewModel> Create(PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken);

        Task<PermissionResultViewModel> Get(long id, CancellationToken cancellationToken);

        Task<PermissionResultViewModel> Update(long id ,PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken);

        Task<bool> Delete(long id, CancellationToken cancellationToken);
    }
}
