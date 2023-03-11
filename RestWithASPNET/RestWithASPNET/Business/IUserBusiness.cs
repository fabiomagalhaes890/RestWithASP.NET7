using RestWithASPNET.CrossCutting.ValueObject;

namespace RestWithASPNET.Business
{
    public interface IUserBusiness
    {
        UserValueObject Create(UserValueObject person);
    }
}
