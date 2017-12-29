using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP2017_2018_Implementation
{
    
    public partial class TravelCompanyContext : DbContext
    {
        public TravelCompanyContext(string connectionString) : base(connectionString)
        {
        }
        public virtual DbSet<LegEntity> Legs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

    }
}
