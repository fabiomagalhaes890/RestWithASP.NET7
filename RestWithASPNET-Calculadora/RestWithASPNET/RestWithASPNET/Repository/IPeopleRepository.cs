using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IPeopleRepository
    {
        People Create(People person);
        People Update(Guid id, People person);
        void Delete(Guid id);
        List<People> Get();
        People FindById(Guid id);
        bool Exists(Guid id);
    }
}
