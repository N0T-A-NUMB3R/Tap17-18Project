using System.Collections.Generic;
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
        public Trip(string @from, string to, ReadOnlyCollection<ILegDTO> path, int totalCost, int totalDistance)
        {
            Utilities.Checker.CheckString(to);
            Utilities.Checker.CheckString(from);
            //todo
            From = @from;
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
            return obj is Trip trip &&
                   From == trip.From &&
                   To == trip.To &&
                   EqualityComparer<ReadOnlyCollection<ILegDTO>>.Default.Equals(Path, trip.Path) &&
                   TotalCost == trip.TotalCost &&
                   TotalDistance == trip.TotalDistance;
        }


        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 1548407081;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(To);
            hashCode = hashCode * -1521134295 + EqualityComparer<ReadOnlyCollection<ILegDTO>>.Default.GetHashCode(Path);
            hashCode = hashCode * -1521134295 + TotalCost.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalDistance.GetHashCode();
            return hashCode;
        }

    }
}