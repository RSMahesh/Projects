using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbContext dbContext;
        private readonly IDbSet<T> dbSet;


        public Repository(DbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public T GetById(Guid id)
        {
           return dbSet.Find(id);
        }

        public IEnumerable<T> All()
        {
            return dbSet;
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
    }
}
