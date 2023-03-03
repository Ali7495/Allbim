using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Articles;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Update;
using Models.Blogs;
using Models.Article;
using Models.PageAble;

namespace Services.ArticleManagement
{
    public class ArticlesManagementService : IArticlesManagementService
    {
        private readonly IArticlesManagementRepository _articlesManagementRepository;
        private readonly IArticleSectionRepository _ArticleSectionRepository;
        private readonly IArticleMetaKeyRepository _ArticleMetaKeyRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ArticlesManagementService(IUserRepository userRepository,
            IArticleSectionRepository articleSectionRepository, IArticlesManagementRepository blogsManagementRepository,
            IArticleMetaKeyRepository articleMetaKeyRepository, IOptionsSnapshot<PagingSettings> pagingSettings,
            IMapper mapper, IPersonRepository personRepository, ICategoryRepository categoryRepository,
            IArticleCategoryRepository articleCategoryRepository)
        {
            _articlesManagementRepository = blogsManagementRepository;
            _ArticleMetaKeyRepository = articleMetaKeyRepository;
            _ArticleSectionRepository = articleSectionRepository;
            _categoryRepository = categoryRepository;
            _personRepository = personRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<ArticleResultViewModel> create(ArticlesInputViewModel articlesInputViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Person person =
                    await _personRepository.GetByCodeNoTracking(articlesInputViewModel.AuthorCode, cancellationToken);
                if (person == null)
                {
                    throw new BadRequestException("کد شخص وجود ندارد");
                }

                Article article = new Article
                {
                    AuthorId = person.Id,
                    CreatedDateTime = DateTime.Now,
                    Description = articlesInputViewModel.Description,
                    Summary = articlesInputViewModel.Summary,
                    Title = articlesInputViewModel.Title,
                    ArticleTypeId = 2 // is article
                };

                foreach (long item in articlesInputViewModel.CategoryIds)
                {
                    article.ArticleCategories.Add(new ArticleCategory()
                    {
                        CategoryId = item
                    });
                }

                foreach (var item in articlesInputViewModel.ArticleMetaKey)
                    article.ArticleMetaKeys.Add(new ArticleMetaKey {Key = item.Key, Value = item.Value});
                //foreach (var item in articlesInputViewModel.ArticleSections)
                //    article.ArticleSections.Add(new ArticleSection { SectionId = item, });
                await _articlesManagementRepository.AddAsync(article, cancellationToken);
                ArticleResultViewModel InsertResultArticle = _mapper.Map<ArticleResultViewModel>(article);
                transaction.Complete();

                return InsertResultArticle;
            }
        }

        public async Task<bool> Delete(long ID, CancellationToken cancellationToken)
        {
            var ArticalData = await _articlesManagementRepository.GetByIdAsync(cancellationToken, ID);
            if (ArticalData == null)
                throw new CustomException("بلاگ");

            ArticalData.IsDeleted = true;
            await _articlesManagementRepository.UpdateAsync(ArticalData, cancellationToken);
            // var articleSetions = await _ArticleSectionRepository.GetAllArticleByIDFromArticleSection(cancellationToken, ID);
            // foreach (var item in articleSetions)
            // await _ArticleSectionRepository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteMine(long UserID, long ID, CancellationToken cancellationToken)
        {
            var ArticalData = await _articlesManagementRepository.GetByIdAsync(cancellationToken, ID);
            if (ArticalData == null)
                throw new CustomException("بلاگ");
            await CheckAuthorByUserID(UserID, ArticalData.AuthorId, cancellationToken);
            ArticalData.IsDeleted = true;
            await _articlesManagementRepository.UpdateAsync(ArticalData, cancellationToken);
            // var articleSetions = await _ArticleSectionRepository.GetAllArticleByIDFromArticleSection(cancellationToken, ID);
            // foreach (var item in articleSetions)
            // await _ArticleSectionRepository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<ArticleResultViewModel> Update(long ID, ArticlesInputViewModel BlogsEditeViewModel,
            CancellationToken cancellationToken)
        {
            return await UpdateCommon(ID, BlogsEditeViewModel, cancellationToken);
        }

        public async Task<PagedResult<ArticleSummaryViewModel>> all(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            var pages = await _articlesManagementRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.ArticleTypeId == 2, // is article
                i => i.ArticleCategories,
                i => i.ArticleMetaKeys
            );

            return _mapper.Map<PagedResult<ArticleSummaryViewModel>>(pages);
        }


        public async Task<ArticleResultViewModel> detailV2(long ID, CancellationToken cancellationToken)
        {
            try
            {
                var GetArticleByID = await _articlesManagementRepository.GetArticlesByID(ID, cancellationToken);
                if (GetArticleByID == null)
                    throw new CustomException("موردی یافت نشد");
                ArticleResultViewModel _articlesDetailsOutputViewModel =
                    _mapper.Map<ArticleResultViewModel>(GetArticleByID);
                return _articlesDetailsOutputViewModel;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<ArticleResultViewModel> detailMine(long UserID, long ID, CancellationToken cancellationToken)
        {
            try
            {
                var GetArticleByID = await _articlesManagementRepository.GetArticlesByID(ID, cancellationToken);
                if (GetArticleByID == null)
                    throw new CustomException("موردی یافت نشد");
                await CheckAuthorByUserID(UserID, GetArticleByID.AuthorId, cancellationToken);
                ArticleResultViewModel _articlesDetailsOutputViewModel =
                    _mapper.Map<ArticleResultViewModel>(GetArticleByID);
                return _articlesDetailsOutputViewModel;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<List<ArticleResultViewModel>> GetBySection(int id, CancellationToken cancellationToken)
        {
            var articles = await _ArticleSectionRepository.GetArticleidByArticleSectionID(cancellationToken, id);
            return _mapper.Map<List<ArticleResultViewModel>>(articles);
        }

        public async Task<bool> DeleteArticleMetakey(long articleId, long ArticleMetaKeyId,
            CancellationToken cancellationToken)
        {
            Article ArticalData = await _articlesManagementRepository.GetByIdAsync(cancellationToken, articleId);
            if (ArticalData == null)
                throw new CustomException("بلاگ");
            ArticleMetaKey ArticleMetakey =
                await _ArticleMetaKeyRepository.GetByIdAsync(cancellationToken, ArticleMetaKeyId);
            if (ArticleMetakey == null)
                throw new CustomException("Metakey NotFount");
            ArticleMetakey.IsDeleted = true;
            await _ArticleMetaKeyRepository.UpdateAsync(ArticleMetakey, cancellationToken);
            return true;
        }

        public async Task<ArticleMetaKeyOutputViewModel> UpdateArticleMetakey(long articleId, long articleMetaKeyId,
            ArticleMetaKeyViewModel ArticleMetakeyinputViewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Article ArticalData = await _articlesManagementRepository.GetByIdAsync(cancellationToken, articleId);
                if (ArticalData == null)
                    throw new CustomException("بلاگ");
                ArticleMetaKey ArticleMetakey =
                    await _ArticleMetaKeyRepository.GetByIdAsync(cancellationToken, articleMetaKeyId);
                if (ArticleMetakey == null)
                    throw new CustomException("Metakey NotFount");
                ArticleMetakey.Key = ArticleMetakeyinputViewModel.Key;
                ArticleMetakey.Value = ArticleMetakeyinputViewModel.Value;
                await _ArticleMetaKeyRepository.UpdateAsync(ArticleMetakey, cancellationToken);
                transaction.Complete();
                return _mapper.Map<ArticleMetaKeyOutputViewModel>(ArticleMetakey);
            }
        }

        public async Task<ArticleResultViewModel> GetArticleBySlug(string Slug, CancellationToken cancellationToken)
        {
            Article article = await _articlesManagementRepository.GetPageBySlug(Slug, cancellationToken);
            if (article == null)
            {
                throw new BadRequestException("مقاله وجود ندارد");
            }

            ArticleResultViewModel MapResult = _mapper.Map<ArticleResultViewModel>(article);
            return MapResult;
        }

        public async Task<bool> DeleteArticleCategory(long articleId, long categoryId,
            CancellationToken cancellationToken)
        {
            Article article = await _articlesManagementRepository.GetByIdAsync(cancellationToken, articleId);
            if (article == null)
            {
                throw new BadRequestException("مقاله وجود ندارد");
            }

            Category category = await _categoryRepository.GetByIdAsync(cancellationToken, categoryId);
            if (article == null)
            {
                throw new BadRequestException("دسته ای وجود ندارد");
            }

            ArticleCategory articleCategory =
                await _articleCategoryRepository.GetByArticleAndCategoryId(articleId, categoryId, cancellationToken);
            if (articleCategory == null)
            {
                throw new BadRequestException("دسته مقاله ای وجود ندارد");
            }

            articleCategory.IsDeleted = true;

            await _articleCategoryRepository.UpdateAsync(articleCategory, cancellationToken);

            return true;
        }

        public async Task<PagedResult<ArticleLatestResultViewModel>> GetTopFiveArticles(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<Article> articles =
                await _articlesManagementRepository.GetTopFiveArticles(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<ArticleLatestResultViewModel>>(articles);
        }

        public async Task<ArticleResultViewModel> UpdateMine(long UserID, long ID,
            ArticlesInputViewModel ArticleEditeViewModel, CancellationToken cancellationToken)
        {
            return await UpdateCommon(ID, ArticleEditeViewModel, cancellationToken);
            // using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            // {
            //     try
            //     {
            //
            //         var ArticalData = await _articlesManagementRepository.GetByIdAsync(cancellationToken, ID);
            //         if (ArticalData == null)
            //             throw new CustomException("بلاگ یافت نشد");
            //         await CheckAuthorByUserID(UserID, ArticalData.AuthorId, cancellationToken);
            //         ArticalData.Description = ArticleEditeViewModel.Description;
            //         ArticalData.Summary = ArticleEditeViewModel.Summary;
            //         ArticalData.Title = ArticleEditeViewModel.Title;
            //
            //         await _articlesManagementRepository.UpdateAsync(ArticalData, cancellationToken);
            //
            //         var articleSetions = await _ArticleSectionRepository.GetAllArticleByIDFromArticleSection(cancellationToken, ID);
            //         foreach (var item in articleSetions)
            //             await _ArticleSectionRepository.DeleteAsync(item, cancellationToken);
            //
            //         //foreach (var item in ArticleEditeViewModel.ArticleSections)
            //         //    await _ArticleSectionRepository.AddAsync(new DAL.Models.ArticleSection { SectionId = item, ArticleId = ID }, cancellationToken);
            //
            //         transaction.Complete();
            //
            //         return _mapper.Map<ArticleResultViewModel>(ArticalData);
            //     }
            //     catch (Exception ex)
            //     {
            //         throw new CustomException(ex.Message);
            //     }
            // }
        }

        public async Task CheckAuthorByUserID(long UserID, long? AuthorID, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, UserID);
            if (user == null)
                throw new NotFoundException("کاربری یافت نشد");
            if (AuthorID != user.PersonId)
                throw new NotFoundException("شما دسترسی لازم برای این متد را ندارید");
        }


        public async Task<ArticleResultViewModel> UpdateCommon(long ID, ArticlesInputViewModel BlogsEditeViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var articleData = await _articlesManagementRepository.GetArticlesByID(ID, cancellationToken);
                    if (articleData == null)
                        throw new CustomException("بلاگ یافت نشد");
                    articleData.Description = BlogsEditeViewModel.Description;
                    articleData.Summary = BlogsEditeViewModel.Summary;
                    articleData.Title = BlogsEditeViewModel.Title;



                    //category
                    await _articleCategoryRepository.DeleteRangeAsync(articleData.ArticleCategories, cancellationToken);

                    foreach (long item in BlogsEditeViewModel.CategoryIds)
                    {
                        var category = await _categoryRepository.GetByIdAsync(cancellationToken, item);
                        if (category != null)
                        {
                            articleData.ArticleCategories.Add(new ArticleCategory()
                            {
                                Category = category
                            });
                        }
                    }


                    //meta_key
                    await _ArticleMetaKeyRepository.DeleteRangeAsync(articleData.ArticleMetaKeys, cancellationToken);
                    foreach (var item in BlogsEditeViewModel.ArticleMetaKey)
                    {
                        
                            articleData.ArticleMetaKeys.Add(new ArticleMetaKey()
                            {
                                Key = item.Key,
                                Value = item.Value,
                            });
                        
                    }

                    await _articlesManagementRepository.UpdateAsync(articleData, cancellationToken);

                    transaction.Complete();

                    return _mapper.Map<ArticleResultViewModel>(articleData);
                }
                catch (Exception ex)
                {
                    throw new CustomException(ex.Message);
                }
            }
        }
    }
}