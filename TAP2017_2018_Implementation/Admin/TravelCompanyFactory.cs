﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public TravelCompanyFactory(string dBCONNECTION)
        {
            this.dBCONNECTION = dBCONNECTION;
        }

        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) // TODO sentire Angelo
        {

            Utilities.CheckConnectionString(travelCompanyConnectionString);
            Utilities.CheckConnectionString(dBCONNECTION);
            Utilities.CheckTwoConnectionString(travelCompanyConnectionString, dBCONNECTION);


            TravelCompanyBroker broker = new TravelCompanyBroker(dBCONNECTION);
            if (broker.KnownTravelCompanies().Contains(name)) // DEVO SOLLEVARE ALCUNE ECCEZIONI
                throw new TapDuplicatedObjectException();


            using (var c = new BrokerContext(dBCONNECTION))
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

            return new TravelCompany(travelCompanyConnectionString);
        }


        public ITravelCompany Get(string name)
        {
            Utilities.CheckConnectionString(name);
            using (var c = new BrokerContext(dBCONNECTION))
            {
                var existentTc = c.TravelCompanies.Single(tc => tc.Name == name);

                return new TravelCompany(existentTc.ConnectionString);
            }
        }
    }
}