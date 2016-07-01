using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IDataContextFactory
    {
        IDataContext CreateInstance<TContext>() where TContext : class, IDataContext;
    }
}
