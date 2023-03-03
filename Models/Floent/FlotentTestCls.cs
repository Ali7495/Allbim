using FluentValidation;
using Models.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Floent
{
    public class FlotentTestCls
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
    public class FlotentTestClsValidator : AbstractValidator<FlotentTestCls>
    {
        public FlotentTestClsValidator()
        {
            
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("نام");
            RuleFor(p => p.LastName).NotEmpty().WithMessage(ValidationRequiredMessage.LastName.DiplayValidationMessage());
        }
    }

}
