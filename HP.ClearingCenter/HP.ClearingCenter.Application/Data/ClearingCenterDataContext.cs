using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data
{
    using HP.ClearingCenter.Application.Data.Entities;
    using HP.ClearingCenter.Application.ProductGroups.Entities;
    using HP.ClearingCenter.Application.Products.Entities;
    using HP.ClearingCenter.Application.TransactionTransports.Entities;
    using HP.ClearingCenter.Infrastructure.Contracts;
    using System.Linq.Expressions;

    //HP.ClearingCenter.Application.Data.ClearingCenterDataContext
    public class ClearingCenterDataContext : DbContext, IDataContext
    {
        #region dbo

        public virtual IDbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<Translator> Translators { get; set; }
        public virtual IDbSet<CustomAttribute> CustomAttributes { get; set; }
        public virtual IDbSet<CustomAttributeDataType> CustomAttributeDataTypes { get; set; }
        public virtual IDbSet<OptionListItem> OptionListItems { get; set; }

        #endregion

        #region rcp

        public virtual IDbSet<StatusCode> StatusCodes { get; set; }
        public virtual IDbSet<ClearingProcessType> ClearingProcessTypes { get; set; }
        public virtual IDbSet<ForwardingInstruction> ForwardingInstructions { get; set; }
        public virtual IDbSet<MarketingProgramType> MarketingProgramTypes { get; set; }
        public virtual IDbSet<ClearingCenter> ClearingCenters { get; set; }
        public virtual IDbSet<MarketingProgram> MarketingPrograms { get; set; }
        public virtual IDbSet<LocalProgram> LocalPrograms { get; set; }
        public virtual IDbSet<TransactionHeader> TransactionHeaders { get; set; }
        public virtual IDbSet<TransactionDetail> TransactionDetails { get; set; }

        #endregion

        #region pdb

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<CategoryAttributeAssignment> CategoryAttributeAssignments { get; set; }
        public virtual IDbSet<ProductCustomAttributeValue> ProductCustomAttributeValues { get; set; }

        #endregion

        #region rpg

        public virtual IDbSet<ProductGroup> ProductGroups { get; set; }
        public virtual IDbSet<ProductGroupCategory> ProductGroupCategories { get; set; }
        public virtual IDbSet<ProductFilterOperator> ProductFilterOperators { get; set; }
        public virtual IDbSet<ProductFilter> ProductFilters { get; set; }        

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(x => x.Height).HasPrecision(18, 4);
            modelBuilder.Entity<Product>().Property(x => x.Width).HasPrecision(18, 4);
            modelBuilder.Entity<Product>().Property(x => x.Length).HasPrecision(18, 4);
            modelBuilder.Entity<Product>().Property(x => x.Weight).HasPrecision(18, 4);

            modelBuilder.Entity<ProductCustomAttributeValue>().Property(x => x.DecimalValue).HasPrecision(18, 4);
        }

        TEntity IDataContext.FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria)
        {
            return this.Set<TEntity>().FirstOrDefault(criteria);
        }

        IQueryable<TEntity> IDataContext.FindAll<TEntity>(Expression<Func<TEntity, bool>> criteria)
        {
            return this.Set<TEntity>();
        }

        void IDataContext.Add<TEntity>(TEntity instance)
        {
            this.Set<TEntity>().Add(instance);
        }

        void IDataContext.Delete<TEntity>(TEntity instance)
        {
            this.Set<TEntity>().Remove(instance);
        }

        int IDataContext.SaveChanges()
        {
            return this.SaveChanges();
        }
    }
}
