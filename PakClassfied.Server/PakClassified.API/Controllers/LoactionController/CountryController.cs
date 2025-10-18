using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Entities.Location;
using PakClassified.Handler.LocationHandler;
using PakClassified.Models.Location;

namespace PakClassified.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryHandler _countryHandler;
        public CountryController(ICountryHandler countryHandler)
        {   
            _countryHandler = countryHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CountryModel> models = _countryHandler.GetAll().ToModelList();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                CountryModel? model = _countryHandler.GetById(id).ToModel();
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
        public IActionResult Create([FromBody] CountryModel request)
        {
            CountryHandler country = new CountryHandler();
            CountryModel? res = country.Create(request.ToEntity()).ToModel();

            if (res == null)
                BadRequest();

            return Created();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            _countryHandler.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] CountryModel req)
        {
            CountryModel? res = _countryHandler.Update(id, req.ToEntity()).ToModel();

            if (res == null)
                return NotFound();

            return Ok();
        }

    }
}
