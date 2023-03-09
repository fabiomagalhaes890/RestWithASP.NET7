namespace RestWithASPNET.Services.People
{
    public class PeopleService : IPeopleService
    {
        public Models.People Create(Models.People person)
        {
            return person;
        }

        public void Delete(Guid id)
        {
            
        }

        public Models.People FindById(Guid id)
        {
            return new Models.People { Name = "Fabio " };
        }

        public IEnumerable<Models.People> Get()
        {
            var person = new Models.People { Name = "Fabio " };
            return new List<Models.People> { person };
        }

        public Models.People Update(Guid id, Models.People person)
        {
            return person;
        }
    }
}
