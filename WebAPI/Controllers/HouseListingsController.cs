using Business.Abstract;
using Business.Entities.Requests;
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
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add(AddHouseListingReq req)
        {
            _houseListingService.Add(req);
            return Ok();
        }
    }
}
