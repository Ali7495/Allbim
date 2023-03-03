<div dir="rtl" align="right">

# فرایند تماس با ما
هدف این فرایند، برقراری ارتباط مخاطب با سامانه بدون احراز هویت می باشد. 


## دیاگرام ER

![contact us erd](./contact-us.bmp)

## دیاگرام توالی

<div dir="ltr" align="left">

```mermaid
sequenceDiagram
Visitor ->> Allbim :Send Contact Us
Allbim ->> Visitor: Tracking Code
Allbim ->> Visitor: Answer To Contact Us (Via mail, phone, etc.)
```
</div>

نکته ای که حائز اهمیت است، به علت عدم احراز هویت مخاطب، جواب باید بصورت ایمیل، پیامک یا بصورت تلفنی به مخاطب ارائه شود.