using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Models.PakClassified;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementTypeController : ControllerBase
    {
        private readonly IAdvertisementTypeHandler _advertisementTypeHandler;
        public AdvertisementTypeController(IAdvertisementTypeHandler advertisementTypeHandler )
        {
            _advertisementTypeHandler = advertisementTypeHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AdvertisementTypeModel>? models = _advertisementTypeHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementTypeModel? model = _advertisementTypeHandler.GetById(id).ToModel();
            if(model == null)
            {
                NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] AdvertisementTypeModel request)
        {
            AdvertisementTypeModel? res = _advertisementTypeHandler.Create(request.ToEntity()).ToModel();
            if(res == null)
            {
                BadRequest();
            }
            return Created();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] AdvertisementTypeModel request)
        {
            AdvertisementTypeModel? res = _advertisementTypeHandler.Update(id, request.ToEntity())?.ToModel();
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
            AdvertisementTypeModel? res = _advertisementTypeHandler.Delete(id)?.ToModel();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

    }
}
