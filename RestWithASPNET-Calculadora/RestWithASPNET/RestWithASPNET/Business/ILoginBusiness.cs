using RestWithASPNET.CrossCutting.ValueObject;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        TokenValueObject ValidateCredentials(UserValueObject user);
        TokenValueObject ValidateCredentials(TokenValueObject token);
        bool RevokeToken(string userName);
    }
}
