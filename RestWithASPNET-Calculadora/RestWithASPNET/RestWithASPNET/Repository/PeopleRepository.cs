using RestWithASPNET.Models;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public class PeopleRepository : Repository<People>, IPeopleRepository
    {
        public PeopleRepository(SQLContext sqlContext) : base(sqlContext) { }

        public People ChangeStatus(Guid id)
        {
            if (!_dataset.Any(p => p.Id == id)) return null;

            var user = _dataset.FirstOrDefault(p => p.Id == id);
            if(user != null)
            {
                user.Enabled = !user.Enabled;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public List<People> FindByName(string name)
        {
            if(!string.IsNullOrWhiteSpace(name))
            {
                return _dataset.Where(p => p.Name.Contains(name)).ToList();
            }

            return null;
        }
    }
}
