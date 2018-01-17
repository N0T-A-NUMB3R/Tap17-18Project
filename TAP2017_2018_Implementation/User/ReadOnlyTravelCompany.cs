﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Checker;


namespace TAP2017_2018_Implementation
{
    /// <summary>
    /// USER LAYER
    /// </summary>
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public string Name { get; set; }
        private readonly string _tconnectionstring;
       
        /// <summary>
        ///  Initializes a new instance of the ReadOnlyTravelCompany 
        /// </summary>
        /// <param name="connectionString"></param>
        public ReadOnlyTravelCompany(string connectionString)
        {
            CheckConnectionString(connectionString);
            _tconnectionstring = connectionString;
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) 
        {
            if (!(obj is ReadOnlyTravelCompany other))
                return false;
            return _tconnectionstring == other._tconnectionstring && Name == other.Name;
        }
        /// <summary>
        /// Finds all legs that satisfy the predicate.
        /// (it will be LINQ-to-object, meaning that all objects matching the original query will have to be loaded into memory from the database)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            CheckNull(predicate);

            using (var c = new TravelCompanyContext(_tconnectionstring))
            {
                var legs = c.Legs.Select(CastToLegDtoExp).AsEnumerable().Where(predicate.Compile());
 
                // todo controllare legs?
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

                return legsFound.ToList().AsReadOnly();
            }
        }
        /// <summary>
        ///  Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();
       
    }
}