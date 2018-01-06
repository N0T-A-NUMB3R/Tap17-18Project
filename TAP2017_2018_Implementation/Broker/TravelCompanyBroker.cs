﻿using System.Collections.ObjectModel;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string _brokerconnectionstring;
      
        public override bool Equals(object obj) //Todo prob va aggiunta toString..
        {
            if (!(obj is TravelCompanyBroker other))
                return false;
            return _brokerconnectionstring == other._brokerconnectionstring;
        }

        public TravelCompanyBroker(string dbConnection)
        {
            CheckConnectionString(dbConnection);
            _brokerconnectionstring = dbConnection;
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
                IQueryable<string> travelCompanyNames = c.TravelCompanies.Select(tc => tc.Name); //TODO ECCEZIONE SE NON TROVA NULLA?
                return new ReadOnlyCollection<string>(travelCompanyNames.ToList());
              }
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
