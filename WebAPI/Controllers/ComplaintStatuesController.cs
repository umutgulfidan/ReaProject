using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintStatuesController : ControllerBase
    {
        IComplaintStatusService _complaintStatusService;
        public ComplaintStatuesController(IComplaintStatusService complaintStatusService)
        {
            _complaintStatusService = complaintStatusService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _complaintStatusService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(ComplaintStatus complaintStatus)
        {
            var result = _complaintStatusService.Add(complaintStatus);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(ComplaintStatus complaintStatus)
        {
            var result = _complaintStatusService.Delete(complaintStatus);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ComplaintStatus complaintStatus)
        {
            var result = _complaintStatusService.Update(complaintStatus);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
