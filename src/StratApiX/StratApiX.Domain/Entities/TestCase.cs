using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Entities
{
    public class TestCase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SpecId { get; set; }

        public MethodTypeName Method { get; set; }
        public string Endpoint { get; set; } = string.Empty;

        public string? RequestBody { get; set; }
        public string? RequestHeadersJson { get; set; }

        public string? ExpectedResponse { get; set; }
        public int? ExpectedStatusCode { get; set; }

        public AuthProfile AuthProfile { get; set; }

        public TestSpec? Spec { get; set; }
    }

    public class TestCaseResponse { 
        public Guid Id { get; set; }
        public Guid TestCaseId { get; set; }
        public Guid TestSpecId { get; set; }
        public string[]? Error { get; set; }
        public bool IsSuccess => Error == null;
    }
}
