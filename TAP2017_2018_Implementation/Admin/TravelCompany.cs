using System;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    internal class TravelCompany : ITravelCompany
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
            return _tcConnectionstring == other._tcConnectionstring && Name == other.Name;
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
                c.Legs.Remove(legToDelete ?? throw new NonexistentObjectException());
                c.SaveChanges();
            }
        }


        public ILegDTO GetLegDTOFromId(int legId)
        {
            CheckNegative(legId);
            using (var c = new TravelCompanyContext(_tcConnectionstring))
            {
                CheckLegEntity(c.Legs.SingleOrDefault(EqualsIdExp(legId)));
                var x = c.Legs.Find(legId);
                if (x == null)
                    throw new NonexistentObjectException();
                return CastToLegDtoExp.Compile()(x);
            
        
               
            }
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}

