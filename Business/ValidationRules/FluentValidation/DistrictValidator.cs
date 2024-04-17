using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class DistrictValidator : AbstractValidator<District>
    {
        public DistrictValidator()
        {
            RuleFor(d=>d.DistrictName).NotEmpty();
            RuleFor(d=>d.CityId).NotEmpty();
            RuleFor(d=>d.DistrictName).MinimumLength(1);
            RuleFor(d => d.DistrictName).MaximumLength(50);
            RuleFor(d=>d.CityId).GreaterThan(0);
        }
    }
}
