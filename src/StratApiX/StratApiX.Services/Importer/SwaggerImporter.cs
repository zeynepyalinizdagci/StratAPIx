using Microsoft.OpenApi.Readers;
using StratApiX.Domain.Entities;
using StratApiX.Domain.Interfaces;

namespace StratApiX.Services.Importer
{
    internal class SwaggerImporter : ISwaggerImporter
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
            if (content.OpenApiDiagnostic.Errors != null)
            {
                throw new Exception($"Swagger error {string.Join(", ", content.OpenApiDiagnostic.Errors)}");
                //TODO use custom Exception like SwaggerException
            }

            return new TestSpec
            {
                BaseUrl = swaggerUri,
                Id = Guid.NewGuid(),
                Name = swaggerUri, //TODO
                TestCases = [] // TODO
            };
        }
    }

    internal interface ISwaggerImporter
    {
        Task<TestSpec> Import(string swaggerUri, AuthProfile authProfile, CancellationToken cancellationToken);
    }
}
