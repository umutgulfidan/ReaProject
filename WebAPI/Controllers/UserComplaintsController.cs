using Business.Abstract;
using Business.Dtos.Requests.UserComplaintReq;
using Core.Extensions.Claims;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserComplaintsController : ControllerBase
    {
        IUserComplaintService _userComplaintService;
        public UserComplaintsController(IUserComplaintService userComplaintService)
        {
            _userComplaintService = userComplaintService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userComplaintService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getpaginatedusercomplaints")]
        public IActionResult GetPaginatedListings([FromBody] UserComplaintRequestModel requestModel)
        {
            var result = _userComplaintService.GetPaginatedUserComplaints(requestModel.Filter, requestModel.Sorting, requestModel.PageNumber, requestModel.PageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(AddUserComplaintReq addUserComplaintReq)
        {

            var result = _userComplaintService.Add(addUserComplaintReq,HttpContext.User.ClaimUserId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(UserComplaint userCompaint)
        {
            var result = _userComplaintService.Delete(userCompaint);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UpdateUserComplaintReq updateUserComplaintReq)
        {
            var result = _userComplaintService.Update(updateUserComplaintReq);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
