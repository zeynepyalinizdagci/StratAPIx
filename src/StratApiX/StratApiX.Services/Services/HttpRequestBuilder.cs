using StratApiX.Domain.Entities;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Text;

namespace StratApiX.Services.Services
{
    internal class HttpRequestBuilder : IHttpRequestBuilder
    {
        public HttpRequestMessage Build(TestCase testCase)
        {
            var method = testCase.Method switch
            {
                MethodTypeName.Get => HttpMethod.Get,
                _ => HttpMethod.Get,
            };

            var httpRequestMessage = new HttpRequestMessage(method, testCase.Url);

            if (testCase.Headers != null)
            {
                foreach (var kv in testCase.Headers)
                {
                    if (kv.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)) continue;
                    httpRequestMessage.Headers.TryAddWithoutValidation(kv.Key, kv.Value);
                }
            }

            if (!string.IsNullOrEmpty(testCase.RequestBodyJson) && (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH"))
            {
                httpRequestMessage.Content = new StringContent(testCase.RequestBodyJson, Encoding.UTF8, "application/json");
            }

            return httpRequestMessage;
        }
    }
}
