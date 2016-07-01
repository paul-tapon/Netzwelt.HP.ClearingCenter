namespace HP.ClearingCenter.Application.Data.Configuration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed partial class Migrations : DbMigrationsConfiguration<HP.ClearingCenter.Application.Data.ClearingCenterDataContext>
    {
        public Migrations()
        {
            AutomaticMigrationsEnabled = false;
#if DEBUG
            AutomaticMigrationsEnabled = true;
#endif
            MigrationsDirectory = @"Data\Migrations";
        }

        protected override void Seed(HP.ClearingCenter.Application.Data.ClearingCenterDataContext context)
        {
            context.Seed();
        }
    }
}
