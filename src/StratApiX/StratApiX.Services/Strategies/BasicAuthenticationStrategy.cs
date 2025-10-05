using StratApiX.Domain.Interfaces;
using System.Net.Http.Headers;
using StratApiX.Domain.Entities;
using System.Text;
using System.Net.Http;
using StratApiX.Domain.Enums;

namespace StratApiX.Services.Strategies
{
    public class NoneAuthStrategy : IAuthStrategy
    {
        public AuthType AuthType => AuthType.None;
        public HttpClient CreateClient() => new();
        public Task Apply(HttpRequestMessage request, AuthProfile profile, CancellationToken cancellationToken) => Task.CompletedTask;
    }
    internal class BasicAuthenticationStrategy : IAuthStrategy
    {
        private readonly IHttpClientFactory _clientFactory;
        public AuthType AuthType => AuthType.Basic;

        public BasicAuthenticationStrategy(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public HttpClient CreateClient() => _clientFactory.CreateClient();

        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile profile, CancellationToken cancellationToken)
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{profile.UserInfo.Name}:{profile.UserInfo.PasswordOrToken}"));
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            return Task.CompletedTask;
        }

    }

    internal class KerberosAuthStrategy : IAuthStrategy
    {
        public AuthType AuthType => AuthType.Kerberos;
        public HttpClient CreateClient()
        {
            return new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
        }
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile profile, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(profile.UserInfo.PasswordOrToken))
            {
                httpRequestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Negotiate", profile.UserInfo.PasswordOrToken);
            }
            return Task.CompletedTask;
        }

    }

    internal class BamTokenAuthStrategy : IAuthStrategy
    {
        public AuthType AuthType => AuthType.BamToken;
        private readonly IHttpClientFactory _clientFactory;
        public HttpClient CreateClient() => _clientFactory.CreateClient();

        public BamTokenAuthStrategy(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public Task Apply(HttpRequestMessage request, AuthProfile profile, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(profile.UserInfo.PasswordOrToken))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("BAM", profile.UserInfo.PasswordOrToken);
            }
            return Task.CompletedTask;
        }
    }

    internal class WindowAuthStragety : IAuthStrategy
    {
        public AuthType AuthType => AuthType.Windows;
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile authProfile, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public HttpClient CreateClient()
        {
            return new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
        }
    }
}
