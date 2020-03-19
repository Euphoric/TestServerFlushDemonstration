using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace TestServerFlushDemonstration.Test
{
    public class TestServerResponseFlush
    {
        [Fact]
        public async Task Correct_request()
        {
            var builder = WebHost.CreateDefaultBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>()
                    .UseTestServer()
                ;

            var testServer = new TestServer(builder)
            {
                // use HTTPS for all requests
                BaseAddress = new Uri("https://localhost/")
            };

            var result = await testServer.CreateClient().GetAsync("weatherforecast");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Synchronous_write_should_fail()
        {
            var builder = WebHost.CreateDefaultBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>()
                    .UseTestServer()
                ;

            var testServer = new TestServer(builder)
            {
                // use HTTPS for all requests
                BaseAddress = new Uri("https://localhost/")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                testServer.CreateClient().GetAsync("weatherforecast?doWrite=true"));
        }

        [Fact]
        public async Task Synchronous_flush_should_fail()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
                .UseTestServer()
                ;

            var testServer = new TestServer(builder)
            {
                // use HTTPS for all requests
                BaseAddress = new Uri("https://localhost/")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                testServer.CreateClient().GetAsync("weatherforecast?doFlush=true"));
        }
    }
}
