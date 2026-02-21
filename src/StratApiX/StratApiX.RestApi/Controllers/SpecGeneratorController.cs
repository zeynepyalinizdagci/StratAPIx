using Microsoft.AspNetCore.Mvc;
using StratApiX.Domain.Entities;
using StratApiX.Services.Importer;

namespace StratApiX.RestApi.Controllers
{
    public class SpecGeneratorController : ControllerBase
    {
        private readonly ISwaggerImporter _importer;
        public SpecGeneratorController(ISwaggerImporter importer) {
            _importer = importer;
        }

        [HttpGet("spec/import")]
        public async Task<TestSpec> Import([FromQuery] string swaggerUri = "https://fakerestapi.azurewebsites.net/swagger/v1/swagger.json") => await _importer.Import(swaggerUri, new Domain.Entities.AuthProfile(), cancellationToken: CancellationToken.None);
        
    }
}
