using System.Linq;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Utilities.Checker;

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
            CheckName(name);
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                return new ReadOnlyTravelCompany(c.TravelCompanies.Single(tc => tc.Name == name).ConnectionString,
                    name);
            }
        }
    }
}

