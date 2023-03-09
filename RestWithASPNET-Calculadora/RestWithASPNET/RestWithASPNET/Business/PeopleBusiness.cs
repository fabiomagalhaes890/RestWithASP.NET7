using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Business
{
    public class PeopleBusiness : IPeopleBusiness // validacoes de regras de negócio
    {
        private readonly IRepository<People> _repository;

        public PeopleBusiness(IRepository<People> repository) => _repository = repository;

        public People Create(People person) => _repository.Create(person);

        public void Delete(Guid id) => _repository.Delete(id);

        public People FindById(Guid id) => _repository.FindById(id);

        public List<People> Get() => _repository.Get();

        public People Update(Guid id, People person) => _repository.Update(id, person);
    }
}
