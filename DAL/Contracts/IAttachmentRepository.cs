using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        Task<Attachment> GetByCode(Guid code,  CancellationToken cancellationToken);
    }
}