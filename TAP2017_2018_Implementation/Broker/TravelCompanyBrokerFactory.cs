using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    { 
    /*
       private string dbConnectionString;
        
        public TravelCompanyBrokerFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }
      */

        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {
            // Crea un nuovo gestore e ne inizializza il db
            //Utilities.CheckConnectionString(dbConnectionString);

            if (Database.Exists(dbConnectionString))
            {
                Database.Delete(dbConnectionString);
                throw new SameConnectionStringException();
                
            }
            using (var c = new BrokerContext(dbConnectionString))
            {
                c.Database.Create();
                c.SaveChanges();

            }
            return new TravelCompanyBroker(dbConnectionString);

        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)
        {
            // carica i dati di un gestore già inizializzato
            //Utilities.CheckNull(dbConnectionString);

            if (Database.Exists(dbConnectionString))
                return new TravelCompanyBroker(dbConnectionString);
            return null;
        }
    }
}
