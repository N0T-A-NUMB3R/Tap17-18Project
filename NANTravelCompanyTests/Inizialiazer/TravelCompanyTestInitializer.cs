using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;

namespace NANTravelCompanyTests
{
    [TestFixture()]
    public class TravelCompanyTestInitializer : BrokerTestInitializer
    {
        protected string pointA;
        protected string pointB;

        protected ITravelCompany travelCompany;
        protected IReadOnlyTravelCompany readOnlyTravelCompany;


        [SetUp]
        public void Setup()
        {
            travelCompany = travelCompanyFactory.Get(ExampleName);
            readOnlyTravelCompany = readOnlyTravelCompanyFactory.Get(ExampleName);
            pointA = "A";
            pointB = "B";

        }
    }
}
