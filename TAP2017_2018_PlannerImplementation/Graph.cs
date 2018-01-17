using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Graph
    {
       public Dictionary<string, List<ILegDTO>> Edges { get; set; }

        public List<ILegDTO> GetNeighbour(string node)
        {
            return Edges[node];
        }

        public void InitGraph(List<IReadOnlyTravelCompany> companies,Expression<Func<ILegDTO,bool>> filter, FindOptions options)
        {
            //Nodes = new List<string>();
            Edges = new Dictionary<string, List<ILegDTO>>();
            foreach (var c in companies)
            {
                foreach (var l in c.FindLegs(filter)) //integrare nella query...
                {
                    if (!Edges.ContainsKey(l.From))
                       Edges.Add(l.From, new List<ILegDTO>());
                    if (!Edges.ContainsKey(l.To))
                        Edges.Add(l.To, new List<ILegDTO>());
                    Edges[l.From].Add(l);
                }
            }
        }

        public List<string> Get_nodes()
        {
            return Edges.Keys.ToList();
        }

    }

   



}




/*
 public static List<Node> FindRoute(Map map, Node sourceNode, Node destinationNode, double transmissionRange)
 {
 List<Node> path = new List<Node>();
 path.Add(sourceNode);

 Node currentNode = sourceNode;
     while (true)
 {
     //get all neighbors of current-node (nodes within transmission range)
     List<Node> allNeighbors = map.GetNeighbors(currentNode, transmissionRange);

     //remove neighbors that are already added to path
     IEnumerable<Node> neighbors = from neighbor in allNeighbors
         where !path.Contains(neighbor)
         select neighbor;

     //stop if no neighbors or destination reached
     if (neighbors.Count() == 0) break;
     if (neighbors.Contains(destinationNode))
     {
         path.Add(destinationNode);
         break;
     }

     //choose next-node (the neighbor with shortest distance to destination)
     Node nearestNode = FindNearestNode(neighbors, destinationNode);
     //The function FindNearestNode() finds the node with shortest distance to the destination-node. The distance between two nodes is computed using euclidean distance.

             path.Add(nearestNode);
     currentNode = nearestNode;
 }

 return (path);
 }

     public List<Node> GetNeighbors(Node currentNode, double transmissionRange)
     { //The following function FindNeighbors() defined in Map class is used to find all the neighbors of a given node, within the current transmission range

         List<Node> neighbors = new List<Node>();
         foreach (Node node in this.Nodes)
         {
             if (!node.Equals(currentNode)) //ensure current-node itself is not taken as neighbor
             {
                 if (PathFinding.FindDistance(currentNode, node) <= transmissionRange)
                 {
                     neighbors.Add(node);
                 }
             }
         }

         return (neighbors);
     }
     

 }
 */