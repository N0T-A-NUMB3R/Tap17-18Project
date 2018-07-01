using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Utilities.Checker;
using static TAP2017_2018_Implementation.Utilities.LegUtilities;

namespace TAP2017_2018_Implementation.User
{
    /// <summary>
    /// USER LAYER
    /// </summary>
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        
        public string Name { get; }
        private readonly string _tconnectionstring;

        /// <summary>
        ///  Initializes a new instance of the ReadOnlyTravelCompany 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="name"></param>
        public ReadOnlyTravelCompany(string connectionString,string name)
        {
            CheckConnectionString(connectionString);
            _tconnectionstring = connectionString;
            Name = name;
        }

        /// <summary>
        /// Finds all legs that satisfy the predicate.
        /// (it will be LINQ-to-object, meaning that all objects matching the original query will have to be loaded into memory from the database)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            CheckNullExp(predicate);
            using (var c = new TravelCompanyContext(_tconnectionstring))
            {
                var legs = c.Legs.Select(CastToLegDtoExp).AsEnumerable().Where(predicate.Compile());
                CheckNull(legs);
                return legs.ToList().AsReadOnly();
            }
        }
        /// <summary>
        /// Finds all legs departing from the city from and using any of the transport types in allowedTransportTypes 
        /// (that query will be executed in the database, if possible.)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="allowedTransportTypes"></param>
        /// <returns></returns>
        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        {
            CheckDepartures(from, allowedTransportTypes);
            using (var c = new TravelCompanyContext(_tconnectionstring))
            {
                var legsFound = c.Legs.Where(EqualsTypeAndFromExp(from, allowedTransportTypes)).Select(CastToLegDtoExp);
                    
                CheckNull(legsFound);
                return legsFound.ToList().AsReadOnly();
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
            return obj.GetType() == GetType() && Equals((ReadOnlyTravelCompany)obj);
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(ReadOnlyTravelCompany other)
        {
            return string.Equals(_tconnectionstring, other._tconnectionstring) && string.Equals(Name, other.Name);
        }

        public static bool operator ==(ReadOnlyTravelCompany left, ReadOnlyTravelCompany right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReadOnlyTravelCompany left, ReadOnlyTravelCompany right)
        {
            return !Equals(left, right);
        }
        /// <summary>
        ///  Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((_tconnectionstring != null ? _tconnectionstring.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }


    }
}