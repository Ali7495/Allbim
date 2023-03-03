<div align="right" dir="rtl">

اکشن سرویس `CreatePolicyRequestAttachment(cancellationToken, viewModel.File, code, viewModel.TypeId)` را فراخوانی میکند.



</div>

```C#

public async Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachment(CancellationToken cancellationToken,
            IFormFile files, string policyCode, int typeId)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var code = Guid.Parse(policyCode);
                var SameTypeExists =
                    await _policyRequestAttachmentRepository.GetByPolicyRequestCodeTypeId(code, typeId, cancellationToken);
                if (SameTypeExists.Count > 0)
                {
                    await _policyRequestAttachmentRepository.DeleteRangeAsync(SameTypeExists, cancellationToken);
                    var attachments = SameTypeExists.Select(x => x.Attachment).ToList();
                    await _attachmentRepository.DeleteRangeAsync(attachments, cancellationToken);
                }

                var extension = Path.GetExtension(files.FileName);
                var newName = Guid.NewGuid() + extension;

                Attachment model = await _attachmentService.CreateAttachment(cancellationToken, files);


                var modelPolicy = await _policyRequestRepository.GetByCode(code, cancellationToken);
                var modelAttachment = new DAL.Models.PolicyRequestAttachment()
                {
                    AttachmentId = model.Id,
                    //Attachment = model,
                    TypeId = typeId,
                    PolicyRequestId = modelPolicy.Id,
                    Name = Path.GetFileName(files.FileName)
                };

                await _policyRequestAttachmentRepository.AddAsync(modelAttachment, cancellationToken);
                // modelAttachment = await _policyRequestAttachmentRepository.Table.Include(c => c.PolicyRequest)
                //     .Include(c => c.Attachment)
                //     .FirstOrDefaultAsync(x => x.Id == modelAttachment.Id, cancellationToken);

                transaction.Complete();
                return _mapper.Map<PolicyRequestAttachmentViewModel>(modelAttachment);
            }
        }


```

<br>

```C#

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



```