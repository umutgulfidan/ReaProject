using Business.Abstract;
using Business.Dtos.Requests.ListingComplaintReq;
using Business.Dtos.Requests.UserComplaintReq;
using Core.Extensions.Claims;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingComplaintsController : ControllerBase
    {
        IListingComplaintService _listingComplaintService;
        public ListingComplaintsController(IListingComplaintService listingComplaintService)
        {
            _listingComplaintService = listingComplaintService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _listingComplaintService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(AddListingComplaintReq addListingComplaintReq)
        {
            var userId = HttpContext.User.ClaimUserId();
            var result = _listingComplaintService.Add(addListingComplaintReq,userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(ListingComplaint listingComplaint)
        {
            var result = _listingComplaintService.Delete(listingComplaint);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UpdateListingComplaintReq listingComplaint)
        {
            var result = _listingComplaintService.Update(listingComplaint);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getpaginatedlistingcomplaints")]
        public IActionResult GetPaginatedListings([FromBody] ListingComplaintRequestModel requestModel)
        {
            var result = _listingComplaintService.GetPaginatedListingComplaints(requestModel.Filter, requestModel.Sorting, requestModel.PageNumber, requestModel.PageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
