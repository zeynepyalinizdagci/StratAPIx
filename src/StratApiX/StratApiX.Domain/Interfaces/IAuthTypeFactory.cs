using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Interfaces
{
    public interface IAuthTypeFactory
    {
        Task Register(AuthType authType, IAuthStrategy strategy);
        IAuthStrategy Create(AuthType authType);
    }
}
