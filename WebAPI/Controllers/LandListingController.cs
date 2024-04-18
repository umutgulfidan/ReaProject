using Business.Abstract;
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

        public IActionResult Update(LandListing landListing)
        {
            var result = _landListingService.Update(landListing);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("add")]

        public IActionResult Add(LandListing landListing)
        {
            var result = _landListingService.Add(landListing);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("delete")]

        public IActionResult Delete(LandListing landListing)
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
