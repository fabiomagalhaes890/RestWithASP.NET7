using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleBusiness _business;

        public PeopleController(IPeopleBusiness service)
        {
            _business = service;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get() => Ok(_business.Get());

        [HttpGet("{id:guid}")]
        public IActionResult FindById(Guid id)
        {
            var person = _business.FindById(id);
            if(person == null) NotFound();

            return base.Ok(person);
        }

        [HttpPut("{id:guid}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update(Guid id, [FromBody] PeopleValueObject person)
        {
            if (person == null || id == Guid.Empty) { return BadRequest(); }
            var result = _business.Update(id, person);
            return Ok(result);
        }

        [HttpPost]
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