namespace StratApiX.Domain.Entities
{
    public class TestSpec
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? SourceUrl { get; set; }
        public string? Version { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }

        public ICollection<TestCase> TestCases { get; set; } = [];
    }
}
