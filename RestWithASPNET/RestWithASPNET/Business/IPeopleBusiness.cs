using RestWithASPNET.CrossCutting.Hypermedia.Utils;
using RestWithASPNET.CrossCutting.ValueObject;
using RestWithASPNET.Models;

namespace RestWithASPNET.Business
{
    public interface IPeopleBusiness
    {
        PeopleValueObject Create(PeopleValueObject person);
        PeopleValueObject Update(Guid id, PeopleValueObject person);
        void Delete(Guid id);
        List<PeopleValueObject> Get();
        PeopleValueObject FindById(Guid id);
        PeopleValueObject ChangeStatus(Guid id);
        List<PeopleValueObject> FindByName(string name);
        PagedSearchValueObject<PeopleValueObject> FindWithPagedSearch(string name, string sortDiretction, int pageSize, int currentPage);
    }
}
