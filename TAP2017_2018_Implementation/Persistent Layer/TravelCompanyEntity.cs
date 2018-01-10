using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP2017_2018_Implementation
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
