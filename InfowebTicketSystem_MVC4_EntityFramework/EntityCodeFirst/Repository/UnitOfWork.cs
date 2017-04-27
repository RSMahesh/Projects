using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EntityCodeFirst.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext: DbContext, new()
    {
        readonly DbContext dbContext;
        bool disposed;

        public UnitOfWork()
        {
            dbContext = new TContext();
            dbContext.Configuration.LazyLoadingEnabled = true;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(dbContext);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
