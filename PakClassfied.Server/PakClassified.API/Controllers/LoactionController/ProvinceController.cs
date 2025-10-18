using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handler.LocationHandler;
using PakClassified.Models.Location;

namespace PakClassified.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceHanlder _provinceHandler;
        public ProvinceController(IProvinceHanlder provinceHandler)
        {
            _provinceHandler = provinceHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ProvinceModel> models = _provinceHandler.GetAll().ToModelList();
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                ProvinceModel? model = _provinceHandler.GetById(id).ToModel();
                if (model == null)
                    NotFound();


                return Ok(model);
            }
            catch (Exception err)
            {

                return StatusCode(505, err.Message);
            }
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] ProvinceModel request)
        {
            ProvinceModel? res = _provinceHandler.Create(request.ToEntity()).ToModel();

            if (res == null)
                BadRequest();

            return Created();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            _provinceHandler.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] ProvinceModel req)
        {
            ProvinceModel? res = _provinceHandler.Update(id, req.ToEntity()).ToModel();

            if (res == null)
                return NotFound();

            return Ok();
        }
    }
}