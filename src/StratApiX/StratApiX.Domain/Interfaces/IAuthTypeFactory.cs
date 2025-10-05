using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Interfaces
{
    public interface IAuthTypeFactory
    {
        IAuthStrategy GetStrategy(AuthType authType);
    }
}
