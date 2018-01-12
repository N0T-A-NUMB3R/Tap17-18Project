using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Trip : ITrip
    {
        public Trip(string @from, string to, ReadOnlyCollection<ILegDTO> path, int totalCost, int totalDistance)
        {
            Utilities.Checker.CheckString(to);
            Utilities.Checker.CheckString(from);
            


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

        public static ITrip FindShortFindTrip(string source, string destination, FindOptions options, List<string> nodes,
            Dictionary<string, int> Dist, Graph graph, Dictionary<string, ILegDTO> Prec)
        {
            // This is a implementation of Dijkstra’s algorithm, its finds a shortest path tree from a single source node, by building a set of nodes that have minimum distance from the source.

            while (nodes.Count > 0)
            {
                var min = nodes[0];
                foreach (var n in nodes)
                    if (Dist[n] < Dist[min])
                        min = n;

                nodes.Remove(min);
                //il percorso è finito
                if (min == destination || Dist[min] == int.MaxValue) break;

                // altrimenti scorriamo il vicinato....

                foreach (var neigh in graph.GetNeighbour(min))
                {
                    var peso = 1;
                    if (options == FindOptions.MinimumCost) peso = neigh.Cost;
                    else if (options == FindOptions.MinimumDistance)
                        peso = neigh.Distance;
                    var newDist = Dist[min] + peso;
                    if (newDist < Dist[neigh.To])
                    {
                        Dist[neigh.To] = newDist;
                        Prec[neigh.To] = neigh;
                    }
                }
            }

            var path = new Stack<ILegDTO>();
            //ora mi ricavo la soluzione
            for (var s = destination; Prec[s] != null; s = Prec[s].From)
            {
                path.Push(Prec[s]);
            }

            if (!path.Any())
                return null;

            var cost = path.Select(x => x.Cost).Sum();
            var dist = path.Select(x => x.Distance).Sum();
            return new Trip(source, destination, path.ToList().AsReadOnly(), cost, dist);
        }
    }
}
