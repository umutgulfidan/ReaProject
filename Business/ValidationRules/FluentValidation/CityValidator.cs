using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName).NotEmpty();
            RuleFor(c=>c.CityName).MaximumLength(50);
            RuleFor(c=>c.CityName).MinimumLength(2);
        }
    }
}
