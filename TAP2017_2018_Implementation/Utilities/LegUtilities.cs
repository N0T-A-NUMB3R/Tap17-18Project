using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation.Utilities
{
    public static class LegUtilities
    {
        /// <summary>
        /// LegDto is a data container for moving data between layers.
        ///  They are also termed as transfer objects. DTO is only used to pass data and does not contain any business logic.
        ///  They only have simple setters and getters.
        /// </summary>
        public class LegDto : ILegDTO
        {
            //todo costruttutore  con check
            public string From { get; set; }

            public string To { get; set; }

            public int Distance { get; set; }

            public int Cost { get; set; }

            public TransportType Type { get; set; }


            /// <summary>
            ///  Determines whether the specified object is equal to the current object
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
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
            /// <summary>
            ///  Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                var hashCode = 807214720;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(From);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(To);
                hashCode = hashCode * -1521134295 + Distance.GetHashCode();
                hashCode = hashCode * -1521134295 + Cost.GetHashCode();
                hashCode = hashCode * -1521134295 + Type.GetHashCode();
                return hashCode;
            }

          

        }
        /// <summary>
        /// a simple expression that converts from  leg Entity to Leg Dto
        /// </summary>
        public static Expression<Func<LegEntity, ILegDTO>> CastToLegDtoExp = l => new LegDto
        {
           
            From = l.From,
            To = l.To,
            Distance = l.Distance,
            Cost = l.Cost,
            Type = l.Type

        };

        /// <summary>
        /// a simple expression that converts from Leg Dto to Leg Entuty
        /// </summary>
        public static Expression<Func<ILegDTO, LegEntity>> CastToLegEntityExp = l => new LegEntity()
        {

            From = l.From,
            To = l.To,
            Distance = l.Distance,
            Cost = l.Cost,
            Type = l.Type

        };

        /// <summary>
        ///  a simple expression that checks if two Legs are equal
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cost"></param>
        /// <param name="distance"></param>
        /// <param name="transportType"></param>
        /// <returns></returns>
        public static Expression<Func<LegEntity, bool>> EqualsLegExp(string from, string to, int cost, int distance, TransportType transportType)
        {
            return tc =>
                tc.From == @from && 
                tc.To == to && 
                tc.Cost == cost && 
                tc.Distance == distance &&
                tc.Type == transportType;
        }

        /// <summary>
        ///  a simple expression that checks if two Id are equal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Expression<Func<LegEntity, bool>> EqualsIdExp(int id)
        {
            return l => 
                l.Id == id;
        }

        /// <summary>
        ///   a simple expression that checks if two From and Type are equal
        /// </summary>
        /// <param name="from"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<LegEntity, bool>> EqualsTypeAndFromExp(string from, TransportType type)
        {
            return l =>
                ((l.Type & type) == l.Type) && l.From == @from;
        }
        /// <summary>
        /// a simple expression that checks if two connection string  are equal
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static Expression<Func<TravelCompanyEntity, bool>> EqualsCsExp(String cs)
        {
            return tc =>
                tc.ConnectionString == cs;
        }

        /// <summary>
        ///  a simple expression that checks if two name are equal
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Expression<Func<TravelCompanyEntity, bool>> EqualsNameExp(String name)
        {
            return tc =>
                tc.Name == name;
        }
    }
}
 