using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Checker;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public string Name { get; set; }
        private readonly string _tconnectionstring;

         public ReadOnlyTravelCompany(string connectionString)
        {
            CheckConnectionString(connectionString);
            _tconnectionstring = connectionString;
        }

        public override bool Equals(object obj) 
        {
            if (!(obj is ReadOnlyTravelCompany other))
                return false;
            return _tconnectionstring == other._tconnectionstring && Name == other.Name;
        }

    

        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        { 
            CheckNull(predicate);
           
            using (var c = new TravelCompanyContext(_tconnectionstring))
            {
                var legsFounds = c.Legs.Select(CastToLegDtoExp).Where(predicate);

                return new ReadOnlyCollection<ILegDTO>(legsFounds.ToList());
            }
        }
      
        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        {
            CheckDepartures(from, allowedTransportTypes);
            using (var c = new TravelCompanyContext(_tconnectionstring))
            {
                var legsFound = c.Legs.Where(EqualsTypeAndFromExp(from, allowedTransportTypes)).Select(CastToLegDtoExp);

                return new ReadOnlyCollection<ILegDTO>(legsFound.ToList());
            }
        }
       



        public override int GetHashCode() => base.GetHashCode();
        /*
         * A = b
         * B = predicate(x)
         */
    }
}