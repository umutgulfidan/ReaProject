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
    public class ListingTypeValidator: AbstractValidator<ListingType>
    {
        public ListingTypeValidator()
        {
            RuleFor(lt=>lt.ListingTypeName).NotEmpty();
            RuleFor(lt => lt.ListingTypeName).MinimumLength(1);
            RuleFor(lt => lt.ListingTypeName).MaximumLength(50);
            RuleFor(lt => lt.ListingTypeName).Must(IsLetter);           
        }

        private bool IsLetter(string arg)
        {
            Regex regex = new Regex(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$");
            return regex.IsMatch(arg);
        }
    }
}
