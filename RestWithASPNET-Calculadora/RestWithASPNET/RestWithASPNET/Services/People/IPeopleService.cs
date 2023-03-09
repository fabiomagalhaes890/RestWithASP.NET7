namespace RestWithASPNET.Services.People
{
    public interface IPeopleService
    {
        public Models.People Create(Models.People person);
        public Models.People Update(Guid id, Models.People person);
        public void Delete(Guid id);
        public IEnumerable<Models.People> Get();
        public Models.People FindById(Guid id);
    }
}
