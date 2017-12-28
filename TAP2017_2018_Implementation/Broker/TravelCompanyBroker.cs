using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            throw new NotImplementedException();
        }
    }
}
