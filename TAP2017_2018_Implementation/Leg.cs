using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class Leg
    {

        public int LegId { set; get; }

        public string From { set; get; }

        public string To { set; get; }


        public int Distance { set; get; }

        public int Cost { set; get; }


        public TransportType Type { set; get; }
    }
}