﻿using System;
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
        //public ITravelCompanyFactory TravelCompanyFactory { get; }
        //public IReadOnlyTravelCompanyFactory ReadOnlyTravelCompanyFactory { get; }

        public override bool Equals(object obj)
        {
            TravelCompanyBroker other = obj as TravelCompanyBroker;
            if (other == null)
                return false;
            return ConnectionString == other.ConnectionString;
        }

        public TravelCompanyBroker(string dbConnection)
        {
            Utilities.CheckConnectionString(dbConnection);
            //TravelCompanyFactory = new TravelCompanyFactory(dbConnection);
            //ReadOnlyTravelCompanyFactory = new ReadOnlyTravelCompanyFactory(dbConnection);
        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(ConnectionString);
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory(ConnectionString);
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
