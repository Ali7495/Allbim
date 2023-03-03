<div align="right" dir="rtl">

عملیات CRUD جدول Article بصورت زیر پیاده سازی شده. 

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

        [HttpPost("")]
        public async Task<ApiResult<ArticleResultViewModel>> Create([FromBody] ArticlesInputViewModel articlesInputViewModel, CancellationToken cancellationToken)
        {
            ArticleResultViewModel ArticalData = await _articlesmanagementservice.create(articlesInputViewModel, cancellationToken);
            return ArticalData;
        }

         [HttpGet("")]
        public async Task<ApiResult<PagedResult<ArticleSummaryViewModel>>> GetAll([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var ArticlesList = await _articlesmanagementservice.all(pageAbleResult, cancellationToken);
            return ArticlesList;
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var company = await _articlesmanagementservice.detailV2(id, cancellationToken);
            return company;
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<ArticleResultViewModel>> Update(long id, [FromBody] ArticlesInputViewModel BlogsEditeViewModel, CancellationToken cancellationToken)
        {
            ArticleResultViewModel ArticalData = await _articlesmanagementservice.Update(id, BlogsEditeViewModel, cancellationToken);
            return ArticalData;
        }


        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            var res = await _articlesmanagementservice.Delete(id, cancellationToken);
            return res.ToString();
        }

        [HttpGet("latest")]
        public async Task<ApiResult<PagedResult<ArticleLatestResultViewModel>>> GetTopFive([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _articlesmanagementservice.GetTopFiveArticles(pageAbleResult, cancellationToken);
        }


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `create(articlesInputViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```


<div align="right" dir="rtl">

پس از بررسی موجودیت شرکت، ویومدل ورودی به مدل اصلی مپ شده و درج انجام میگیرد.

<br>

ویومدل ورودی این سرویس : 

</div>

```C#

 public class ArticlesInputViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("article_meta_key")]
        public List<ArticleMetaKeyViewModel> ArticleMetaKey { get; set; }
        
        [JsonPropertyName("author_code")]
        public Guid AuthorCode { get; set; }

        [JsonPropertyName("category_ids")]
        public virtual List<long> CategoryIds { get; set; }

    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `Update(id, BlogsEditeViewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#
 public async Task<ArticleResultViewModel> Update(long ID, ArticlesInputViewModel BlogsEditeViewModel,
            CancellationToken cancellationToken)
        {
            return await UpdateCommon(ID, BlogsEditeViewModel, cancellationToken);
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



```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `detailV2(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

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

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `all(pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```


<div align="right" dir="rtl">

**دریافت آخرین مقالات (سرویس Get)** : این سرویس متد `GetTopFiveArticles(pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

 public async Task<PagedResult<ArticleLatestResultViewModel>> GetTopFiveArticles(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<Article> articles =
                await _articlesManagementRepository.GetTopFiveArticles(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<ArticleLatestResultViewModel>>(articles);
        }



```




<div align="right" dir="rtl">


**حذف (سرویس Delete)** : این سرویس متد `Delete(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

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

```

