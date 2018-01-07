using System.Collections.Generic;
using System.Collections.ObjectModel;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Trip : ITrip
    {
        public Trip(string @from, string to, ReadOnlyCollection<ILegDTO> path, int totalCost, int totalDistance)
        {
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

        public override bool Equals(object obj)
        {
            return obj is Trip trip &&
                   From == trip.From &&
                   To == trip.To &&
                   EqualityComparer<ReadOnlyCollection<ILegDTO>>.Default.Equals(Path, trip.Path) &&
                   TotalCost == trip.TotalCost &&
                   TotalDistance == trip.TotalDistance;
        }

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
