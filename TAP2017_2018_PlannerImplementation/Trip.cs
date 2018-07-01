using System.Collections.ObjectModel;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Trip : ITrip
    {
       /// <summary>
        /// A trip is represented as a list of subsequent hops between cities, a cost and a distance.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="path"></param>
        /// <param name="totalCost"></param>
        /// <param name="totalDistance"></param>
        public Trip(string from, string to, ReadOnlyCollection<ILegDTO> path, int totalCost, int totalDistance)
        {
            From = from;
            To = to;
            Path = path;
            TotalCost = totalCost;
            TotalDistance = totalDistance;
        }

        public string From { get; }
        public string To { get; }
        public ReadOnlyCollection<ILegDTO> Path { get; }
        public int TotalCost { get; }
        public int TotalDistance { get; }

        // <summary>
        /// Determines whether the specified object is equal to the current object
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Trip) obj);
        }
        // <summary>
        /// Determines whether the specified object is equal to the current object
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Trip other)
        {
            return string.Equals(From, other.From) && string.Equals(To, other.To) && Equals(Path, other.Path) && TotalCost == other.TotalCost && TotalDistance == other.TotalDistance;
        }

        public static bool operator ==(Trip left, Trip right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Trip left, Trip right)
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
                var hashCode = (From != null ? From.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ TotalCost;
                hashCode = (hashCode * 397) ^ TotalDistance;
                return hashCode;
            }
        }

    }
}