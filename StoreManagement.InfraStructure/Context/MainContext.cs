
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using StoreManagement.Core.Domain;

namespace StoreManagement.InfraStructure.Context
{

    public class MainContext :DbContext, IUnitOfWork
    {

        public virtual DbSet<User> Users { get; set; }// This is virtual because Moq needs to override the behaviour
        public virtual DbSet<Product> Products { get; set; }
        

        #region IUnitOfWork Members

        public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();

        }

        public virtual void MarkAsModified<TEntity>(TEntity entity) where TEntity : class
        {
            base.Entry<TEntity>(entity).State = EntityState.Modified;
          
        }
       
        public virtual void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            base.Entry<TEntity>(entity).State = EntityState.Deleted;
           
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string query)
        {
            return await base.Database.ExecuteSqlCommandAsync(query);
        }

        public virtual int ExecuteSqlCommand(string query)
        {
            return  base.Database.ExecuteSqlCommand(query);
        
        }

        public virtual IList<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return this.Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public virtual IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }
        #endregion

         /// <summary>
        /// It looks for a connection string named connectionString1 in the web.config file.
        /// </summary>
        public MainContext()
            : base("MainContext")
        {
            //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
          //  modelBuilder.Configurations.Add(new AutoMobileMapping());
            
        }



        public override Task<int> SaveChangesAsync()
        {
            try
            {
                return base.SaveChangesAsync();
            }
            //ثبت خطاهای مربوط به پراپرتی EntityValidationErrors
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            //ثبت خطاهای مربوط به پراپرتی EntityValidationErrors
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }


 
        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
    }
}
