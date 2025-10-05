using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Entities
{
    public class TestCase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public MethodTypeName Method { get; set; } = MethodTypeName.Get;
        public string Url { get; set; } = string.Empty;
        public Dictionary<string, string>? Headers { get; set; }
        public string? RequestBodyJson { get; set; }
        public int? ExpectedStatusCode { get; set; }
        public AuthProfile? AuthProfile { get; set; } = new AuthProfile { AuthType = AuthType.None };
    }

    public class TestCaseResult {
        public Guid TestCaseId { get; set; }
        public string TestCaseName { get; set; } = string.Empty;
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string? ResponseBody { get; set; }
        public long DurationMs { get; set; }
    }
}
