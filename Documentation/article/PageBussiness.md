<div align="right" dir="rtl">

عملیات CRUD جدول Page بصورت زیر پیاده سازی شده. 

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

       [HttpGet("slug/{slug}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetArticleBySlug(string slug, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.GetArticleBySlug(slug, cancellationToken);
            return SlugArticle;
        }
        
        [HttpGet("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetById(long id, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.GetArticleById(id, cancellationToken);
            return SlugArticle;
        }

        
        
        [HttpPost("")]
        public async Task<ApiResult<ArticleResultViewModel>> CreatePage([FromBody] PageInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.create(articlesInputViewModel, cancellationToken);
            return SlugArticle;
        }
        
        
        
        
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ArticleSummaryViewModel>>> GetAll( [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.allPages(pageAbleResult, cancellationToken);
            return SlugArticle;
        }
        
        
        
        
        [HttpPut("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> UpdatePage(long id,[FromBody] PageInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.Update(id,articlesInputViewModel, cancellationToken);
            return SlugArticle;
        }
        
        
        
        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> DeletePage(long id, CancellationToken cancellationToken)
        {
            var SlugArticle = await _blogService.Delete(id, cancellationToken);
            return SlugArticle.ToString();
        


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `create(articlesInputViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

       public async Task<ArticleResultViewModel> create(PageInputViewModel articlesInputViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                

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

```


<div align="right" dir="rtl">

پس از بررسی موجودیت شرکت، ویومدل ورودی به مدل اصلی مپ شده و درج انجام میگیرد.

<br>

ویومدل ورودی این سرویس : 

</div>

```C#

  public class PageInputViewModel
    {
        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("summary")] public string Summary { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("priority")] public byte? Priority { get; set; }
        [Required]
        [JsonPropertyName("slug")] 
        public string Slug { get; set; }

        /// <summary>
        /// 1:UI
        /// 2:Users Panel
        /// 3:Admins Panel
        /// </summary>
        // [JsonPropertyName("article_section")]
        // public List<int> ArticleSections { get; set; }

        [JsonPropertyName("article_meta_key")] public List<ArticleMetaKeyViewModel> ArticleMetaKey { get; set; }

        // [JsonPropertyName("author_code")] public Guid AuthorCode { get; set; }

        // [JsonPropertyName("category_ids")] public virtual List<long> CategoryIds { get; set; }

    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `Update(id,articlesInputViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#
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



```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetArticleById(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `allPages(pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#
 public async Task<PagedResult<ArticleSummaryViewModel>> allPages(PageAbleResult pageAbleResult, 
            CancellationToken cancellationToken)
        {

            
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            var pages = await _articlesManagementRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel,
                x => x.ArticleTypeId==1, // is page
                i => i.ArticleCategories,
                i => i.ArticleMetaKeys
            );
            
            
            
            
            
            return _mapper.Map<PagedResult<ArticleSummaryViewModel>>(pages);
        }

```


<div align="right" dir="rtl">

**دریافت بر اساس کلمه کلیدی (سرویس Get)** : این سرویس متد `GetArticleBySlug(slug, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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


```




<div align="right" dir="rtl">

**حذف (سرویس Delete)** : این سرویس متد `Delete(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

