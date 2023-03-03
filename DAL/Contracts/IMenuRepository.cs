using DAL.Models;
using Services.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<Menu>> GetMenusByPremission(List<long> UserRolesPermissionsArray, CancellationToken cancellationToken);
    }
}
