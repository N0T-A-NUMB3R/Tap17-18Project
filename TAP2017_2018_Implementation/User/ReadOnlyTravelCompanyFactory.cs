using System.Linq;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_Implementation.Utilities;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation.User
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
            Checker.CheckConnectionString(dBconnection);
            _brokerconnectionstring = dBconnection;
        }
       
        /// <summary>
        /// Load the read-only interface to a given travel company
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IReadOnlyTravelCompany Get(string name)
        {
            Checker.CheckString(name);
            //todo andrebbe fatto il controllo se esiste la corrisponte TC
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                return new ReadOnlyTravelCompany(c.TravelCompanies.Single(tc => tc.Name == name).ConnectionString);
            }
        }
    }
}

