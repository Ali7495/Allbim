using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IInsuredRequestVehicleRepository : IRepository<InsuredRequestVehicle>
    {
        Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestId(long policyRequestId,  CancellationToken cancellationToken);
        Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestIdNoTracking(long policyRequestId, CancellationToken cancellationToken);

      Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestIdWithoutRelation(
            long policyRequestId, CancellationToken cancellationToken);
    }
}