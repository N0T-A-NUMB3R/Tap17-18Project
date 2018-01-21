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
                .HasKey(l => l.Id); //rindondante per via della convenzione.
            // .HasIndex(l => new {l.From, l.To,l.Distance,l.Cost,l.Type}).IsUnique(); 
            //non ha senso perchè il campo Id mi fa si che non possono esistere Leg uguali
        }

        /*
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException != null &&
                    e.InnerException.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
             
                            if (sqlException.Message.Contains(GlobalRequirementClass.GetCompanyNameIndex()))
                            {
                                throw new TapDuplicatedObjectException(e.Message);
                            }
                            else if (sqlException.Message.Contains(GlobalRequirementClass
                                .GetCompanyConnectionStringIndex()))
                            {
                                throw new SameConnectionStringException(e.Message);
                            }
                   
                

                throw e;

            }

        }
    }
}
*/
    }
}
