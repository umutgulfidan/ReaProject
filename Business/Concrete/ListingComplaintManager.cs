using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.Dtos.Requests.ListingComplaintReq;
using Business.Dtos.Requests.UserComplaintReq;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions.Claims;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ListingComplaintManager : IListingComplaintService
    {
        IListingComplaintDal _listingComplaintDal;
        IComplaintService _complaintService;
        IHttpContextAccessor _httpContextAccessor;
        public ListingComplaintManager(IListingComplaintDal listingComplaintDal, IComplaintService complaintService, IHttpContextAccessor httpContextAccessor)
        {
            _listingComplaintDal = listingComplaintDal;
            _complaintService = complaintService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IResult Add(AddListingComplaintReq addListingComplaintReq,int userId)
        {
            var complaint = new Complaint() {
                UserId = userId, 
                ComplaintDate = DateTime.Now, 
                ComplaintResponseDate = null, 
                ComplaintStatusId = 1,
            };

            _complaintService.Add(complaint);
            var listingComplaint = new ListingComplaint()
            {
                ComplaintId = complaint.ComplaintId,
                ComplaintReason = addListingComplaintReq.ComplaintReason,
                ComplaintTitle = addListingComplaintReq.ComplaintTitle,
                ListingId = addListingComplaintReq.ListingId,
                ListingComplaintReasonId = addListingComplaintReq.ListingComplaintReasonId
            };



            _listingComplaintDal.Add(listingComplaint);
            return new SuccessResult(Messages.ListingComplaintAdded);
        }

        public IResult Delete(ListingComplaint listingComplaint)
        {
            _listingComplaintDal.Delete(listingComplaint);
            return new SuccessResult(Messages.ListingComplaintDeleted);
        }

        public IDataResult<List<ListingComplaint>> GetAll()
        {
            return new SuccessDataResult<List<ListingComplaint>>(_listingComplaintDal.GetAll(),Messages.ListingComplaintListed);
        }

        [SecuredOperation("admin",false)]
        public IDataResult<List<ListingComplaintDto>> GetPaginatedListingComplaints(ListingComplaintFilterObject? filter, SortingObject? sorting, int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<ListingComplaintDto>>(_listingComplaintDal.GetPaginatedListingComplaints(filter,sorting,pageNumber,pageSize),Messages.ListingComplaintListed);
        }

        [SecuredOperation("admin", true)] 
        [TransactionScopeAspect]
        public IResult Update(UpdateListingComplaintReq updateListingComplaintReq)
        {
            // Öncelikle şikayeti getiriyoruz
            var listingComplaint = _listingComplaintDal.Get(x => x.ListingComplaintId == updateListingComplaintReq.ListingComplaintId);
            if (listingComplaint == null)
            {
                return new ErrorResult(Messages.ListingComplaintNotFound);
            }

            // Complaint tablosundan UserId'yi alıyoruz
            var complaint = _complaintService.GetById(listingComplaint.ComplaintId).Data;
            if (complaint == null)
            {
                return new ErrorResult(Messages.ComplaintNotFound);
            }

            // Kullanıcı güncelleme işlemi için kontrol
            if (!_httpContextAccessor.HttpContext.User.IsInRole("admin"))
            {
                // Kullanıcı ID'si kontrolü (Sadece kendi şikayetlerini güncelleyebilirler)
                var currentUserId = _httpContextAccessor.HttpContext.User.ClaimUserId();
                if (complaint.UserId != currentUserId)
                {
                    return new ErrorResult(Messages.AuthorizationDenied);
                }
            }

            // Şikayet bilgilerini güncelle (Sadece adminler bu kısımları güncelleyebilir)
            if (_httpContextAccessor.HttpContext.User.IsInRole("admin"))
            {
                complaint.ComplaintDate = updateListingComplaintReq.ComplaintDate;
                complaint.ComplaintResponseDate = updateListingComplaintReq.ComplaintResponseDate;
                complaint.ComplaintStatusId = updateListingComplaintReq.ComplaintStatusId;
                complaint.UserId = updateListingComplaintReq.UserId;

                _complaintService.Update(complaint);
            }

            // ListingComplaint bilgilerini güncelle (Herkes bu kısımları güncelleyebilir)
            listingComplaint.ComplaintReason = updateListingComplaintReq.ComplaintReason;
            listingComplaint.ComplaintTitle = updateListingComplaintReq.ComplaintTitle;
            listingComplaint.ListingComplaintReasonId = updateListingComplaintReq.ListingComplaintReasonId;

            _listingComplaintDal.Update(listingComplaint);
            return new SuccessResult(Messages.ListingComplaintUpdated);
        }

    }
}
