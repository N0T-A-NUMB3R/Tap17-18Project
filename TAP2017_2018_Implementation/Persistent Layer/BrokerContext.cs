using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP2017_2018_Implementation
{
    public partial class BrokerContext : DbContext
    {
        public BrokerContext(string connectionString) : base(connectionString)
        {
        }
        public virtual DbSet<TravelCompanyEntity> TravelCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}