using StratApiX.Domain.Enums;
using StratApiX.Domain.Interfaces;
using System.Collections.Concurrent;

namespace StratApiX.Services.Factories
{
    internal class HttpCommandFactory : IHttpCommandFactory
    {
        private readonly ConcurrentDictionary<MethodTypeName, IHttpCommand> _httpCommands = new();
        public Task Register(MethodTypeName methodType, IHttpCommand command)
        {
            _httpCommands.TryAdd(methodType, command);

            return Task.CompletedTask;
        }
        public IHttpCommand Create(MethodTypeName methodType)
        {
            if (_httpCommands.TryGetValue(methodType, out var httpCommand)) return httpCommand;
            throw new InvalidOperationException($"Invalid method type {methodType}");
        }
    }
}
