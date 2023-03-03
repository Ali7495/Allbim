using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.Factor;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.FactorServices
{
    public class FactorServices : IFactorServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPolicyRequestFactorRepository _policyRequestFactorRepository;
        private readonly IMapper _mapper;

        public FactorServices(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IPolicyRequestFactorRepository policyRequestFactorRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _policyRequestFactorRepository = policyRequestFactorRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<FactorViewModel>> GetAllFactorsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            UserRole userRole = await _userRoleRepository.GetWithUserAndRole(userId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<PolicyRequestFactor> factors = new PagedResult<PolicyRequestFactor>(); 

            switch (userRole.Role.Name)
            {
                case "Admin":
                    factors = await _policyRequestFactorRepository.GetAllFactors(pageAbleModel, cancellationToken);
                    break;
                case "User":
                    factors = await _policyRequestFactorRepository.GetAllPersonFactors(userRole.User.PersonId, pageAbleModel, cancellationToken);
                    break;
            }

            return _mapper.Map<PagedResult<FactorViewModel>>(factors);
        }

        public async Task<FactorViewModel> GetFactorMine(long userId, long id, CancellationToken cancellationToken)
        {
            UserRole userRole = await _userRoleRepository.GetWithUserAndRole(userId, cancellationToken);
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            PolicyRequestFactor factor = await _policyRequestFactorRepository.GetFactorByIdNoTrakingWithDetails(id, cancellationToken);

            return _mapper.Map<FactorViewModel>(factor);
        }
    }
}
