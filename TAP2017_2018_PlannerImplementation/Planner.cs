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
        /// <summary>
        /// the choice to use a simple list instead of a db, because a Planner will have to use a few Travelcompanies
        /// </summary>
        private readonly List<IReadOnlyTravelCompany> _companies;

        public Planner(List<IReadOnlyTravelCompany> companies)
        {
            CheckList(companies);
            _companies = companies;
        }
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
            return Equals((Planner) obj);
        }
        /// <summary>
        ///  Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Planner other)
        {
            return Equals(_companies, other._companies);
        }

        public static bool operator ==(Planner left, Planner right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Planner left, Planner right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (_companies != null ? _companies.GetHashCode() : 0);
        }
        /// <summary>
        /// Adds a travel company destination the collection of those destination be used for the planning.

        /// </summary>
        /// <param name="readonlyTravelCompany"></param>
        public void AddTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            ContainsToAdd(readonlyTravelCompany);

            _companies.Add(readonlyTravelCompany);

        }
        /// <summary>
        /// Removes a travel company from the collection of those destination be used for the planning
        /// </summary>
        /// <param name="readonlyTravelCompany"></param>
        public void RemoveTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            ContainsToRemove(readonlyTravelCompany);

            _companies.Remove(readonlyTravelCompany);
        }
        /// <summary>
        /// Veriﬁes if the travel company in the collection of those in use by the planner
        /// </summary>
        /// <param name="readonlyTravelCompany"></param>
        /// <returns></returns>
        public bool ContainsTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            return _companies.Contains(readonlyTravelCompany);
        }
        /// <summary>
        /// Aux method for AddTravelCompany, for check Execption
        /// </summary>
        /// <param name="readonlyTravelCompany"></param>
        private void ContainsToAdd(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (ContainsTravelCompany(readonlyTravelCompany))
                throw new TapDuplicatedObjectException("TravelCompany has already been added");
        }
        /// <summary>
        ///  Aux method for RemoveTravelCompany, for check Execption
        /// </summary>
        /// <param name="readonlyTravelCompany"></param>
        private void ContainsToRemove(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (!ContainsTravelCompany(readonlyTravelCompany))
                throw new NonexistentTravelCompanyException("TravelCompany not exist");
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies() => _companies.Select(tc => tc);

        /// <summary>
        // Using a stack we get the complete path(in reverse) and save it in a trip.(It is an auxiliary method of FindTrip)
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="previousDictionary"></param>
        /// <returns></returns>
        private static ITrip GetTheShortestTrip(string destination, string source, Dictionary<string, ILegDTO> previousDictionary)
        {
            CheckString(destination);
            CheckString(source);
            CheckDictionary(previousDictionary);
            var path = new Stack<ILegDTO>();

            for (var s = destination; previousDictionary[s] != null; s = previousDictionary[s].From)
            {
                path.Push(previousDictionary[s]);
            }

            var cost = path.Select(x => x.Cost).Sum();
            var distance = path.Select(x => x.Distance).Sum();
            // null if there is no path from source to destination  (as indicated in the documentation)
            return !path.Any() ? null : new Trip(source, destination, path.ToList().AsReadOnly(), cost, distance);

        }

        /// <summary>
        /// Finds the best trip source source destination destination , according destination options , using only transport of a type ﬂagged in 
        /// allowedTransportTyp, this method was built using the famous Dijktra algorithm, 
        /// is a greedy algorithm that solves the single-source shortest path problem for a directed graph with non negative edge weights.
        /// FOR MORE INFO CHECKOUT: https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="options"></param>
        /// <param name="allowedTransportTypes"></param>
        /// <returns></returns>

        public ITrip FindTrip(string source, string destination, FindOptions options,
            TransportType allowedTransportTypes)
        {
            CheckTrip(source, destination, allowedTransportTypes);
            var distance = new Dictionary<string, int>();
            var previous = new Dictionary<string, ILegDTO>();
            var nodes = new List<string>() {source, destination};

            // A city is always considered connected destination itself, so an empty trip is returned when source and destination coincide
            if (source == destination)
                return new Trip(source, destination, new List<ILegDTO>().AsReadOnly(), 0, 0);

            previous[source] = null;
            distance[source] = 0; // We know the distance in source is 0 by definition
            previous[destination] = null;
            distance[destination] =
                int.MaxValue; // We know the distance from source->destination is infinite by definition

            //----------------------------------------------------------------------------------------------------------------
            //-------------------------------Implementation of Dijktra Algorithm----------------------------------------------
            //----------------------------------------------------------------------------------------------------------------

            while (nodes.Count > 0)
            {
                var min = nodes.OrderBy(n => distance[n]).First();
                // remove best vertex (that is, connection with minimum distance)
                nodes.Remove(min);

                foreach (var company in _companies)
                {
                    var legs = company.FindDepartures(min, allowedTransportTypes);

                    foreach (var neighbour in legs)
                    {
                        // the loop is the only problem according to the teacher but 
                        // isnt possible (i hope) because I keep track of the nodes visited in the dictionary, which I never delete.
                        if (!previous.ContainsKey(neighbour.To))
                        {
                            // if they arent present, I add them in 2 dictionaries 
                            previous.Add(neighbour.To, null);
                            distance.Add(neighbour.To, int.MaxValue);
                            nodes.Add(neighbour.To);
                        }

                        //same thing for the from...
                        if (!previous.ContainsKey(neighbour.From))
                        {
                            previous.Add(neighbour.From, null);
                            distance.Add(neighbour.From,
                                int.MaxValue);
                            nodes.Add(neighbour.From);
                        }

                        //check the option of the trip, --> BY DEFAULT IT'S MINHOP
                        var weight = 1;
                        switch (options)
                        {
                            case FindOptions.MinimumCost:
                                weight = neighbour.Cost;
                                break;
                            case FindOptions.MinimumDistance:
                                weight = neighbour.Distance;
                                break;
                        }

                        // The positive distance between node and it's neighbor, added to the distance of the current node
                        var newDist = distance[min] + weight;
                        if (newDist < distance[neighbour.To])
                        {
                            distance[neighbour.To] = newDist;
                            previous[neighbour.To] = neighbour;
                        }

                        if (min == destination) break;
                        // If we're at the target node? Ok break
                    }
                }
            }

            return GetTheShortestTrip(destination, source, previous);
       }
        
    }

}