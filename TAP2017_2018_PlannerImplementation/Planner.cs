using System;
using System.Collections.Generic;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class Planner : IPlanner
    {
        public void AddTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            throw new NotImplementedException();
        }

        public void RemoveTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            throw new NotImplementedException();
        }

        public bool ContainsTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies()
        {
            throw new NotImplementedException();
        }

        public ITrip FindTrip(string source, string destination, FindOptions options, TransportType allowedTransportTypes)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
