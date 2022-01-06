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
    public class TimeoutController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TimeoutController> _logger;

        public TimeoutController(ILogger<TimeoutController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();

            Thread.Sleep(11000);

            return Enumerable.Range(1, 5).Select(index => Summaries[rng.Next(Summaries.Length)])
                .ToArray();
        }
    }
}
