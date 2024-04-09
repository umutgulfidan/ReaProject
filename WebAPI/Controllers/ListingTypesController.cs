using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingTypesController : ControllerBase
    {
        IListingTypeService _listingTypeService;
        public ListingTypesController(IListingTypeService listingTypeService)
        {
            _listingTypeService = listingTypeService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _listingTypeService.GetAll();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _listingTypeService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(ListingType listingType)
        {
            var result = _listingTypeService.Add(listingType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(ListingType listingType)
        {
            var result = _listingTypeService.Delete(listingType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ListingType listingType)
        {
            var result = _listingTypeService.Update(listingType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
