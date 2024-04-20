using Business.Abstract;
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
        public IActionResult GetByUserId()
        {
            var userId = HttpContext.User.ClaimUserId();
            if (userId == null)
            {
                return BadRequest();
            }
            var result = _listingService.GetByUserId(userId);
            if(result.IsSuccess)
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
        public IActionResult GetById(int id) {
            var result = _listingService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
