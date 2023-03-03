<div dir="rtl" align="right">

# ساختار صفحه بندی

ساختار صفحه بندی به دو صورت مختلف پیاده سازی شده است:

## 1. دریافت pageو pageSize و orderBy

در این روش، از ابتدا سه پارامتر فوق بصورت QueryParameter دریافت می شود و در لایه Repository به متد GetPagedAsync ارسال می شود. این متد یک Extension در DAL/Context می باشد که سه پارامتر query ,page, pagesize را می گیرد و کوئری را با رعایت صفحه بندی شده برمی گرداند.
در صورتی که orderBy خالی باشد کد زیر فراخوانی می شود:

<div dir="ltr" align="left">

```c#
public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken) where T : class
{
  var result = new PagedResult<T>();
 result.CurrentPage = page;
 result.PageSize = pageSize;
 result.RowCount = await query.CountAsync();
  int skip = GetDataSkip(page, pageSize, result);

 result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

  return result;
}
private static int GetDataSkip<T>(int page, int pageSize, PagedResult<T> result) where T : class
{
  var pageCount = (double)result.RowCount / pageSize;
 result.PageCount = (int)Math.Ceiling(pageCount);

  var skip = (page - 1) * pageSize;
  return skip;
}
```

</div>

در صورتی که orderBy خالی نباشد کد زیر فراخوانی می شود:

<div dir="ltr" align="left">

```c#
public static async Task<PagedResult<T>> GetOrderedPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, string orderby, CancellationToken cancellationToken) where T : class
{
  var result = new PagedResult<T>();
 result.CurrentPage = page;
 result.PageSize = pageSize;
 result.RowCount = await query.CountAsync();
  int skip = GetDataSkip(page, pageSize, result);


  string orderMode;
  Expression<Func<T, object>> orderByFunc;
  GetOrderByDetails(orderby, out orderMode, out orderByFunc);

  if (orderMode.ToLower() == "desc")
 query = query.OrderByDescending(orderByFunc);
  else
  query = query.OrderBy(orderByFunc);

 result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

  return result;
}
private static void GetOrderByDetails<T>(string orderby, out string orderMode, out Expression<Func<T, object>> orderByFunc) where T : class
{
  var orderInfo = orderby.Split(",");
  var orderByPropName = orderInfo.FirstOrDefault().ToPascalCase();
 orderMode = orderInfo.LastOrDefault();
  var parameter = Expression.Parameter(typeof(T));
  var property = Expression.Property(parameter, orderByPropName);
  var propAsObject = Expression.Convert(property, typeof(object));
 orderByFunc = Expression.Lambda<Func<T, object>>(propAsObject, parameter);
}
```

</div>

## 2. دریافت ویومدل PageAbleResult

این ویومدل در اکشن کنترلر بصورت [FromQuery] دریافت شده و سپس در لایه سرویس به کلاس PageAbleModel نگاشت می شود.
بدلیل اینکه پروژه های Common و Model با هم ارتباطی ندارند برای حفظ قاعده کلی سیستم ناچار به این تغییر شدیم.

<div dir="ltr" align="left">

```c#
public async Task<PagedResult<PublicFaqResultViewModel>> GetAllPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
{
  PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

  PagedResult<Faq> FAQs = await _publicFaqRepository.GetAll( pageAbleModel, cancellationToken);

  return _mapper.Map<PagedResult<PublicFaqResultViewModel>>(FAQs);
}
```

</div>

سپس در لایه ریپوزیتوری از متد GetNewPagedAsync استفاده می کنیم.

<div dir="ltr" align="left">

```c#
public static async Task<PagedResult<T>> GetNewPagedAsync<T>(this IQueryable<T> query,CancellationToken cancellationToken, PageAbleModel pageAbleModel, Expression<Func<T, bool>> condition,params Expression<Func<T,object>>[] includes) where T : class
{
  var result = new PagedResult<T>();
  if (includes != null)
 {  foreach (var include in includes)
 {  var memberExpression = include.Body as MemberExpression;
  if (memberExpression != null)
 query = query.Include(memberExpression.Member.Name);
 } }  if (condition != null)
 { query = query.Where(condition);
 }     result.CurrentPage = pageAbleModel.PageNumber;
 result.PageSize = pageAbleModel.PageSize;
 result.RowCount = await query.CountAsync();
 result.TotalCount = result.RowCount;
  if (pageAbleModel.SortField != null)
 {  string orderMode=pageAbleModel.SortOrder;
  string orderby = pageAbleModel.SortField.ToPascalCase();
  Expression<Func<T, object>> orderByFunc;
  GetNewOrderByDetails(orderby, orderMode, out orderByFunc);
  if (orderMode.ToLower() == "desc")
 query = query.OrderByDescending(orderByFunc);
  else
  query = query.OrderBy(orderByFunc);
 }  if (pageAbleModel.AllowPaginate)
 {  int skip = GetDataSkip(pageAbleModel.PageNumber, pageAbleModel.PageSize, result);
 result.Results = await query.Skip(skip).Take(pageAbleModel.PageSize).ToListAsync(cancellationToken);
 }  else
  {
 result.Results = await query.ToListAsync(cancellationToken);
 }

  return result;
}

private static void GetNewOrderByDetails<T>(string orderby, string orderMode, out Expression<Func<T, object>> orderByFunc) where T : class
{
  var parameter = Expression.Parameter(typeof(T));
  var property = Expression.Property(parameter, orderby);
  var propAsObject = Expression.Convert(property, typeof(object));
 orderByFunc = Expression.Lambda<Func<T, object>>(propAsObject, parameter);
}
```

</div>

در روش فوق، فیلد order هم داخل یک متد هندل می شود و لزومی به ایجاد متد جداگانه نیست. همچنین امکان شرط گذاری بر روی موجودیت و دریافت ارتباطات آن نیز فراهم می باشد.

ساختار 1 در ابتدای پروژه ایجاد شده است و نسخه بهینه آن ساختار 2 می باشد که بمرور زمان، ریفکتور صورت می گیرد.
