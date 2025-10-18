using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.AdvertisementHandler.SubCategoryHandler;
using PakClassified.Models.PakClassified;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryHandler _subCategoryHandler;
        public SubCategoryController(ISubCategoryHandler subCategoryHandler)
        {
            _subCategoryHandler = subCategoryHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AdvertisementSubCategoryModel>? models = _subCategoryHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementSubCategoryModel? model = _subCategoryHandler.GetById(id).ToModel();
            if (model == null)
            {
                NotFound();
            }
            return Ok(model);
        }

        [HttpGet("ByCategory/{categoryId}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            IEnumerable<AdvertisementSubCategoryModel> models = _subCategoryHandler.GetByCategoryId(categoryId).ToModelList();

            if (models == null)
            {
                NotFound();
            }

            return Ok(models);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] AdvertisementSubCategoryModel request)
        {
            AdvertisementSubCategoryModel? res = _subCategoryHandler.Create(request.ToEntity()).ToModel();
            if (res == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] AdvertisementSubCategoryModel request)
        {
            AdvertisementSubCategoryModel? res = _subCategoryHandler.Update(id, request.ToEntity())?.ToModel();
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
            _subCategoryHandler.Delete(id);
            return NoContent();
        }
    }
}
