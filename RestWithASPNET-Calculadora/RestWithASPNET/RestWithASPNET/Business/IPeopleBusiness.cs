using RestWithASPNET.Models;

namespace RestWithASPNET.Business
{
    public interface IPeopleBusiness
    {
        public People Create(People person);
        public People Update(Guid id, People person);
        public void Delete(Guid id);
        public List<People> Get();
        public People FindById(Guid id);
    }
}
