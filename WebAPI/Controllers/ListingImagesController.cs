using Business.Abstract;
using Business.Dtos.Requests.ListingImageReq;
using Core.Extensions.Claims;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingImagesController : ControllerBase
    {
        IListingImageService _listingImageService;
        public ListingImagesController(IListingImageService listingImageService)
        {
            _listingImageService = listingImageService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll() 
        {
            var result = _listingImageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] CreateListingImageReq listingImageReq)
        {
            var userId = HttpContext.User.ClaimUserId();
            var result = _listingImageService.Add(file, listingImageReq,userId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] ListingImage listingImage)
        {
            var result = _listingImageService.Update(file, listingImage);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] IFormFile file, [FromForm] DeleteListingImageReq listingImage)
        {
            var result = _listingImageService.Delete(listingImage);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
