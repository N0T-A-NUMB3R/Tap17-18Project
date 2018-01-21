
using System.ComponentModel.DataAnnotations;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class TravelCompanyEntity
    {
        //public int Id { get; set; }

        
        [MaxLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMaxLength)]
        [MinLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMinLength)]
        public string Name { set; get; }

        [Required]
        [MaxLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.ConnectionStringMaxLength)]
        [MinLength(TAP2017_2018_TravelCompanyInterface.DomainConstraints.ConnectionStringMinLength)]
        //aggiungere unique
        public string ConnectionString { set; get; }
    }
}
