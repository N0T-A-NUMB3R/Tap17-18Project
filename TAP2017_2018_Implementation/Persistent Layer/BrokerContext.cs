using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class BrokerContext : DbContext
    {
        public BrokerContext(string connectionString) : base(connectionString)
        {

        }

        public virtual DbSet<TravelCompanyEntity> TravelCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TravelCompanyEntity>()
                .ToTable("TravelCompany")
                .Property(t => t.ConnectionString).IsRequired();
                /*
                .HasColumnAnnotation(
                    "ConnectionString",
                    new IndexAnnotation(new IndexAttribute("ConnectionString") { IsUnique = true }));
                */

            modelBuilder.Entity<TravelCompanyEntity>()
                .Property(t => t.Name).IsRequired();
            /*
            .HasColumnAnnotation(
                "Name",
                new IndexAnnotation(new IndexAttribute("Name") {IsUnique = true}));
            */
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
            // catch di exceptionm
            
        }

    }
}