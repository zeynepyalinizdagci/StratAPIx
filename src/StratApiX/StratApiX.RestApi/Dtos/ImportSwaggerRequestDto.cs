using StratApiX.Domain.Enums;

namespace StratApiX.RestApi.Dtos
{
    public class ImportSwaggerRequestDto
    {
        public string SwaggerUrl { get; set; } = string.Empty;
    }

    public class ImportSwaggerResponseDto
    {
        public Guid SpecId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalEndpoints { get; set; }
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
    }

    public class TestSpecDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CaseCount { get; set; }
    }

    public class TestCaseDto
    {
        public Guid Id { get; set; }
        public MethodTypeName Method { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public int? ExpectedStatusCode { get; set; }
        public string? ExpectedResponse { get; set; }
    }
    public class TestRunDto
    {
        public Guid Id { get; set; }
        public Guid SpecId { get; set; }
        public RunStatus Status { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class TestResultDto
    {
        public Guid CaseId { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public long DurationMs { get; set; }
    }

}
