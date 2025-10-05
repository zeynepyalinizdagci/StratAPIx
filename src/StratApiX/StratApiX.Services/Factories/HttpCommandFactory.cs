using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Collections.Concurrent;

namespace StratApiX.Services.Factories
{
    internal class HttpCommandFactory : IHttpCommandFactory
    {
        private readonly IReadOnlyDictionary<MethodTypeName, IHttpCommand> _httpCommands;
        public HttpCommandFactory(IEnumerable<IHttpCommand> httpCommands)
        {
            _httpCommands = httpCommands.ToDictionary(s => s.MethodTypeName, s => s);

        }
        public IHttpCommand Create(MethodTypeName methodType)
        {
            if (_httpCommands.TryGetValue(methodType, out var httpCommand)) return httpCommand;
            throw new InvalidOperationException($"Invalid method type {methodType}");
        }
    }
}
