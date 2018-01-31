using System.ComponentModel.DataAnnotations;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class LegEntity
    {
        
        public int Id { set; get; }

        [MaxLength(DomainConstraints.NameMaxLength)]
        [MinLength(DomainConstraints.NameMinLength)]
        public string From { set; get; }

        [MaxLength(DomainConstraints.NameMaxLength)]
        [MinLength(DomainConstraints.NameMinLength)]
        public string To { set; get; }

        
        public int Distance { set; get; }

        public int Cost { set; get; }

        public TransportType Type { set; get; }
    }

}