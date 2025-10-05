using StratApiX.Domain.Entities;
using StratApiX.Domain.Interfaces;

namespace StratApiX.Services.Commands
{
    internal class HttpGetCommand : IHttpCommand
    {
        private readonly IAuthTypeFactory _authTypeFactory;

        public HttpGetCommand(IAuthTypeFactory authTypeFactory)
        {
            _authTypeFactory = authTypeFactory;            
        }
        public async Task Execute(TestCase testCase, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, testCase.Endpoint);
            var authStragety = _authTypeFactory
               .Create(testCase.AuthProfile.AuthType);

            await authStragety.Apply(request, testCase.AuthProfile, cancellationToken);

            var client = authStragety.CreateClient();

            var response = await client.SendAsync(request, cancellationToken);

            var testCaseResponse = new TestCaseResponse
            {
                TestCaseId = testCase.Id,
                TestSpecId = testCase.SpecId
            };


            if (response == null) {
                testCaseResponse.Error = new[] { "Error : response is null" }; 
            }
            var responseStr = await response.Content.ReadAsStringAsync();

            

            return Task.CompletedTask;
        }
    }

    internal class HttpPostCommand : IHttpCommand
    {
        public Task Execute(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }

    internal class HttpPutCommand : IHttpCommand
    {
        public Task Execute(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }

    internal class HttpDeleteCommand : IHttpCommand
    {
        public Task Execute(TestCase testCase)
        {
            throw new NotImplementedException();
        }
    }
}
