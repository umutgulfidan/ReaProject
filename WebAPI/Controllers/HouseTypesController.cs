using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseTypesController : ControllerBase
    {

        IHouseTypeService _houseTypeService;
        public HouseTypesController(IHouseTypeService houseTypeService)
        {
            _houseTypeService = houseTypeService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _houseTypeService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _houseTypeService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(HouseType houseType)
        {
            var result = _houseTypeService.Add(houseType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(HouseType houseType)
        {
            var result = _houseTypeService.Delete(houseType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(HouseType houseType)
        {
            var result = _houseTypeService.Update(houseType);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
