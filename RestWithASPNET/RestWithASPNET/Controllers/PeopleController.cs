using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;
using RestWithASPNET.CrossCutting.ValueObject;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleBusiness _business;

        public PeopleController(IPeopleBusiness service)
        {
            _business = service;
        }

        [HttpGet("sort/{sortDirection}/pageSize/{pageSize}/page/{page}")]
        [ProducesResponseType(200, Type = typeof(List<PeopleValueObject>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string? name, string sortDirection, int pageSize, int page)
        {        
            return Ok(_business.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(PeopleValueObject))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(Guid id)
        {
            var person = _business.FindById(id);
            if (person == null) NotFound();

            return base.Ok(person);
        }

        [HttpGet("findByName")]
        [ProducesResponseType(200, Type = typeof(List<PeopleValueObject>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetByName([FromQuery] string name)
        {
            var person = _business.FindByName(name);
            if (person == null) NotFound();

            return base.Ok(person);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(PeopleValueObject))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update(Guid id, [FromBody] PeopleValueObject person)
        {
            if (person == null || id == Guid.Empty) { return BadRequest(); }
            var result = _business.Update(id, person);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PeopleValueObject))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] PeopleValueObject person)
        {
            if (person == null) { return BadRequest(); }
            var result = _business.Create(person);
            return Ok(result);
        }

        [HttpDelete("/{id}")]
        public IActionResult Delete(Guid id)
        {
            _business.Delete(id);
            return NoContent();
        }
    }
}