using Business.Abstract;
using Business.Dtos.Requests;
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

        [HttpPost("add")]
        public IActionResult Add(CreateHouseListingReq req)
        {
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
            var result = _houseListingService.Update(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
