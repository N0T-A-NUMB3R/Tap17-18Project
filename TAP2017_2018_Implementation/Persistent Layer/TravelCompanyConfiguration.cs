using System.Data.Entity.ModelConfiguration;


namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class TravelCompanyConfiguration : EntityTypeConfiguration<LegEntity>
    {
        public TravelCompanyConfiguration()
        { 
            ToTable("Legs");
        }
    }
}



