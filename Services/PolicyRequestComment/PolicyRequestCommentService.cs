using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
using Models.PolicyRequestCommet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper.Internal;
using Models.Attachment;

namespace Services.PolicyRequestComment
{
    public class PolicyRequestCommentService : IPolicyRequestCommentService
    {

        private readonly IPolicyRequestCommentRepository _PolicyRequestCommentRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IPolicyRequestCommentAttachmentRepository _policyRequestCommentAttachmentRepository;
        public PolicyRequestCommentService(IPolicyRequestCommentAttachmentRepository policyRequestCommentAttachmentRepository, IAttachmentRepository attachmentRepository, IRoleRepository roleRepository, IUserRepository userRepository, IPolicyRequestCommentRepository policyRequestCommentRepository, IMapper mapper, IUserRoleRepository userRoleRepository, IPolicyRequestRepository policyRequestRepository, IPersonCompanyRepository personCompanyRepository)
        {
            _PolicyRequestCommentRepository = policyRequestCommentRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _personCompanyRepository = personCompanyRepository;
            _policyRequestRepository = policyRequestRepository;
            _UserRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _attachmentRepository = attachmentRepository;
            _policyRequestCommentAttachmentRepository = policyRequestCommentAttachmentRepository;
        }
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

        public async Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllWithoutPaging(Guid code, CancellationToken cancellationToken)
        {
            var modelPolicy = await _policyRequestRepository.GetByCodeNoTracking(code, cancellationToken);
            if (modelPolicy == null)
                throw new CustomException("موردی یافت نشد");
            var GetPolicyRequestCommentListByID = await _PolicyRequestCommentRepository.GetAllPolicyRequestCommentById(modelPolicy.Id, cancellationToken);
            
            
            
            List<PolicyRequestCommentGetAllOutputViewModel> PolicyRequestCommentResult = _mapper.Map<List<PolicyRequestCommentGetAllOutputViewModel>>(GetPolicyRequestCommentListByID);

            return PolicyRequestCommentResult;
        }



    }
}
