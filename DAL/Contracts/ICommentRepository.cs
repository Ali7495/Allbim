using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetArticleComments(long id, CancellationToken cancellationToken);
        Task<PagedResult<Comment>> GetAllComments(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
