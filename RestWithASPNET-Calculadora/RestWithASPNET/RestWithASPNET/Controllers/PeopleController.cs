using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;
using RestWithASPNET.Business;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PeopleValueObject>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get() => Ok(_business.Get());

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(PeopleValueObject))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(Guid id)
        {
            var person = _business.FindById(id);
            if(person == null) NotFound();

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
            if(person == null) { return BadRequest(); }
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