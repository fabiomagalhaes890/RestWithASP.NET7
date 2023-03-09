using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models;
using System.Reflection;

namespace RestWithASPNET.Repository.Generic
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly SQLContext _context;
        private DbSet<TEntity> _dataset;
        private string _tableName;

        public Repository(SQLContext sqlContext)
        {
            _context = sqlContext;
            _dataset = _context.Set<TEntity>();
            _tableName = typeof(TEntity).GetTypeInfo().Name;
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                _dataset.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} | Table: {_tableName}");
            }

            return entity;
        }

        public void Delete(Guid id)
        {
            var result = _dataset.FirstOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message} | Table: {_tableName}");
                }
            }
        }

        public TEntity FindById(Guid id)
        {
            return _dataset.FirstOrDefault(p => p.Id == id);
        }

        public List<TEntity> Get()
        {
            return _dataset.ToList();
        }

        public TEntity Update(Guid id, TEntity person)
        {
            if (!Exists(id)) return null;

            var result = _dataset.FirstOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message} | Table: {_tableName}");
                }
            }

            return person;
        }

        public bool Exists(Guid id)
        {
            return _dataset.Any(p => p.Id.Equals(id));
        }
    }
}
