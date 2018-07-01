using System.Collections.ObjectModel;
using System.Linq;
using TAP2017_2018_Implementation.Admin;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_Implementation.User;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Utilities.Checker;

namespace TAP2017_2018_Implementation.Broker
{
    /// <summary>
    /// BROKER
    /// </summary>
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string _brokerconnectionstring;

        /// <summary>
        /// Initializes a new instance of the TravelCompany Broker
        /// </summary>
        /// <param name="dbConnection"></param>
        public TravelCompanyBroker(string dbConnection)
        {
            CheckConnectionString(dbConnection);
            _brokerconnectionstring = dbConnection;
        }
        /// <summary>
        /// Creates a travel company factory. The travel companies created by the resulting factory 
        /// will be part of the group managed by the broker. 
        /// The resulting factory will be able to upload only travel companies managed by the broker. 
        /// </summary>
        /// <returns></returns>
        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(_brokerconnectionstring);
          
        }

        /// <summary>
        /// Creates a factory for read-only interfaces to travel companies. 
        /// The resulting factory will be able to upload only read only versions
        ///  of the travel companies managed by the broker. 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory(_brokerconnectionstring);
        }


        /// <summary>
        /// Returns a ReadOnltcollection of all the travel 
        /// companies created using this broker.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            using (var c = new BrokerContext(_brokerconnectionstring))
            {
                var travelCompanyNames = c.TravelCompanies.Select(tc => tc.Name);
                return travelCompanyNames.ToList().AsReadOnly();
            }
        }

        //------------------------------------ EQUALITY MEMBERS---------------------------------------------

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TravelCompanyBroker)obj);
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(TravelCompanyBroker other)
        {
            return string.Equals(_brokerconnectionstring, other._brokerconnectionstring);
        }

        public static bool operator ==(TravelCompanyBroker left, TravelCompanyBroker right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TravelCompanyBroker left, TravelCompanyBroker right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (_brokerconnectionstring != null ? _brokerconnectionstring.GetHashCode() : 0);
        }

    }
}
