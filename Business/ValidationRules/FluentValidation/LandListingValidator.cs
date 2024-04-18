using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class LandListingValidator : AbstractValidator<LandListing>
    {
       public LandListingValidator() 
       {
            RuleFor(ll=>ll.ParcelNo).NotEmpty();
            RuleFor(ll=>ll.IslandNo).NotEmpty(); 
            RuleFor(ll=>ll.SheetNo).NotEmpty();           
       }
    }
}
