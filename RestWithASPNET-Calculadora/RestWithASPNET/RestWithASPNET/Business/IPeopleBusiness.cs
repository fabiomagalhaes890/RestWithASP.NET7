using RestWithASPNET.CrossCutting.ValueObject;

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
    }
}
