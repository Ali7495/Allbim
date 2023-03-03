using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
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
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IRepository<Permission> repository, IMapper mapper)
        {
            _permissionRepository = repository;
            _mapper = mapper;
        }

        public async Task<PermissionResultViewModel> Create(PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken)
        {
            Permission model = new Permission()
            {
                Name = permissionViewModel.Name
            };

            await _permissionRepository.AddAsync(model, cancellationToken);

            return _mapper.Map<PermissionResultViewModel>(model);
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var model = await _permissionRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("مجوز وجود ندارد");

            await _permissionRepository.DeleteAsync(model, cancellationToken);

            return true;
        }

        public async Task<PermissionResultViewModel> Get(long id, CancellationToken cancellationToken)
        {
            var model = await _permissionRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("مجوز وجود ندارد");

            return _mapper.Map<PermissionResultViewModel>(model);
        }

        public async Task<PermissionResultViewModel> Update(long id, PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken)
        {
            var model = await _permissionRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("مجوز وجود ندارد");

            model.Name = permissionViewModel.Name;
            await _permissionRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<PermissionResultViewModel>(model);
        }
    }
}
