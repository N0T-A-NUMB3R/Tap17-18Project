using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    class TravelCompany : ITravelCompany
    {
        public readonly string TcConnectionstring;
        public string Name { get; }
        public TravelCompany(string connectionString, string agencyName)
        {
            CheckConnectionString(connectionString);
            TcConnectionstring = connectionString;
            Name = agencyName;
        }

        public override bool Equals(object obj) //Todo prob va aggiunta toString..
        {
            if (!(obj is TravelCompany other))
                return false;
            return TcConnectionstring == other.TcConnectionstring && Name==other.Name;
        }


        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            CheckLeg(from, to, cost, distance, transportType);

            using (var c = new TravelCompanyContext(TcConnectionstring))
            {
                if (c.Legs.Any(tc =>
                    tc.From == from && tc.To == to && tc.Cost == cost && tc.Distance == distance &&
                    tc.Type == transportType))
                    throw new TapDuplicatedObjectException();

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
            using (var c = new TravelCompanyContext(TcConnectionstring))
            {
                var legToDelete = c.Legs.SingleOrDefault(l => l.ID == legToBeRemovedId);
                if (legToDelete == null)
                    throw new NonexistentObjectException();
                c.Legs.Remove(legToDelete);
                c.SaveChanges();
            }
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            CheckNegative(legId);
            using (var c = new TravelCompanyContext(TcConnectionstring))
            {
                var legs = c.Legs.SingleOrDefault(l => l.ID == legId);
                if (legs == null)
                    throw new NonexistentObjectException();
                return c.Legs.Where(l => l.ID == legId).Select(CastToLegDtoExp).First(); //Todo da togliere il first, perchè non credo sia corretta
             
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

