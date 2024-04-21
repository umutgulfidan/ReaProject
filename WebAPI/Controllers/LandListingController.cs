using Business.Abstract;
using Business.Dtos.Requests.LandListingReq;
using Core.Extensions.Claims;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandListingController : ControllerBase
    {
        ILandListingService _landListingService;

        public LandListingController(ILandListingService landListingService)
        {
            _landListingService = landListingService;
        }


        [HttpGet("getlandlistingdetail")]
        public IActionResult GetLandListingDetail(int listingId)
        {
            var result = _landListingService.GetLandListingDetail(listingId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlandlistings")]
        public IActionResult GetLandListings()
        {
            var result = _landListingService.GetLandListings();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpGet("getall")]

        public IActionResult GetAll()
        {
            var result = _landListingService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("getbyid")]

        public IActionResult GetById(int id)
        {
            var result = _landListingService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("update")]

        public IActionResult Update(UpdateLandListingReq req)
        {
            var userId = HttpContext.User.ClaimUserId();
            if (userId == null)
            {
                return BadRequest();
            }
            req.UserId = userId;

            var result = _landListingService.Update(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("add")]

        public IActionResult Add(CreateLandListingReq req)
        {
            var userId = HttpContext.User.ClaimUserId();
            if(userId == null)
            {
                return BadRequest();
            }
            req.UserId = userId;

            var result = _landListingService.Add(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("delete")]

        public IActionResult Delete(DeleteLandListingReq landListing)
        {
            var result = _landListingService.Delete(landListing);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
