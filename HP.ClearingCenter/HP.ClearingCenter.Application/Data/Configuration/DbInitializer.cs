using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    public class DbInitializer
    {
        public static IDatabaseInitializer<TContext> CreateInstance<TContext>() where TContext : DbContext
        {
#if DEBUG
            return new DevDatabaseInitializer<TContext>();
#else
            return new ReleaseDatabaseInitializer<TContext>();
#endif
        }

        class DevDatabaseInitializer<TContext> : DropCreateDatabaseIfModelChanges<TContext> where TContext : DbContext
        {
            public DevDatabaseInitializer() : base() {}

            protected override void Seed(TContext context)
            {
                Data.Seed(context as ClearingCenterDataContext);
            }
        }

        class ReleaseDatabaseInitializer<TContext> : CreateDatabaseIfNotExists<TContext> where TContext : DbContext
        {
            public ReleaseDatabaseInitializer() : base() { }

            protected override void Seed(TContext context)
            {
                Data.Seed(context as ClearingCenterDataContext);
            }
        }
    }
}
