using System.Linq;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.Utilities.Checker;
using static TAP2017_2018_Implementation.Utilities.LegUtilities;

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
            CheckConnectionString(connectionString);
            CheckName(agencyName);

            _tcConnectionstring = connectionString;
            Name = agencyName;
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
            CheckLeg(from, to, cost, distance, transportType);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
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
            CheckNegative(legToBeRemovedId); 
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                var legToDelete = c.Legs.SingleOrDefault(EqualsIdExp(legToBeRemovedId));
                c.Legs.Remove(legToDelete ?? throw new NonexistentObjectException("leg to delete not exist!"));
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
            CheckNegative(legId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
               CheckLegEntity(c.Legs.SingleOrDefault(EqualsIdExp(legId))); 
               
               var leg = c.Legs.Find(legId);
               CheckNull(leg);
               return CastToLegDtoExp.Compile()(leg);
            }
        }

        //------------------------------------ EQUALITY MEMBERS---------------------------------------------

        /// <summary>
        ///  Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TravelCompany)obj);
        }
        /// <summary>
        ///  Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(TravelCompany other)
        {
            return string.Equals(_tcConnectionstring, other._tcConnectionstring) && string.Equals(Name, other.Name);
        }

        public static bool operator ==(TravelCompany left, TravelCompany right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TravelCompany left, TravelCompany right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((_tcConnectionstring != null ? _tcConnectionstring.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        

    }
}

