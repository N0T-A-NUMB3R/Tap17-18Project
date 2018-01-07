using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Trip : ITrip
    {
        public Trip()
        {
        }

        public string From { get; }
        public string To { get; }
        public ReadOnlyCollection<ILegDTO> Path { get; }
        public int TotalCost { get; }
        public int TotalDistance { get; }

        public override bool Equals(object obj)
        {
            var trip = obj as Trip;
            return trip != null &&
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
