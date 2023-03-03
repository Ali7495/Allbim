<div dir="rtl" align="right">

# Server socket api | رابط نرم افزاری چت

جهت برقراری ارتباط کلاینت با سرور از سوکت (SignalR Core) استفاده می شود.
لطفا اگر با SignalR Core آشنایی ندارید، از
[اینجا](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0)
مستندات آن را قبل از مطالعه این بخش، نگاه کنید.

> دز این مستند کلمات متد، دستور و تابع به جای هم استفاده شده اند.

## Server Methods
توابعی که از سمت کلاینت میتوان در سرور صدا زد.

### Ping() -> `Void`
کلاینت این متد را صدا می زند و سرور برای کلاینت
کامند `Pong` را ارسال می کند.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.
---
### Pong() -> `Void`
سرور با ارسال
دستور `Ping`
سمت کلاینت، منتظر می ماند تا کلاینت این دستور را
در سرور صدا بزند.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.
---
### Authorize([RequestAuthorizationModel](#requestauthorizationmodel)) -> `Void`

در صورتی که یک ارتباط
stable
با کلاینت برقرار باشد، کلاینت
می تواند با ارسال
token و identityId،
احراز هویت خود را از طریق این دستور، کامل کند.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.
---
### GetConversationInstanceMessages([GetConversationInstanceMessagesParameters](#getconversationinstanceMessagesparameters)) -> List of [ClientMessageModel](#clientmessagemodel)

دریافت لیست تمام پیام های یک مکالمه.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### SendMessageToConversation([SendMessageToConversationParameters](#sendmessagetoconversationparameters)) -> [ClientMessageModel](#clientmessagemodel)

ارسال یک پیام در یک مکالمه.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### SeenMessage([SeenMessageParameters](#seenmessageparameters)) -> `Void`

علامت گذاری یک پیام به عنوان خوانده شده.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### SetConversationTypingStatus([SetConversationTypingStatusParameters](#setconversationtypingstatusparameters)) -> `Void`

کلاینت می تواند با استفاده از این متد به بقیه افراد داخل مکالمه
اطلاع دهد که او در حال تایپ می باشد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### GetConversationList([GetConversationListParameters](#getconversationlistparameters)) -> List of [ClientConversation](#clientconversation)

دریافت لیست مکالمه های کاربر فعلی.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### StartReceivingConversationUpdates() -> `Void`

با صدا زدن این متد، تغییرات لیست مکالمات یا اطلاعات کلی یک مکالمه مثل
نام، برای کاربر از سمت سرور ارسال می شود.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### StopReceivingConversationUpdates() -> `Void`

با صدا زدن این متد، کاربر دیگر تغییرات مربوط به لیست مکالمات یا
اطلاعات کلی یک مکالمه را دریافت نخواهد کرد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### SubscribeToIdentitiesStatusUpdates([SubscribeToIdentitiesStatusUpdatesParameters](#subscribetoidentitiesstatusupdatesparameters)) -> `Void`

با صدا زدن این متد و دادن یک لیست از شناسه هویت ها
در صورتی که با هویت های مورد نظر، مکالمه مشترک داشته باشید
سرور در آینده وضعیت آنلاین یا آفلاین شدن آن ها را برای شما
ارسال خواهد کرد.

نکته: پس از صدا زدن این متد، سرور همان لحظه وضعیت فعلی هویت ها را
برای شما ارسال می کند.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### UnsubscribeToIdentitiesStatusUpdates([SubscribeToIdentitiesStatusUpdatesParameters](#subscribetoidentitiesstatusupdatesparameters)) -> `Void`

با صدا زدن این متد، کلاینت دیگر تغییر وضعیت هویت های
مورد نظر را دریافت نخواهد کرد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---
### SetConversationNotificationStatus([SetConversationNotificationStatusParameters](#setconversationnotificationstatusparameters)) -> `Void`

خاموش یا روشن کردن دریافت اعلانات
(Notifications)
برای یک مکالمه خاص.

>برای اطلاعات بیشتر در مورد این متد
[اعلان](./notification.md)
را مطالعه کنید.
---
### GetGeneralInfo() -> [GeneralInfo](#generalinfo)

اطلاعات کلی سیستم پیام رسان فعلی مانند نام فارسی انواع مکالمات،
نام فارسی گروه های کاربری را برای کلاینت ارسال می کند.

---
## Client Methods
توابعی که از سمت سرور میتوان در کلاینت صدا زد.
### OnConversationUpdate([ConversationUpdateEvent](#conversationupdateevent)) -> `Void`

اگر کلاینت به تغییرات مکالمه ای
subscribe
کرده باشد، از طریق این متد مطلع خواهد شد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.

---
### Pong() -> `Void`

زمانی که کلاینت دستور 
`Ping`
را به سمت سرور ارسال کند، سرور یک بار
دستور
`Pong`
را در کلاینت اجرا می کند.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.

---
### Ping() -> `Void`

سرور با صدا زدن این دستور، از کلاینت انتظار دارد
که دستور
`Pong`
در سرور صدا زده شود.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.

---
### AuthorizationResult([AuthorizationResult](#authorizationresult)) -> `Void`

نتیجه دستور
`Authorize()`
سرور از طریق این متد ارسال خواهد شد.

>برای اطلاعات بیشتر در مورد این متد
[فرایند احراز هویت](./authentication-process.md)
را مطالعه کنید.

---
### OnIdentityStatusUpdate([IdentityStatusUpdate](#identitystatusupdate)) -> `Void`
در صورتی که کلاینت به تغییر وضعیت یک هویت
subscribe
کرده باشد، تغییر از طریق این متد ارسال خواهند شد.

نکته: همیشه وضعیت اولیه یک هویت همان لحظه بعد از
subscribe
کردن از طریق این متد ارسال خواهد شد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.

---
### OnNewMessage([NewMessageEvent](#newmessageevent)) -> `Void`

هر زمان که در یک مکالمه پیام جدیدی ارسال شود،
این متد از سمت سرور صدا زده می شود.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.

---
### OnMessageUpdate([MessageUpdateEvent](#messageupdateevent)) -> `Void`

در صورتی که یک پیام تغییر کند
(دیده شود یا ویرایش شود)
کلاینت از طریق این متد مطلع خواهد شد.

>برای اطلاعات بیشتر در مورد این متد
[مکالمه](./conversation.md)
را مطالعه کنید.
---

<div dir="ltr" align="left">

## Models
> All fields will be serialized in camelCase
#### RequestAuthorizationModel

```cs
class RequestAuthorization
{
    /// توکن ورود یا توکن سشن
    string Token
    /// شناسه هویت (مانند نام کاربری)
    string Identity
}
```
#### ConnectionStatesEnum
```cs
enum ConnectionStates
{
    UnknownUnstableConnection = 0,
    UnknownOneWayStableConnection = 1,
    UnknownStableConnection = 2,
    IdentifiedStableConnection = 3
}
```
#### GetConversationInstanceMessagesParameters
```cs
class GetConversationInstanceMessagesParameters
{
    // شناسه مکالمه
    string ConversationId
}
```
#### ClientMessageModel
```cs
class ClientMessage
{
    /// شناسه پیام
    string Id
    /// متن پیام
    string Content
    /// شناسه هویت ارسال کننده
    string SenderId
    /// شناسه مکالمه ای که این پیام در آن وجود دارد
    string ConversationId
    /// تعداد افرادی که پیام را خوانده اند
    /// این تعداد شامل خود فرد هم می شود.
    /// این تعداد شامل فرد ارسال کننده نمی شود.
    int SeenCount
    /// آیا این پیام توسط شما قبلا دیده شده است؟
    bool isSeenByYou
    /// تاریخ ایجاد پیام
    DateTime CreatedDate

    /// محفظه ارتباط با سیستم های دیگر
    Dictionary<string, object> UserMeta
}
```
#### SendMessageToConversationParameters
```cs
class SendMessageToConversationParameters
{
    string Content
    string ConversationId
    Dictionary<string, object> UserMeta
}
```
#### SeenMessageParameters
```cs
class SeenMessageParameters
{
    string[] MessageIds
}
```
#### SetConversationNotificationStatusParameters
```cs
class SetConversationNotificationStatusParameters
{
    string ConversationId
    ConversationNotificationStatus Status
}

```
#### ConversationNotificationStatus
```cs
enum ConversationNotificationStatus
{
    On = 1,
    Off = 0
}
```
#### SetConversationTypingStatusParameters
```cs
class SetConversationTypingStatusParameters
{
    string ConversationId
    TypingStatus TypingStatus
}
```
#### TypingStatus
```cs
enum TypingStatus
{
    Typing = 1,
    NotTyping = 0
}
```
#### ClientConversation
```cs
class ClientConversation
{
    string Id
    DateTime CreatedDate
    /// شناسه هویت هایی که داخل مکالمه اند
    string[] Participants
    /// آیا کاربر فعلی اعلانات مربوط به این
    /// مکالمه را خاموش کرده است؟
    bool IsTurnedTheNotificationOff
    /// نوع مکالمه
    string Type
    string Title
    /// گروه های کاربری مکالمه و لیست هویت هایی
    /// که در هرکدام عضو هستند.
    Dictionary<string, string[]> IdentityGroups
}
```
#### GetConversationListParameters
```cs
class GetConversationListParameters
{
    // placeholder
}
```
#### GeneralInfo
```cs
class GeneralInfo
{
    Dictionary<string, ConversationType> ConversationTypes
}

class ConversationTypeValidation
{
    string Type
    object Value
}

class ConversationType
{
    string Title
    ConversationTypeValidation[] Validations
    IdentityGroup[] IdentityGroups
}


class IdentityGroup
{
    string Name
    IdentityGroupIndicator Indicator
}

class IdentityGroupIndicator
{
    string Title
}
```
#### SubscribeToIdentitiesStatusUpdatesParameters
```cs
class SubscribeToIdentitiesStatusUpdatesParameters
{
    string[] Identities
}
```
#### AuthorizationResult
```cs
class AuthorizationResult
{
    bool IsOk
    string Message
}
```
#### IdentityStatusUpdate
```cs
enum IdentityStatus
{
    Online = 1,
    Offline = 2,
    Unknown = 0,
}

class IdentityStatusUpdate
{
    IdentityStatus Status
    string IdentityId
}
```
#### NewMessageEvent
```cs
class NewMessageEvent
{
    string MessageId
    string ConversationId
    string Content
    string SenderId
    int SeenCount
    Dictionary<string, object> UserMeta
}
```
#### MessageUpdateEvent
```cs
class MessageUpdateEvent
{
    string MessageId
    string ConversationId
    int SeenCount
}
```
#### ConversationUpdateEvent
```cs
class ConversationUpdateEvent
{
    string ConversationId
    string ConversationTitle
    string[] ConversationParticipants
    Dictionary<string, string[]> IdentityGroups
}
```