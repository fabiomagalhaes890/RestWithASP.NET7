using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserValueObject user);
        User ValidateCredentials(string userName);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
