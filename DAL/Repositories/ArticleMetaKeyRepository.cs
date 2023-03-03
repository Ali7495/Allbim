using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ArticleMetaKeyRepository : Repository<ArticleMetaKey>, IArticleMetaKeyRepository
    {
        public ArticleMetaKeyRepository(AlbimDbContext dbContext)
         : base(dbContext)
        {
        }

        public async Task<ArticleMetaKey> GetarticleMetaKeyById(long MetaKeyID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Id == MetaKeyID).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ArticleMetaKey>> GetarticleMetaKeyListByArticleID(long ArticleID, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.ArticleId == ArticleID).ToListAsync();
        }
    }
}
