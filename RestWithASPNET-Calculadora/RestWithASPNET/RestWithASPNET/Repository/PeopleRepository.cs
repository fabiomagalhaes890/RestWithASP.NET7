using RestWithASPNET.Models;
using System;

namespace RestWithASPNET.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly SQLContext _context;

        public PeopleRepository(SQLContext sqlContext)
        {
            _context = sqlContext;
        }

        public People Create(People person)
        {
            try
            {
                _context.People.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return person;
        }

        public void Delete(Guid id)
        {
            var result = _context.People.SingleOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.People.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public People FindById(Guid id)
        {
            return _context.People.SingleOrDefault(p => p.Id == id);
        }

        public List<People> Get()
        {
            return _context.People.ToList();
        }

        public People Update(Guid id, People person)
        {
            if (!Exists(id)) return new People();

            var result = _context.People.SingleOrDefault(p => p.Id == id);

            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }            

            return person;
        }

        public bool Exists(Guid id)
        {
            return _context.People.Any(p => p.Id.Equals(id));
        }
    }
}
