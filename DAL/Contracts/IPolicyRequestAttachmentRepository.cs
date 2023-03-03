using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IPolicyRequestAttachmentRepository : IRepository<PolicyRequestAttachment>
    {
        Task<List<PolicyRequestAttachment>> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken);
        Task<List<PolicyRequestAttachment>> GetByPolicyRequestCodeTypeId(Guid policyCode,int typeId, CancellationToken cancellationToken);
    
    }
}