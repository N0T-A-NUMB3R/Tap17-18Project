using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class BrokerContext : DbContext
    {
        public BrokerContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<TravelCompanyEntity> TravelCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BrokerConfiguration());
        }

        public override int SaveChanges() 
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException error)
            {
                if (error.InnerException?.InnerException != null && (error.InnerException != null && error.InnerException.InnerException.Message.Contains("Name")))
                    throw new TapDuplicatedObjectException();
                else if (error.InnerException?.InnerException != null && (error.InnerException != null && error.InnerException.InnerException.Message.Contains("ConnectionString")))
                    throw new SameConnectionStringException();

                throw new DbConnectionException("",error);
            }
            
            
        }

    }
}