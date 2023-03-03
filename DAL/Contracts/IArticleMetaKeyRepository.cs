using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IArticleMetaKeyRepository : IRepository<ArticleMetaKey>
    {
        Task<ArticleMetaKey> GetarticleMetaKeyById(long MetaKeyID, CancellationToken cancellationToken);
        Task<List<ArticleMetaKey>> GetarticleMetaKeyListByArticleID(long ArticleID, CancellationToken cancellationToken);
    }
}
