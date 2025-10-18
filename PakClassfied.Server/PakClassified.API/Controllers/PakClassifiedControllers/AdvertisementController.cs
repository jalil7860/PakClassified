        using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Models.PakClassified;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementHandler _advertisementHandler;
        public AdvertisementController(IAdvertisementHandler advertisementHandler)
        {
            _advertisementHandler = advertisementHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AdvertisementModel> models = _advertisementHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult SearchByQuery([FromQuery(Name = "name")] string? name,
                              [FromQuery(Name = "category")] int? categoryId,
                              [FromQuery(Name = "cityArea")] int? cityAreaId)
        {
            var result = _advertisementHandler.SearchByQuery(name, categoryId, cityAreaId).ToModelList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementModel? model = _advertisementHandler.GetById(id).ToModel();
            if (model == null)
            {
                NotFound();
            }
            return Ok(model);
        }
        [HttpGet("GetPostByUserId/{userId}")]
        public IActionResult GetPostByUserId(int userId)
        {
            IEnumerable<AdvertisementModel> models = _advertisementHandler.GetPostByUserId(userId).ToModelList();
            if (models == null)
                NotFound();

            return Ok(models);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] AdvertisementModel request)
        {
            AdvertisementModel? res = _advertisementHandler.Create(request.ToEntity()).ToModel();
            if (res == null)
            {
                return BadRequest();
            }
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] AdvertisementModel request)
        {
            AdvertisementModel? res = _advertisementHandler.Update(request.ToEntity(), id).ToModel();
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
            _advertisementHandler.Delete(id);
            return Ok();
        }
    }
}
