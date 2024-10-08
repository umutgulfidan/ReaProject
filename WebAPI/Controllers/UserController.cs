﻿using Business.Abstract;
using Core.Extensions.Claims;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("getusercount")]
        public IActionResult GetUserCount()
        {
            var result = _userService.GetUserCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getactiveusercount")]
        public IActionResult GetActiveUserCount()
        {
            var result = _userService.GetActiveUserCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getpassiveusercount")]
        public IActionResult GetPassiveUserCount()
        {
            var result = _userService.GetPassiveUserCount();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlatestusers")]
        public IActionResult GetLatestUsers(int pageSize)
        {
            var result = _userService.GetLatestUsers(pageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getuserstatus")]
        public IActionResult GetUserStatus(int userId)
        {
            var result = _userService.GetUserStatus(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getpaginatedusers")]
        public IActionResult GetPaginatedListings([FromBody] UserRequestModel requestModel)
        {
            var result = _userService.GetPaginatedUsers(requestModel.Filter, requestModel.Sorting, requestModel.PageNumber, requestModel.PageSize);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("setuseractive")]
        public IActionResult SetUserActive(int userId)
        {
            var result = _userService.SetUserActive(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("setuserinactive")]
        public IActionResult SetUserInactive(int userId)
        {
            var result = _userService.SetUserInactive(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
