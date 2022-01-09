using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PollyService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollyServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PollyServiceController> _logger;

        public PollyServiceController(ILogger<PollyServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Timeout")]
        public IEnumerable<string> Timeout()
        {
            var rng = new Random();

            Thread.Sleep(11000);

            return Enumerable.Range(1, 5).Select(index => Summaries[rng.Next(Summaries.Length)])
                .ToArray();
        }

        [HttpGet("Error")]
        public IEnumerable<string> Error()
        {
            var rng = new Random();

            throw new Exception("Error occured");

            return Enumerable.Range(1, 5).Select(index => Summaries[rng.Next(Summaries.Length)])
                .ToArray();
        }

        [HttpGet("Working")]
        public IEnumerable<string> Working()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => Summaries[rng.Next(Summaries.Length)])
                .ToArray();
        }

        [HttpGet("WorkingDelayed")]
        public IEnumerable<string> WorkingDelayed()
        {
            var rng = new Random();
            Thread.Sleep(1000);

            return Enumerable.Range(1, 5).Select(index => Summaries[rng.Next(Summaries.Length)])
                .ToArray();
        }

        [HttpGet("Cache")]
        public IEnumerable<string> Cache(string start)
        {
            return Summaries.Where(m => m.StartsWith(start));
        }
    }
}
