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
    public class PolicyRequestCommentAttachmentRepository : Repository<PolicyRequestCommentAttachment>, IPolicyRequestCommentAttachmentRepository
    {
        public PolicyRequestCommentAttachmentRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<PolicyRequestCommentAttachment>> GetCommentAttachmentByCommentId(long PolicyRequestCommentID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.Attachment).Where(x => x.PolicyRequestCommentId == PolicyRequestCommentID).ToListAsync(cancellationToken);
        }
    }
}
