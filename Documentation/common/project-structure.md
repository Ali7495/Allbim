<div dir="rtl" align="right">

# ساختار کلی پروژه
## Models
در این لایه، تمام ویومدل های خروجی سیستم تعریف شده است که در لایه های بالاتر استفاده می شود.
## Common
در این پروژه موارد مشترک پروژه ماننده extensions, exceptions, ApiResultStatusCodes و ... تعریف شده است تا در لایه های دیگر پروژه استفاده شود.
## DAL
در این لایه context دیتابیس ، موجودیت ها، ریپوزیتوری ها و اینترفیس ها تعریف شده است.
همچنین در پوشه Context علاوه بر AlbimDbContext، دو فایل PagedExtensions و QueryFilter .نیز وجود دارد
- PagedExtensions کلیه متدهای لازم برای Paging در این فایل تعریف شده است. برای توضیحات بیشتر به فایل [ساختار صفحه بندی](./paging.md) مراجعه نمایید.
- QueryFilter برای تعریف کوئری های دیفالت برای موجودیت ها استفاده شده است. در اینجا برای موجودیت هایی که فیلد IsDeleted دارند. شرط False بودن آن اضافه شده است. برای اطلاعات بیشتر به فایل  [حذف منطقی موجودیت ها](./soft-delete.md) مراجعه نمایید.

## Logging
در این لایه هم مشابه DAL فایل های مربوط به Context, ریپوزیتوری و موجودیت ها ذخیره شده است. با این تفاوت که در اینجا دیتابیس AlbimLogDb می باشد.
برای توضیحات بیشتر در مورد نحوه لاگ گیری  به فایل [ساختار لاگ](./logging.md) مراجعه نمایید.


## Services
در این لایه فایل های مربوط به Serivce ها قرار می گیرد. 
سرویس های در اصل رابط بین اکشن کنترلرها و ریپوزیتوری می باشند. همچنین علاوه بر سرویس ها، فایل Mapper و Pipeline در این قسمت موجود می باشد.
- Mapper: وظیفه نگاشت بین موجودیت و کلاس های ViewModel واقع در لایه Model را برعهده دارد.
- PipeLine: مکانیزم پایپلاین محاسبه قیمت بیمه که کلاس های آن در این لایه تعریف شده است. برای توضیحات بیشتر به فایل [محاسبه قیمت بیمه](./logging.md) مراجعه نمایید. 

## AllbimDbUp
در این لایه با استفاده از DbUp دیتابیس های پروژه را بروز می کنیم.

## Albim
پروژه اصلی که در آن کنترلرها و میدلور و اکشن فیلترها تعریف شده است.

## SQL
پروژه خروجی دیتابیس بصورت SQL

## AlbimTest
در این پروژه، تست ها تعریف و اجرا می شوند.


## ترتیب Build پرژوه
![enter image description here](./build%20order.PNG)