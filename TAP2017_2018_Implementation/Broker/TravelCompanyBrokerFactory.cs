using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        { // Crea un nuovo gestore e ne inizializza il db
            Utilities.CheckNull(dbConnectionString);
            if (Database.Exists(dbConnectionString))
                Database.Delete(dbConnectionString);
            using (var c = new TravelCompanyContext(dbConnectionString))
            {
                c.Database.Create();
                c.SaveChanges();
            }

        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)                   
        {   // carica i dati di un gestore già inizializzato
            Utilities.CheckNull(dbConnectionString);
            if(Database.Exists(dbConnectionString))
                return new TravelCompanyBroker(dbConnectionString);
            return null;
        }
    }
}
