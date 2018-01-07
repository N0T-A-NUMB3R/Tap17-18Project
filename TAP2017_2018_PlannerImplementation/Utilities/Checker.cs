using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_PlannerImplementation.Utilities
{
    class Checker
    {
        public static void CheckList(List<IReadOnlyTravelCompany> companies)
        {
            if (companies == null)
                throw new ArgumentNullException();
        }

        public static void CheckTc(IReadOnlyTravelCompany company)
        {
            if (company == null)
                throw new ArgumentNullException();
        }

        
    }
}
