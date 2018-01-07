using System.ComponentModel.DataAnnotations;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class LegEntity : ILegDTO
    {
        [Key]
        public int ID { set; get; }

        [Required]
        public string From { set; get; }

        [Required]
        public string To { set; get; }

        [Required]
        public int Distance { set; get; }

        public int Cost { set; get; }

        public TransportType Type { set; get; }
    }

}