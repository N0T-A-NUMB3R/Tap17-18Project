﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static System.Data.Entity.Database;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {        
            // Crea un nuovo gestore e ne inizializza il db
            CheckConnectionString(dbConnectionString);
            if (Exists(dbConnectionString))
            {
                Delete(dbConnectionString);
                // TODO throw new SameConnectionStringException();  // la prof parla di arrabbiarsi?!
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
            CheckConnectionString(dbConnectionString);
            if (Exists(dbConnectionString))
                return new TravelCompanyBroker(dbConnectionString);
            throw new NonexistentObjectException();
        }
    }
}