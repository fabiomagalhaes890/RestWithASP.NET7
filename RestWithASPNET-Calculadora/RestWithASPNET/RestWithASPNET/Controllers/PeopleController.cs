using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Models;
using RestWithASPNET.Services.People;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _service;

        public PeopleController(IPeopleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.Get());

        [HttpGet("/{id}")]
        public IActionResult FindById(Guid id)
        {
            var person = _service.FindById(id);
            if(person == null) NotFound();

            return base.Ok(person);
        }

        [HttpPut("/{id}")]
        public IActionResult Update(Guid id, [FromBody] People person)
        {
            if (person == null || id == null) { return BadRequest(); }
            var result = _service.Update(id, person);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] People person)
        {
            if(person == null) { return BadRequest(); }
            var result = _service.Create(person);
            return Ok(result);
        }

        [HttpDelete("/{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }    
}