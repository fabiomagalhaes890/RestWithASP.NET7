using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IPeopleRepository
    {
        public People Create(People person);
        public People Update(Guid id, People person);
        public void Delete(Guid id);
        public List<People> Get();
        public People FindById(Guid id);
    }
}
