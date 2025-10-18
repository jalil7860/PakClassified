using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.UserHandler;
using PakClassified.Models.PakClassified;
using PakClassified.Models.User;

namespace PakClassified.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userHandler;
        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserModel> models = _userHandler.GetAll().ToModelList();
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            UserModel? model = _userHandler.GetById(id).ToModel();
            if (model == null)
            {
                NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] UserModel request)
        {
            UserModel? res = _userHandler.Create(request.ToEntity()).ToModel();
            if (res == null)
                return BadRequest();
            return Created("", res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UserModel request)
        {
            UserModel? res = _userHandler.Update(id, request.ToEntity()).ToModel();
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            UserModel? res = _userHandler.Delete(id).ToModel();
            if (res == null)
                return NotFound();
            return Ok(res);
        }
    }
}
