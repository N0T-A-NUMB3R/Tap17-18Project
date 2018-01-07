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
    
    public  class TravelCompanyContext : DbContext
    {
        public TravelCompanyContext(string connectionString) : base(connectionString)
        {
        }
        public virtual DbSet<LegEntity> Legs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegEntity>()
                .ToTable("Leg")
                .HasKey(l => l.Id);
            // .HasIndex(l => new {l.From, l.To,l.Distance,l.Cost,l.Type}).IsUnique(); Todo controllare vincolo unique

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
