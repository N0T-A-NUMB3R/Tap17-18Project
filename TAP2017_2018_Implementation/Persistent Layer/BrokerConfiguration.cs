﻿using System.Data.Entity.ModelConfiguration;


namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class BrokerConfiguration : EntityTypeConfiguration<TravelCompanyEntity>
    {
        public BrokerConfiguration()
        { 
            ToTable("TravelCompany");
         
             Property(t => t.ConnectionString)
            .HasColumnName("TCConnectionString");
        }
    }
}
