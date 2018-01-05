using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
                var legsFound = c.Legs.Select(CastToLegDto.Compile()).AsQueryable().Where(dto => pred(dto));
                return new ReadOnlyCollection<ILegDTO>(legsFound.ToList());
                
            }

        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string from, TransportType allowedTransportTypes)
        {
           
            CheckDepartures(from, allowedTransportTypes);
            using (var c = new TravelCompanyContext(tcCONNECTIONSTRING))
            {
                var legsFound = c.Legs.Where(x => x.From == from && (x.Type & allowedTransportTypes) == x.Type);
                var legFoundToDto = legsFound.Select(CastToLegDto.Compile()).ToList();
             return new ReadOnlyCollection<ILegDTO>(legFoundToDto);
            }
        }
    

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}