using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintResponsesController : ControllerBase
    {
        IComplaintResponseService _complaintResponseService;
        public ComplaintResponsesController(IComplaintResponseService complaintResponseService)
        {
            _complaintResponseService = complaintResponseService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _complaintResponseService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(ComplaintResponse complaintResponse)
        {
            var result = _complaintResponseService.Add(complaintResponse);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(ComplaintResponse complaintResponse)
        {
            var result = _complaintResponseService.Delete(complaintResponse);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ComplaintResponse complaintResponse)
        {
            var result = _complaintResponseService.Update(complaintResponse);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
