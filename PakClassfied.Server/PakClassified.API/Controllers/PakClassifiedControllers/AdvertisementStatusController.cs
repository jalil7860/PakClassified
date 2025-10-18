using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Models.PakClassified;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementStatusController : ControllerBase
    {
        private readonly IAdvertisementStatusHandler _advertisementStatusHandler;
        public AdvertisementStatusController(IAdvertisementStatusHandler advertisementStatusHandler)
        {
            _advertisementStatusHandler = advertisementStatusHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AdvertisementStatusModel>? models = _advertisementStatusHandler.GetAll().ToModelList();
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementStatusModel? model = _advertisementStatusHandler.GetById(id).ToModel();
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] AdvertisementStatusModel request)
        {
            AdvertisementStatusModel? res = _advertisementStatusHandler.Create(request.ToEntity())?.ToModel();
            if (res == null)
            {
                return BadRequest();
            }
            return Created();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] AdvertisementStatusModel request)
        {
            AdvertisementStatusModel? res = _advertisementStatusHandler.Update(id, request.ToEntity())?.ToModel();
            if (res == null)
            {
                return NotFound();
            }
            return Created();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            AdvertisementStatusModel? res = _advertisementStatusHandler.Delete(id)?.ToModel();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
