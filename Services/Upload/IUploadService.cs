using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Attachment;
using Models.Upload;

namespace Services
{
    public interface IUploadService
    {

        Task<UploadViewModel> CreateFile( CancellationToken cancellationToken, IFormFile file);
        Task<bool> DeleteFileFromPublic( string path, CancellationToken cancellationToken);
    }
}
