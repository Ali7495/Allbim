using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
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
    public class RoleService : IRoleService
    {

        #region Fields

        private readonly IRepository<Role> _repository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region CTOR

        public RoleService(IRepository<Role> repository, IRepository<RolePermission> rolePermissionRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _repository = repository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        #endregion

        #region Role

        public async Task<RoleResultViewModel> GetRole(long id, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetByIdAsync(cancellationToken, id);

            if (role == null)
                throw new CustomException("سمت");

            return _mapper.Map<RoleResultViewModel>(role);
        }
        public async Task<List<RoleResultViewModel>> GetListByUserId(long userId,long roleId, CancellationToken cancellation)
        {
            //todo: حتما باید براساس نقش کاربر فراخوانی کننده، نقش ها فیلتر شود فقط زیرمجموعه خودش را ببیند
            List<Role> roles = new List<Role>();
            // اگر ادمین بود
            if (roleId == 3)
            {
                roles = await _roleRepository.GetAllAsync(cancellation);
            }
            else
            {
                roles = await _roleRepository.GetByParentId(roleId,cancellation);
            }
           

            return _mapper.Map<List<RoleResultViewModel>>(roles);
        }

        public async Task<RoleResultViewModel> Create(RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            Role role = new Role()
            {
                Name = roleViewModel.Name
            };

            await _repository.AddAsync(role,cancellationToken);
            
            return _mapper.Map<RoleResultViewModel>(role);
        }

        public async Task<RoleResultViewModel> Update(long id, RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            Role role = await _repository.GetByIdAsync(cancellationToken, id);

            if (role == null)
                throw new BadRequestException("سمت");

            role.Name = roleViewModel.Name;

            await _repository.UpdateAsync(role, cancellationToken);

            return _mapper.Map<RoleResultViewModel>(role);
        }

        
        
        #endregion

        
        
        
        

        #region Role Permission


        public async Task<RolePermissionResultViewModel> CreateRolePermission(RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken)
        {
            RolePermission rolePermission = new RolePermission()
            {
                RoleId = rolePermissionViewModel.RoleId,
                PermissionId = rolePermissionViewModel.PermissionId
            };

            await _rolePermissionRepository.AddAsync(rolePermission, cancellationToken);


            return _mapper.Map<RolePermissionResultViewModel>(rolePermission);
        }
        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            Role role = await _repository.GetByIdAsync(cancellationToken, id);

            if (role == null)
                throw new CustomException("سمت");

            await _repository.DeleteAsync(role, cancellationToken);

            return true;
        }

        #endregion

        #region Update


        public async Task<RolePermissionResultViewModel> UpdateRolePermission(long id, RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken)
        {
            RolePermission rolePermission = await _rolePermissionRepository.GetByIdAsync(cancellationToken, id);

            if (rolePermission == null)
                throw new CustomException("مجوز سمت");

            rolePermission.PermissionId = rolePermissionViewModel.PermissionId;
            rolePermission.RoleId = rolePermissionViewModel.RoleId;

            await _rolePermissionRepository.UpdateAsync(rolePermission, cancellationToken);

            return _mapper.Map<RolePermissionResultViewModel>(rolePermission);
        }

        public async Task<bool> UpdatePermissions(long roleId, long[] permissionIds, CancellationToken cancellationToken)
        {
            Role role = await _roleRepository.GetRole(roleId, cancellationToken);

            if (role == null)
                throw new BadRequestException("سمت");

            List<RolePermission> permissions = role.RolePermissions.Where(r => permissionIds.Contains(r.PermissionId)).ToList();

            role.RolePermissions = permissions;

            await _repository.UpdateAsync(role, cancellationToken);

            return true;
        }

        #endregion

        #region Delete


        public async Task<bool> DeleteRolePermission(long id, CancellationToken cancellationToken)
        {
            RolePermission rolePermission = await _rolePermissionRepository.GetByIdAsync(cancellationToken, id);

            if (rolePermission == null)
                throw new CustomException("مجوز سمت");

            await _rolePermissionRepository.DeleteAsync(rolePermission, cancellationToken);

            return true;
        }

        #endregion


    }
}
