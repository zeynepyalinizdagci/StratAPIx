using StratApiX.Domain.Entities;

namespace StratApiX.Domain.Interfaces
{
    public interface IAuthStrategy
    {
        HttpClient CreateClient();
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile authProfile, CancellationToken cancellationToken);
    }
}
