using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AlbimDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Comment>> GetArticleComments(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.ArticleId == id).Include(i => i.Article).Include(i => i.Author).Include(i=> i.Parent).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<Comment>> GetAllComments(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Article).Include(i => i.Parent).Include(i => i.Author).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
