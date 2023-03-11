using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.Hypermedia.Filters;
using RestWithASPNET.CrossCutting.ValueObject;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;

        public UserController(IUserBusiness service)
        {
            _business = service;
        }        

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UserValueObject))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] UserValueObject person)
        {
            if (person == null) { return BadRequest(); }
            var result = _business.Create(person);
            return Ok(result);
        }
    }
}