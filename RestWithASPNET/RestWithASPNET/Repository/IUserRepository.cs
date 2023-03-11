using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User ValidateCredentials(UserValueObject user);
        User ValidateCredentials(string userName);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
