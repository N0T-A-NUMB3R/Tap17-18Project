using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                .HasKey(t => t.ID) //rindondante, bastavano le convenzioni
                .HasIndex(t => new { t.ConnectionString, t.Name  }).IsUnique();
        
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
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