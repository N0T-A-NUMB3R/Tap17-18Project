using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_Implementation;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public static class LegUtilities
    {

        public class LegDTO : ILegDTO  
        {
            

            public string From { get; set; }

            public string To { get; set; }

            public int Distance { get; set; }

            public int Cost { get; set; }

            public TransportType Type { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is LegDTO other))
                    return false;
                return From == other.From
                       && To == other.To
                       && Distance == other.Distance
                       && Cost == other.Cost
                       && Type == other.Type;
            }
           
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
        
        public static Expression<Func<LegEntity, bool>> DetechDeparturesExp(string from, TransportType allowedTransportTypes)
        {
            return l =>
                (l.From == from)
                && (l.Type & allowedTransportTypes) == l.Type;
        } 
        
        public static Expression<Func<LegEntity, ILegDTO>> CastToLegDtoExp = l => new LegDTO
        {
            From = l.From,
            To = l.To,
            Distance = l.Distance,
            Cost = l.Cost,
            Type = l.Type

        };
    }
}

