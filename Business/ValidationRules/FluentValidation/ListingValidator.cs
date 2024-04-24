using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ListingValidator : AbstractValidator<Listing>
    {
        public ListingValidator()
        {
            RuleFor(l => l.Title).NotEmpty();
            RuleFor(l => l.Price).NotEmpty();
            RuleFor(l => l.SquareMeter).NotEmpty();
            RuleFor(l=>l.Address).NotEmpty();

            RuleFor(l => l.Price).GreaterThan(0);
            RuleFor(l=> l.SquareMeter).GreaterThan(0);
            RuleFor(l=>l.Title).MaximumLength(50);
            RuleFor(l => l.Description).MinimumLength(0);
            RuleFor(l=>l.Description).MaximumLength(2048);

            RuleFor(l=>l.Address).MinimumLength(0);
            RuleFor(l=>l.Address).MaximumLength(255);

        }
    }
}
