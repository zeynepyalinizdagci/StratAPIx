using Microsoft.Extensions.DependencyInjection;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using StratApiX.Services.Commands;
using StratApiX.Services.Factories;
using StratApiX.Services.Importer;
using StratApiX.Services.Services;
using StratApiX.Services.Strategies;

namespace StratApiX.Services.DependencyContainer
{
    public static class RegisterService
    {
        public static IServiceCollection AddHttpCommands(this IServiceCollection services)
        {
            services.AddScoped<IHttpCommand, HttpGetCommand>();
            services.AddScoped<IHttpCommandFactory, HttpCommandFactory>();

            return services;
        }

        public static IServiceCollection AddAuthTypeStrategies(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IAuthStrategy, NoneAuthStrategy>();
            services.AddScoped<IAuthStrategy, BasicAuthenticationStrategy>();
            services.AddScoped<IAuthStrategy, KerberosAuthStrategy>();
            services.AddScoped<IAuthStrategy, BamTokenAuthStrategy>();
            services.AddScoped<IAuthStrategy, WindowAuthStragety>();
            services.AddScoped<IAuthTypeFactory, AuthTypeFactory>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHttpRequestBuilder, HttpRequestBuilder>();
            services.AddScoped<ISwaggerImporter, SwaggerImporter>();
            return services;
        }
    }
}
