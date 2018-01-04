using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public string Name { get; set; }
        public readonly string tcCONNECTIONSTRING;
        

        public ReadOnlyTravelCompany(string connectionString)
        {
            Utilities.CheckConnectionString(connectionString);
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
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        {
            throw new NotImplementedException();
        }
    }
}