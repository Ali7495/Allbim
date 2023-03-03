<div align="right" dir="rtl">

عملیات ویرایش و درج جدول PolicyRequestComment بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر های Company و PolicyRequest قرار دارند چراکه هم کاربران می توانند نماینده نظرات ارسال کنند هم شرکت باید این دسترسی را داشته باشد. 



>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*


سرویس های کنترلر PolicyRequest:
</div>

```C#

       [HttpPost("{code}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> Create([FromRoute] Guid code, [FromBody] PolicyRequestCommentInputViewModel _PolicyRequestCommentInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            

            var result = await _policyRequestCommentService.Create(userId, code, _PolicyRequestCommentInputViewModel , cancellationToken);
            return result;
        }

        [HttpGet("{code}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetAllWithoutPaging([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            return await _policyRequestCommentService.GetAllWithoutPaging(code, cancellationToken);
        }

```

<div align="right" dir="rtl">

سرویس های کنترلر Company:

</div>


```C#

 [HttpPost("{code}/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> CreateCompanyPolicyComment(Guid code, Guid policyCode, CompanyCommentInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.CreateCompanyComment(userId, code, policyCode, viewModel, cancellationToken);
        }

        [HttpGet("{code}/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetCompanyPolicyComment(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            return await _companyService.GetAllPolicyComments(code, policyCode, cancellationToken);
        }



        [HttpPost("mine/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> CreateCompanyPolicyCommentMine(Guid policyCode, CompanyCommentInputMineViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.CreateCompanyCommentMine(userId, policyCode, viewModel, cancellationToken);
        }

        [HttpGet("mine/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetCompanyPolicyCommentMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetAllPolicyCommentsMine(userId, policyCode, cancellationToken);
        }

```






<div align="right" dir="rtl">

**درج  (سرویس Post)** : این سرویس متد `Create(userId, code, _PolicyRequestCommentInputViewModel , cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

 public async Task<PolicyRequestCommentGetAllOutputViewModel> Create(long userId, Guid code, PolicyRequestCommentInputViewModel _PolicyRequestCommentInputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
                if (user == null)
                    throw new BadRequestException("کاربر وجود ندارد");



                var modelPolicy = await _policyRequestRepository.GetByCode(code, cancellationToken);
                if (modelPolicy == null)
                    throw new BadRequestException("کد درخواست بیمه وجود ندارد");

                if (modelPolicy.RequestPersonId != user.PersonId)
                {
                    throw new BadRequestException("این درخواست متعلق به شما نیست");
                }

                byte? AuthorTypeId = user.PersonId == modelPolicy.RequestPersonId ? (byte?)1 : (byte?)2;


                DAL.Models.PolicyRequestComment ModelPolicyRequestComment = new DAL.Models.PolicyRequestComment
                {
                    PolicyRequestId = modelPolicy.Id,
                    AuthorId = user.PersonId,
                    AuthorTypeId = AuthorTypeId,
                    Description = _PolicyRequestCommentInputViewModel.Description,
                    IsDeleted = false,
                    CreatedDateTime = DateTime.Now
                };

                await _PolicyRequestCommentRepository.AddAsync(ModelPolicyRequestComment, cancellationToken);
                PolicyRequestCommentOutputViewModel InsertResult = _mapper.Map<PolicyRequestCommentOutputViewModel>(ModelPolicyRequestComment);
                for (int i = 0; i < _PolicyRequestCommentInputViewModel.AttachmentCodes.Count; i++)
                {
                    var attachmentData = _attachmentRepository.GetByCode(_PolicyRequestCommentInputViewModel.AttachmentCodes[i], cancellationToken);
                    if (attachmentData != null)
                    {
                        PolicyRequestCommentAttachment _policyRequestCommentAttachment = new PolicyRequestCommentAttachment
                        {
                            AttachmentId = attachmentData.Result.Id,
                            IsDeleted = false,
                            AttachmentTypeId = 1,
                            PolicyRequestCommentId = InsertResult.Id
                        };
                        await _policyRequestCommentAttachmentRepository.AddAsync(_policyRequestCommentAttachment, cancellationToken);
                    }
                }

                var PolicyRequestCommentResult = await _PolicyRequestCommentRepository.GetPolicyRequestCommentById(InsertResult.Id, cancellationToken);
                PolicyRequestCommentGetAllOutputViewModel ResultOutput = _mapper.Map<PolicyRequestCommentGetAllOutputViewModel>(PolicyRequestCommentResult);
                ResultOutput.Attachments = _mapper.Map<List<PolicyRequestCommentAttachmentViewModel>>(PolicyRequestCommentResult.PolicyRequestCommentAttachments.Select(s => s.Attachment));
                transaction.Complete();

                return ResultOutput;


            }
        }



```

<div align="right" dir="rtl">

<br>

ویومدل ورودی:


</div>


```C#

public class PolicyRequestCommentInputViewModel
    {
        public string Description { get; set; }
        //public long Status { get; set; }
        public List<Guid> AttachmentCodes { get; set; }

    }


```



<div align="right" dir="rtl">

<br>

**دریافت لیست (سرویس Get)** : این سرویس متد `GetAllWithoutPaging(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

       public async Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllWithoutPaging(Guid code, CancellationToken cancellationToken)
        {
            var modelPolicy = await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);
            if (modelPolicy == null)
                throw new CustomException("موردی یافت نشد");
            var GetPolicyRequestCommentListByID = await _PolicyRequestCommentRepository.GetAllPolicyRequestCommentById(modelPolicy.Id, cancellationToken);
            
            
            
            List<PolicyRequestCommentGetAllOutputViewModel> PolicyRequestCommentResult = _mapper.Map<List<PolicyRequestCommentGetAllOutputViewModel>>(GetPolicyRequestCommentListByID);

            return PolicyRequestCommentResult;
        }




```


<div align="right" dir="rtl">

این ها نمونه های کنترلر policy بودند. سایر سرویس ها دقیقا با همین مکانیزم پیاده شده اند و تنها تفاوت اعتبار سنجی ها می باشد.

</div>


