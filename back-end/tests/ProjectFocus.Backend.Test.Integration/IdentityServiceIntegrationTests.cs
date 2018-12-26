using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RawRabbit;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ProjectFocus.Backend.Test.Integration
{
    public class IdentityServiceIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IdentityServiceIntegrationTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Service.Identity.Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task IdentityAccountTryAuthenticate()
        {
            // Creating a token for a test user ID
            var userId = Guid.NewGuid();
            var provider = _server.Host.Services;
            var token = Common.Auth.getJwt(provider, userId);

            // Adding an authorization header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

            // Awaiting results and checking for success
            var result = await _client.GetAsync("/account/try");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            // Checking for specific response
            var stringResult = await result.Content.ReadAsStringAsync();
            Assert.Equal("Success!", stringResult);
        }
    }
}
