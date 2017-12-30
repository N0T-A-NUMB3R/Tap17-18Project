using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string ConnectionString;

        public TravelCompanyBroker(string dbConnectionString)
        {
            Utilities.CheckConnectionString(dbConnectionString);
            ConnectionString = dbConnectionString;
        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(ConnectionString);
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory();
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            using (var c = new BrokerContext(ConnectionString))
            {
                return new ReadOnlyCollection<string>(c.TravelCompanies.Select(x => x.ConnectionString).ToList());
            }
        }
    }
}
