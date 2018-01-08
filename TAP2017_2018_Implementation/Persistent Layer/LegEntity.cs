using System.ComponentModel.DataAnnotations;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class LegEntity   //todo è sbagliato perchè accoppio fortemente al db
    {
        
        public int Id { set; get; }

        [Required]
        public string From { set; get; }

        [Required]
        public string To { set; get; }

        //todo ?
        public int Distance { set; get; }

        public int Cost { set; get; }

        public TransportType Type { set; get; }
    }

}