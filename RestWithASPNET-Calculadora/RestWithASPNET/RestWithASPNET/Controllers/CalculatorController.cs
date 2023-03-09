using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.WebSockets;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {      

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            var tuple1 = TryParse(firstNumber);
            var tuple2 = TryParse(secondNumber);

            if (tuple1.Item1 
                && tuple2.Item1)
            {   
                return Ok(tuple1.Item2 + tuple2.Item2);
            }

            return BadRequest("invalid input");
        }

        private Tuple<bool, decimal> TryParse(string number)
        {
            var isConvertible = decimal.TryParse(number, 
                NumberStyles.Any, 
                NumberFormatInfo.InvariantInfo, 
                out decimal result);

            return new Tuple<bool, decimal>(isConvertible, result);
        }
    }    
}