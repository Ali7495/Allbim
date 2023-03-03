using DAL.Models;
using Models;
using Models.Role;
using Models.RolePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoleService
    {
        #region Role

        Task<RoleResultViewModel> Create(RoleInputViewModel roleViewModel, CancellationToken cancellationToken);

        
    

        Task<RoleResultViewModel> GetRole(long id, CancellationToken cancellation);
        Task<List<RoleResultViewModel>> GetListByUserId(long userId, long roleId, CancellationToken cancellation);

     

        Task<RoleResultViewModel> Update(long id ,RoleInputViewModel roleViewModel, CancellationToken cancellationToken);

      
       

        Task<bool> Delete(long id, CancellationToken cancellationToken);


        #endregion



        #region Role Permission
        Task<RolePermissionResultViewModel> CreateRolePermission(RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken);
        Task<RolePermissionResultViewModel> UpdateRolePermission(long id, RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken);

        Task<bool> UpdatePermissions(long roleId, long[] permissionIds, CancellationToken cancellationToken);
        
        Task<bool> DeleteRolePermission(long id, CancellationToken cancellationToken);
        #endregion


    }
}
