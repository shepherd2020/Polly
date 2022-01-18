using FluentAssertions;
using NUnit.Framework;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polly.Tests.SomeMethod
{
    [TestFixture]
    public class SomeMethodTests
    {
        [Test]
        public void CallSomeMethod_WithRetryPolicy()
        {
            var sut = new SomeMethod();

            var result = Policy
              .Handle<Exception>()
              .Retry(3)
              .Execute(() => sut.Do());

            result.Should().NotBeEmpty();
        }
    }
}
