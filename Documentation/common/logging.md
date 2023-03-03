<div dir="rtl" align="right">

# ساختار لاگ گیری
در پروژه فعلی، سه مدل لاگ گیری وجود دارد.
## 1. لاگ عملکرد برنامه 
در این مدل، یک میدلور با نام LogMiddleware تعریف شده است تا از جزئیات درخواست ورودی و پاسخ آن لاگ بگیرد و در جدول OperationLog درج کند.

## 2. لاگ خطای هندل شده برنامه
یک میدلور با نام CustomExceptionHandlerMiddleware تعریف شده است تا خطایی که از سمت کد برمی گردد را پردازش کند و پاسخ مناسب به کاربر برگرداند. در اینجا اگر خطا از جنس کلاس های تعریف شده توسط ما باشد (مانند BadRequestException، AppException) خطا در جدول HandledError درج می شود.

## 3. لاگ خطای هندل نشده
اگر خطای برگشت داده شده توسط برنامه نویس هندل نشده باشد در جدول SystemErrorLog درج خواهد شد.


همچنین لازم بذکر است برای هرکدام از این جداول، یک سرویس GET در کنترلر Log وجود دارد تا از طریق api نیز به داده های جداول دسترسی داشته باشیم. البته دیدن داده ها فقط با نقش admin امکان پذیر می باشد.


<div  dir="ltr"  align="left">

```c#
[HttpGet("HandledError")]  
public async Task<ApiResult<PagedResult<HandledErrorLogOutputViewModel>>> AllHandledErrorPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)  
{  
  long UserId = long.Parse(HttpContext.User.Identity.GetUserId());  
  var claims = HttpContext.User.Claims.ToList();  
  var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();  
  if (UserRole == null)  
  throw new BadRequestException("شما نقشی در این سیستم ندارید");  
  long RoleId = long.Parse(UserRole);  
  var HandledErrorLogResult = await _logsService.GetAllPagedResult_HandledError(pageAbleResult,RoleId , UserId, cancellationToken);  
  return HandledErrorLogResult;  
}  
[HttpGet("Operation")]  
public async Task<ApiResult<PagedResult<OprationLogOutputViewModel>>> AllOperationLogPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)  
{  
  long UserId = long.Parse(HttpContext.User.Identity.GetUserId());  
  var claims = HttpContext.User.Claims.ToList();  
  var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();  
  if (UserRole == null)  
  throw new BadRequestException("شما نقشی در این سیستم ندارید");  
  long RoleId = long.Parse(UserRole);  
  var OperationLogResult = await _logsService.GetAllPagedResult_Operation(pageAbleResult,RoleId , UserId, cancellationToken);  
  return OperationLogResult;  
}  
[HttpGet("SystemError")]  
public async Task<ApiResult<PagedResult<SystemErrorLogOutputViewModel>>> AllSystemErrorPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)  
{  
  long UserId = long.Parse(HttpContext.User.Identity.GetUserId());  
  var claims = HttpContext.User.Claims.ToList();  
  var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();  
  if (UserRole == null)  
  throw new BadRequestException("شما نقشی در این سیستم ندارید");  
  long RoleId = long.Parse(UserRole);  
  var SystemErrorLogResult = await _logsService.GetAllPagedResult_SystemError(pageAbleResult, RoleId, UserId, cancellationToken);  
  return SystemErrorLogResult;  
}

```
</div>