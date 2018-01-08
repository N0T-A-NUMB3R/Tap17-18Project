using System.Collections.Generic;
using System.Linq;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_PlannerImplementation.Utilities.Checker;

namespace TAP2017_2018_PlannerImplementation
{
    public class Planner : IPlanner
    {
        private readonly List<IReadOnlyTravelCompany> _companies;

        public Planner(List<IReadOnlyTravelCompany> companies)
        {
            CheckList(companies);
            _companies = companies;
        }

        public void AddTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            ContainsToAdd(readonlyTravelCompany);

            _companies.Add(readonlyTravelCompany);

        }

        public void RemoveTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            ContainsToRemove(readonlyTravelCompany);

            _companies.Remove(readonlyTravelCompany);
        }

        public bool ContainsTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            return _companies.Contains(readonlyTravelCompany);
        }

        private void ContainsToAdd(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (ContainsTravelCompany(readonlyTravelCompany))
                throw new TapDuplicatedObjectException();
        }

        private void ContainsToRemove(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (!ContainsTravelCompany(readonlyTravelCompany))
                throw new NonexistentTravelCompanyException();
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies() => _companies.Select(tc => tc);


        public ITrip FindTrip(string source, string destination, FindOptions options, TransportType allowedTransportTypes)
        {
           
            var graph = new Graph();
            graph.InitGraph(_companies, x => (x.Type & allowedTransportTypes) > 0, options);

            Dictionary <string, int> Dist = new Dictionary<string, int>();
            Dictionary<string, string>Prec = new Dictionary<string, string>();
            List<string> nodes = graph.Get_nodes();
            foreach (var node in nodes)
            {
                Dist[node] = int.MaxValue;
                Prec[node] = string.Empty;
            }

            Dist[source] = 0;

            while (nodes.Count > 0)
            {
                var min = nodes[0];
                foreach (var n in nodes)
                    if (Dist[n] < Dist[min]) min = n;

                nodes.Remove(min);
                
                if (Dist[min] == int.MaxValue)
                    break;

                // altrimenti scorriamo il vicinato....

                foreach (var neigh in graph.GetNeighbour(min))
                {
                     var newDist = Dist[min] + neigh.Cost;
                    if (newDist < Dist[min])
                    {
                        Dist[min] = newDist;
                        Prec[min] = min;
                    }

                }

            }


      }
    }

   
}
