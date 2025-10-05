using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Collections.Concurrent;

namespace StratApiX.Services.Factories
{
    internal class AuthTypeFactory : IAuthTypeFactory
    {
        private readonly IReadOnlyDictionary<AuthType, IAuthStrategy> _authStrategies;
        public AuthTypeFactory(IEnumerable<IAuthStrategy> strategies)
        {
            _authStrategies = strategies.ToDictionary(s => s.AuthType, s => s);
        }
        public IAuthStrategy GetStrategy(AuthType authType) {
            if (_authStrategies.TryGetValue(authType, out var selectedStragety)) { 
                return selectedStragety;
            }
            throw new InvalidOperationException($"Auth Type {authType} is not registered!");
        }
    }
}
