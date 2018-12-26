using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RawRabbit;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ProjectFocus.Backend.Test.Integration
{
    public class ApiIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiIntegrationTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Api.Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ApiCreateProblem()
        {
            // A new create problem command to post
            var createProblemCommand = new
            {
                Name = "test problem 1",
                Description = "test problem 1 description",
                Content = "test problem content"
            };

            // A user to authenticate
            var userId = Guid.NewGuid();
            var provider = _server.Host.Services;
            var token = Common.Auth.getJwt(provider, userId);

            // The message queue
            var bus = Common.Bus.get(provider);

            async Task<Task<int>> waitForCommand()
            {
                var tcs = new TaskCompletionSource<int>();
                await bus.SubscribeAsync<Common.Command.AuthenticatedCommand>(async x =>
                {
                    Assert.Equal(userId, x.UserId);
                    Assert.Equal(createProblemCommand.Name, x.Command.Item.Name);
                    Assert.Equal(createProblemCommand.Description, x.Command.Item.Description);
                    Assert.Equal(createProblemCommand.Content, x.Command.Item.Content);
                    tcs.SetResult(1);
                });
                return tcs.Task;
            }
            var commandWaitingTask = await waitForCommand();

            // The authorization header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            var result = await _client.PostAsJsonAsync("/api/problems", createProblemCommand);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            // Right now all methods return results as JSON, so we deserialize the result 
            var stringResult = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
            Assert.Equal("Accepted!", stringResult);

            // We make sure that the problem creation command is sent properly
            Assert.Equal(1, await commandWaitingTask);
        }
    }
}
