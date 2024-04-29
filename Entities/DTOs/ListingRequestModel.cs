namespace Entities.DTOs
{
    public class ListingRequestModel
    {
        public ListingFilterObject? Filter { get; set; }
        public SortingObject? Sorting { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
