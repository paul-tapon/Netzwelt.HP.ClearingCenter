using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IDataContext
    {
        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        IQueryable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        void Add<TEntity>(TEntity instance) where TEntity : class;
        void Delete<TEntity>(TEntity instance) where TEntity : class;
        int SaveChanges();
    }
}
