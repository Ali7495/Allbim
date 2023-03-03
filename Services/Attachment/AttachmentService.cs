using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Attachment;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;
using AutoMapper;

namespace Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private PagingSettings _pagingSettings;
        private readonly SiteSettings _siteSettings;
        private readonly IMapper _mapper;

        public AttachmentService(IAttachmentRepository attachmentRepository,
            IOptionsSnapshot<PagingSettings> pagingSettings,
            IMapper mapper,
            IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _attachmentRepository = attachmentRepository;
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
            _siteSettings = siteSettings.Value;
        }

        public async Task<Attachment> CreateAttachment(CancellationToken cancellationToken, IFormFile file,
            string path = null)
        {
            if (!Directory.Exists(_siteSettings.FilePath))
            {
                Directory.CreateDirectory(_siteSettings.FilePath);
            }

            var ext = Path.GetExtension(file.FileName);
            // هم اسم فایل ذخیره شده و هم کد attachment 
            var uniqueName = Guid.NewGuid();

            path ??= Path.Combine(_siteSettings.FilePath, uniqueName + ext);
            var model = new Attachment();
            using (var target = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(target);
                model.Name = file.FileName;
                var fileName = Path.GetFileName(file.FileName);
                model.Extension = Path.GetExtension(fileName);
                model.Path = path;
                model.Code = uniqueName;
            }

            await _attachmentRepository.AddAsync(model, cancellationToken);
            return model;
        }


        public async Task<Attachment> GetAttachmentAsync(string code, CancellationToken cancellationToken)
        {
            return await _attachmentRepository.GetByIdAsync(cancellationToken, code);
        }

        public async Task<Attachment> GetByCode(Guid code, CancellationToken cancellationToken, string fileupload)
        {
            var list = await _attachmentRepository.GetAllAsync(cancellationToken);
            return list.Where(x => x.Code == code).FirstOrDefault();
        }


        public async Task<FileContentResult> DownloadByFullName(string filename, CancellationToken cancellationToken)
        {
            // Guid code = Guid.Parse(filename);
            // var attachment = await _attachmentRepository.GetByCode(code,cancellationToken);
            // if (attachment == null)
            // {
            //     throw new BadRequestException("کد فایل وجود ندارد");
            //     
            // }

            // var fileName=attachment.Name;
            
            
            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(filename);
            var path = Path.Combine(_siteSettings.FilePath, filename);
            // byte[] fileBytes = await GetFileBytesByPath(attachment.Path,cancellationToken);
            byte[] fileBytes = await GetFileBytesByPath(path, cancellationToken);

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filename
            };
        }

        public async Task<byte[]> GetFileBytesByPath(string path, CancellationToken cancellationToken)
        {
            byte[] readText = await File.ReadAllBytesAsync(path, cancellationToken);
            return readText;
        }

        public async Task<AttachmentResultViewModel> GetAttachmentDetailByCode(Guid code, CancellationToken cancellationToken)
        {
            var attachment = await _attachmentRepository.GetByCode(code, cancellationToken);
            if (attachment == null)
            {
                throw new BadRequestException("کد فایل وجود ندارد");
            }

            return _mapper.Map<AttachmentResultViewModel>(attachment);
        }

        public async Task<bool> DeleteAttachment(Guid code, CancellationToken cancellationToken)
        {
            var attachment = await _attachmentRepository.GetByCode(code, cancellationToken);
            if (attachment == null)
            {
                throw new NotFoundException("فایلی با کد وارد شده یافت نشد");
            }

            attachment.IsDeleted = true;
            await _attachmentRepository.UpdateAsync(attachment, cancellationToken);
            return true;
        }

        public async Task<AttachmentResultViewModel> CreateAttachmentWithDefrentVM(CancellationToken cancellationToken,
            IFormFile file, string path = null)
        {
            var model = await CreateAttachment(cancellationToken, file);
            return _mapper.Map<AttachmentResultViewModel>(model);
            
            //
            // if (!Directory.Exists(_siteSettings.FilePath))
            // {
            //     Directory.CreateDirectory(_siteSettings.FilePath);
            // }
            //
            // var ext = Path.GetExtension(file.FileName);
            // // هم اسم فایل ذخیره شده و هم کد attachment 
            // var uniqueName = Guid.NewGuid();
            //
            // path ??= Path.Combine(_siteSettings.FilePath, uniqueName + ext);
            // var model = new Attachment();
            // using (var target = new FileStream(path, FileMode.Create))
            // {
            //     await file.CopyToAsync(target);
            //     model.Name = file.FileName;
            //     var fileName = Path.GetFileName(file.FileName);
            //     model.Extension = Path.GetExtension(fileName);
            //     model.Path = path;
            //     model.Code = uniqueName;
            // }
            //
            // await _attachmentRepository.AddAsync(model, cancellationToken);
            //
            // return _mapper.Map<AttachmentInputViewModel>(model);
        }

        public async Task<AttachmentInputViewModel> UpdateAttachment(Guid Code, CancellationToken cancellationToken,
            IFormFile file, string path = null)
        {
            if (!Directory.Exists(_siteSettings.FilePath))
            {
                Directory.CreateDirectory(_siteSettings.FilePath);
            }

            var model = await _attachmentRepository.GetByCode(Code, cancellationToken);
            var ext = Path.GetExtension(file.FileName);
            path ??= Path.Combine(_siteSettings.FilePath, Code + ext);
            using (var target = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(target);
                model.Name = file.FileName;
                var fileName = Path.GetFileName(file.FileName);
                model.Extension = Path.GetExtension(fileName);
                model.Path = path;
            }

            await _attachmentRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<AttachmentInputViewModel>(model);
        }
    }
}