using System.Collections.Generic;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_PlannerImplementation.Utilities.Checker;

namespace TAP2017_2018_PlannerImplementation.Utilities
{
    public static class ReadOnlyTravelCompanyExtensions
    {
        /// <summary>
        /// Using a method of the IreadOnlyTravelCompany interface called FindDePartures 
        /// we get step by step the neighbors graph and return them with an iterator
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="from"></param>
        /// <param name="allowedTransportTypes"></param>
        /// <returns></returns>
        public static IEnumerable<ILegDTO> GetNeighborsDtos(this IEnumerable<IReadOnlyTravelCompany> companies,
            string from, TransportType allowedTransportTypes)
        {
            var readOnlyTravelCompanies = companies.ToList();
            CheckList(readOnlyTravelCompanies);
            CheckString(from);
            CheckTransportType(allowedTransportTypes);

            foreach (var company in readOnlyTravelCompanies)
            {
                foreach (var leg in company.FindDepartures(from, allowedTransportTypes))
                {
                    yield return leg;
                }
            }
        }
    }
}


