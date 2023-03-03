using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PolicyRequestCommentRepository : Repository<PolicyRequestComment>, IPolicyRequestCommentRepository
    {
        public PolicyRequestCommentRepository(AlbimDbContext dbContext)
         : base(dbContext)
        {
        }

        public async Task<List<PolicyRequestComment>> GetAllPolicyRequestCommentByCompanyId(long companyId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.PolicyRequest.Insurer.CompanyId == companyId).Include(i=> i.PolicyRequestCommentAttachments).ThenInclude(th=> th.Attachment).Include(i=> i.Author).Include(i=> i.PolicyRequest).ToListAsync(cancellationToken);
        }

        public async Task<List<PolicyRequestComment>> GetAllPolicyRequestCommentById(long ID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().
                Include(x => x.PolicyRequestCommentAttachments)
                .ThenInclude(x=>x.Attachment)
                .Include(r => r.PolicyRequest)
                .Include(p => p.Author)
                .ThenInclude(p => p.Users)
                .ThenInclude(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .Where(x => x.PolicyRequestId == ID).ToListAsync();
        }
        public async Task<PolicyRequestComment> GetPolicyRequestCommentById(long ID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(r => r.PolicyRequest)
                .Include(x => x.PolicyRequestCommentAttachments)
                .ThenInclude(x=>x.Attachment)
                .Include(p => p.Author)
                .ThenInclude(p => p.Users).ThenInclude(p => p.UserRoles)
                .ThenInclude(p => p.Role).Where(x => x.Id == ID)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PolicyRequestComment>> GetPolicyRequestCommentsByCompanyIdAndPolicyCode(long companyId, Guid policyCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.PolicyRequest.Insurer.CompanyId == companyId && c.PolicyRequest.Code == policyCode).Include(i => i.PolicyRequestCommentAttachments).ThenInclude(th => th.Attachment).Include(i => i.Author).Include(i => i.PolicyRequest).ToListAsync(cancellationToken);
        }
    }
}
