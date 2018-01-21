using System.Collections.Generic;
using System.Linq;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_Implementation.Utilities;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation.Admin
{
    /// <summary>
    /// ADMIN LAYER
    /// </summary>
    public class TravelCompany : ITravelCompany
    {
        private readonly string _tcConnectionstring;
        public string Name { get; }

        /// <summary>
        ///  Initializes a new instance of the TravelCompany 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="agencyName"></param>
        public TravelCompany(string connectionString, string agencyName)
        {
            Checker.CheckConnectionString(connectionString);
            Checker.CheckString(agencyName);

            _tcConnectionstring = connectionString;
            Name = agencyName;
        }
        /// <summary>
        ///  Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TravelCompany other))
                return false;
            return _tcConnectionstring == other._tcConnectionstring && Name == other.Name;
        }

        /// <summary>
        /// Creates an ILeg which connects two cities. 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cost"></param>
        /// <param name="distance"></param>
        
        /// <param name="transportType"></param>
        /// <returns></returns>
        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            Checker.CheckLeg(from, to, cost, distance, transportType);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                if (c.Legs.Any(LegUtilities.EqualsLegExp(from,to,cost,distance,transportType)))
                    throw new TapDuplicatedObjectException();

                var legtoAdd = new LegEntity()
                {
                    From = from,
                    To = to,
                    Cost = cost,
                    Distance = distance,
                    Type = transportType
                };

                c.Legs.Add(legtoAdd);
                c.SaveChanges();
                return legtoAdd.Id;
            }
        }

        /// <summary>
        /// Deletes the leg from an Id
        /// </summary>
        /// <param name="legToBeRemovedId"></param>
        public void DeleteLeg(int legToBeRemovedId)
        {
            Checker.CheckNegative(legToBeRemovedId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                var legToDelete = c.Legs.SingleOrDefault(LegUtilities.EqualsIdExp(legToBeRemovedId));
                c.Legs.Remove(legToDelete ?? throw new NonexistentObjectException());
                c.SaveChanges();
            }
        }

        /// <summary>
        /// Gets a leg DTO from an Leg Id
        /// </summary>
        /// <param name="legId"></param>
        /// <returns></returns>
        public ILegDTO GetLegDTOFromId(int legId)
        {
            Checker.CheckNegative(legId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                Checker.CheckLegEntity(c.Legs.SingleOrDefault(LegUtilities.EqualsIdExp(legId))); 
                //todo da mettere su linea unica
                var leg = c.Legs.Find(legId);
                Checker.CheckNull(leg);
               return LegUtilities.CastToLegDtoExp.Compile()(leg);
            }
        }
        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 1386540176;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_tcConnectionstring);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        

    }
}

/*
 *  class DbConnectionException Thrown to notify connection errors • class NonexistentObjectException Thrown if the code try to access a nonexistent object • class NonexistentTravelCompanyException Thrown if the code try to get a nonexistent (read only) travel company • class SameConnectionStringException Thrown to notify that the connection string is already in use for a different purpose • class TapDuplicatedObjectException Thrown to notify the attempt to create two instances of the same object • class TapException Superclass of most exceptions for the component
   
 */

