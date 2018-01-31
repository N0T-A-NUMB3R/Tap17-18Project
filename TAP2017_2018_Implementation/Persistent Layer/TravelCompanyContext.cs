using System.Data.Entity;

namespace TAP2017_2018_Implementation.Persistent_Layer
{

    public class TravelCompanyContext : DbContext
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
            modelBuilder.Entity<LegEntity>()
                .Property(l => l.From).IsRequired();
            modelBuilder.Entity<LegEntity>()
                .Property(l => l.To).IsRequired();
        }
       
    }
}