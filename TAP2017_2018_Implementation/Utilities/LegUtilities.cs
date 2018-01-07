using System;
using System.Linq.Expressions;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public static class LegUtilities
    {

        public class LegDto : ILegDTO
        {
           
            public string From { get; set; }

            public string To { get; set; }

            public int Distance { get; set; }

            public int Cost { get; set; }

            public TransportType Type { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is LegDto other))
                    return false;
                return From == other.From
                       && To == other.To
                       && Distance == other.Distance
                       && Cost == other.Cost
                       && Type == other.Type;
            }

            public override int GetHashCode() => base.GetHashCode();
        }


        public static Expression<Func<LegEntity, ILegDTO>> CastToLegDtoExp = l => new LegDto
        {
            From = l.From,
            To = l.To,
            Distance = l.Distance,
            Cost = l.Cost,
            Type = l.Type

        };



        public static Expression<Func<LegEntity, bool>> EqualsLegExp(string from, string to, int cost, int distance, TransportType transportType)
        {
            return tc =>
                tc.From == from && 
                tc.To == to && 
                tc.Cost == cost && 
                tc.Distance == distance &&
                tc.Type == transportType;
        }


        public static Expression<Func<LegEntity, bool>> EqualsIdExp(int id)
        {
            return l => 
                l.Id == id;
        }


        public static Expression<Func<LegEntity, bool>> EqualsTypeAndFromExp(string from, TransportType type)
        {
            return l =>
                ((l.Type & type) == l.Type) && l.From == from;
        }

    }
}
 