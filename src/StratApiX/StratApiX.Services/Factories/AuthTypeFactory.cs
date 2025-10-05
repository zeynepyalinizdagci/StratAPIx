using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Collections.Concurrent;

namespace StratApiX.Services.Factories
{
    internal class AuthTypeFactory : IAuthTypeFactory
    {
        private readonly ConcurrentDictionary<AuthType, IAuthStrategy> _authStrategies = new();
        public Task Register(AuthType authType, IAuthStrategy strategy) {
            _authStrategies.TryAdd(authType, strategy);

            return Task.CompletedTask;
        }
        public IAuthStrategy Create(AuthType authType) {
            if (_authStrategies.TryGetValue(authType, out var selectedStragety)) { 
                return selectedStragety;
            }
            throw new InvalidOperationException($"Auth Type {authType} is not registered!");
        }
    }
}
