using System.Linq;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private readonly string _brokerconnectionstring;


        public ReadOnlyTravelCompanyFactory(string dBconnection)
        {
            Utilities.CheckConnectionString(dBconnection);
            _brokerconnectionstring = dBconnection;
        }
        
        public IReadOnlyTravelCompany Get(string name)
        {
            Utilities.CheckName(name);

            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                return new ReadOnlyTravelCompany(c.TravelCompanies.Single(tc => tc.Name == name).ConnectionString);
            }
        }
    }
}

