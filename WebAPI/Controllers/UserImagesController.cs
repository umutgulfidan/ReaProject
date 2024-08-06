using Business.Abstract;
using Business.Dtos.Requests.UserImageReq;
using Core.Extensions.Claims;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        IUserImageService _userImageService;
        public UserImagesController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userImageService.GetAll();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyuserid")]
        public IActionResult GetAllByUserId(int userId) 
        {
            var result = _userImageService.GetAllByUserId(userId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbytoken")]
        public IActionResult GetByToke()
        {
            int userId = HttpContext.User.ClaimUserId();
            if(userId == null)
            {
                return BadRequest();
            }

            var result = _userImageService.GetAllByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] CreateUserImageReq req)
        {
            req.UserId = HttpContext.User.ClaimUserId();
            var result = _userImageService.Add(file, req);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] UserImage userImage)
        {
            var result = _userImageService.Update(file, userImage);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(DeleteUserImageReq req)
        {
            var result = _userImageService.Delete(req);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteallimages")]
        public IActionResult DeleteAllImages()
        {
            int userId = HttpContext.User.ClaimUserId();
            var result = _userImageService.DeleteAll(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getprofileimagepath")]
        public IActionResult GetProfileImage(int userId)
        {
            var result = _userImageService.GetProfileImagePath(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
