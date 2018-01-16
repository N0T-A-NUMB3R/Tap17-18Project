using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.Checker;
using static TAP2017_2018_Implementation.LegUtilities;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyFactory : ITravelCompanyFactory
    {
        private readonly string _brokerconnectionstring;
        //private string agencyName; //TODO ha senso?

        public TravelCompanyFactory(string connectionstring)
        {
            CheckConnectionString(connectionstring);
            _brokerconnectionstring = connectionstring;
            // this.agencyName = agencyName;
        }

        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) // TODO sentire prof
        {

            CheckConnectionString(travelCompanyConnectionString);
            CheckConnectionString(_brokerconnectionstring);
            CheckTwoStringEquals(travelCompanyConnectionString,_brokerconnectionstring);
            CheckString(name);

            TravelCompanyBroker broker = new TravelCompanyBroker(_brokerconnectionstring);
            if (broker.KnownTravelCompanies().Contains(name)) 
                throw new TapDuplicatedObjectException(); //todo
            
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                
                if (c.TravelCompanies.Any(EqualsCsExp(travelCompanyConnectionString)))
                    throw new SameConnectionStringException(); //todo
                
                
                TravelCompanyEntity tc = new TravelCompanyEntity()
                {
                    ConnectionString = travelCompanyConnectionString,
                    Name = name
                };
                c.TravelCompanies.Add(tc);
                c.SaveChanges();
            }

            using (var c = new TravelCompanyContext(travelCompanyConnectionString))
            {
                c.Database.Delete(); // come dice la prof se c' erano dati prima CIAONEEEEE
                c.Database.Create();
                c.SaveChanges();
            }

            return new TravelCompany(travelCompanyConnectionString, name);
        }


        public ITravelCompany Get(string name)
        {
            CheckString(name);
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                var travelAgency = c.TravelCompanies.SingleOrDefault(EqualsNameExp(name));
                if (travelAgency == null)
                    throw new NonexistentTravelCompanyException();
                return new TravelCompany(travelAgency.ConnectionString, name);

            }
        }
    }
}