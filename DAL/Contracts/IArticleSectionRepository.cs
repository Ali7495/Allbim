using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IArticleSectionRepository:IRepository<ArticleSection>
    {
        Task<List<ArticleSection>> GetAllArticleByIDFromArticleSection(CancellationToken cancellationToken, long ArticleId);
        Task<List<ArticleSection>> GetArticleidByArticleSectionID(CancellationToken cancellationToken, int SectionID);
        Task<ArticleSection> GetByCode(int code, CancellationToken cancellationToken);
    }
}
