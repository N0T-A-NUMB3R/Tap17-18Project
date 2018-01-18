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
        /// <summary>
        ///  Initializes a new instance of the TravelCompany Factory
        /// </summary>
        /// <param name="connectionstring"></param>
        public TravelCompanyFactory(string connectionstring)
        {
            CheckConnectionString(connectionstring);
            _brokerconnectionstring = connectionstring;
            
        }
        /// <summary>
        /// Creates new travelcompany. The data of the newly created travel company are permanently stored by the component. 
        /// </summary>
        /// <param name="travelCompanyConnectionString"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) 
        {

            CheckConnectionString(travelCompanyConnectionString);
            CheckConnectionString(_brokerconnectionstring);
            CheckTwoConnString(travelCompanyConnectionString,_brokerconnectionstring);
            CheckString(name);

            TravelCompanyBroker broker = new TravelCompanyBroker(_brokerconnectionstring);
            if (broker.KnownTravelCompanies().Contains(name)) 
                throw new TapDuplicatedObjectException(); //todo
            if(_brokerconnectionstring == travelCompanyConnectionString)
                throw new SameConnectionStringException();
            
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

        /// <summary>
        /// Load the speciﬁed TravelCompany from a Name

        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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