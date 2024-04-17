using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ListingImageValidator :AbstractValidator<ListingImage>
    {
        public ListingImageValidator()
        {
            RuleFor(li=>li.ListingId).NotEmpty();
            RuleFor(li => li.ListingId).GreaterThan(0);
        }
    }
}
