using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyFactory : ITravelCompanyFactory
    {
        private readonly string BROKERCONNECTIONSTRING;
        //private string agencyName; //TODO ha senso?

        public TravelCompanyFactory(string connectionstring)
        {
            BROKERCONNECTIONSTRING = connectionstring;
            // this.agencyName = agencyName;
        }

        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) // TODO sentire prof
        {

            Utilities.CheckConnectionString(travelCompanyConnectionString);
            Utilities.CheckConnectionString(BROKERCONNECTIONSTRING);
            Utilities.CheckTwoConnectionString(travelCompanyConnectionString,BROKERCONNECTIONSTRING);
            Utilities.CheckName(name);

            TravelCompanyBroker broker = new TravelCompanyBroker(BROKERCONNECTIONSTRING);
            if (broker.KnownTravelCompanies().Contains(name)) 
                throw new TapDuplicatedObjectException();


            using (var c = new BrokerContext(BROKERCONNECTIONSTRING))
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

            return new TravelCompany(travelCompanyConnectionString, name); //usavo agency name
        }


        public ITravelCompany Get(string name)
        {
            Utilities.CheckName(name);
            using (var c = new BrokerContext(BROKERCONNECTIONSTRING))
            {
                var travelAgency = c.TravelCompanies.SingleOrDefault(tc => tc.Name == name);
                if (travelAgency == null)
                    throw new NonexistentTravelCompanyException();
                return new TravelCompany(travelAgency.ConnectionString, name);

            }
        }
    }
}