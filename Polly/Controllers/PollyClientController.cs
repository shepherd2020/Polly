using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PollyClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollyClientController : ControllerBase
    {
        private readonly IPollyApiProvider _pollyApiProvider;

        public PollyClientController(IPollyApiProvider pollyApiProvider)
        {
            _pollyApiProvider = pollyApiProvider;
        }

        [HttpGet("Timeout")]
        public async Task<ActionResult> Timeout()
        {
            var result = await _pollyApiProvider.GetTimeout();
            return Ok(result);
        }

        [HttpGet("Retry")]
        public async Task<ActionResult> Retry()
        {
            var result = await _pollyApiProvider.GetError();
            return Ok(result);
        }

        [HttpGet("CircuitBreaker")]
        public async Task<ActionResult> CircuitBreaker()
        {
            var result = await _pollyApiProvider.GetError();
            return Ok(result);
        }

        [HttpGet("Fallback")]
        public async Task<ActionResult> Fallback()
        {
            var result = await _pollyApiProvider.GetError();
            return Ok(result);
        }

        [HttpGet("BulkheadIsolation")]
        public async Task<ActionResult> BulkheadIsolation()
        {
            var result = await _pollyApiProvider.GetWorkingDelayed();
            return Ok(result);
        }
    }
}
