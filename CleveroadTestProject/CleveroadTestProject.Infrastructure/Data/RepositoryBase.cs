namespace CleveroadTestProject.Infrastructure
{
    #region namespaces
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    #endregion

    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable
           where TEntity : EntityBase<TKey>
    {
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet
        {
            get {
                return this.dbContext.Set<TEntity>();
            }
        }  

        public RepositoryBase(DbContext context)
        {
            this.dbContext = context;
        }

        public void Delete(TKey id)
        {
            var model = Get(id);
            if (model is TEntity)
            {
                this.dbSet.Remove(model);
                this.dbContext.SaveChanges();
            }
        }

        public TEntity Get(TKey id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.dbSet.ToList();
        }

        public void Post(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Put(TEntity entity)
        {
            var entry = this.dbContext.Entry<TEntity>(entity);
            if (entry != null)
            {
                if (entry.State == EntityState.Detached)
                {
                    var attachedEntity = this.dbSet.Attach(entity);
                    entry = this.dbContext.Entry<TEntity>(attachedEntity.Entity);
                }
                entry.State = EntityState.Modified;
                this.dbContext.SaveChanges();
            }
            else
            {
                this.Post(entity);
            }
        }

        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
