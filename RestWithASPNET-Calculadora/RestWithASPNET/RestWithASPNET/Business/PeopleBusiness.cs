using RestWithASPNET.Models;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Business
{
    public class PeopleBusiness : IPeopleBusiness // validacoes de regras de negócio
    {
        private readonly IPeopleRepository _repository;

        public PeopleBusiness(IPeopleRepository repository) => _repository = repository;

        public People Create(People person) => _repository.Create(person);

        public void Delete(Guid id) => _repository.Delete(id);

        public People FindById(Guid id) => _repository.FindById(id);

        public List<People> Get() => _repository.Get();

        public People Update(Guid id, People person) => _repository.Update(id, person);
    }
}
