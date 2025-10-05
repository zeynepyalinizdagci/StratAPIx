using StratApiX.Domain.Entities;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Diagnostics;

namespace StratApiX.Services.Commands
{
    internal class HttpGetCommand : IHttpCommand
    {
        private readonly IAuthTypeFactory _authTypeFactory;
        private readonly IHttpRequestBuilder _httpRequestBuilder;
        public MethodTypeName MethodTypeName => MethodTypeName.Get;

        public HttpGetCommand(IAuthTypeFactory authTypeFactory, IHttpRequestBuilder httpRequestBuilder)
        {
            _authTypeFactory = authTypeFactory;
            _httpRequestBuilder = httpRequestBuilder;
        }
        public async Task<TestCaseResult> Execute(TestCase testCase, CancellationToken cancellationToken)
        {
            var request = _httpRequestBuilder.Build(testCase);

            var authStragety = _authTypeFactory.GetStrategy(testCase.AuthProfile?.AuthType ?? Domain.Enums.AuthType.None);

            await authStragety.Apply(request, testCase.AuthProfile ?? new AuthProfile(), cancellationToken);

            using var client = authStragety.CreateClient();

            var sw = Stopwatch.StartNew();
            var response = await client.SendAsync(request, cancellationToken);
            sw.Stop();

            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            return new TestCaseResult
            {
                DurationMs = sw.ElapsedMilliseconds,
                ResponseBody = body,
                StatusCode = response.StatusCode,
                Success = response.IsSuccessStatusCode,
                TestCaseId = testCase.Id,
                TestCaseName = testCase.Name,
            };
        }
    }
}
