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
        private string dBCONNECTION;
        //private string agencyName; //TODO ha senso?

        public TravelCompanyFactory(string dBCONNECTION)
        {
            this.dBCONNECTION = dBCONNECTION;
            // this.agencyName = agencyName;
        }

        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) // TODO sentire prof
        {

            Utilities.CheckConnectionString(travelCompanyConnectionString);
            Utilities.CheckConnectionString(dBCONNECTION);
            Utilities.CheckTwoConnectionString(travelCompanyConnectionString, dBCONNECTION);
            Utilities.CheckName(name);

            TravelCompanyBroker broker = new TravelCompanyBroker(dBCONNECTION);
            if (broker.KnownTravelCompanies().Contains(name)) // DEVO SOLLEVARE ALCUNE ECCEZIONI
                throw new TapDuplicatedObjectException();


            using (var c = new BrokerContext(dBCONNECTION))
            {
                if (c.TravelCompanies.Any(agency => agency.ConnectionString == travelCompanyConnectionString))
                    throw new SameConnectionStringException("E' gia presente una Travel Company con questa Cs");
                /*
                if (c.TravelCompanies.Any(agency => agency.Name == name))
                    throw new TapDuplicatedObjectException("E' gia presente una Travel Company con questo nome");
                */


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
            using (var c = new BrokerContext(dBCONNECTION))
            {
                var travelAgency = c.TravelCompanies.SingleOrDefault(tc => tc.Name == name);
                if (travelAgency == null)
                    throw new NonexistentTravelCompanyException();
                return new TravelCompany(travelAgency.ConnectionString, name);

            }
        }
    }
}