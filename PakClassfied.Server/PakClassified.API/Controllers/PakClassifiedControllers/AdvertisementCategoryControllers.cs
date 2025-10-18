using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Models.PakClassified;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementCategoryController : ControllerBase
    {
        private readonly IAdvertisementCategoryHandler _advertisementCategoryHandler;
        public AdvertisementCategoryController(IAdvertisementCategoryHandler advertisementCategoryHandler)
        {   
            _advertisementCategoryHandler = advertisementCategoryHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AdvertisementCategoryModel> models = _advertisementCategoryHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementCategoryModel? model = _advertisementCategoryHandler.GetById(id).ToModel();
            if (model == null)
            {
                NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] AdvertisementCategoryModel request)
        {
            AdvertisementCategoryModel? res = _advertisementCategoryHandler.Create(request.ToEntity()).ToModel();
            if (res == null)
                return BadRequest();
            return Created("", res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] AdvertisementCategoryModel request)
        {
            AdvertisementCategoryModel? res = _advertisementCategoryHandler.Update(id, request.ToEntity()).ToModel();
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            AdvertisementCategoryModel? res = _advertisementCategoryHandler.Delete(id).ToModel();
            if (res == null)
                return NotFound();
            return Ok(res);
        }
    }
}
