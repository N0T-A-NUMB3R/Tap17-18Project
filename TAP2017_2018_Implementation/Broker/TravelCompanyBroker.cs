using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string _brokerconnectionstring;
        

        public ITravelCompanyFactory TravelCompanyFactory { get; }
        public IReadOnlyTravelCompanyFactory ReadOnlyTravelCompanyFactory { get; }


        public override bool Equals(object obj) //Todo prob va aggiunta toString..
        {
            if (!(obj is TravelCompanyBroker other))
                return false;
            return _brokerconnectionstring == other._brokerconnectionstring;
        }

        public TravelCompanyBroker(string dbConnection)
        {
            Utilities.CheckConnectionString(dbConnection);
            _brokerconnectionstring = dbConnection;
           // this.AgencyName = AgencyName;

        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(_brokerconnectionstring); //TODO: devo valutare se va passata la cs
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory(_brokerconnectionstring);
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                IQueryable<string> travelCompanyNames = c.TravelCompanies.Select(tc => tc.Name);
                return new ReadOnlyCollection<string>(travelCompanyNames.ToList());
              
            }

        }

        public override int GetHashCode()
        {
            var hashCode = -1037104971;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_brokerconnectionstring);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITravelCompanyFactory>.Default.GetHashCode(TravelCompanyFactory);
            hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyTravelCompanyFactory>.Default.GetHashCode(ReadOnlyTravelCompanyFactory);
            return hashCode;
        }

      
    }
}
