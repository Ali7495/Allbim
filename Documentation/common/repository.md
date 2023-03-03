<div dir="rtl" align="right">

# ساختار Repository

فایل های ریپوزیتوری هر موجودیت در فولدر Repositories پروژه DAL ذخیره می شوند اما Interface آن در فولدر Contracts موجود می باشد.
تمام ریپوزیتوری ها از یک کلاس جنریک Repository ارث بری می کنند که متدهای عمومی مانند GetAsync, UpdateAsync, AddAsync, DeleteAsync تعریف شده است. در صورت لزوم برای سفارشی سازی این متدها و یا درج متدهای جدید در فایل ریپوزیتوری مربوط به آن موجودیت تغییرات را لحاظ می کنیم.

نمونه یک ریپوزیتوری

<div  dir="ltr"  align="left">

```c#
namespace DAL.Repositories
{
  public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
  {
  public ArticleCategoryRepository(AlbimDbContext dbContext) : base(dbContext)
 { }

  public async Task<ArticleCategory> GetByArticleAndCategoryId(long articleId, long categoryId, CancellationToken cancellationToken)
 {  return await Table.AsNoTracking().Where(ac => ac.ArticleId == articleId && ac.CategoryId == categoryId && ac.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
 }

  public async Task<List<ArticleCategory>> GetByArticleId(long articleId, CancellationToken cancellationToken)
 {  return await Table.AsNoTracking().Where(ac => ac.ArticleId == articleId ).ToListAsync(cancellationToken);
 }
}
}
```

</div>


لازم بذکر است بغیر از متدهای کلی EF، یک متد دیگر با نام GetAsyncAdvanced به این کلاس اضافه شده که امکان صفحه بندی، شرط گذاری و دریافت ارتباطات آن موجودیت را فراهم می کند.
برای توضیح بیشتر به فایل [ساختار پروژه](./project-structure.md) مراجعه نمایید. 

<div  dir="ltr"  align="left">

```c#
public async Task<PagedResult<TEntity>> GetAsyncAdvanced(CancellationToken cancellationToken,
            PageAbleModel pageAbleModel,
            Expression<Func<TEntity, bool>> condition,
            params Expression<Func<TEntity,object>>[] includes)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken,pageAbleModel,condition,includes);
        }

```
</div>


برای آشنایی با نحوه ساختار موجودیت ها و ریپوزیتوری به فایل [ساختار صفحه بندی](./paging.md) مراجعه نمایید.

