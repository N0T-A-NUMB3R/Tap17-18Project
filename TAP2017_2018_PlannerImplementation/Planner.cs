using System;
using System.Collections.Generic;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using Ninject;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using static TAP2017_2018_PlannerImplementation.Utilities.Checker;

namespace TAP2017_2018_PlannerImplementation
{
    public class Planner : IPlanner
    {
        private List<IReadOnlyTravelCompany> _companies;

        public Planner(List<IReadOnlyTravelCompany> companies)
        {
            CheckList(companies);
            this._companies = companies;
        }

        public void AddTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            if (!ContainsTravelCompany(readonlyTravelCompany))
                _companies.Add(readonlyTravelCompany);
            throw new TapDuplicatedObjectException();
        }


        public void RemoveTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            if (ContainsTravelCompany(readonlyTravelCompany))
                _companies.Remove(readonlyTravelCompany);
            throw new NonexistentTravelCompanyException();
        }

        public bool ContainsTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            CheckTc(readonlyTravelCompany);
            return _companies.Contains(readonlyTravelCompany);
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies()
        {
            return _companies;
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
