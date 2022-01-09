using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.Caching;
using Polly.Registry;
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
        private readonly IPolicyRegistry<string> _policyRegistry;

        public PollyClientController(IPollyApiProvider pollyApiProvider, IPolicyRegistry<string> policyRegistry)
        {
            _pollyApiProvider = pollyApiProvider;
            _policyRegistry = policyRegistry;
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

        [HttpGet("Cache")]
        public async Task<ActionResult> Cache(string start)
        {
            var cachePolicy = _policyRegistry.Get<AsyncCachePolicy<List<string>>>("CachingPolicy");
            Context policyExecutionContext = new Context($"GetCache-{start}");

            var result = await cachePolicy.ExecuteAsync(context => _pollyApiProvider.GetCache(start), policyExecutionContext);
            return Ok(result);
        }
    }
}
