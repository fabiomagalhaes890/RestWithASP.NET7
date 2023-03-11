using AutoMapper;
using RestWithASPNET.CrossCutting.Hypermedia.Utils;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using System.Linq.Expressions;

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

        public List<PeopleValueObject> FindByName(string name)
        {
            var users = _repository.FindByName(name);
            return _mapper.Map<List<PeopleValueObject>>(users);
        }

        public PagedSearchValueObject<PeopleValueObject> FindWithPagedSearch(string name, string sort, int pageSize, int page)
        {            
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var result = 
                string.IsNullOrWhiteSpace(name) 
                ? _repository.Get().Skip(offset).Take(size).ToList()
                : _repository.FindWithPagedSearch((p => p.Name.Contains(name)), (p => p.Name), offset, sort, size);

            var total = _repository.GetCount((p => p.Name.Contains(name)));

            return new PagedSearchValueObject<PeopleValueObject>
            {
                CurrentPage = offset,
                List = _mapper.Map<List<PeopleValueObject>>(result),
                PageSize = size,
                SortDirections = (!string.IsNullOrWhiteSpace(sort)) && !sort.Equals("desc") ? "asc" : "desc",
                TotalResults = total
            };
        }
    }
}
