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
using Models.Upload;

namespace Services
{
    public class UploadService : IUploadService
    {
        private PagingSettings _pagingSettings;
        private readonly SiteSettings _siteSettings;
        private readonly IMapper _mapper;

        public UploadService(
            IOptionsSnapshot<PagingSettings> pagingSettings,
            IMapper mapper,
            IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
            _siteSettings = siteSettings.Value;
        }

        public async Task<UploadViewModel> CreateFile(CancellationToken cancellationToken, IFormFile file)
        {
            String base_path = $".\\{_siteSettings.PublicPath}";
            if (!Directory.Exists(base_path))
            {
                Directory.CreateDirectory(base_path);
            }

            var datetime = DateTime.Now;
            string year = datetime.Year.ToString();
            string month = datetime.Month.ToString();

            String path = Path.Combine(base_path, year, month);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            String file_name = file.FileName;
            String file_path = Path.Combine(path, file_name);
            int number = 1;
            while (File.Exists(file_path))
            {
                file_name = "copy_" + number + "_" + file.FileName;
                file_path=Path.Combine(path,file_name );
                number++;
            }
            using (var target = new FileStream(file_path, FileMode.Create))
            {
                await file.CopyToAsync(target);
            }

            var output = new UploadViewModel()
            {
                url = $"/{_siteSettings.PublicPath}/{year}/{month}/{file_name}"
            };
            return output;
        }

        public async Task<bool> DeleteFileFromPublic(string path, CancellationToken cancellationToken)
        {

            String file_path = path.TrimStart('/');
            if (File.Exists(file_path))
            {
                File.Delete(file_path);
                return true;
            }
            else
            {
                throw new BadRequestException("فایل مورد نظر وجود ندارد");
            }
            
        }
    }
}