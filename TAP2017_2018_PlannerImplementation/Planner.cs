using System;
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

        private IEnumerable<ILegDTO> GetNeighbor(List<IReadOnlyTravelCompany> companies, string from, TransportType allowedTransportTypes)
        {
            foreach (var company in companies)
            {
                foreach (var leg in company.FindDepartures(from, allowedTransportTypes))
                {
                    yield return leg;
                }
            }
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies() => _companies.Select(tc => tc);


        public ITrip FindTrip(string source, string destination, FindOptions options,
            TransportType allowedTransportTypes)
        {
            CheckTrip(source, destination, allowedTransportTypes);
            CheckTrip(source, destination, allowedTransportTypes);

            var Dist = new Dictionary<string, int>();
            var Prec = new Dictionary<string, ILegDTO>();
            var nodes = new List<string>() {source, destination}; //inf del nodo parziali

            // ma se io cerco un viaggio con source = destination ma in realta non esiste non dovrei lanciare null? il test passa esclusivamente se io metto null.

            if (source == destination)
                return new Trip(source, destination, new List<ILegDTO>().AsReadOnly(), 0, 0);

            Prec[source] = null;
            Dist[source] = 0;
            Prec[destination] = null;
            Dist[destination] = int.MaxValue;
            //dijkstra
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

                foreach (var neigh in GetNeighbor(_companies, min, allowedTransportTypes))
                {
                    if (!Prec.ContainsKey(neigh.To))
                    {
                        //se non ci sono li aggiungo
                        Prec.Add(neigh.To, null);
                        Dist.Add(neigh.To, int.MaxValue); // infinito per dijtra
                        nodes.Add(neigh.To);
                    }

                    if (!Prec.ContainsKey(neigh.From))
                    {
                        //se non ci sono li aggiungo
                        Prec.Add(neigh.From, null);
                        Dist.Add(neigh.From,
                            int.MaxValue); //non è possibile il loop perchè tengo traccia dei nodi visitati nel dizionario (che non elimino mai)
                        nodes.Add(neigh.From);
                    }

                    //inseriamo cose nei vari dizionari

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


        /*
        public ITrip FindTrip(string source, string destination, FindOptions options,
            TransportType allowedTransportTypes)
        {
            CheckTrip(source, destination,allowedTransportTypes);

            var graph = new Graph();
            graph.InitGraph(_companies, x => (x.Type & allowedTransportTypes) > 0, options);

            var Dist = new Dictionary<string, int>();
            var Prec = new Dictionary<string, ILegDTO>();
            var nodes = graph.Get_nodes();


            if (!nodes.Contains(source) || !nodes.Contains(destination))
                return null;

            // ma se io cerco un viaggio con source = destination ma in realta non esiste non dovrei lanciare null? il test passa esclusivamente se io metto null.

            if (source == destination)
                return new Trip(source, destination, new List<ILegDTO>().AsReadOnly(), 0, 0);

            //init per Diijktra
            foreach (var node in nodes)
            {
                Dist[node] = int.MaxValue;
                Prec[node] = null;
            }

            Dist[source] = 0;
            //dijkstra
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
        */
    }
}