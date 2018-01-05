using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.LegUtilities;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public string Name { get; set; }
        public readonly string tcCONNECTIONSTRING;
        

        public ReadOnlyTravelCompany(string connectionString)
        {
            CheckConnectionString(connectionString);
            this.tcCONNECTIONSTRING = connectionString;
        }

        public override bool Equals(object obj) //Todo prob va aggiunta toString..
        {
            if (!(obj is ReadOnlyTravelCompany other))
                return false;
            return tcCONNECTIONSTRING == other.tcCONNECTIONSTRING && Name == other.Name;
        }

        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentException();


            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {

                var pred = predicate.Compile();
                var legs = c.Legs.Select(LegToLegDto.Compile()).AsQueryable().Where(x => pred(x));
                return new ReadOnlyCollection<ILegDTO>(legs.ToList());
;
            }

        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        //leg.tt && allowed == 1
        {
            CheckDepartures(from, allowedTransportTypes);
            return new ReadOnlyCollection<ILegDTO>(new List<ILegDTO>());
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}