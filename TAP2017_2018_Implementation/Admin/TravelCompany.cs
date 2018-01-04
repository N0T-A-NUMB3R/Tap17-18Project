using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        public readonly string tcCONNECTIONSTRING;
        public string Name { get; }
        public TravelCompany(string connectionString, string agencyName)
        {
            CheckConnectionString(connectionString);
            this.tcCONNECTIONSTRING = connectionString;
            Name = agencyName;
        }

        public override bool Equals(object obj) //Todo prob va aggiunta toString..
        {
            if (!(obj is TravelCompany other))
                return false;
            return tcCONNECTIONSTRING == other.tcCONNECTIONSTRING && Name==other.Name;
        }


        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            CheckLeg(from, to, cost, distance, transportType);

            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                if (c.Legs.Any(tc =>
                    tc.From == from && tc.To == to && tc.Cost == cost && tc.Distance == distance &&
                    tc.Type == transportType))
                    throw new ArgumentException();

                var legtoAdd = new LegEntity()
                {
                    From = from,
                    To = to,
                    Cost = cost,
                    Distance = distance,
                    Type = transportType
                };

                c.Legs.Add(legtoAdd);
                c.SaveChanges();
                return legtoAdd.ID;
            }
        }

        public void DeleteLeg(int legToBeRemovedId)
        {
            CheckNegative(legToBeRemovedId);
            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                if (c.Legs.Any(l => l.ID == legToBeRemovedId))
                    throw new NonexistentObjectException();

                var legToDelete = c.Legs.Single(l => l.ID == legToBeRemovedId);
                c.Legs.Remove(legToDelete);
                c.SaveChanges();
            }
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            CheckNegative(legId);
            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                if (c.Legs.Any(l => l.ID == legId))
                    throw new NonexistentObjectException();
                var t = c.Legs.Where(l => l.ID == legId).Select(LegToLegDto);
                return t.First();
            }
        }
    }
}

