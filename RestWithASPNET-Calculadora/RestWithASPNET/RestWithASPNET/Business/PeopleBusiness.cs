using AutoMapper;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Business
{
    public class PeopleBusiness : IPeopleBusiness
    {
        private readonly IPeopleRepository _repository;
        private readonly IMapper _mapper;

        public PeopleBusiness(
            IPeopleRepository repository,
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

        public PeopleValueObject ChangeStatus(Guid id)
        {
            var entity = _repository.ChangeStatus(id);
            return _mapper.Map<PeopleValueObject>(entity);
        }

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
