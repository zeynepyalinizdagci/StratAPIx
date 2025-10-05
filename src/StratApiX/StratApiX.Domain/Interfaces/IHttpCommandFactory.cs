using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Interfaces
{
    public interface IHttpCommandFactory
    {
        IHttpCommand Create(MethodTypeName methodType);
        Task Register(MethodTypeName methodType, IHttpCommand command);
    }
}
