using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Qoniac.API.Convertors;

namespace Qoniac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyToWordController : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string Get(string number)
        {
            return CurrencyToWords.CurrencyToWord(number);
        }
    }
}
