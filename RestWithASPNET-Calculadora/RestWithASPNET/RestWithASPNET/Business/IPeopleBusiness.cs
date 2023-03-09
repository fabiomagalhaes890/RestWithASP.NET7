using RestWithASPNET.Models;

namespace RestWithASPNET.Business
{
    public interface IPeopleBusiness
    {
        People Create(People person);
        People Update(Guid id, People person);
        void Delete(Guid id);
        List<People> Get();
        People FindById(Guid id);
    }
}
