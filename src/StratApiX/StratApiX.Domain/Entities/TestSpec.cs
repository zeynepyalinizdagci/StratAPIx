namespace StratApiX.Domain.Entities
{
    public class TestSpec
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public List<TestCase> TestCases { get; set; } = new();
    }
}
