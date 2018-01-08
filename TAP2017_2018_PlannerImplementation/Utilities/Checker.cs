using System;
using System.Collections.Generic;
using TAP2017_2018_TravelCompanyInterface;


namespace TAP2017_2018_PlannerImplementation.Utilities
{
    class Checker
    {
        public static void CheckList(List<IReadOnlyTravelCompany> companies)
        {
            if (companies == null)
                throw new ArgumentNullException();
            /*
            if ((companies.Select(c => c.GetType()).Distinct().Count() != 1))
                throw new ArgumentException();
            */
        }

        public static void CheckTc(IReadOnlyTravelCompany company)
        {
            if (company == null)
                throw new ArgumentNullException();
        }

        
    }
}
