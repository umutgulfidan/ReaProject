using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserComplaintDal : EfRepositoryBase<ReaContext, UserComplaint>, IUserComplaintDal
    {
        public List<UserComplaintDto> GetPaginatedUserComplaints(UserComplaintFilterObject? filter, SortingObject? sortingObject, int pageNumber, int pageSize)
        {
            using (var context = new ReaContext())
            {
                var skipAmount = (pageNumber - 1) * pageSize;

                var query = from complaint in context.Complaints
                            join userComplaint in context.UserComplaints on complaint.ComplaintId equals userComplaint.ComplaintId
                            join userComplaintReason in context.UserComplaintReasons on userComplaint.UserComplaintReasonId equals userComplaintReason.Id
                            select new { complaint, userComplaint, userComplaintReason };

                // Filtreleme
                if (filter != null)
                {
                    if (filter.UserId.HasValue)
                    {
                        query = query.Where(x => x.complaint.UserId == filter.UserId.Value);
                    }
                    if (filter.ReportedUserId.HasValue)
                    {
                        query = query.Where(x => x.userComplaint.ReportedUserId == filter.ReportedUserId.Value);
                    }
                    if (filter.ComplaintStatusId.HasValue)
                    {
                        query = query.Where(x => x.complaint.ComplaintStatusId == filter.ComplaintStatusId.Value);
                    }
                    if (filter.UserComplaintReasonId.HasValue)
                    {
                        query = query.Where(x => x.userComplaint.UserComplaintReasonId == filter.UserComplaintReasonId.Value);
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
                                                 x.userComplaint.ComplaintReason.Contains(filter.SearchText) ||
                                                 x.userComplaint.ComplaintTitle.Contains(filter.SearchText) ||
                                                 x.userComplaint.UserComplaintId.ToString().Contains(filter.SearchText));
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
                                query.OrderBy(x => x.userComplaint.UserComplaintId) :
                                query.OrderByDescending(x => x.userComplaint.UserComplaintId);
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
                    .Select(x => new UserComplaintDto
                    {
                        ComplaintId = x.complaint.ComplaintId,
                        UserId = x.complaint.UserId,
                        ComplaintDate = x.complaint.ComplaintDate,
                        ComplaintStatusId = x.complaint.ComplaintStatusId,
                        ComplaintResponseDate = x.complaint.ComplaintResponseDate,
                        UserComplaintId = x.userComplaint.UserComplaintId,
                        ReportedUserId = x.userComplaint.ReportedUserId,
                        ComplaintTitle = x.userComplaint.ComplaintTitle,
                        ComplaintReason = x.userComplaint.ComplaintReason,
                        UserComplaintReasonId = x.userComplaint.UserComplaintReasonId,
                        UserComplaintReasonName = x.userComplaintReason.ReasonName // Şikayet nedeni ismi
                    })
                    .ToList();

                return result;
            }
        }

    }
}
