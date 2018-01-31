using System.Data.Entity;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.Utilities.Checker;

namespace TAP2017_2018_Implementation.Broker
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        /// <summary>
        /// Creates a new broker for travel companies.The database denoted by dbConnectionString is initialized (previousdata, if any, will be lost) 
        /// and will be used to store information about the travel companies managed by the resulting broker. 
        /// </summary>
       /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {        
            
            CheckConnectionString(dbConnectionString);
            if (Database.Exists(dbConnectionString))
                Database.Delete(dbConnectionString);
            
            using (var c = new BrokerContext(dbConnectionString))
            {   
                c.Database.Create();
                c.SaveChanges();
            }
            return new TravelCompanyBroker(dbConnectionString);
        }


        /// <summary>
        /// Load an existing broker for travel companies. 
        /// The database denoted by dbConnectionString must be already initialized.
        /// </summary>
        /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        public ITravelCompanyBroker GetBroker(string dbConnectionString)
        {
            CheckConnectionString(dbConnectionString);
            if (Database.Exists(dbConnectionString)) 
                return new TravelCompanyBroker(dbConnectionString);
            throw new NonexistentObjectException();
        }
    }
}
