using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private readonly string _brokerconnectionstring;


        public ReadOnlyTravelCompanyFactory(string dBconnection)
        {
            CheckConnectionString(dBconnection);
            _brokerconnectionstring = dBconnection;
        }
        
        public IReadOnlyTravelCompany Get(string name)
        {
            CheckString(name);

            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                return new ReadOnlyTravelCompany(c.TravelCompanies.Single(tc => tc.Name == name).ConnectionString);
            }
        }
    }
}

