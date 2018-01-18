﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_PlannerImplementation.Utilities.Checker;

namespace TAP2017_2018_PlannerImplementation.Utilities
{
   public static class ReadOnlyTravelCompanyExtensions
    {
        /// <summary>
        /// Using a method of the IreadOnlyTravelCompany interface called FindDeprtures 
        /// we get step by step the neighbors graph and return them with an iterator
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="from"></param>
        /// <param name="allowedTransportTypes"></param>
        /// <returns></returns>
        public static IEnumerable<ILegDTO> GetNeighborsDtos(this IEnumerable<IReadOnlyTravelCompany> companies, string from, TransportType allowedTransportTypes)
        {
            CheckList(companies.ToList());
            CheckString(from); //superflui perchè questo extension method è usato solo in FindTrip con input gia puliti dal chiamante
            CheckTransportType(allowedTransportTypes);

            foreach (var company in companies)
            {
                foreach (var leg in company.FindDepartures(from, allowedTransportTypes))
                {
                    yield return leg;
                }
            }
        }
    }
}