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
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<Attachment> GetByCode(Guid code,  CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
