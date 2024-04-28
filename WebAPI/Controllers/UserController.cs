using Business.Abstract;
using Core.Extensions.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getuserdetailsbytoken")]
        public IActionResult GetUserDetail()
        {
            var userId = HttpContext.User.ClaimUserId();
            if (userId == null)
            {
                return BadRequest();
            }
            var result = _userService.GetUserDetailsById(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getuserdetailsbyid")]
        public IActionResult GetUserDetailById(int userId)
        {
            var result = _userService.GetUserDetailsById(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
