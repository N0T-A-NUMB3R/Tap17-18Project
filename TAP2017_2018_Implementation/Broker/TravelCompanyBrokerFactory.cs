using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {
            throw new NotImplementedException();
        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)                   
        {
            throw new NotImplementedException();
        }
    }
}
