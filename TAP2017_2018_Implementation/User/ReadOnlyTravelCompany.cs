using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public string Name { get; set; }

        private readonly string Tconnectionstring;


        public ReadOnlyTravelCompany(string connectionString)
        {
            CheckConnectionString(connectionString);
            Tconnectionstring = connectionString;
        }

        public override bool Equals(object obj) 
        {
            if (!(obj is ReadOnlyTravelCompany other))
                return false;
            return Tconnectionstring == other.Tconnectionstring && Name == other.Name;
        }

        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            CheckNull(predicate);     
            using (var c = new TravelCompanyContext(Tconnectionstring))
            {
                 var pred = predicate.Compile();
                 var legsFound = c.Legs.Select(CastToLegDtoExp).Where(dto => pred(dto)); //TODO controllare se Enum
                return new ReadOnlyCollection<ILegDTO>(legsFound.ToList());
            }

        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        {
           
            CheckDepartures(from, allowedTransportTypes);
            using (var c = new TravelCompanyContext(Tconnectionstring))
            {
                var legsFound = c.Legs.Where(x => x.From == from && (x.Type & allowedTransportTypes) == x.Type)
                    .Select(CastToLegDtoExp);

                return new ReadOnlyCollection<ILegDTO>(legsFound.ToList());
            }
        }
    

        public override int GetHashCode() => base.GetHashCode();

      
    }
}