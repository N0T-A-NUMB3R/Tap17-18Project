using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TAP2017_2018_TravelCompanyInterface.DomainConstraints;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class TravelCompanyEntity
    {
        public  int Id { get; set; }

        [Required][MaxLength(NameMaxLength)][MinLength(NameMinLength)]
        [Index("Name", IsUnique = true)]
        public  string Name { set; get; }

        [Required][MaxLength(ConnectionStringMaxLength)][MinLength(ConnectionStringMinLength)]
        [Index("ConnectionString", IsUnique = true)]
        public  string ConnectionString { set; get; }
    }
}
