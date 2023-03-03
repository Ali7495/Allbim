using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum CustomHttpStatusCodes
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 200,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 500,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 400,

        [Display(Name = "یافت نشد")]
        NotFound = 404,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 401,

        [Display(Name = "شما دسترسی ندارید")]
        Forbidden = 403,

        [Display(Name = "با این شناسه یافت نشد" )]
        EntityNotFound = 404

    }
}
