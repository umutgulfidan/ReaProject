using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserComplaintReasonsController : ControllerBase
    {
        IUserComplaintReasonService _userComplaintReasonService;
        public UserComplaintReasonsController(IUserComplaintReasonService userComplaintReasonService)
        {
            _userComplaintReasonService = userComplaintReasonService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userComplaintReasonService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(UserComplaintReason userComplaintReason)
        {
            var result = _userComplaintReasonService.Add(userComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(UserComplaintReason userComplaintReason)
        {
            var result = _userComplaintReasonService.Delete(userComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserComplaintReason userComplaintReason)
        {
            var result = _userComplaintReasonService.Update(userComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
