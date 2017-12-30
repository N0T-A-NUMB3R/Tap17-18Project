using System;
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
        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name) //crea una nuova TC e ne inizializza il fb
        {
            Utilities.CheckConnectionString(travelCompanyConnectionString);

            if (Database.Exists(travelCompanyConnectionString))
            {
                Database.Delete(travelCompanyConnectionString);
               // throw new SameConnectionStringException();

            }
            using (var c = new TravelCompanyContext(travelCompanyConnectionString))
            {
                c.Database.Create();
                c.SaveChanges();

            }
            return new TravelCompany(travelCompanyConnectionString);

        }

        public ITravelCompany Get(string name)
        {
            throw new NotImplementedException(); // carica i dati di una TC già inizializzata
        }
    }
}
    
