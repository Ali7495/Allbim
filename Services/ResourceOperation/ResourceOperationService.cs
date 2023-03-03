using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Options;
using Models.InsuranceRequest;
using Models.Place;
using Models.Resource;
using Models.Settings;
using Services.ViewModels;

namespace Services
{
    public class ResourceOperationService : IResourceOperationService
    {
        private readonly PagingSettings _pagingSettings;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IResourceOperationRepository _resourceOperationRepository;
        private readonly IMapper _mapper;

        public ResourceOperationService(
            IOptionsSnapshot<PagingSettings> pagingSettings, IResourceOperationRepository resourceOperationRepository, IMapper mapper,IRolePermissionRepository rolePermissionRepository, IUserRepository userRepository)
        {
            _mapper = mapper;

            _pagingSettings = pagingSettings.Value;
            _resourceOperationRepository= resourceOperationRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _userRepository = userRepository;
        }

        public async Task<ResourceOperationViewModel> Create(ResourceOperationInputViewModel resourceOperationInputViewModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResourceOperationViewModel>> GetByResourceName(string ResourceName,long userId,long RoleId, CancellationToken cancellationToken)
        {
            List<Role> UserRoles = await _userRepository.GetUserRolesAsync((long)userId);
            List<RolePermission> UserRolesPermissions = await _rolePermissionRepository.GetRolesPermissionsAsync(UserRoles.Select(s => s.Id).ToList()) ;
            List<long> permissionIds = UserRolesPermissions.Select(x => x.PermissionId).ToList();
            
            var resourceOperations = await _resourceOperationRepository.GetAllByResourceAndPermissionIds(ResourceName,permissionIds, cancellationToken);

            var MapResult = _mapper.Map<List<ResourceOperationViewModel>>(resourceOperations);
            return MapResult;
        }

        public async Task<ResourceOperationViewModel> Update(long id, ResourceOperationInputViewModel resourceOperationInputViewModel,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ResourceOperationViewModel> Delete(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
