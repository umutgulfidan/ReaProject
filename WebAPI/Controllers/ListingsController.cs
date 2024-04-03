using Business.Abstract;
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
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id) {
            var result = _listingService.GetById(id);
            return Ok(result);
        }
    }
}
