using StratApiX.Domain.Entities;
using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Interfaces
{
    public interface IHttpCommand
    {
        MethodTypeName MethodTypeName { get; }
        Task<TestCaseResult> Execute(TestCase testCase, CancellationToken cancellationToken);
    }
}
