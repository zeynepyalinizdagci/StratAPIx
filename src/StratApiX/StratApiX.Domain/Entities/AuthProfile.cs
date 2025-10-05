using StratApiX.Domain.Enums;

namespace StratApiX.Domain.Entities
{
    public class AuthProfile
    {
        public AuthType AuthType { get; set; } = AuthType.None;
        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string PasswordOrToken { get; set; }
    }
}
