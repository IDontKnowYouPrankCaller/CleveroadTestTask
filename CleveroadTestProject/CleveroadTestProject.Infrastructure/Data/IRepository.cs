namespace CleveroadTestProject.Infrastructure
{
    #region namespaces
    using System;
    using System.Collections.Generic;
    #endregion

    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : EntityBase<TKey>
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        void Post(TEntity entity);
        void Put(TEntity entity);
        void Delete(TKey id);
    }
}
