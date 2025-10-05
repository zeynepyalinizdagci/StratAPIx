using StratApiX.Domain.Interfaces;
using System.Net.Http.Headers;
using StratApiX.Domain.Entities;
using System.Text;

namespace StratApiX.Services.Strategies
{
    public class NoneAuthStrategy : IAuthStrategy
    {
        public HttpClient CreateClient() => new HttpClient();
        public Task Apply(HttpRequestMessage request, AuthProfile profile, CancellationToken cancellationToken) => Task.CompletedTask;
    }
    internal class BasicAuthenticationStrategy : IAuthStrategy
    {
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile profile, CancellationToken cancellationToken)
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{profile.UserInfo.Name}:{profile.UserInfo.PasswordOrToken}"));
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            return Task.CompletedTask;
        }

        public HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }

    internal class KerberosAuthStrategy : IAuthStrategy
    {
        public Task Apply(HttpRequestMessage httpRequestMessage, AuthProfile profile, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(profile.UserInfo.PasswordOrToken))
            {
                httpRequestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Negotiate", profile.UserInfo.PasswordOrToken);
            }
            return Task.CompletedTask;
        }

        public HttpClient CreateClient()
        {
            return new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
        }
    }

    internal class BamTokenAuthStrategy : IAuthStrategy
    {
        public Task Apply(HttpRequestMessage request, AuthProfile profile, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(profile.UserInfo.PasswordOrToken))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("BAM", profile.UserInfo.PasswordOrToken);
            }
            return Task.CompletedTask;
        }
        public HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }

    internal class WindowAuthStragety : IAuthStrategy
    {
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
