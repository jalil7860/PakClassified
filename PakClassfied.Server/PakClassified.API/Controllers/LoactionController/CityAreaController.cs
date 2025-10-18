using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handler.LocationHandler;
using PakClassified.Models.Location;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityAreaController : ControllerBase
    {
        private readonly ICityAreaHandler _cityAreaHandler;
        public CityAreaController(ICityAreaHandler cityAreaHandler)
        {
            _cityAreaHandler = cityAreaHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CityAreaModel> models = _cityAreaHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            CityAreaModel? model = _cityAreaHandler.GetById(id).ToModel();
            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] CityAreaModel request)
        {
            CityAreaModel? res = _cityAreaHandler.Create(request.ToEntity())?.ToModel();
            if (res == null)
                return BadRequest();
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] CityAreaModel request)
        {
            CityAreaModel? res = _cityAreaHandler.Update(id, request.ToEntity())?.ToModel();
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            CityAreaModel? res = _cityAreaHandler.Delete(id)?.ToModel();
            if (res == null)
                return NotFound();
            return Ok($"Post With Id: {id} has been deleted.");
        }
    }
}
