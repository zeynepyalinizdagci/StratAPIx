using Microsoft.Extensions.DependencyInjection;
using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using StratApiX.Services.Commands;
using StratApiX.Services.Factories;
using StratApiX.Services.Strategies;

namespace StratApiX.Services.DependencyContainer
{
    public static class Services
    {
        public static IServiceCollection AddHttpCommands(this IServiceCollection services)
        {
            services.AddSingleton<IHttpCommandFactory>(provider =>
            {
                var factory = new HttpCommandFactory();

                factory.Register(MethodTypeName.Get, new HttpGetCommand());
                factory.Register(MethodTypeName.Post, new HttpPostCommand());
                factory.Register(MethodTypeName.Put, new HttpPutCommand());
                factory.Register(MethodTypeName.Delete, new HttpDeleteCommand());

                return factory;
            });
            return services;
        }

        public static IServiceCollection AddAuthTypeStrategies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthTypeFactory>(provider =>
            {
                var factory = new AuthTypeFactory();

                factory.Register(AuthType.None, new NoneAuthStrategy());
                factory.Register(AuthType.Basic, new BasicAuthenticationStrategy());
                factory.Register(AuthType.Windows, new WindowAuthStragety());
                factory.Register(AuthType.Kerberos, new KerberosAuthStrategy());
                factory.Register(AuthType.BamToken, new BamTokenAuthStrategy());

                return factory;
            });
            return services;
        }
    }
}
