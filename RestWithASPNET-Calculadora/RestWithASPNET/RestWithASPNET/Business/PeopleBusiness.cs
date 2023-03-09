using AutoMapper;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Business
{
    public class PeopleBusiness : IPeopleBusiness
    {
        private readonly IRepository<People> _repository;
        private readonly IMapper _mapper;

        public PeopleBusiness(
            IRepository<People> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PeopleValueObject Create(PeopleValueObject person)
        {
            var entity = _mapper.Map<People>(person);            
            var result = _repository.Create(entity);
            return _mapper.Map<PeopleValueObject>(result);
        }

        public void Delete(Guid id) => _repository.Delete(id);

        public PeopleValueObject FindById(Guid id)
        {
            var result = _repository.FindById(id);
            return _mapper.Map<PeopleValueObject>(result);
        }

        public List<PeopleValueObject> Get()
        {
            var result = _repository.Get();
            return _mapper.Map<List<PeopleValueObject>>(result);
        }

        public PeopleValueObject Update(Guid id, PeopleValueObject person)
        {
            var entity = _mapper.Map<People>(person);
            var result = _repository.Update(id, entity);
            return _mapper.Map<PeopleValueObject>(result);            
        }
    }
}
