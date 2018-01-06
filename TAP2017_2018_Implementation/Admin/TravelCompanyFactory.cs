using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.Checker;
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
            CheckTwoString(travelCompanyConnectionString,_brokerconnectionstring);
            CheckString(name);

            TravelCompanyBroker broker = new TravelCompanyBroker(_brokerconnectionstring);
            if (broker.KnownTravelCompanies().Contains(name)) 
                throw new TapDuplicatedObjectException();


            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                if (c.TravelCompanies.Any(agency => agency.ConnectionString == travelCompanyConnectionString))
                    throw new SameConnectionStringException("E' gia presente una Travel Company con questa Cs");
                
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
                c.Database.Delete(); // se ci fosse gia....
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
                var travelAgency = c.TravelCompanies.SingleOrDefault(tc => tc.Name == name);
                if (travelAgency == null)
                    throw new NonexistentTravelCompanyException();
                return new TravelCompany(travelAgency.ConnectionString, name);

            }
        }
    }
}