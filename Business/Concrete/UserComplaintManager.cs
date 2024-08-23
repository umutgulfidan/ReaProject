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
    public class UserComplaintManager : IUserComplaintService
    {
        IUserComplaintDal _userComplaintDal;
        IComplaintService _complaintService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserComplaintManager(IUserComplaintDal userComplaintDal, IComplaintService complaintService, IHttpContextAccessor httpContextAccessor)
        {
            _userComplaintDal = userComplaintDal;
            _complaintService = complaintService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IResult Add(AddUserComplaintReq addUserComplaintReq,int userId)
        {
            var complaint = new Complaint()
            { UserId = userId,
                ComplaintDate=DateTime.Now,
                ComplaintResponseDate=null,
                ComplaintStatusId=1};

            _complaintService.Add(complaint);


            var userComplaint = new UserComplaint()
            {
                ComplaintId = complaint.ComplaintId,
                ComplaintReason = addUserComplaintReq.ComplaintReason,
                ComplaintTitle = addUserComplaintReq.ComplaintTitle,
                ReportedUserId = addUserComplaintReq.ReportedUserId,
                UserComplaintReasonId = addUserComplaintReq.UserComplaintReasonId
            };



            _userComplaintDal.Add(userComplaint);
            return new SuccessResult(Messages.UserComplaintAdded);
        }

        public IResult Delete(UserComplaint userComplaint)
        {
            _userComplaintDal.Delete(userComplaint);
            return new SuccessResult(Messages.UserComplaintDeleted);
        }

        public IDataResult<List<UserComplaint>> GetAll()
        {
            return new SuccessDataResult<List<UserComplaint>>(_userComplaintDal.GetAll(), Messages.UserComplaintListed);
        }

        [SecuredOperation("admin", true)]
        [TransactionScopeAspect]
        public IResult Update(UpdateUserComplaintReq updateUserComplaintReq)
        {

            // Öncelikle şikayeti getiriyoruz
            var userComplaint = _userComplaintDal.Get(x => x.UserComplaintId == updateUserComplaintReq.UserComplaintId);
            if (userComplaint == null)
            {
                return new ErrorResult(Messages.ListingComplaintNotFound);
            }

            // Complaint tablosundan UserId'yi alıyoruz
            var complaint = _complaintService.GetById(userComplaint.ComplaintId).Data;
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
                complaint.ComplaintDate = updateUserComplaintReq.ComplaintDate;
                complaint.ComplaintResponseDate = updateUserComplaintReq.ComplaintResponseDate;
                complaint.ComplaintStatusId = updateUserComplaintReq.ComplaintStatusId;
                complaint.UserId = updateUserComplaintReq.UserId;

                _complaintService.Update(complaint);
            }

            // ListingComplaint bilgilerini güncelle (Herkes bu kısımları güncelleyebilir)
            userComplaint.ComplaintReason = updateUserComplaintReq.ComplaintReason;
            userComplaint.ComplaintTitle = updateUserComplaintReq.ComplaintTitle;
            userComplaint.UserComplaintReasonId = updateUserComplaintReq.UserComplaintReasonId;

            _userComplaintDal.Update(userComplaint);
            return new SuccessResult(Messages.ListingComplaintUpdated);
        }

        [SecuredOperation("admin",false)]
        public IResult GetPaginatedUserComplaints(UserComplaintFilterObject? filter, SortingObject? sorting,int pageNumber ,int pageSize)
        {
            return new SuccessDataResult<List<UserComplaintDto>>(_userComplaintDal.GetPaginatedUserComplaints(filter,sorting,pageNumber,pageSize),Messages.UserComplaintListed);
        }
    }
}
