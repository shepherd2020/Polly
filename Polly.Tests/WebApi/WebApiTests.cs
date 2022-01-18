using FluentAssertions;
using NUnit.Framework;
using Polly.Timeout;
using Polly.Wrap;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polly.Tests.WebApi
{
    [TestFixture]
    public class WebApiTests
    {
        private IPollyApiProvider _pollyApiProvider;

        [SetUp]
        public void Init()
        {
            _pollyApiProvider = RestService.For<IPollyApiProvider>("http://localhost:5000/");
        }

        [Test]
        public void CallHttp_WithTimeoutPolicy()
        {
            var timeoutPolicy = Policy
                .TimeoutAsync(5, TimeoutStrategy.Pessimistic, (context, timespan, task) =>
                {
                    throw new TimeoutRejectedException();
                });


            Assert.ThrowsAsync<TimeoutRejectedException>(() => timeoutPolicy.ExecuteAsync(() => _pollyApiProvider.GetTimeout()));
        }

        [Test]
        public void CallHttp_WithRetryPolicy()
        {
            var retryPolicy = Policy
              .Handle<Exception>()
              .RetryAsync(3);

            
        }

        [Test]
        public void CallHttp_WithRetryWaitPolicy()
        {
            var retryPolicy = Policy
              .Handle<Exception>()
              .WaitAndRetryAsync(3, (n) => TimeSpan.FromSeconds(Math.Pow(2, n) / 2));

            Assert.ThrowsAsync<ApiException>(() => retryPolicy.ExecuteAsync(() => _pollyApiProvider.GetError()));
        }

        [Test]
        public void CallHttp_WithFallbackPolicy()
        {
            var fallbackPolicy = Policy<List<string>>
               .Handle<Exception>()
               .FallbackAsync<List<string>>(new List<string>() { "2", "4" })
               .ExecuteAsync(() => _pollyApiProvider.GetError()).Result;

            Assert.IsNotNull(fallbackPolicy);
        }

        [Test]
        public void CallHttp_WithCircuitBreakerPolicy()
        {
            var breaker = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var result = breaker.ExecuteAsync(() => _pollyApiProvider.GetError()).Result;
                }
                catch (Exception)
                {
                }

            }

        }

        [Test]
        public void CallHttp_WithMultiplePolicy()
        {
            var breaker = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

            var waitAndRetry = Policy
              .Handle<Exception>()
              .WaitAndRetryAsync(3, (n) => TimeSpan.FromSeconds(Math.Pow(2, n) / 2));

            var timeoutPolicy = Policy
               .TimeoutAsync(10);

            AsyncPolicyWrap policyWrap = Policy.WrapAsync(breaker, waitAndRetry, timeoutPolicy);

            var result = policyWrap.ExecuteAsync(() => _pollyApiProvider.GetError()).Result;

            Assert.IsNotNull(result);
        }
    }
}
