using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingComplaintReasonsController : ControllerBase
    {
        IListingComplaintReasonService _listingComplaintReasonService;
        public ListingComplaintReasonsController(IListingComplaintReasonService listingComplaintReasonService)
        {
            _listingComplaintReasonService = listingComplaintReasonService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _listingComplaintReasonService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(ListingComplaintReason listingComplaintReason)
        {
            var result = _listingComplaintReasonService.Add(listingComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(ListingComplaintReason listingComplaintReason)
        {
            var result = _listingComplaintReasonService.Delete(listingComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ListingComplaintReason listingComplaintReason)
        {
            var result = _listingComplaintReasonService.Update(listingComplaintReason);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
