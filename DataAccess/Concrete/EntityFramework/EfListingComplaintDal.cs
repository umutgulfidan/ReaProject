using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfListingComplaintDal : EfRepositoryBase<ReaContext, ListingComplaint>, IListingComplaintDal
    {
        public List<ListingComplaintDto> GetPaginatedListingComplaints(ListingComplaintFilterObject? filter, SortingObject? sortingObject, int pageNumber, int pageSize)
        {
            using (var context = new ReaContext())
            {
                var skipAmount = (pageNumber - 1) * pageSize;

                var query = from complaint in context.Complaints
                            join listingComplaint in context.ListingComplaints on complaint.ComplaintId equals listingComplaint.ComplaintId
                            join listingComplaintReason in context.ListingComplaintReasons on listingComplaint.ListingComplaintReasonId equals listingComplaintReason.Id
                            select new { complaint, listingComplaint, listingComplaintReason };

                // Filtreleme
                if (filter != null)
                {
                    if (filter.ListingId.HasValue)
                    {
                        query = query.Where(x => x.listingComplaint.ListingId == filter.ListingId.Value);
                    }
                    if (filter.ReportedUserId.HasValue)
                    {
                        query = query.Where(x => x.complaint.UserId == filter.ReportedUserId.Value);
                    }
                    if (filter.ComplaintStatusId.HasValue)
                    {
                        query = query.Where(x => x.complaint.ComplaintStatusId == filter.ComplaintStatusId.Value);
                    }
                    if (filter.UserComplaintReasonId.HasValue)
                    {
                        query = query.Where(x => x.listingComplaint.ListingComplaintReasonId == filter.UserComplaintReasonId.Value);
                    }
                    if (filter.MinComplaintDate.HasValue)
                    {
                        query = query.Where(x => x.complaint.ComplaintDate >= filter.MinComplaintDate.Value);
                    }
                    if (filter.MaxComplaintDate.HasValue)
                    {
                        query = query.Where(x => x.complaint.ComplaintDate <= filter.MaxComplaintDate.Value);
                    }
                    if (!string.IsNullOrEmpty(filter.SearchText))
                    {
                        query = query.Where(x => x.complaint.ComplaintId.ToString().Contains(filter.SearchText) ||
                                                 x.listingComplaint.ComplaintReason.Contains(filter.SearchText) ||
                                                 x.listingComplaint.ComplaintTitle.Contains(filter.SearchText) ||
                                                 x.listingComplaint.ListingComplaintId.ToString().Contains(filter.SearchText));
                    }
                }

                // Sıralama
                if (sortingObject != null && !string.IsNullOrEmpty(sortingObject.SortBy))
                {
                    switch (sortingObject.SortBy.ToLower())
                    {
                        case "complaintdate":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.complaint.ComplaintDate) :
                                query.OrderByDescending(x => x.complaint.ComplaintDate);
                            break;
                        case "id":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.listingComplaint.ListingComplaintId) :
                                query.OrderByDescending(x => x.listingComplaint.ListingComplaintId);
                            break;
                        default:
                            query = query.OrderBy(x => x.complaint.ComplaintDate);
                            break;
                    }
                }
                else
                {
                    query = query.OrderBy(x => x.complaint.ComplaintDate);
                }

                // Sonuçları topla ve sayfala
                var result = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .Select(x => new ListingComplaintDto
                    {
                        ComplaintId = x.complaint.ComplaintId,
                        UserId = x.complaint.UserId,
                        ComplaintDate = x.complaint.ComplaintDate,
                        ComplaintStatusId = x.complaint.ComplaintStatusId,
                        ComplaintResponseDate = x.complaint.ComplaintResponseDate,
                        ListingComplaintId = x.listingComplaint.ListingComplaintId,
                        ListingId = x.listingComplaint.ListingId,
                        ComplaintTitle = x.listingComplaint.ComplaintTitle,
                        ComplaintReason = x.listingComplaint.ComplaintReason,
                        ListingComplaintReasonId = x.listingComplaint.ListingComplaintReasonId,
                        ListingComplaintReasonName = x.listingComplaintReason.ReasonName // Şikayet nedeni ismi
                    })
                    .ToList();

                return result;
            }
        }
    }
}
