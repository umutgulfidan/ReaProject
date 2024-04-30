namespace Entities.DTOs
{
    public class LandListingRequestModel
    {
        public LandListingFilterObject? Filter { get; set; }
        public SortingObject? Sorting { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
