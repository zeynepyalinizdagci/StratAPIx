using StratApiX.Domain.Entities;
using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Interfaces
{
    public interface IAuthStrategy
    {
        AuthType AuthType { get; }
        HttpClient CreateClient();
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile authProfile, CancellationToken cancellationToken);
    }
}
