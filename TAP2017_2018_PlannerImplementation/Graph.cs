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

