using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class PropertyTypeValidator : AbstractValidator<PropertyType>
    {
        public PropertyTypeValidator()
        {
            RuleFor(pt=>pt.Name).NotEmpty();
            RuleFor(pt => pt.Name).MinimumLength(1);
            RuleFor(pt => pt.Name).MaximumLength(50);
            RuleFor(pt => pt.Name).Must(IsLetter);
        }

        private bool IsLetter(string arg)
        {
            Regex regex = new Regex(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$");
            return regex.IsMatch(arg);
        }
    }
}
