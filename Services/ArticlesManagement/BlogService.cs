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
    public class BlogService : IBlogService
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

        public BlogService(IUserRepository userRepository, IArticleSectionRepository articleSectionRepository,
            IArticlesManagementRepository blogsManagementRepository, IArticleMetaKeyRepository articleMetaKeyRepository,
            IOptionsSnapshot<PagingSettings> pagingSettings, IMapper mapper, IPersonRepository personRepository,
            ICategoryRepository categoryRepository, IArticleCategoryRepository articleCategoryRepository)
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


        public async Task<ArticleResultViewModel> create(PageInputViewModel articlesInputViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Person person =
                //     await _personRepository.GetByCodeNoTracking(articlesInputViewModel.AuthorCode, cancellationToken);
                // if (person == null)
                // {
                //     throw new BadRequestException("کد شخص وجود ندارد");
                // }

                var exists =
                    await _articlesManagementRepository.GetPageBySlug(articlesInputViewModel.Slug,
                        cancellationToken);
                if (exists != null)
                {
                    throw new BadRequestException("آدرس صفحه تکراری است");
                }

                Article article = new Article
                {
                    AuthorId = 6, // مدیر سیستم
                    CreatedDateTime = DateTime.Now,
                    Description = articlesInputViewModel.Description,
                    Priority = articlesInputViewModel.Priority,
                    Summary = articlesInputViewModel.Summary,
                    Title = articlesInputViewModel.Title,
                    Slug = articlesInputViewModel.Slug,
                    ArticleTypeId = 1 // is page
                };
                
                //category
                
                // foreach (long item in articlesInputViewModel.CategoryIds)
                // {
                //     var category = await _categoryRepository.GetByIdAsync(cancellationToken, item);
                //     if (category != null)
                //     {
                //         article.ArticleCategories.Add(new ArticleCategory()
                //         {
                //             Category = category
                //         });
                //     }
                // }

                foreach (var item in articlesInputViewModel.ArticleMetaKey)
                    article.ArticleMetaKeys.Add(new ArticleMetaKey {Key = item.Key, Value = item.Value});
                // foreach (var item in articlesInputViewModel.ArticleSections)
                //     article.ArticleSections.Add(new ArticleSection {SectionId = item,});
                await _articlesManagementRepository.AddAsync(article, cancellationToken);
                ArticleResultViewModel InsertResultArticle = _mapper.Map<ArticleResultViewModel>(article);
                transaction.Complete();

                return InsertResultArticle;
            }
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var ArticalData = await _articlesManagementRepository.GetPageByIdWithRelation(id,cancellationToken);
            if (ArticalData == null)
                throw new NotFoundException("صفحه وجود ندارد");

            ArticalData.IsDeleted = true;
            await _articlesManagementRepository.UpdateAsync(ArticalData, cancellationToken);
            // var articleSetions = await _ArticleSectionRepository.GetAllArticleByIDFromArticleSection(cancellationToken, ID);
            // foreach (var item in articleSetions)
            //     await _ArticleSectionRepository.DeleteAsync(item, cancellationToken);
            return true;
        }

        public async Task<ArticleResultViewModel> Update(long id, PageInputViewModel BlogsEditeViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var articleData = await _articlesManagementRepository.GetPageByIdWithRelation(id,cancellationToken);
                if (articleData == null)
                    throw new NotFoundException("صفحه وجود ندارد");
                
                
                
                var exists =
                    await _articlesManagementRepository.GetPageBySlug(BlogsEditeViewModel.Slug,
                        cancellationToken);
                if (exists != null && exists.Id != articleData.Id)
                {
                    throw new BadRequestException("آدرس صفحه تکراری است");
                }
                
                
                articleData.Description = BlogsEditeViewModel.Description;
                articleData.Priority = BlogsEditeViewModel.Priority;
                articleData.Summary = BlogsEditeViewModel.Summary;
                articleData.Title = BlogsEditeViewModel.Title;
                articleData.Slug = BlogsEditeViewModel.Slug;




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
        }

        public async Task<PagedResult<ArticleSummaryViewModel>> allPages(PageAbleResult pageAbleResult, 
            CancellationToken cancellationToken)
        {

            // var Articles =
            //     await _articlesManagementRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            //
            //
            
            
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            var pages = await _articlesManagementRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.ArticleTypeId==1, // is page
                i => i.ArticleCategories,
                i => i.ArticleMetaKeys
            );
            
            
            
            
            
            return _mapper.Map<PagedResult<ArticleSummaryViewModel>>(pages);
        }

        public async Task<ArticleResultViewModel> GetArticleBySlug(string Slug, CancellationToken cancellationToken)
        {
            Article article = await _articlesManagementRepository.GetPageBySlug(Slug, cancellationToken);
            if (article == null)
            {
                throw new BadRequestException("صفحه وجود ندارد");
            }

            ArticleResultViewModel MapResult = _mapper.Map<ArticleResultViewModel>(article);
            return MapResult;
        }
        public async Task<ArticleResultViewModel> GetArticleById(long id, CancellationToken cancellationToken)
        {
            Article article = await _articlesManagementRepository.GetPageById(id, cancellationToken);
            if (article == null)
            {
                throw new BadRequestException("صفحه وجود ندارد");
            }

            ArticleResultViewModel MapResult = _mapper.Map<ArticleResultViewModel>(article);
            return MapResult;
        }
    }
}