using Business.Abstract;
using Core.Entities.Concrete;
using Core.Extensions.Claims;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        IListingService _listingService;
        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }
        [HttpGet("getDetailView")]
        public IActionResult GetView()
        {
            var result = _listingService.GetView();
            if(result.IsSuccess )
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletebyid")]
        public IActionResult DeleteById(int listingId)
        {
            var result = _listingService.DeleteById(listingId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _listingService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _listingService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("getbytoken")]
        public IActionResult GetByToken()
        {
            var userId = HttpContext.User.ClaimUserId();
            if (userId == null)
            {
                return BadRequest();
            }
            var result = _listingService.GetByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getallbyfilter")]
        public IActionResult GetAllByFilter(ListingFilterObject filter)
        {
            var result = _listingService.GetByFilter(filter);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _listingService.GetListingDetails();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _listingService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getpaginatedlistings")]
        public IActionResult GetPaginatedListings([FromBody] ListingRequestModel requestModel)
        {
            var result = _listingService.GetPaginatedListingsWithFilterAndSorting(requestModel.Filter, requestModel.Sorting, requestModel.PageNumber, requestModel.PageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getlistingcount")]
        public IActionResult GetListingCount()
        {
            var result = _listingService.GetListingCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getactivelistingcount")]
        public IActionResult GetActiveListingCount()
        {
            var result = _listingService.GetActiveListingCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getpassivelistingcount")]
        public IActionResult GetPassiveListingCount()
        {
            var result = _listingService.GetPassiveListingCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlistingstatus")]
        public IActionResult GetListingStatus(int listingId)
        {
            var result = _listingService.GetListingStatus(listingId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
