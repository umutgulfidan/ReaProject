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
    public class HouseTypeValidator : AbstractValidator<HouseType>
    {
        public HouseTypeValidator()
        {
            RuleFor(ht => ht.Name).NotEmpty();
            RuleFor(ht => ht.Name).MinimumLength(1);
            RuleFor(ht=>ht.Name).MaximumLength(50);
            RuleFor(ht=>ht.Name).Must(IsLetter);
        }

        private bool IsLetter(string arg)
        {
            Regex regex = new Regex(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$");
            return regex.IsMatch(arg);
        }
    }
}
