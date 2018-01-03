using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Planning.Bindings;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string BROKERCONNECTIONSTRING;

        public ITravelCompanyFactory TravelCompanyFactory { get; }
        public IReadOnlyTravelCompanyFactory ReadOnlyTravelCompanyFactory { get; }


        public override bool Equals(object obj)
        {
            if (!(obj is TravelCompanyBroker other))
                return false;
            return BROKERCONNECTIONSTRING == other.BROKERCONNECTIONSTRING;
        }

        public TravelCompanyBroker(string dbConnection)
        {
            Utilities.CheckConnectionString(dbConnection);
            BROKERCONNECTIONSTRING = dbConnection;

        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(BROKERCONNECTIONSTRING); //TODO: devo valutare se va passata la cs
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory(BROKERCONNECTIONSTRING);
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            using (var c = new BrokerContext(BROKERCONNECTIONSTRING))
            {
                IQueryable<string> travelCompanyNames = c.TravelCompanies.Select(tc => tc.Name);
                var knownTcList= new ReadOnlyCollection<string>(travelCompanyNames.ToList());
                return knownTcList;
            }

        }

        public override int GetHashCode()
        {
            var hashCode = -1037104971;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BROKERCONNECTIONSTRING);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITravelCompanyFactory>.Default.GetHashCode(TravelCompanyFactory);
            hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyTravelCompanyFactory>.Default.GetHashCode(ReadOnlyTravelCompanyFactory);
            return hashCode;
        }
    }
}
