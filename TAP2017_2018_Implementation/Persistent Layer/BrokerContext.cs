using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
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
                .HasKey(t => t.Name) //rindondante, basta la convenzione?!
                .HasIndex(t => new { t.ConnectionString}).IsUnique(); //todo mettere chiave composta
        
        }
        public override int SaveChanges() //Todo controllare bene i save changes
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbConnectionException)
            {
                throw new DbConnectionException();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DBConcurrencyException();
            }
            catch (DbUpdateException)
            {
                throw new TapDuplicatedObjectException();
            }
            
        }
    }
}