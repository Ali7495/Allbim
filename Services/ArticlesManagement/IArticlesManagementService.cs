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
    public interface IArticlesManagementService
    {
        Task<ArticleResultViewModel> create(ArticlesInputViewModel companyViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long code, CancellationToken cancellationToken);
        Task<bool> DeleteMine(long UserID,long code, CancellationToken cancellationToken);
        Task<bool> DeleteArticleMetakey(long articleId, long articleMetaKeyId, CancellationToken cancellationToken);
        Task<bool> DeleteArticleCategory(long articleId, long categoryId, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> Update(long ID, ArticlesInputViewModel ArticleEditeViewModel, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> UpdateMine(long UserID, long ID, ArticlesInputViewModel ArticleEditeViewModel, CancellationToken cancellationToken);
        Task<ArticleMetaKeyOutputViewModel> UpdateArticleMetakey(long articleId, long articleMetaKeyId, ArticleMetaKeyViewModel ArticleMetakeyinputViewModel, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> detailV2(long ID, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> detailMine(long UserID,long ID, CancellationToken cancellationToken);
        Task<PagedResult<ArticleSummaryViewModel>> all(PageAbleResult pageAbleResult,  CancellationToken cancellationToken);
        Task<List<ArticleResultViewModel>> GetBySection(int id, CancellationToken cancellationToken);
        Task<ArticleResultViewModel> GetArticleBySlug(string Slug, CancellationToken cancellationToken);
        Task<PagedResult<ArticleLatestResultViewModel>> GetTopFiveArticles(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
    }
}
