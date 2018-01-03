using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Planning;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    class TravelCompany : ITravelCompany
    {
        public string Name { get; set; }
        private readonly string tcCONNECTIONSTRING;

        public TravelCompany(string connectionString)
        {
            CheckConnectionString(connectionString);
            this.tcCONNECTIONSTRING = connectionString;
        }

       

        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            Utilities.CheckLeg(from,to,cost,distance,transportType);

            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                if (c.Legs.Any(tc => tc.From == from && tc.To == to && tc.Cost == cost && tc.Distance == distance && tc.Type == transportType))
                    throw new ArgumentException(); 
                
                var leg = new LegEntity()
                {
                    From = from,
                    To = to,
                    Cost = cost,
                    Distance = distance,
                    Type = transportType
                };

                c.Legs.Add(leg);
                c.SaveChanges();
                return leg.ID;
            }
        }
       
        public void DeleteLeg(int legToBeRemovedId)
        {
            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                CheckNegative(legToBeRemovedId);
                
                if (c.Legs.Any(l => l.ID == legToBeRemovedId))
                    throw new NonexistentObjectException();

                var legToDelete = c.Legs.Single(l => l.ID == legToBeRemovedId);
                c.Legs.Remove(legToDelete);
                c.SaveChanges();
            }
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            throw new NotImplementedException();
        }
    }
}
