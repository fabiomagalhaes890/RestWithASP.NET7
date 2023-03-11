using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.CrossCutting.ValueObject;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _login;

        public AuthController(ILoginBusiness login)
        {
            _login = login;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenValueObject))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Route("signin")]
        public IActionResult SignIn([FromBody] UserValueObject user)
        {
            if (user == null) return BadRequest("Invalid client request");

            var token = _login.ValidateCredentials(user);
            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenValueObject))]
        [ProducesResponseType(400)]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenValueObject tokenValueObject)
        {
            if (tokenValueObject == null) return BadRequest("Invalid client request");

            var token = _login.ValidateCredentials(tokenValueObject);
            if (token == null) return BadRequest();

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _login.RevokeToken(userName);

            if (!result) return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}