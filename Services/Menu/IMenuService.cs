using DAL.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.ViewModels.Menu;

namespace Services
{
    public interface IMenuService
    {
        
        Task<MenuResultViewModel> Get(long id, CancellationToken cancellationToken);

        Task<List<MenuTreeItemViewModel>> GetAdminMenu(long? userId, CancellationToken cancellationToken);
        Task<Menu> Create(MenuInputViewModel menuViewModel, CancellationToken cancellationToken);

        Task<Menu> Update(long id, MenuInputViewModel menuViewModel, CancellationToken cancellationToken);

        Task<bool> Delete(long id, CancellationToken cancellationToken);
    }
}
