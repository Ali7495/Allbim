using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;

namespace Models.Extensions
{
    public enum ValidationRequiredMessage
    {
        [Display(Name = "وارد کردن نام اجباری است")]
        FirstName,
        [Display(Name = "وارد کردن نام خانوادگی اجباری است")]
        LastName,
        [Display(Name = "وارد کردن کدملی اجباری است")]
        NationalCode,
        [Display(Name = "وارد کردن شناسه یکتا اجباری است")]
        Identity,
        [Display(Name = "وارد کردن جنسیت اجباری است")]
        Gender,
        [Display(Name = "وارد کردن وضعیت تاهل اجباری است")]
        Marriage,
        [Display(Name = "وارد کردن نام پدر اجباری است")]
        FatherName,
        [Display(Name = "وارد کردن کد شخص اجباری است")]
        PersonCode,
        [Display(Name = "وارد کردن پست الکترونیکی اجباری است")]
        Email,
        [Display(Name = "وارد کردن نام کاربری اجباری است")]
        Username,
        [Display(Name = "وارد کردن کلمه عبور اجباری است")]
        Password,
        [Display(Name = "وارد کردن توضیحات اجباری است")]
        Description,
        [Display(Name = "وارد کردن شرکت بیمه اجباری است")]
        Insurance,
        [Display(Name = "وارد کردن کد شرکت اجباری است")]
        CompanyId,
        [Display(Name = "وارد کردن کد وسیله نقلیه اجباری است")]
        Vehicle,
        [Display(Name = "وارد کردن کد شخص اجباری است")]
        PersonId,
        [Display(Name = "وارد کردن کد درخواست بیمه اجباری است")]
        InsuredRequestId
    }
    public enum Models_DisplayProperty
    {
        Name
    }
    public static class Models_EnumExtensions
    {
        public static string DiplayValidationMessage(this Enum value, Models_DisplayProperty property = Models_DisplayProperty.Name)
        {
            Models_Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }
    }

    public static class Models_Assert
    {
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void NotNull<T>(T? obj, string name, string message = null)
            where T : struct
        {
            if (!obj.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }

        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (obj == defaultValue
                || (obj is string str && string.IsNullOrWhiteSpace(str))
                || (obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }

}
