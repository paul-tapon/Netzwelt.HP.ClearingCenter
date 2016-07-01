using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data
{
    public abstract class DataContextAdapter
    {
        public DataContextAdapter(IDataContext db)
        {   
            this.DataContext = db;
        }

        public virtual IDataContext DataContext { get; private set; }

        public virtual void Add<TEntity>(TEntity instance) where TEntity : class
        {
            this.DataContext.Add<TEntity>(instance);
        }

        public virtual void Delete<TEntity>(TEntity instance) where TEntity : class
        {
            this.DataContext.Delete<TEntity>(instance);
        }

        public virtual int SaveChanges()
        {
            return this.DataContext.SaveChanges();
        }
    }
}
