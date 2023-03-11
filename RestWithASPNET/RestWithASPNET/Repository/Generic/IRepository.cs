using RestWithASPNET.Models;
using System.Linq.Expressions;

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
        List<TEntity> FindWithPagedSearch(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, string>> order,
            int skip,
            string sort,
            int take);
        int GetCount(Expression<Func<TEntity, bool>> filter);
    }
}
