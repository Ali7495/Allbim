using Common.Utilities;
using Models.Comment;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ICommentServices
    {
        Task<CommentResultViewModel> PostNewComment(long id, long? userId,CommentInputViewModel model, CancellationToken cancellationToken);
        Task<List<CommentResultViewModel>> GetArticleComments(long id, CancellationToken cancellationToken);
        Task<List<CommentResultViewModel>> GetArticleCommentsMine(long UserID,long id, CancellationToken cancellationToken);
        Task<PagedResult<CommentResultViewModel>> GetAllComments(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<CommentResultViewModel> GetCommentMine(long UserID, long id, CancellationToken cancellationToken);
        Task<CommentResultViewModel> GetComment(long id, CancellationToken cancellationToken);
        Task<CommentResultViewModel> UpdateComment(long id, CommentInputViewModel model, CancellationToken cancellationToken);
        Task<CommentResultViewModel> UpdateCommentMine(long UserID,long id, CommentInputViewModel model, CancellationToken cancellationToken);
        Task<CommentResultViewModel> ApproveComment(long id, CancellationToken cancellationToken);
        Task<bool> DeleteComment(long id, CancellationToken cancellationToken);
        Task<bool> DeleteCommentMine(long UserID,long id, CancellationToken cancellationToken);
    }
}
