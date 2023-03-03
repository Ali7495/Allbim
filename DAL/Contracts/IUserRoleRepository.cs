using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetUserRole(long userId, long roleId, CancellationToken cancellationToken);
        Task<UserRole> GetUserRoleTracking(long userId, long roleId, CancellationToken cancellationToken);
        Task<List<UserRole>> GetUserRolesByUserId(long userId, CancellationToken cancellationToken);
        Task<UserRole> GetSingleUserRoleByUserId(long userId, CancellationToken cancellationToken);
        Task<UserRole> GetWithUserAndRole(long userId, CancellationToken cancellationToken);
    }
}
