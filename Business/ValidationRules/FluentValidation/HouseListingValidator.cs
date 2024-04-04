using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class HouseListingValidator : AbstractValidator<HouseListing>
    {
        public HouseListingValidator()
        {
            RuleFor(hl=> hl.Address).NotEmpty();
            RuleFor(hl=> hl.BuildingAge).NotEmpty();
            RuleFor(hl=>hl.CurrentFloor).NotEmpty();
            RuleFor(hl=>hl.FloorCount).NotEmpty();
            RuleFor(hl=>hl.BathroomCount).NotEmpty();
            RuleFor(hl=>hl.LivingRoomCount).NotEmpty();
            RuleFor(hl=>hl.RoomCount).NotEmpty();

            RuleFor(hl => hl.BuildingAge).GreaterThan(0);
            RuleFor(hl => hl.CurrentFloor).GreaterThan(0);
            RuleFor(hl => hl.FloorCount).GreaterThan(0);
            RuleFor(hl => hl.BathroomCount).GreaterThan(0);
            RuleFor(hl => hl.LivingRoomCount).GreaterThan(0);
            RuleFor(hl => hl.RoomCount).GreaterThan(0);
            
            RuleFor(hl=>hl.Address).MinimumLength(5);

        }
    }
}
