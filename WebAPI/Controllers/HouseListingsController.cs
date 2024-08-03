using Business.Abstract;
using Business.Dtos.Requests;
using Core.Extensions.Claims;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseListingsController : ControllerBase
    {
        IHouseListingService _houseListingService;
        public HouseListingsController(IHouseListingService houseListingService)
        {
            _houseListingService = houseListingService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _houseListingService.GetAll();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("gethouselistingdtos")]
        public IActionResult GetHouseListingDtos()
        {
            var result = _houseListingService.GetHouseListingDtos();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getdetails")]
        public IActionResult GetHouseListings(int listingId)
        {
            var result = _houseListingService.GetHouseListingDetail(listingId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getpaginatedlistings")]
        public IActionResult GetPaginatedListings([FromBody] HouseListingRequestModel requestModel)
        {
            var result = _houseListingService.GetPaginatedListingsWithFilterAndSorting(requestModel.Filter, requestModel.Sorting, requestModel.PageNumber, requestModel.PageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreateHouseListingReq req)
        {
            var userId = HttpContext.User.ClaimUserId();
            req.UserId = userId;

            var result = _houseListingService.Add(req);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(DeleteHouseListingReq req)
        {

            var result = _houseListingService.Delete(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("update")]
        public IActionResult Update(UpdateHouseListingReq req)
        {
            var userId = HttpContext.User.ClaimUserId();

            var result = _houseListingService.Update(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getallbyfilter")]
        public IActionResult GetAllByFilter(HouseFilterObject filter)
        {
            var result = _houseListingService.GetAllByFilter(filter);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
