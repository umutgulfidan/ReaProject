using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        IComplaintService _complaintService;
        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _complaintService.GetAll();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
