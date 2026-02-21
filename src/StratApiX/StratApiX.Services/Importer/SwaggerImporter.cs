using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using StratApiX.Domain.Entities;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Text;

namespace StratApiX.Services.Importer
{
    public class SwaggerImporter : ISwaggerImporter
    {
        private readonly IAuthTypeFactory _authTypeFactory;
        public SwaggerImporter(IAuthTypeFactory authTypeFactory)
        {
            _authTypeFactory = authTypeFactory;
        }

        public async Task<TestSpec> Import(string swaggerUri, AuthProfile authProfile, CancellationToken cancellationToken)
        {
            using var client = _authTypeFactory.GetStrategy(authProfile.AuthType).CreateClient();
            using var stream = await client.GetStreamAsync(swaggerUri, cancellationToken);

            var reader = new OpenApiStreamReader();
            var content = await reader.ReadAsync(stream, cancellationToken);
            if (content.OpenApiDiagnostic.Errors.Any())
            {
                throw new Exception($"Swagger error {string.Join(", ", content.OpenApiDiagnostic.Errors)}");
                //TODO use custom Exception like SwaggerException
            }

            var spec = new TestSpec
            {
                BaseUrl = content.OpenApiDocument.Servers?.FirstOrDefault()?.Url ?? ExtractBaseUrl(swaggerUri),
                Id = Guid.NewGuid(),
                Name = content.OpenApiDocument.Info.Title
            };
            var schemas = content.OpenApiDocument.Components?.Schemas ?? new Dictionary<string, OpenApiSchema>();

            foreach (var (path, pathItem) in content.OpenApiDocument.Paths)
            {
                foreach (var (opType, operation) in pathItem.Operations)
                {
                    var testCase = new TestCase
                    {
                        Name = operation.OperationId ?? $"{opType} {path}",
                        Method = MapHttpVerb(opType),
                        Url = CombineUrls(spec.BaseUrl, path),
                        Headers = new Dictionary<string, string>(),
                        RequestBodyJson = GenerateSampleRequestBody(operation, schemas),
                        ExpectedResponses = GenerateExpectedResponses(operation.Responses),
                        AuthProfile = authProfile,
                    };

                    spec.TestCases.Add(testCase);
                }
            }
            return spec;
        }

        private string GenerateSampleRequestBody(OpenApiOperation operation, IDictionary<string, OpenApiSchema> schemas)
        {
            var pathParameter = operation.Parameters.Where(s => s.In == ParameterLocation.Path);

            var queryParameters = operation.Parameters.Where(s => s.In == ParameterLocation.Query);

            var headerParameters = operation.Parameters.Where(s => s.In == ParameterLocation.Header);

            foreach (var param in pathParameter)
            {
                var sb = new StringBuilder();
                sb.Append(param.Name);
                sb.Append("=");
                return sb.ToString();
            }

            if (queryParameters.Any())
            {
                return string.Join("&", queryParameters);
            }

            if (headerParameters.Any())
            {
                return string.Join("&", headerParameters);
            }


            return string.Empty;
        }

        private IEnumerable<ExpectedResponse> GenerateExpectedResponses(OpenApiResponses responses)
        {
            var expectedResponse = new List<ExpectedResponse>();
            foreach (var (key, val) in responses)
            {
                if (!int.TryParse(key, out var statusCode))
                {
                    continue;
                }
                var response = new ExpectedResponse
                {
                    ExpectedStatusCode = statusCode
                };
                expectedResponse.Add(response);
            }
            return expectedResponse;
        }

        private string GeneratExpectedRequest(OpenApiResponse val)
        {
            throw new NotImplementedException();
        }

        private static string CombineUrls(string baseUrl, string path)
            => (baseUrl?.TrimEnd('/') ?? "") + (path.StartsWith("/") ? path : "/" + path);
        private static MethodTypeName MapHttpVerb(OperationType opType) =>
                    opType switch
                    {
                        OperationType.Get => MethodTypeName.Get,
                        OperationType.Post => MethodTypeName.Post,
                        OperationType.Put => MethodTypeName.Put,
                        OperationType.Delete => MethodTypeName.Delete,
                        OperationType.Patch => MethodTypeName.Patch,
                        _ => MethodTypeName.Get
                    };
        private static string ExtractBaseUrl(string swaggerUrl)
        {
            try
            {
                var u = new Uri(swaggerUrl);
                return $"{u.Scheme}://{u.Host}{(u.IsDefaultPort ? "" : ":" + u.Port)}";
            }
            catch { return swaggerUrl; }
        }
    }

    public interface ISwaggerImporter
    {
        Task<TestSpec> Import(string swaggerUri, AuthProfile authProfile, CancellationToken cancellationToken);
    }
}
