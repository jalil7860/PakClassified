using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PakClassified.API.Helper;
using PakClassified.Handlers.UserHandler;
using PakClassified.Models.User;

namespace PakClassified.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleHandler _roleHandler;
        public RolesController(IRoleHandler roleHandler)
        {
            _roleHandler = roleHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable <RoleModel> models= _roleHandler.GetAll().ToModelList();
            return Ok(models);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            RoleModel model = _roleHandler.GetById(id).ToModel();
            if (model == null)
                NotFound();
            return Ok(model);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] RoleModel request)
        {
            RoleModel? res = _roleHandler.Create(request.ToEntity()).ToModel();
            if(res == null)
            {
                BadRequest();
            }
            return Created("", res);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] RoleModel request)
        {
            RoleModel? res = _roleHandler.Update(id, request.ToEntity()).ToModel();
            if(res == null)
            {
                NotFound();
            }
            return Ok("Updated.");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteById(int id)
        {
            _roleHandler.Delete(id);
            return Ok();
        } 
    }
}
