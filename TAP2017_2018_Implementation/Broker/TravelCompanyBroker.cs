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
        private readonly string DBCONNECTION;

        public ITravelCompanyFactory TravelCompanyFactory { get; }
        public IReadOnlyTravelCompanyFactory ReadOnlyTravelCompanyFactory { get; }
        
        
        public override bool Equals(object obj)
        {
            TravelCompanyBroker other = obj as TravelCompanyBroker;
            if (other == null)
                return false;
            return DBCONNECTION == other.DBCONNECTION;
        }
        
        public TravelCompanyBroker(string dbConnection)
        {
            Utilities.CheckConnectionString(dbConnection);
            DBCONNECTION = dbConnection;

        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory();
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory();
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
           // Utilities.CheckConnectionString(DBCONNECTION);
            List<string> travelCompanies = new List<string>();
          
            using (var c = new BrokerContext(DBCONNECTION))
            {
                foreach (var tc in c.TravelCompanies.ToList())
                {
                    travelCompanies.Add(tc.ConnectionString);
                }
                ReadOnlyCollection<string> readOnlyTc = new ReadOnlyCollection<string>(travelCompanies);
                return readOnlyTc;
            }
        }
    }
}
