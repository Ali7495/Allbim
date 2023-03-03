using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.PageAble;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models.Insurance;
using Models.Product;
using Models.QueryParams;
using Services.PipeLine;
using System.Reflection;
using System.Transactions;
using Models.PolicyRequest;

namespace Services.PolicyRequestStatus
{
    public class PolicyRequestStatusService : IPolicyRequestStatusService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IPolicyRequestStatusRepository _policyRequestStatusRepository;
        #endregion

        #region CTOR

        public PolicyRequestStatusService(IPolicyRequestStatusRepository policyRequestStatusRepository, IMapper mapper, IRepository<DAL.Models.PolicyRequestStatus> policyRepository
            )
        {
            _mapper = mapper;
            _policyRequestStatusRepository = policyRequestStatusRepository;
        }
        #endregion


        public async Task<List<PolicyRequestStatusViewModel>> GetPolicyRequestStatus(CancellationToken cancellationToken)
        {
            var model = await _policyRequestStatusRepository.GetAllAsync(cancellationToken);
            if (model == null)
                throw new CustomException("policy Request Status");
            return _mapper.Map<List<PolicyRequestStatusViewModel>>(model);
        }


    }
}