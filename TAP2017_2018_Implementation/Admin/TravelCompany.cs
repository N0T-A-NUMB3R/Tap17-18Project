using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    class TravelCompany : ITravelCompany
    {
        private readonly string _tcConnectionstring;
        public string Name { get; }

        public TravelCompany(string connectionString, string agencyName)
        {
            CheckConnectionString(connectionString);
            CheckString(agencyName);
            _tcConnectionstring = connectionString;
            Name = agencyName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TravelCompany other))
                return false;
            return _tcConnectionstring == other._tcConnectionstring && Name==other.Name;
        }


        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            CheckLeg(from, to, cost, distance, transportType);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                if (c.Legs.Any(EqualsLegExp(from,to,cost,distance,transportType)))
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
                return legtoAdd.Id;
            }
        }

        public void DeleteLeg(int legToBeRemovedId)
        {
            CheckNegative(legToBeRemovedId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                var legToDelete = c.Legs.SingleOrDefault(EqualsIdExp(legToBeRemovedId));
                if (legToDelete == null)
                    throw new NonexistentObjectException();
                c.Legs.Remove(legToDelete);
                c.SaveChanges();
            }
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            CheckNegative(legId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                var legs = c.Legs.SingleOrDefault(EqualsIdExp(legId));
                if (legs == null)
                    throw new NonexistentObjectException();

                return c.Legs.Where(EqualsIdExp(legId)).Select(CastToLegDtoExp).First();
                //Todo da togliere il first, funziona eh ma non credo sia corretta a livello di semantica in quanto forzo il first su una cosa che so gia sia una
            }
        }

        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        public override int GetHashCode() => base.GetHashCode();
    }
}

