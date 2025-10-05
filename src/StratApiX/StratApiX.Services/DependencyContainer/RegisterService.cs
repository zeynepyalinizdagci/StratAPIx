using Microsoft.Extensions.DependencyInjection;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using StratApiX.Services.Commands;
using StratApiX.Services.Factories;
using StratApiX.Services.Services;
using StratApiX.Services.Strategies;

namespace StratApiX.Services.DependencyContainer
{
    public static class RegisterService
    {
        public static IServiceCollection AddHttpCommands(this IServiceCollection services)
        {
            services.AddSingleton<IHttpCommand, HttpGetCommand>();
            services.AddSingleton<IHttpCommandFactory, HttpCommandFactory>();

            return services;
        }

        public static IServiceCollection AddAuthTypeStrategies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthStrategy, NoneAuthStrategy>();
            services.AddSingleton<IAuthStrategy, BasicAuthenticationStrategy>();
            services.AddSingleton<IAuthStrategy, KerberosAuthStrategy>();
            services.AddSingleton<IAuthStrategy, BamTokenAuthStrategy>();
            services.AddSingleton<IAuthStrategy, WindowAuthStragety>();
            services.AddSingleton<IAuthTypeFactory, AuthTypeFactory>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpRequestBuilder, HttpRequestBuilder>();
            return services;
        }
    }
}
