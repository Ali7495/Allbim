using Common.Utilities;
using DAL.Models;
using Models.Article;
using Models.Articles;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.ArticleManagement
{
    public interface IBlogService
    {
        Task<ArticleResultViewModel> create(PageInputViewModel companyViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken);
        
        Task<ArticleResultViewModel> Update(long id, PageInputViewModel ArticleEditeViewModel, CancellationToken cancellationToken);

        Task<PagedResult<ArticleSummaryViewModel>> allPages(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);
        Task<ArticleResultViewModel> GetArticleBySlug(string Slug, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> GetArticleById(long id, CancellationToken cancellationToken);
    }
}
