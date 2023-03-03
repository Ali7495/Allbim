<div dir="rtl" align="right">

# سرویس های Enumeration
در اصل هسته تمام سرویس های Enumeration یکسان می باشد اما برای راحتی تفکیک هر کدام در آینده، آدرس، اکشن، متد سرویس، ریپوزیتوری جداگانه ساخته شده است که همگی از ریپوزیتوری زیر ارث بری می کنند:
<div dir="ltr" align="left">

```c#
    public class EnumRepository : Repository<Enumeration>, IEnumRepository 
    {
        public EnumRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {

        }
        public Task<List<Enumeration>> GetEnumsBytype(string type, CancellationToken cancellationToken)
        {
            return Table.Where(e => e.CategoryName == type).OrderBy(x=>x.Order).ToListAsync(cancellationToken);
        }
    }

```
</div>

نمونه ریپوزیتوری ارث بری شده:

<div dir="ltr" align="left">

```c#
 public class DriverRepository : EnumRepository, IDriverRepository
    {
        public DriverRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Enumeration>> Get(CancellationToken cancellation)
        {
            return GetEnumsBytype("DriverDamage", cancellation);
        }
```
</div>
لازم بذکر است که تمامی ریپوزیتوری های Enumeration در مسیر DAL/Repositories/EnumRepositories ذخیره شده است.
