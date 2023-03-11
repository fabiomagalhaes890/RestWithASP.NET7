using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public interface IPeopleRepository : IRepository<People>
    {
        People ChangeStatus(Guid id);
        List<People> FindByName(string firstName, string secondName);
    }
}
