using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;
using Ninject;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class PlannerFactory : IPlannerFactory
    {
        public IPlanner CreateNew()
        {
            List<IReadOnlyTravelCompany> companies = new List<IReadOnlyTravelCompany>();
            return new Planner(companies);
        }
    }
}
