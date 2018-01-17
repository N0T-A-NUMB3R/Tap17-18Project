using System.Collections.Generic;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class PlannerFactory : IPlannerFactory
    {
        public IPlanner CreateNew()
        {
            var companies = new List<IReadOnlyTravelCompany>();
            return new Planner(companies);
        }
    }
}
