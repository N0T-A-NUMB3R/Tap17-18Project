using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation.Persistent_Layer
{ 
    public class TravelCompanyContext : DbContext
    {
        public TravelCompanyContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<LegEntity> Legs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TravelCompanyConfiguration());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }

            catch (DbUpdateException error)
            {
                if (error.InnerException?.InnerException != null && (error.InnerException != null && error.InnerException.InnerException.Message.Contains("Leg")))
                    throw new TapDuplicatedObjectException();
                throw new DbConnectionException("", error);
            }


        }

    }
}