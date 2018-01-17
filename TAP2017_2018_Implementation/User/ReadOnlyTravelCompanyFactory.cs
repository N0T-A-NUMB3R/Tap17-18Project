using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private readonly string _brokerconnectionstring;

        /// <summary>
        ///  Initializes a new instance of the ReadOnlyTravelCompany Factory
        /// </summary>
        /// <param name="dBconnection"></param>
        public ReadOnlyTravelCompanyFactory(string dBconnection)
        {
            CheckConnectionString(dBconnection);
            _brokerconnectionstring = dBconnection;
        }
       
        /// <summary>
        /// Load the read-only interface to a given travel company
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IReadOnlyTravelCompany Get(string name)
        {
            CheckString(name);
            //todo andrebbe fatto il controllo se esiste la corrisponte TC
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                return new ReadOnlyTravelCompany(c.TravelCompanies.Single(tc => tc.Name == name).ConnectionString);
            }
        }
    }
}

