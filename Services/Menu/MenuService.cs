using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.ViewModels.Menu;
using Common.Extensions;

namespace Services
{
    public class MenuService : IMenuService
    {
        #region Fields

        private readonly IRepository<Menu> _repository;
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRepository _userRepository;
        #endregion

        #region CTOR
        public MenuService(IRepository<Menu> repository, IMenuRepository menuRepository, IMapper mapper, IRolePermissionRepository rolePermissionRepository, IUserRepository userRepository)
        {
            _menuRepository = menuRepository;
            _repository = repository;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _userRepository = userRepository;
        }

        #endregion

        
        
        public static IEnumerable<MenuTreeItemViewModel> GenerateTreeWithRecursion(
            List<MenuTreeItemViewModel> collection,
            long? id_to_match)
        {
            var tree = new List<MenuTreeItemViewModel>();
            var nodesWithMatchedId = collection.Where(c => c.ParentId==id_to_match).ToList();
            foreach (var c in nodesWithMatchedId)
            {
                tree.Add(new MenuTreeItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Icon = c.Icon,
                    Name = c.Name,
                    Order = c.Order,
                    Children = GenerateTreeWithRecursion(collection, c.Id)
                });
            }
            return tree;
        }

        
        
        public async Task<MenuResultViewModel> Get(long id, CancellationToken cancellationToken)
        {
            Menu menu = await _repository.GetByIdAsync(cancellationToken, id);

            if (menu == null)
                throw new CustomException("منو");

            return _mapper.Map<MenuResultViewModel>(menu);
        }
        public async Task<List<MenuTreeItemViewModel>> GetAdminMenu(long? userId, CancellationToken cancellationToken)
        {
            List<Role> UserRoles = await _userRepository.GetUserRolesAsync((long)userId);
            List<RolePermission> UserRolesPermissions = await _rolePermissionRepository.GetRolesPermissionsAsync(UserRoles.Select(s => s.Id).ToList()) ;
            List<Menu> menu = await _menuRepository.GetMenusByPremission(UserRolesPermissions.Select(s=>s.PermissionId).ToList(), cancellationToken);
            
            // تبدیل لیست به درخت
            
            var items =  _mapper.Map<List<MenuTreeItemViewModel>>(menu);
            if (items.Count > 0)
            {
                var root = items.First();
                items = GenerateTreeWithRecursion(items,root.ParentId).ToList(); // a.GetParentId() is null, which in this example is the required root id

            }
            return items;
        }

        public async Task<Menu> Create(MenuInputViewModel menuViewModel, CancellationToken cancellationToken)
        {
            Menu menu = new Menu()
            {
                Name = menuViewModel.Name,
                PermissionId = menuViewModel.PermissionId
            };

            await _repository.AddAsync(menu, cancellationToken);

            return menu;
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            Menu menu = await _repository.GetByIdAsync(cancellationToken, id);

            if (menu == null)
                throw new CustomException("منو");

            await _repository.DeleteAsync(menu, cancellationToken);

            return true;
        }

        public async Task<Menu> Update(long id, MenuInputViewModel menuViewModel, CancellationToken cancellationToken)
        {
            Menu menu = await _repository.GetByIdAsync(cancellationToken, id);

            if (menu == null)
                throw new CustomException("منو");

            menu.Name = menuViewModel.Name;
            menu.PermissionId = menuViewModel.PermissionId;

            await _repository.UpdateAsync(menu, cancellationToken);

            return menu;
        }
    }
}
