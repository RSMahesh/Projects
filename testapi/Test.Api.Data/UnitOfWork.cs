// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="">
//   
// </copyright>
// <summary>
//   The unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.Api.Data
{
    using System;
    using System.Data.Entity;

    using Repository;

    /// <summary>
    /// The unit of work.
    /// </summary>
    /// <typeparam name="TContext">
    /// </typeparam>
    public class UnitOfWork : IUnitOfWork
       
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly DbContext dbContext;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        public UnitOfWork()
        {
            this.dbContext = new TicketContext();
            this.dbContext.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// The get repository.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository"/>.
        /// </returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(this.dbContext);
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}