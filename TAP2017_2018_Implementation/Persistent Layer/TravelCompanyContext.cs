using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
                .HasKey(l => l.Id); //rindondante per via della convenzione.
            // .HasIndex(l => new {l.From, l.To,l.Distance,l.Cost,l.Type}).IsUnique(); 
            //non ha senso perchè il campo Id mi fa si che non possono esistere Leg uguali
        }

        public override int SaveChanges()
        { 
                return base.SaveChanges();
        }

    }
}
