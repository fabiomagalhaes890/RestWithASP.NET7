using RestWithASPNET.Models;

namespace RestWithASPNET.Repository.Generic
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Create(TEntity person);
        TEntity Update(Guid id, TEntity person);
        void Delete(Guid id);
        List<TEntity> Get();
        TEntity FindById(Guid id);
        bool Exists(Guid id);
    }
}
