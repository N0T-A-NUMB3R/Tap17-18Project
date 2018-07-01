using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_TravelCompanyInterface.DomainConstraints;

namespace TAP2017_2018_Implementation.Persistent_Layer
{
    public class LegEntity
    {
        public int Id { set; get; }

        [Required, MaxLength(NameMaxLength), MinLength(NameMinLength)]
        [Index("Leg", 1, IsUnique = true)]
        public string From { set; get; }

        [Required, MaxLength(NameMaxLength), MinLength(NameMinLength)]
        [Index("Leg", 2, IsUnique = true)]
        public string To { set; get; }

        [Index("Leg", 3, IsUnique = true)]
        public int Distance { set; get; }
        [Index("Leg", 4, IsUnique = true)]
        public int Cost { set; get; }
        [Index("Leg", 5, IsUnique = true)]
        public TransportType Type { set; get; }
    }

}