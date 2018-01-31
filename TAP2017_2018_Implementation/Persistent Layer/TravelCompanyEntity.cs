
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class TravelCompanyEntity
    {
        public int Id { get; set; }

        //[Required] 
        [MaxLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMaxLength)]
        [MinLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMinLength)]
        [Index("Name", IsUnique = true)]
        public string Name { set; get; }

        //[Required]
        [MaxLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.ConnectionStringMaxLength)]
        [MinLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.ConnectionStringMinLength)]
        [Index("ConnectionString", IsUnique = true)]
        public string ConnectionString { set; get; }
    }
}
