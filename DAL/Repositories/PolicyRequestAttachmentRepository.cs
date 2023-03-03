using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class PolicyRequestAttachmentRepository : Repository<PolicyRequestAttachment>,
        IPolicyRequestAttachmentRepository
    {
        public PolicyRequestAttachmentRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<List<PolicyRequestAttachment>> GetByPolicyRequestCode(Guid code,CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Attachment)
                .Include(x => x.PolicyRequest)
                .Where(x => x.PolicyRequest.Code == code)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PolicyRequestAttachment>> GetByPolicyRequestCodeTypeId(Guid policyCode,int typeId, CancellationToken cancellationToken)
        {
            return await Table.Include(i => i.Attachment)
                .Include(x => x.PolicyRequest)
                .Where(x=>x.TypeId==typeId && x.PolicyRequest.Code==policyCode).ToListAsync();
        }
        
    }
}