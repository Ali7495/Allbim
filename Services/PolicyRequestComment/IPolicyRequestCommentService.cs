using Models.PolicyRequestCommet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.PolicyRequestComment
{
    public interface IPolicyRequestCommentService
    {
        Task<PolicyRequestCommentGetAllOutputViewModel> Create(long userId, Guid code, PolicyRequestCommentInputViewModel _PolicyRequestCommentInputViewModel, CancellationToken cancellationToken);
        Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllWithoutPaging(Guid code, CancellationToken cancellationToken);
    }
}
