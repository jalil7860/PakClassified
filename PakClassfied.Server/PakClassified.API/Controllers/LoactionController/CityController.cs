using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handler.LocationHandler;
using PakClassified.Models.Location;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityHandler _cityHandler;
        public CityController(ICityHandler cityHandler)
        {
            _cityHandler = cityHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CityModel>? models = _cityHandler.GetAll().ToModelList();
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            CityModel? model = _cityHandler.GetById(id).ToModel();
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] CityModel request)
        {
            CityModel? res = _cityHandler.Create(request.ToEntity()).ToModel();
            if (res == null)
            {
                return BadRequest();
            }
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] CityModel request)
        {
            CityModel? res = _cityHandler.Update(id, request.ToEntity())?.ToModel();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            CityModel? res = _cityHandler.Delete(id)?.ToModel();
            if (res == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
