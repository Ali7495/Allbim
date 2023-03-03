using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Attachment;

namespace Services
{
    public interface IAttachmentService
    {

        Task<Attachment> CreateAttachment( CancellationToken cancellationToken, IFormFile file, string path=null);
        Task<AttachmentResultViewModel> CreateAttachmentWithDefrentVM( CancellationToken cancellationToken, IFormFile file, string path=null);
        Task<AttachmentInputViewModel> UpdateAttachment( Guid Code,CancellationToken cancellationToken, IFormFile file, string path=null);
        Task<Attachment> GetAttachmentAsync(string code, CancellationToken cancellationToken);
        Task<Attachment> GetByCode(Guid code, CancellationToken cancellationToken, string fileupload);
        Task<FileContentResult> DownloadByFullName(string filename, CancellationToken cancellationToken);
        Task<AttachmentResultViewModel> GetAttachmentDetailByCode(Guid code, CancellationToken cancellationToken);
        Task<bool> DeleteAttachment(Guid code, CancellationToken cancellationToken);
    }
}
